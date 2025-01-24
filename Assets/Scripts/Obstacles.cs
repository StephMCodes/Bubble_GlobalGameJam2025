using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private Vector3 knockbackDirection; // Define the knockback direction

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the Player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerController component
            MvtPlayer playerController = other.GetComponent<MvtPlayer>();

            if (playerController != null) // Ensure playerController is not null
            {
                Vector3 knockbackDir = (other.transform.position - transform.position).normalized;
                // Apply damage and knockback
                //playerController.DamageKnockBack(knockbackDir, 10f, damageAmount);

                // Play knockback animation
                Animator anim = playerController.GetComponent<Animator>();
                anim.SetTrigger("getHit");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MvtPlayer playerController = other.GetComponent<MvtPlayer>();
            if (playerController != null)
            {
                Animator anim = playerController.GetComponent<Animator>();
                anim.ResetTrigger("getHit");
            }
        }
    }
}
