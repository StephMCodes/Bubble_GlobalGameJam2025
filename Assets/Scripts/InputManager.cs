using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    //declare components
    public static PlayerInput PlayerInput;

    //declare vars
    public static Vector2 Movement;
    public static bool JumpPressed;
    public static bool JumpHeld;
    public static bool JumpReleased;
    public static bool RunHeld;

    //declare input actions (from input package)
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;


    private void Awake()
    {
        //get components
        PlayerInput = GetComponent<PlayerInput>();
        //instatiate inputs
        _moveAction = PlayerInput.actions["Move"];
        _jumpAction = PlayerInput.actions["Jump"];
        _runAction = PlayerInput.actions["Run"];
    }

    // Update is called once per frame
    void Update()
    {
       //get and apply apply movements and vectors
        Movement = _moveAction.ReadValue<Vector2>();
        
        //jump
        JumpPressed = _jumpAction.WasPressedThisFrame();
        JumpHeld = _jumpAction.IsPressed();
        JumpReleased = _jumpAction.WasReleasedThisFrame();

        //run
        RunHeld = _runAction.IsPressed();
    }
}
