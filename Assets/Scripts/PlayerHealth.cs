using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;


    // Start is called before the first frame update
    void Start()
    {
        //sets current health yp full
        health = maxHealth;
    }

    //called whenever damage is taken
   public void TakeDamage(int amount) //amount damge player takes
    {
        //if the damage brings player to zero or less the player willbe destroyed 
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject); 
        }
    }
}
