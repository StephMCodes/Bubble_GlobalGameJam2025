using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _leftEdge;
    [SerializeField] private Transform _rightEdge;
    [SerializeField] private Transform _enemy;
    [SerializeField] private float _speed;



    private void MoveTowards(int _direction)
    {
        //make enemy face correctly

        //move
        _enemy.position = new Vector3(_enemy.position.x + Time.deltaTime * _direction * _speed, _enemy.position.y, _enemy.position.z);
    }

}
