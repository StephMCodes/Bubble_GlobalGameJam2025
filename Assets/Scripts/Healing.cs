using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] private int healAmount;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the Player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerController component
            MvtPlayer playerController = other.GetComponent<MvtPlayer>();

            // Get the AudioSource component from the playerController
            //AudioSource audioSource = playerController.GetComponent<AudioSource>();

            if (playerController != null) // Ensure playerController is not null
            {
                // Heal the player
                playerController.Heal(healAmount);
                Destroy(gameObject);

                //// Play heal sound effect
                //if (playerController.healAudioClip != null)
                //{
                //    audioSource.PlayOneShot(playerController.healAudioClip);
                //}
            }
        }
    }
}
