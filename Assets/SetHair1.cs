using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetHair1 : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("isShortHair", true);
    }

}

