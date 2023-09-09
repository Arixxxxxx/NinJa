using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D Rb;
    private int Bullet_DMG;
    Transform OriginBullet;
    Vector2 ArrowDir;
    TrailRenderer trail;
    arrowAttack Arrowbox;
    

    float z;

    private void Awake()
    {
        Arrowbox = GameObject.FindAnyObjectByType<arrowAttack>();
        Rb = GetComponent<Rigidbody2D>();
        Bullet_DMG = 1;
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();
     }

    private void Update()
    {
       transform.right = Rb.velocity;
        
    }
  
    private void OnEnable()
    {
         Invoke("F_BulletReturn", 1.5f);
    }


    private void F_BulletReturn ()
    {
        Rb.velocity = Vector3.zero;
        Arrowbox.F_SetArrow(gameObject);
        trail.Clear();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemys sc = collision.gameObject.GetComponent<Enemys>();
            sc.F_OnHIt(Bullet_DMG);
            F_BulletReturn();
        }
        if(collision.gameObject.CompareTag("Ghost"))
        {
            Ghost sc = collision.gameObject.GetComponent<Ghost>();
            sc.F_OnHIt(Bullet_DMG);
            F_BulletReturn();
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            F_BulletReturn();
        }

    }
}
