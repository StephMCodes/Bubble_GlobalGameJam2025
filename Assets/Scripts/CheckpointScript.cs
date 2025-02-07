using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    //reference respawn
    private RespawnScript respawnScript;
    private PlayerHealth respawnHealthScript;
    private BoxCollider2D colliderCheckpoint;

    private void Awake()
    {
        respawnScript = GameObject.FindGameObjectWithTag("Respawn").GetComponent< RespawnScript>();
        respawnHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        colliderCheckpoint = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Checkpoint got: " + this.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            //set the new checkpoint in respawn script to the checkpoint
            RespawnScript.checkpoint = this.gameObject;
            PlayerHealth.checkpoint = this.gameObject;


            colliderCheckpoint.enabled = false; //so you cant activate it again by going backwards
            Debug.Log(RespawnScript.checkpoint);
        }
    }
}
