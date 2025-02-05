using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    //get reference to player and checkpoint
    [Header("References")]
    public GameObject player;
    public GameObject checkpoint;
    public AudioSource respawnClip;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       //instant death collider
        if (other.gameObject.CompareTag("Player"))
        {
            //respawn to current checkpoint
            Respawn();
        }
    }

    public void Respawn()
    {
        //set new position
        player.transform.position = checkpoint.transform.position;
        //play sound effect
        respawnClip.Play();
    }
}
