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
    private float _cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Layer")]
    [SerializeField] private LayerMask _playerLayer;

    [Header("Enemy Type (F: obstacle, T: NPC")]
    [SerializeField] private bool _type;
    private bool popped;

    [Header("Player")]
    [SerializeField] private GameObject _player;

    //player health

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
        _cooldownTimer += Time.deltaTime;
        //when cool down is done enemy can attack again
        if (SeesPlayer())
        {
            if (_cooldownTimer >= _attackCooldown)
            {
               //reset cooldown
               _cooldownTimer = 0;
                _player.GetComponent<Animator>().SetBool("isPopping", true);
                popped = true;
            }
        }

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
            
            if (_type && !popped)
            {
                Debug.Log("Player attacked");
                _player.GetComponent<Animator>().SetBool("isPopping", true);
                animator.SetBool("isAttack", true);
                Invoke("ReturnBubble", 5.0f);

            }
        }
        else
        {
            if (_type)
            {
                animator.SetBool("isAttack", false);
            }
        }

        return hit.collider != null;
    }

    private void DamagePlayer()
    {
        if (SeesPlayer())
        {
            //damage player
            //take damage
            Debug.Log("Player attacked");
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance, new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_type && !popped)
        {
            Debug.Log("Player hurt");
            popped = true;
            _player.GetComponent<Animator>().SetBool("isPopping", true);
            Invoke("ReturnBubble", 5.0f);
        }
    }

    private void ReturnBubble()
    {
        _player.GetComponent<Animator>().SetBool("isPopping", false);
        popped = false;

    }


}
