using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        SpikeBox, Saw, MovingPlatForm, HitBox
    }

   
    Rigidbody2D Rb;
    public EnemyType Type;
    
    Vector3 OriginPosition;
    Animator Ani;
    

    //플라잉팬 [이동플랫폼]
    private Vector2 flying_vec;
    [SerializeField] private float flying_speed;
    private Vector2 Scan_Vec;
    [SerializeField] bool ScanOk;
    
    //스파이크맨 [트랩]
     public float Timer;
    public bool AttackEnd;
    
    //톱날바퀴 [트랩]

    private Vector2 saw_vec;
    [SerializeField] private float saw_speed;
    private Vector2 scanSaw_Vec;

    [Space]
    public bool isSawOk;

    //히트박스
    private int HitBoxHp;
    Transform[] brokenbox;
    bool boxhit;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
        Scan_Vec = Vector2.left;
        saw_vec = Vector2.left;
        HitBoxHp = 2;
        brokenbox = new Transform[6];

    }


    void Update()
    {
    }

    private void FixedUpdate()
    {
        F_ObjectMove();
    }

    private void F_ObjectMove() 
    {
        Timer += Time.deltaTime;
        switch (Type)
        {
            case EnemyType.SpikeBox:

                if (Timer < 3.2f && AttackEnd)
                {
                    Rb.gravityScale = 0;
                    Rb.MovePosition(Rb.position + Vector2.up * 4f * Time.fixedDeltaTime);

                    if (Timer > 3f)
                    {
                        AttackEnd = false;
                    }
                }
                if (!AttackEnd)
                {
                    Rb.gravityScale = 6f;
                }
                break;

            case EnemyType.MovingPlatForm:
                
                flying_vec = Scan_Vec * flying_speed * Time.fixedDeltaTime;
                RaycastHit2D ScanWall = Physics2D.Raycast(transform.position, Scan_Vec, 0.8f, LayerMask.GetMask("Wall"));
                RaycastHit2D ScanWall2 = Physics2D.Raycast(transform.position, Scan_Vec, 0.8f, LayerMask.GetMask("Ground"));

                if (ScanWall)
                {
                    Scan_Vec = Vector2.Reflect(Scan_Vec, ScanWall.normal);
                }
                else if(ScanWall2)
                {
                    Scan_Vec = Vector2.Reflect(Scan_Vec, ScanWall2.normal);
                }
                
                Rb.MovePosition(Rb.position + flying_vec);
                break;

            case EnemyType.Saw:

                saw_vec = scanSaw_Vec * saw_speed * Time.fixedDeltaTime;
                
                RaycastHit2D sawhit = Physics2D.Raycast(transform.position, scanSaw_Vec, 2, LayerMask.GetMask("Wall"));
                isSawOk = Physics2D.Raycast(transform.position, scanSaw_Vec, 2, LayerMask.GetMask("Wall"));
                if (sawhit)
                {
                    scanSaw_Vec = Vector2.Reflect(scanSaw_Vec, sawhit.normal);
                }
              
                Rb.MovePosition(Rb.position + saw_vec);
                break;

         
               

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (Type)
        {
            case EnemyType.SpikeBox:
                if (collision.gameObject.CompareTag("Ground") && !AttackEnd)
                {
                    StartCoroutine(ReturnSpikeEnemy());
                    Ani.SetBool("BHIT", true);
                }

                if (collision.gameObject.CompareTag("Enemy"))
                {
                    Enemys sc = collision.gameObject.GetComponent<Enemys>();
                    sc.F_OnHIt(1);

                }
                break;
            case EnemyType.HitBox:
                {
                    if (collision.gameObject.layer== 15 || collision.gameObject.CompareTag("Bullet"))
                        {
                             
                           if(HitBoxHp > 0 & !boxhit)
                           {
                            boxhit = true;
                            HitBoxHp--;
                            Ani.SetTrigger("Hit");
                            Invoke("boxhitok", 0.3f);
                            }

                            else if(HitBoxHp <= 0)
                            {
                                
                              for(int i = 0; i < brokenbox.Length-3; i++)
                              {
                                brokenbox[i] = transform.GetChild(i).GetComponent<Transform>();
                                Rigidbody2D Rbs = brokenbox[i].GetComponent<Rigidbody2D>();

                                brokenbox[i].gameObject.SetActive(true);

                                Rbs.AddForce(new Vector3(Random.Range(-2.2f, 0), Random.Range(3.5f,4.2f)), ForceMode2D.Impulse);
                                
                              }
                            for (int i = 3; i < brokenbox.Length; i++)
                            {
                                brokenbox[i] = transform.GetChild(i).GetComponent<Transform>();
                                Rigidbody2D Rbs = brokenbox[i].GetComponent<Rigidbody2D>();

                                brokenbox[i].gameObject.SetActive(true);

                                Rbs.AddForce(new Vector3(Random.Range(0, 2.2f), Random.Range(3.5f, 4.2f)), ForceMode2D.Impulse);

                            }
                            Invoke("offPayun", 2);
                            SpriteRenderer Sr = transform.GetComponent<SpriteRenderer>();
                                BoxCollider2D bx = transform.GetComponent<BoxCollider2D>();
                                Sr.color = new Color(0, 0, 0, 0);
                                bx.enabled = false;

                            }
                    }
                    break;

                }

        }

    }

    private void boxhitok()
    {
        boxhit = false;
    }
    private void offPayun()
    {
        for(int i = 0; i < brokenbox.Length; i++) 
        {
            brokenbox[i].gameObject.SetActive(false);
        }

    }
    //스파이크맨 위로올라기기위한 이넘레이터
    IEnumerator ReturnSpikeEnemy()
    {
        Timer = 0;
        yield return new WaitForSeconds(1.5f);
        AttackEnd = true;
        Ani.SetBool("BHIT", false);
        OriginPosition = Rb.position;
    }

    private void OnDrawGizmos()
    {
        if(Type == EnemyType.MovingPlatForm)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Scan_Vec * 1);
        }

        if (Type == EnemyType.Saw)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, scanSaw_Vec * 1);
        }
    }
}
