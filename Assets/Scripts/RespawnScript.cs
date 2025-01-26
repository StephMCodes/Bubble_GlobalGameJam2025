using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    //get reference to player and checkpoint
    [Header("References")]
    public GameObject player;
    public GameObject checkpoint;
    
    
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
       //instant death collider
        if (other.gameObject.CompareTag("Player"))
        {
           //respawn to current checkpoint
            player.transform.position = checkpoint.transform.position;
        }
    }

    public void Respawn()
    {
        player.transform.position = checkpoint.transform.position;
    }
}
