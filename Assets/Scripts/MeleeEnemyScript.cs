using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    private float _cooldownTimer = Mathf.Infinity;

    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask _playerLayer;


    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        //when cool down is done enemy can attack again
        if (SeesPlayer())
        {
            if (_cooldownTimer >= _attackCooldown)
            {
                //attack
            }
        }
    }

    private bool SeesPlayer()  
    {
       //enemy range of sight
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x, _boxCollider.bounds.size,0, Vector2.left, 0, _playerLayer);
        return hit.collider != null;
    }



}
