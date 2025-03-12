using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public string levelToLoad;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the Player and touches door
        if (other.CompareTag("Player"))
        {
            //changes scene
            SceneManager.LoadScene(levelToLoad);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
