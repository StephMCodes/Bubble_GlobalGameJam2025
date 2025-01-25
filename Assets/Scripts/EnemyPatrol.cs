using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Edges")]
    [SerializeField] private Transform _leftEdge;
    [SerializeField] private Transform _rightEdge;

    [Header("Enemy Values and Idle")]
    [SerializeField] private Transform _enemy;
    [SerializeField] private float _speed;
    private bool _movingLeft;
    [SerializeField] private float _idleDuration;
    private float _idleTimer;


    private Vector3 initScale; //initial scale

    // [SerializeField] private animator anim

    private void Awake()
    {
        initScale = _enemy.localScale;
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (_enemy.position.x >= _leftEdge.position.x)
            {
                MoveTowards(-1); //LEFT
            }
            else //change direction now
            {
                DirectionChange();
            }
        }
        else
        {
            if (_enemy.position.x <= _rightEdge.position.x)
            {
                MoveTowards(1); //RIGHT
            }
            else
            {
                DirectionChange();
            }

        }
    }

    private void MoveTowards(int _direction)
    {
        //begin walk anim
        //Animation.SetBool("move", true);

        //reset idle timer
        _idleTimer = 0;

        //make enemy face correctly
        _enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        //move
        _enemy.position = new Vector3(_enemy.position.x + Time.deltaTime * _direction * _speed, _enemy.position.y, _enemy.position.z);

    }

    private void DirectionChange()
    {
        //stop walk anim
        //Animation.SetBool("move", false);
        _idleTimer += Time.deltaTime;
        if (_idleTimer > _idleDuration)
        {
            _movingLeft = !_movingLeft; //swap moving direction false is right
        }
    }

    private void OnDisable()
    {
        //Animation.SetBool("move", false);
    }

}
