using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eagle : MonoBehaviour
{
     
    Rigidbody2D Rb;
    Transform a, b;
     //2가지 유형을 가짐 랜덤유형
    // 직선
    // 곡선
    Vector3 dir;
    float diry;
    [Range(0, 20f)] public float dis = 1;
    [Range(0,20f)] public float height = 1;
    [Range(0,20f)] public float bindo = 1;
    Animator ani;
    Vector3 curVecX;
    bool dead;
     
    [SerializeField] public int moveType;
    //속도 
    [SerializeField] private float speed;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        a = transform.parent.GetChild(1).GetComponent<Transform>();
        b = transform.parent.GetChild(2).GetComponent<Transform>();
        
        dir = Vector3.left;
        moveType = Random.Range(0, 2);
        ani = GetComponent<Animator>();
    }
   

    // Update is called once per frame
    void Update()
    {
        EagleMove();
    }

    bool once;
    private void EagleMove()
    {
        switch (moveType)
        {
            case 0:
                if (!dead)
                {
                    if (Vector2.Distance(transform.position, a.position) < 0.1f)
                    {
                        dir = Vector3.left;
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (Vector2.Distance(transform.position, b.position) < 0.1f)
                    {
                        dir = Vector3.right;
                        transform.localScale = new Vector3(-1, 1, 1);

                    }
                    Rb.velocity = dir * speed;
                }
                else
                {
                    if (!once)
                    {
                        once = true;
                        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
                        Rb.velocity = Vector3.zero;
                    }
                 
                    Rb.gravityScale =0.3f;
                }
              

                break;

                case 1:
                if (!dead)
                {
                    if (transform.position.x > curVecX.x + dis)
                    {
                        dir = Vector3.left;
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (transform.position.x < curVecX.x + -dis)
                    {
                        dir = Vector3.right;
                        transform.localScale = new Vector3(-1, 1, 1);

                    }
                    diry = Mathf.Sin(Time.time * bindo) * height;

                    Rb.velocity = new Vector2(dir.x * speed, diry);
                }
                else
                {
                    if (!once)
                    {
                        once = true;
                        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
                        Rb.velocity = Vector3.zero;
                    }
                    Rb.gravityScale = 0.3f;
                }
                break;

        }
    }

    bool once1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (!once1)
            {
                once1 = true;
                StartCoroutine(DeadEagle());
                GameManager.Instance.curEagle--;
            }
    
        }
    }
    IEnumerator DeadEagle()
    {
        dead = true;
        ani.SetBool("Dead", true);
        GameManager.Instance.deathEagleConter++;

        yield return new WaitForSecondsRealtime(1.5f);
        transform.parent.gameObject.SetActive(false);
    }
    public void F_SetEaglePos()
    {
        curVecX = transform.position;
    }
}
