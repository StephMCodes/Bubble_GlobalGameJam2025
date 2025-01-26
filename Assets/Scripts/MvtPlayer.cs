using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class MvtPlayer : MonoBehaviour
{
    //inspector variables
    [Header("References")]
    public MvtStats MoveStats;
    //the underscore is a naming convention for private values
    [SerializeField] private Collider2D _bodyColl;
    [SerializeField] private Collider2D _feetColl;
    //[SerializeField] public HeartHealthSystem _heartHealthSys;
    [SerializeField] public HeartHealthVisual _HeartHealthVisual;

    //to access the rigidbody and assign it our physics
    private Rigidbody2D _rb;
    //access anims
    private Animator _animator;

    //call heart class
    public HeartHealthSystem TheHealthSystemBackend;

    //call audio source
    private AudioSource _audioSource;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip reversePop;


    //mvt vars
    private Vector2 _moveVelocity;
    private bool _isFacingRight;

    //collision checking vars
    private RaycastHit2D _groundHit;
    private RaycastHit2D _headHit;
    private bool _isGrounded;
    private bool _bumpedHead;

    //jump vars
    public float VerticalVelocity { get; private set; }
    private bool _isJumping;
    private bool _isFastFalling;
    private bool _isFalling;
    private float _fastFallTime;
    private float _fastFallReleaseSpeed;
    private float _numOfJumpsUsed;

    //apex vars
    private float _apexPoint;
    private float _timePastApexThreshold;
    private bool _isPastApexThreshold;

    //jump buffer vars
    private float _jumpBufferTimer;
    private bool _jumpReleasedDuringBuffer; //bool

    //coyote time vars
    private float _coyoteTimer;

    [SerializeField] GameObject _respawn;

    private void Awake()
    {
        //init.bool and get component rb
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _isFacingRight = true;
        _animator.SetBool("isWalking", true);

    }

    //checking for jump happens in update and jump happens in fixed
    private void Update()
    {
        CountTimers();
        JumpChecks();
        if (_isJumping)
        {
            _animator.SetBool("isJump", true);
        }
        else
        {
            _animator.SetBool("isJump", false);
        }
        //Debug.Log(_isPastApexThreshold);
        //Debug.Log(_isJumping);

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Respawn();
        }
    }

    //heal method to access through Healing script
    public void Heal(int healAmount)
    {
        //Debug.Log(HeartHealthVisual.heartHealthSysytemStatic);
        //references health system 
        //HeartHealthVisual.heartHealthSysytemStatic.HealMe(healAmount); 

        _HeartHealthVisual.dmgTest(healAmount);
        _audioSource.PlayOneShot(reversePop);
    }

    public void DamageKnockBack(Vector3 knockbackDir, float knockbackDistance, int damageAmount)
    {
        //_HeartHealthVisual.dmgTest(1);
        transform.position += knockbackDir * knockbackDistance;
        //HeartHealthVisual.heartHealthSysytemStatic.Damage(damageAmount);
        _animator.SetBool("isAttack", true);
        Invoke("Attacking", 10f);

    }

    public void Attacking()
    {
        _animator.SetBool("isAttack", false);
    }

    public void GettingHit(bool ouch, bool heal)
    {
        if (ouch == true)
        {
            _HeartHealthVisual.dmgTest(1);
            ouch = false;
        } else if (heal == true)
        {
            _HeartHealthVisual.healTest(1);
            heal = false;
        }
    }

    private void FixedUpdate() //fixed allows for more consistency
    {
        CollisionChecks();
        Jump();
        //Debug.Log("Move Input: " + InputManager.Movement);

        if (_isGrounded)
        {
            Move(MoveStats.GroundAcceleration, MoveStats.GroundDeceleration, InputManager.Movement);
        }
        else
        {
            Move(MoveStats.AirAcceleration, MoveStats.AirDeceleration, InputManager.Movement);
        }
    }
    #region Jump

    private void JumpChecks()
    {
        //button pressed
        if (InputManager.JumpPressed)
        {
            _jumpBufferTimer = MoveStats.JumpBufferTime;
            _jumpReleasedDuringBuffer = false;
            if (_numOfJumpsUsed < 2)
            {
                _audioSource.PlayOneShot(jump);
            }

        }

        //button released
        if (InputManager.JumpReleased)
        {
            if (_jumpBufferTimer > 0f)
            {
                _jumpReleasedDuringBuffer = true;
            }

            if (_isJumping && VerticalVelocity > 0f)
            {
                if (_isPastApexThreshold)
                {
                    //reset flags
                    _isPastApexThreshold = false;
                    _isFastFalling = true;
                    _fastFallTime = MoveStats.TimeForUpwardsCancel;
                    VerticalVelocity = 0f; //reduce floatyness to change direction
                }
                else
                {
                    _isFastFalling = true;
                    _fastFallReleaseSpeed = VerticalVelocity;
                }
            }
        }
        //begin jump mvt with buffer + coyote time
        if (!_isJumping && _jumpBufferTimer > 0f && (_isGrounded || _coyoteTimer > 0f))
        {
            InitiateJump(1);

            //bunny hop if press and release same time
            if (_jumpReleasedDuringBuffer)
            {
                _isFastFalling = true;
                _fastFallReleaseSpeed = VerticalVelocity;
            }
        }
        //double or extra jumps
        else if (_isJumping && _jumpBufferTimer > 0f && _numOfJumpsUsed < MoveStats.NumJumpsAllowed)
        {
            _isFastFalling = false;
            InitiateJump(1);
        }
        //handle jump after coyote time is over so we cant fall and then get a double jump from it
        else if (_isFalling && _jumpBufferTimer > 0f && _numOfJumpsUsed < MoveStats.NumJumpsAllowed - 1) ////////
        {
            InitiateJump(2);
            _isFastFalling = false;
        }

        //check landing
        if (VerticalVelocity <= 0f && _isGrounded && (_isJumping || _isFalling))
        {
            //reset all flags timers and used jumps and reset gravity
            _isJumping = false;
            _isFalling = false;
            _isFastFalling = false;
            _isPastApexThreshold = false;
            _fastFallTime = 0f;
            _numOfJumpsUsed = 0;
            VerticalVelocity = Physics2D.gravity.y;
        }
    }



    private void InitiateJump(int numOfJumpsUsed)
    {
        //set flag
        if (!_isJumping)
        {
            _isJumping = true;
        }
        //_jumpBufferTimer = 0f;
        //_numOfJumpsUsed += _numOfJumpsUsed;
        //VerticalVelocity = MoveStats.InitialJumpVelocity;
        //reset buffer
        _jumpBufferTimer = 0f;

        //increment used jumps
        _numOfJumpsUsed += numOfJumpsUsed;

        //set velocity to initial velocity
        VerticalVelocity = MoveStats.InitialJumpVelocity;
    }
    private void Jump()
    {
        //apply gravity
        if (_isJumping)
        {
            //check for head bump
            if (_bumpedHead)
            {
                _isFastFalling = true;
            }
            //gravity upon ascending
            if (VerticalVelocity >= 0f)
            {
                //apex controls
                _apexPoint = Mathf.InverseLerp(MoveStats.InitialJumpVelocity, 0f, VerticalVelocity);

                if (_apexPoint > MoveStats.ApexThreshold)
                {
                    if (!_isPastApexThreshold)
                    {
                        _isPastApexThreshold = true;
                        //reset values
                        _timePastApexThreshold = 0f;
                    }

                    if (_isPastApexThreshold)
                    {
                        //increment timer
                        _timePastApexThreshold += Time.fixedDeltaTime;
                        //hang time
                        if (_timePastApexThreshold < MoveStats.ApexHangTime)
                        {
                            //hangtime
                            VerticalVelocity = 0f;
                        }
                        else
                        {
                            //hangtime over begin falling
                            VerticalVelocity = -0.01f;
                        }
                    }
                }

                //gravity upon asc but not past apex point
                else
                {
                    VerticalVelocity += MoveStats.Gravity * Time.fixedDeltaTime;
                    if (_isPastApexThreshold)
                    {
                        _isPastApexThreshold = false; //reset flag
                    }
                }
            }

            //descending
            //can be without multiplier for game feel //
            else if (!_isFastFalling)
            {
                VerticalVelocity += MoveStats.Gravity * MoveStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }

            else if (VerticalVelocity < 0f)
            {
                if (!_isFalling)
                {
                    _isFalling = true;
                }
            }
        }

        //jump cut
        if (_isFastFalling)
        {
            if (_fastFallTime >= MoveStats.TimeForUpwardsCancel)
            {
                VerticalVelocity += MoveStats.Gravity * MoveStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }
            else if (_fastFallTime < MoveStats.TimeForUpwardsCancel)
            //APPLY JUMP CUT
            {
                VerticalVelocity = Mathf.Lerp(_fastFallReleaseSpeed, 0f, (_fastFallTime / MoveStats.TimeForUpwardsCancel));
            }

            _fastFallTime += Time.fixedDeltaTime;
        }

        //falling gravity for falling
        if (!_isGrounded && !_isJumping)
        {
            if (!_isFalling)
            {
                _isFalling = true;
            }

            VerticalVelocity += MoveStats.Gravity * Time.fixedDeltaTime;
        }

        //clamp fall speed and apply to rb
        VerticalVelocity = Mathf.Clamp(VerticalVelocity, -MoveStats.MaxFallSpeed, 50f);
        _rb.velocity = new Vector2(_rb.velocity.x, VerticalVelocity);
    }
    #endregion

    #region Timers

    private void CountTimers()
    {
        _jumpBufferTimer -= Time.deltaTime;
        if (!_isGrounded)
        {
            _coyoteTimer -= Time.deltaTime;
        }
        else { _coyoteTimer = MoveStats.JumpCoyoteTime; }
    }
    #endregion

    #region Movement

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            //check for player turning
            TurnCheck(moveInput);

            Vector2 targetVelocity = Vector2.zero;
            //check for run speed or walk speed
            if (InputManager.RunHeld)
            {
                _animator.SetBool("isWalking", true);
                targetVelocity = new Vector2(moveInput.x, 0f) * MoveStats.MaxRunSpeed;
            }
            else
            {
                _animator.SetBool("isWalking", false);
                targetVelocity = new Vector2(moveInput.x, 0f) * MoveStats.MaxWalkSpeed;
            }

            //DEF: Lerp is a technique for smoothly transitioning between two values over time.
            //lerp the velocity
            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

            //apply to rigidbody
            _rb.velocity = new Vector2(_moveVelocity.x, _rb.velocity.y);
        }
        else if (moveInput == Vector2.zero) //if mvt is zero
        {
            _moveVelocity = Vector2.Lerp(_moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            _rb.velocity = new Vector2(_moveVelocity.x, _rb.velocity.y);
            _animator.SetBool("isWalking", false);

        }

    }

    private void TurnCheck(Vector2 moveInput)
    {
        //based off direction and input determine facing left or right
        if (_isFacingRight && moveInput.x < 0)
        {
            Turn(false);
        }
        else if (!_isFacingRight && moveInput.x > 0)
        {
            Turn(true);
        }
    }

    private void Turn(bool turnRight)
    {
        if (turnRight)
        {
            _isFacingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            _isFacingRight = false;
            transform.Rotate(0f, -180f, 0f);
        }
    }


    #endregion

    #region Collision Checks

    private void IsGrounded()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, MoveStats.GroundDetectionRaycastLength);

        //draw the ray
        Debug.DrawRay(boxCastOrigin, Vector2.down * MoveStats.GroundDetectionRaycastLength, Color.red);
        //cast ray
        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, MoveStats.GroundDetectionRaycastLength, MoveStats.GroundLayer);
        //if it finds ground collider we are grounded and vice versa
        if (_groundHit.collider != null)
        {
            _isGrounded = true;
            // Debug.Log("Grounded");
        }
        else
        {
            _isGrounded = false;
            // Debug.Log("NOT Grounded");

        }

    }

    private void BumpedHead()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _bodyColl.bounds.max.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x * MoveStats.HeadWidth, MoveStats.HeadDetectionRaycastLength);

        //cast ray
        _headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, MoveStats.HeadDetectionRaycastLength, MoveStats.GroundLayer);
        //if it finds ground collider we are grounded and vice versa
        if (_headHit.collider != null)
        {
            _bumpedHead = true;
        }
        else
        {
            _bumpedHead = false;
        }

    }

    private void CollisionChecks()
    {
        IsGrounded();
        BumpedHead();
    }
    #endregion
}
