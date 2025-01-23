using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MvtPlayer : MonoBehaviour
{
    //inspector variables
    [Header("References")]
    public MvtStats MoveStats;
    //the underscore is a naming convention for private values
    [SerializeField] private Collider2D _bodyColl;
    [SerializeField] private Collider2D _feetColl;

    //to access the rigidbody and assign it our physics
    private Rigidbody _rb;

    //mvt vars
    private Vector2 _moveVelocity;
    private bool _isFacingRight;

    //collision checking vars
    private RaycastHit2D _groundHit;
    private RaycastHit2D _headHit;
    private bool _isGrounded;
    private bool bumpedHead;


    private void Awake()
    {
        //init.bool and get component rb
        _rb = GetComponent<Rigidbody>();
        _isFacingRight = true;
    }

    private void FixedUpdate() //fixed allows for more consistency
    {
        CollisionChecks();

        if (_isGrounded )
        {
            Move(MoveStats.GroundAcceleration, MoveStats.GroundDeceleration, InputManager.Movement);
        }
        else
        {
            Move(MoveStats.AirAcceleration, MoveStats.AirDeceleration, InputManager.Movement);

        }
    }

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
                targetVelocity = new Vector2(moveInput.x, 0f) * MoveStats.MaxRunSpeed;
            }
            else
            {
                targetVelocity = new Vector2(moveInput.x, 0f) * MoveStats.MaxWalkSpeed;
            }

            //DEF: Lerp is a technique for smoothly transitioning between two values over time.
            //lerp the velocity
            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

            //apply to rigidbody
            _rb.velocity = new Vector2(_moveVelocity.x, _moveVelocity.y);
        }
        else if (moveInput == Vector2.zero) //if mvt is zero
        {
            _moveVelocity = Vector2.Lerp(_moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            _rb.velocity = new Vector2(_moveVelocity.x, _moveVelocity.y);

        }

    }

    private void TurnCheck(Vector2 moveInput)
    {
        //based off direction and input determine facing left or right
        if (_isFacingRight && moveInput.x <0)
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
            transform.Rotate(0f,180f,0f);
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
        Vector2 boxCastSize = new Vector2 (_feetColl.bounds.size.x, MoveStats.GroundDetectionRaycastLength);

        //cast ray
        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, MoveStats.GroundDetectionRaycastLength, MoveStats.GroundLayer);
        //if it finds ground collider we are grounded and vice versa
        if (_groundHit.collider != null)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    
    }

    private void CollisionChecks()
    {
        IsGrounded();
    }
    #endregion
}
