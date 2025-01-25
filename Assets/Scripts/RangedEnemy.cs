using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Values")]
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;

    [Header("Ranged Values")]
    [SerializeField] private Transform _firingPoint;
    [SerializeField] private GameObject[] _missiles;

    [Header("Attack Values")]
    [SerializeField] private float _colliderDistance;
    private float _cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Layer")]
    [SerializeField] private LayerMask _playerLayer;

    ProjectileScript _projectileScript;

    //player health

    //ANIM
    //private Animator anim;

    private EnemyPatrol enemyPatrol;
    private void Awake()
    {
        //anim get component
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
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

        if (enemyPatrol != null)
        {
            //if player not in sight, keep patrolling
            enemyPatrol.enabled = !SeesPlayer();
        }


    }

    private void Attack()
    {
        //anim attack
        _cooldownTimer = 0;

        _missiles[0].transform.position = _firingPoint.transform.position;
        _missiles[0].GetComponent<ProjectileScript>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private bool SeesPlayer()
    {
        //enemy _range of sight
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
        new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z),
        0, Vector2.left, 0, _playerLayer);

        // if (hit.collider != null)
        // {
        //get health of player
        // z}

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance, new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RangedAttack();
    }
    private void RangedAttack()
    {
        _cooldownTimer = 0;
        //shoot
        _missiles[FindMissile()].transform.position = _firingPoint.position;
        _missiles[FindMissile()].GetComponent<ProjectileScript>().ActivateProjectile();
    }

    private int FindMissile()
    {
        for (int i = 0; i < _missiles.Length; i++)
        {
            if (!_missiles[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }


}
