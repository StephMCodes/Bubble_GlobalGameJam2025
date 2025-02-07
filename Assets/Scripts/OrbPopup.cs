using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrbPopup : MonoBehaviour
{
    public GameObject orb;
    private BoxCollider2D boxColliderButton;

    private bool playerNearby = false;

    private void Awake()
    {
        boxColliderButton = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (playerNearby) 
        {
            if (orb != null)
            {
                orb.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) //detects the collison with player
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) //detects the collsion with player
        {
            playerNearby = false;
            boxColliderButton.enabled = false;
        }
    }

}
