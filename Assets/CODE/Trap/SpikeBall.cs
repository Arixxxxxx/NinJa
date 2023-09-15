using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    public enum whereIgo
    {
        left,center,right 
    }

    public whereIgo where;


    Rigidbody2D Rb;
    SpikeTrap_Ground parent;
    private Vector2 OriginPos;
    public bool AttackEnd;
    [Range(0f,20f)] public float Power;
    SpikeTrap_Ground spike_parent;
    private void Awake()
    {
        parent = transform.GetComponentInParent<SpikeTrap_Ground>();
        Rb = GetComponent<Rigidbody2D>();
        OriginPos =  new Vector2(transform.position.x, transform.position.y);
    }
    private void Update()
    {
        if(Rb.velocity.y < -8.5f)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, -8.5f);
        }
    }
    private void OnEnable()
    {
        switch (where)
        {
                case whereIgo.left:
                Rb.AddForce(new Vector2(-1 * Power, Rb.velocity.y), ForceMode2D.Impulse);
                break;

                case whereIgo.center:
                Rb.AddForce(new Vector2(0 * Power, Rb.velocity.y), ForceMode2D.Impulse);
                break;

                case whereIgo.right:
                Rb.AddForce(new Vector2(1 * Power, Rb.velocity.y), ForceMode2D.Impulse);
                break;

        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Ground")) ;
        {
            Debug.Log("´êÀ½");
            Invoke("F_OffSpike", 2);
        }
    }

    private void F_OffSpike()
    {
        gameObject.SetActive(false);
        transform.position = OriginPos;
        AttackEnd=true;

    }
}
