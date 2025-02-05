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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Checkpoint got: " + this.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            respawn.checkpoint = this.gameObject;
            colliderCheckpoint.enabled = false; //so you cant activate it again by going backwards
        }
    }
}
