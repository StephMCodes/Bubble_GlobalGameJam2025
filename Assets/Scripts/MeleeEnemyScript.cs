using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _colliderDistance;
    private float _cooldownTimer = Mathf.Infinity;

    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask _playerLayer;

    //player health

    //ANIM
    //private Animator anim;

    private void Awake()
    {
        //anim get component
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
                //anim.SetTrigger("NAME");
            }
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
            //get health of player
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance, new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

}
