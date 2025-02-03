using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyDamage : MonoBehaviour
{
    //Lets EnenmyDamage script know where to find PlayerHealth script 
    public PlayerHealth playerHealth;
    public int damage = 1;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
            player.GetComponent<Animator>().SetBool("isPopping", true);
            Invoke("ReturnBubble", 2.0f);
        }
    }

    private void ReturnBubble()
    {
        player.GetComponent<Animator>().SetBool("isPopping", false);
    }
}
