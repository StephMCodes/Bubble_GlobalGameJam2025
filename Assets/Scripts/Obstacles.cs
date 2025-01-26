using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] public MvtPlayer MvtPlayer;
    // [SerializeField] private Vector3 knockbackDirection; // Define the knockback direction
     


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the Player
        if (other.CompareTag("Player"))
        {
            //Debug.Log("DOES THIS WORK?");
            //Debug.Log("IM GONNA CRY");
            // Get the PlayerController component
            //MvtPlayer playerController = other.GetComponent<MvtPlayer>();
            //Debug.Log(MvtPlayer);

            //MvtPlayer.GettingHit(true, false);


            if (MvtPlayer != null) // Ensure playerController is not null
            {
                //Debug.Log("YES IT DOES");
            Vector3 knockbackDir = (other.transform.position - transform.position).normalized;
             //Apply damage and knockback
            MvtPlayer.DamageKnockBack(knockbackDir, 10f, damageAmount);

                // Play knockback animation
                //Animator anim = playerController.GetComponent<Animator>();
                //anim.SetTrigger("getHit");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
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
