using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Values")]
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;

    [Header("Attack Values")]
    [SerializeField] private float _colliderDistance;
    //private float _cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Layer")]
    [SerializeField] private LayerMask _playerLayer;

    [Header("Enemy Type (F: obstacle, T: NPC")]
    [SerializeField] private bool isGhost;
    public static bool popped = false;

    [Header("Player")]
    [SerializeField] private GameObject _player;

    //player health
    //Lets EnenmyDamage script know where to find PlayerHealth script 
    public PlayerHealth playerHealth;
    public int damage = 1;
    //ANIM
    //private Animator anim;

    private EnemyPatrol enemyPatrol;
    private Animator animator;


    private void Awake()
    {
        //anim get component
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //check if the ghost came in contact with the player in dangerzone
        SeesPlayer();

        if (enemyPatrol != null)
        {
            //if player not in sight, keep patrolling
            enemyPatrol.enabled = !SeesPlayer();
        }

    }

    private bool SeesPlayer()
    {
        //enemy _range of sight
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
        new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z),
        0, Vector2.left, 0, _playerLayer);

        if (hit.collider != null)
        {
            if (isGhost && !popped)
            {
                popped = true;
                //play ghost attack anim
                animator.SetBool("isAttack", true);
                //play pop anim
                _player.GetComponent<Animator>().SetBool("isPopping", true);
                //modify UI
                playerHealth.TakeDamage(damage);
                //in 2 seconds get the bubble back
                Invoke("ReturnBubble", 2.0f);
                return true;
            }
        }
        else
        {
            if (isGhost)
            {
                animator.SetBool("isAttack", false);
            }
        }

        return hit.collider != null;
    }
    private void ReturnBubble()
    {
        _player.GetComponent<Animator>().SetBool("isPopping", false);
        popped = false;
        //Debug.Log(popped);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance, new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));
    //}

    // private void DamagePlayer()
    // {
    //   if (SeesPlayer())
    //   {
    //damage player
    //take damage
    //      Debug.Log("Player attacked");
    //  }
    // }

    //  private void OnCollisionEnter2D(Collision2D collision)
    // {
    //   if (!isGhost && !popped)
    //  {
    //Debug.Log("Player hurt");
    //popped = true;
    //_player.GetComponent<Animator>().SetBool("isPopping", true);
    //Invoke("ReturnBubble", 5.0f);
    // }
    // }


}
