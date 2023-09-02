using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        SpikeBox, Saw, MovingPlatForm
    }

   
    Rigidbody2D Rb;
    public EnemyType Type;
    
    Vector3 OriginPosition;
    Animator Ani;
    

    //«√∂Û¿◊∆“ [¿Ãµø«√∑ß∆˚]
    private Vector2 flying_vec;
    [SerializeField] private float flying_speed;
    private Vector2 Scan_Vec;
    [SerializeField] bool ScanOk;
    
    //Ω∫∆ƒ¿Ã≈©∏« [∆Æ∑¶]
     public float Timer;
    public bool AttackEnd;
    
    //≈È≥ØπŸƒ˚ [∆Æ∑¶]

    private Vector2 saw_vec;
    [SerializeField] private float saw_speed;
    private Vector2 scanSaw_Vec;

    [Space]
    public bool isSawOk;
    

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
        Scan_Vec = Vector2.left;
        saw_vec = Vector2.left;


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

            
        }

    }

    //Ω∫∆ƒ¿Ã≈©∏« ¿ß∑Œø√∂Û±‚±‚¿ß«— ¿Ã≥—∑π¿Ã≈Õ
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
