using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D Rb;
    private int Bullet_DMG;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();

        Bullet_DMG = 1;
    }

    private void Update()
    {
       
    }
    Vector2 ShootPower;
    private void OnEnable()
    {
        if (GameManager.Instance.player.Sr.flipX == true)
        {
               ShootPower = new Vector3(-1, 0.25f);
        }
        else if (GameManager.Instance.player.Sr.flipX == false)
        {
               ShootPower = new Vector3(1, 0.25f);
        }

        

        Invoke("F_BulletReturn", 0.5f);
        // ¹ß»ç ÈûÀü´Þ

        Rb.AddForce(ShootPower *20, ForceMode2D.Impulse);
    }


    private void F_BulletReturn ()
    {
        Rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
        PoolManager.Instance.F_ReturnObj(gameObject,"Bullet");
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemys sc = collision.gameObject.GetComponent<Enemys>();
            sc.F_OnHIt(Bullet_DMG);
            F_BulletReturn();
        }
    }
}
