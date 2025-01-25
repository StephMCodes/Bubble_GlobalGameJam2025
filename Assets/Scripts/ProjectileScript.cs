using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _direction;
    [SerializeField] private float resetTime;
    private bool _hit;
    private float _lifetime;
    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        boxCollider=GetComponent<BoxCollider2D>();
        anim=GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ActivateProjectile()
    {
        _hit = false;
        _lifetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (_hit) return; //if we hit something move on
        {
            float mvtSpeed = _speed * Time.deltaTime * _direction;
            transform.Translate(mvtSpeed, 0, 0); //move projectile on x axis

            _lifetime = Time.deltaTime;
            if (_lifetime > resetTime)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hit = true;
        boxCollider.enabled = false;
        //projectile gone
    }

    public void SetDirection(float direction)
    {  
       _direction = direction;
        gameObject.SetActive(true);
        //reset state
        _hit = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);

    }
}
