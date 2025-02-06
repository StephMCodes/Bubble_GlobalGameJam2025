using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHair2 : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("isShortHair", false);
        _animator.SetBool("isMediumHair", true);
    }
}
