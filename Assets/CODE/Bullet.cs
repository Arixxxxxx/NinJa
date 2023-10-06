using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum ArrowType
    {
        normal, boomArrow, boom, triple
    }
    public ArrowType type;


    private Rigidbody2D Rb;
    private float Bullet_DMG;
    Transform OriginBullet;
    Vector2 ArrowDir;
    TrailRenderer trail;
    arrowAttack Arrowbox;
    public AudioSource Audio;
    SpriteRenderer Sr;

    float z;

    private void Awake()
    {
        Arrowbox = GameObject.FindAnyObjectByType<arrowAttack>();
        Rb = GetComponent<Rigidbody2D>();
        Bullet_DMG = 1;
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();
        Sr = GetComponent<SpriteRenderer>();
        Audio = GetComponent<AudioSource>();
     }
   
    private void Update()
    {
       transform.right = Rb.velocity;
        
    }
  
    private void OnEnable()
    {
         Invoke("F_BulletReturn", 1.5f);
        Audio.volume = 0.8f;
    }


    private void F_BulletReturn (ArrowType type)
    {
        gameObject.SetActive(false);
        Rb.velocity = Vector3.zero;
        Arrowbox.F_SetArrow(gameObject, type);
        if(transform.GetChild(0).GetComponent<ParticleSystem>() != null)
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
      
        if (trail != null) 
            {
                trail.Clear();
            } 
    }

    IEnumerator Returns(GameObject obj)
    {
        Sr.enabled = false;

        yield return new WaitForSecondsRealtime(3f);
        PoolManager.Instance.F_ReturnObj(obj, "Dust");
        obj.SetActive(false);

        Sr.enabled = true;
        gameObject.SetActive(false);
       
        
        Arrowbox.F_SetArrow(gameObject,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Eagle"))
        {
            switch (type)
            {
                case ArrowType.normal:

                    //SoundManager.instance.F_SoundPlay(SoundManager.instance.rangeHit, 0.7f);

                    //GameManager.Instance.curEagle--;
                    //GameObject obj = PoolManager.Instance.F_GetObj("Dust");
                    //obj.transform.position = this.gameObject.transform.position;
                    //ParticleSystem sc1 = obj.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
                    //sc1.Play();
                    GameManager.Instance.curEagle--;
                    normalArrow();
                    F_BulletReturn(ArrowType.normal);
                    break;

                case ArrowType.boomArrow:
                    Boom();
                    break;

                case ArrowType.triple:
                    break;

            }
           
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            switch (type)
            {
                case ArrowType.normal:
                    normalArrow();

                    Enemys sc = collision.gameObject.GetComponent<Enemys>();
                    sc.F_OnHIt(SkillManager.instance.RangeDmg);

                    F_BulletReturn(ArrowType.normal);
                    break;

                case ArrowType.triple:
                    TripleShot();

                    Enemys scs = collision.gameObject.GetComponent<Enemys>();
                    scs.F_OnHIt(SkillManager.instance.tripleShotDmg);
                    F_BulletReturn(type);
                    break;

                case ArrowType.boomArrow:
                    Boom();
                    break;
            }
          
        }


        if (collision.gameObject.CompareTag("Ghost"))
        {
            switch (type)
            {
                case ArrowType.normal:
                    normalArrow();

                    Ghost sc = collision.gameObject.GetComponent<Ghost>();
                    sc.F_OnHIt(SkillManager.instance.RangeDmg);
                    F_BulletReturn(ArrowType.normal);
                    break;

                case ArrowType.triple:

                    break;

                case ArrowType.boomArrow:
                    Boom();
                    break;
            }
            
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            switch (type)
            {
                case ArrowType.normal:

                    normalArrow();
                    F_BulletReturn(type);

                    break;

                case ArrowType.boomArrow:
                    Boom();
                    break;

                    case ArrowType.triple:
                    TripleShot();
                    F_BulletReturn(type);
                    break;

            }
           
        }

    }

    private void normalArrow()
    {
        SoundManager.instance.F_SoundPlay(SoundManager.instance.rangeHit, 0.7f);
        GameObject obj = PoolManager.Instance.F_GetObj("Dust");
        obj.transform.position = this.gameObject.transform.position;
        ParticleSystem sc1 = obj.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        sc1.Play();
    }

    private void TripleShot()
    {
        SoundManager.instance.F_SoundPlay(SoundManager.instance.rangeHit, 0.7f);
        GameManager.Instance.curEagle--;
        GameObject obj = PoolManager.Instance.F_GetObj("Dust");
        obj.transform.position = this.gameObject.transform.position;
        ParticleSystem sc1 = obj.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        sc1.Play();
    }


    private void Boom()
    {
        arrowAttack.Instance.AttackCameraShake();
        SoundManager.instance.F_SoundPlay(SoundManager.instance.boomArrow, 0.7f);
        GameObject obj = arrowAttack.Instance.F_Get_Boom();
        obj.transform.position = transform.position;
        obj.GetComponent<ParticleSystem>().Play();

        F_BulletReturn(type);
    }

    
}
