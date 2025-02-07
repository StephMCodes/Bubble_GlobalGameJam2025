using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHair3 : MonoBehaviour
{
    Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("isShortHair", false);
        _animator.SetBool("isLongHair", true);
    }
}
