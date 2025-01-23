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

}
