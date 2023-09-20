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

    SpriteRenderer Sr;

    float z;

    private void Awake()
    {
        Arrowbox = GameObject.FindAnyObjectByType<arrowAttack>();
        Rb = GetComponent<Rigidbody2D>();
        Bullet_DMG = 1;
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();
        Sr = GetComponent<SpriteRenderer>();
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
        gameObject.SetActive(false);
        Rb.velocity = Vector3.zero;
        Arrowbox.F_SetArrow(gameObject);
        trail.Clear();
    }

    IEnumerator Returns(GameObject obj)
    {
        Sr.enabled = false;

        yield return new WaitForSecondsRealtime(3f);
        PoolManager.Instance.F_ReturnObj(obj, "Dust");
        obj.SetActive(false);

        Sr.enabled = true;
        gameObject.SetActive(false);
       
        
        Arrowbox.F_SetArrow(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Eagle"))
        {
            Debug.Log("¡¯¿‘");
            GameManager.Instance.curEagle--;
            GameObject obj = PoolManager.Instance.F_GetObj("Dust");
            obj.transform.position = this.gameObject.transform.position;
            ParticleSystem sc1 = obj.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
            sc1.Play();

            F_BulletReturn();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject obj = PoolManager.Instance.F_GetObj("Dust");
            obj.transform.position = this.gameObject.transform.position;
            ParticleSystem sc1 = obj.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
            sc1.Play();
            Enemys sc = collision.gameObject.GetComponent<Enemys>();
            sc.F_OnHIt(Bullet_DMG);
            F_BulletReturn();
        }


        if (collision.gameObject.CompareTag("Ghost"))
        {
            GameObject obj = PoolManager.Instance.F_GetObj("Dust");
            obj.transform.position = this.gameObject.transform.position;
            ParticleSystem scc = obj.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
            scc.Play();
            Ghost sc = collision.gameObject.GetComponent<Ghost>();
            sc.F_OnHIt(Bullet_DMG);
            F_BulletReturn();
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            GameObject obj = PoolManager.Instance.F_GetObj("Dust");
            obj.transform.position = this.gameObject.transform.position;
            ParticleSystem sc = obj.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
            sc.Play();

            //StartCoroutine(Returns(obj));
            F_BulletReturn();
        }

    }
}
