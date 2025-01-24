using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu(menuName = "Player Movement")]
public class MvtStats : ScriptableObject
{
    //header for editor
    [Header("Walk")]
    //range allows to adjust value of var in editor
    [Range(1f, 100f)] public float MaxWalkSpeed = 12.5f;
    [Range(0.25f, 50f)] public float GroundAcceleration = 5f;
    [Range(0.25f, 50f)] public float GroundDeceleration = 20f;
    [Range(0.25f, 50f)] public float AirAcceleration = 5f;
    [Range(0.25f, 50f)] public float AirDeceleration = 5f;

    [Header("Run")]
    [Range(1f, 100f)] public float MaxRunSpeed = 20f;

    [Header("Ground/Collision Checking")]
    //knowing whether we are on the ground or not lets us choose between
    //applying ground or air accelaration/decelaration
    public LayerMask GroundLayer;
    public float GroundDetectionRaycastLength = 0.02f;
    public float HeadDetectionRaycastLength = 0.02f;
    [Range(0f, 1f)] public float HeadWidth = 0.75f;

    [Header("Jump")]
    //controls default jump height
    public float JumpHeight = 6.5f;

    //tweaks jump height in later calculations for game feel
    //can affect depending on situation such as low gravity level etc
    [Range(1f, 1.1f)] public float JumpHeightCompensationFactor = 1.054f;

    //makes jump take longer to reach apex. decrease for quicker hops
    public float TimeUntilJumpApex = 0.35f;

    //increase for player to fall faster when jump button is let go
    [Range(0.01f, 5f)] public float GravityOnReleaseMultiplier = 2f;
    //max fall speed no matter what
    public float MaxFallSpeed = 26f;

    [Range(1, 5)] public int NumJumpsAllowed = 2;

    [Header("Jump Cancel")]
    //more or less time to cancel jump
    [Range(0.02f, 0.3f)] public float TimeForUpwardsCancel = 0.027f;

    [Header("Jump Apex")]
    //detect when close enough to consider apex aka when to start falling or hanging
    [Range(0.05f, 1f)] public float ApexThreshold = 0.97f;
    [Range(0.01f, 1f)] public float ApexHangTime = 0.075f;

    [Header("Jump Buffer")]
    //allows jump to be executed when character hits ground even if clicked too early
    [Range(0f, 1f)] public float JumpBufferTime = 0.125f;

    [Header("Coyote Time")]
    //lets you jump a few seconds after leaving a platform
    [Range(0f, 1f)] public float JumpCoyoteTime = 0.1f;

    //GRAVITY

    public float Gravity { get; private set; }

    public float InitialJumpVelocity { get; private set; }

    public float AdjustedJumpHeight { get; private set; }

    private void CalculateValues()
    {
        //AdjustedJumpHeight = JumpHeight * JumpHeightCompensationFactor;
        Gravity = -(2f * JumpHeight) / Mathf.Pow(TimeUntilJumpApex, 2f);
        InitialJumpVelocity = Mathf.Abs(Gravity) * TimeUntilJumpApex;
    }

    public void OnValidate()
    {
        CalculateValues();
    }

    private void OnEnable()
    {
        CalculateValues();
    }

}
