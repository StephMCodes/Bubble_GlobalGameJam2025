using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;

    //public SpriteRenderer playerSr;
    //public MvtPlayer playerMovement;
    public CheckpointScript checkpoints;
    public RespawnScript respawn;


    // Start is called before the first frame update
    void Start()
    {
        //sets current health yp full
        health = maxHealth;
    }

    //called whenever damage is taken
   public void TakeDamage(int amount) //amount damge player takes
    {
        //if the damage brings player to zero or less the player movement disabled and sprite renderer disabled 
        health -= amount;
        if (health <= 0)
        {
            //reference the respawn/ updated spawn points
            //playerSr.enabled = false;
            //playerMovement.enabled = false;
           
        }
    }
}
