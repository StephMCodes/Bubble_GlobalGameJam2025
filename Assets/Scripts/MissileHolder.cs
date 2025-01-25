using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
