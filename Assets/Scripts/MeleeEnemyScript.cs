using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _damage;
    private float _cooldownTimer = Mathf.Infinity;

    [SerializeField] private CircleCollider2D _circleCollider;
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
        RaycastHit2D hit = Physics2D.BoxCast(_circleCollider.bounds.center, _circleCollider.bounds.size,0, Vector2.left, 0, _playerLayer);
        return hit.collider != null;
    }



}
