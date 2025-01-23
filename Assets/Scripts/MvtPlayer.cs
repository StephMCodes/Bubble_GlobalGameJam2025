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

    #region Movement

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            //check for player turning
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
    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
