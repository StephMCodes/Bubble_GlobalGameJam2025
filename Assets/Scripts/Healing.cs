using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] private int healAmount;
    [SerializeField] public MvtPlayer MvtPlayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the Player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerController component
            MvtPlayer playerController = other.GetComponent<MvtPlayer>();

            // Get the AudioSource component from the playerController
            //AudioSource audioSource = playerController.GetComponent<AudioSource>();
            //Debug.Log(MvtPlayer);
            if(MvtPlayer != null)
            {
                MvtPlayer.Heal(healAmount);
                Debug.Log(healAmount);
                Destroy(gameObject);
            }


            //if (playerController != null) // Ensure playerController is not null
            {
        //Debug.Log("hiiiiiii");
                // Heal the player

                //// Play heal sound effect
                //if (playerController.healAudioClip != null)
                //{
                //    audioSource.PlayOneShot(playerController.healAudioClip);
                //}
            }
        }
    }
}
