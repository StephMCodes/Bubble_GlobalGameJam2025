using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public GameObject door; 
    private int collectedOrbs = 0;
    private int totalOrbs = 3; 

    public void CollectOrb()
    {
        collectedOrbs++; // Increase when orb is collected

        Debug.Log("Collected Orbs: " + collectedOrbs);

        if (collectedOrbs >= totalOrbs) // If all orbs are collected
        {
            if (door != null)
            {
                door.SetActive(true); // Activate the door
                Debug.Log("All orbs collected! ");
            }
        }
    }
}
