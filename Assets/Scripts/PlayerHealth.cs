using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;

    //get sfx
    public AudioSource respawnClip;

    public static GameObject checkpoint;

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        //sets current health yp full
        health = maxHealth;
        checkpoint.transform.position = player.GetComponent<Transform>().position;
    }

    //called whenever damage is taken
    public void TakeDamage(int amount) //amount damge player takes
    {
        //if the damage brings player to zero or less the player movement disabled and sprite renderer disabled 
        health -= amount;
        if (health <= 0)
        {
            Respawn();
            health = maxHealth;
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
