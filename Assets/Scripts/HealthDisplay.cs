using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth;

    //stores empty bubble
    public Sprite emptyHeart;
    // stores full bubble
    public Sprite fullHeart;
    // array of objects *tea*
    public Image[] hearts; 

    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //makes sure that the health and maxHealth values match the PlayerHealth script
        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;

        //checks if each heart to see if it should be empty or full
        for (int i = 0; i < hearts.Length; i++) //integer equal to how many hearts put in list in Unity
        {
            
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i< maxHealth)
            {
                hearts[i].enabled = true; // Checks if heart in UI to see if it should be turned on
            }
            else
            {
                hearts[i].enabled = false; //turns off hearts that should not be active
            }
        }
    }
}
