using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    //reference respawn
    private RespawnScript respawn;
    private BoxCollider2D colliderCheckpoint;

    private void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent< RespawnScript>();
        colliderCheckpoint = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Checkpoint got: " + this.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            //set the new checkpoint in respawn script to the checkpoint
            respawn.checkpoint = this.gameObject;

            colliderCheckpoint.enabled = false; //so you cant activate it again by going backwards
            Debug.Log(respawn.checkpoint);
        }
    }
}
