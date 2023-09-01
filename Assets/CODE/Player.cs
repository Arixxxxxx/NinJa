using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//23년 8월 28일 시작
//23년 8월 30일 플라잉팬, 스파이크트랩,구르기(회피),근접무기 애니메이션, 에너미B제작
public class Player : MonoBehaviour
{

    Rigidbody2D Rb;
    Animator Ani;
    public SpriteRenderer Sr;

    //캐릭터이동
    public Vector2 Char_Vec;
    private Vector2 VZ = Vector2.zero;
    [SerializeField] private float Char_Speed;
    [SerializeField] private float Char_MaxSpeed;
    private bool isCharMove;
    bool OnDMG;
    private bool isGround;
    public bool isDodge;
    public float DodgeSpeed;

    //벽체크
    [Header("벽체크")]
    public Transform WallCheck;
    public float WallCheakDis;
    public LayerMask Wall_Layer;
    public bool Iswall;
    public float SliedSpeed;
    public float WalljumpPower;
    Vector2 CastDir;
    Transform weapon1;

    //바람타기
    private bool isflying;

    //NPC 검색
    [Header("# NPC 탐색")]
    public GameObject ScanObject;
    RaycastHit2D Scanobj;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
        weapon1 = gameObject.GetComponentsInChildren<Transform>()[1];

    }

    void Update()
    {
        
        Debug.DrawRay(WallCheck.position, CastDir * 1f, Color.blue);

        F_CharJump();
        F_CharMoveStop();
        F_ShootWeapone();
        F_WallJump();
        SuchTalk();
        F_SpRecovery();
        F_HpRecovery();

    }
    private void FixedUpdate()
    {
        F_MovdChar();
        F_CharAniParameter();
        F_WallCheaking();
        
    }

    Vector3 ScanDir;
    private void SuchTalk()
    { 
        if (Sr.flipX)
        {
            ScanDir = Vector3.left;
        }
        else if(!Sr.flipX) 
        {
            ScanDir = Vector3.right;
        }
        Debug.DrawRay(transform.position, ScanDir * 1.2f,Color.red);

        if (Input.GetKeyDown(KeyCode.F))
        {
             Scanobj = Physics2D.Raycast(transform.position, ScanDir, 1.2f, LayerMask.GetMask("NPC"));

            if(Scanobj.collider != null)
            {
                ScanObject = Scanobj.collider.gameObject;
                GameManager.Instance.F_TalkSurch(ScanObject);
            }
        }
    }


    //캐릭터 이동
    private void F_MovdChar()
    {
        //이동
        if (!OnDMG && !GameManager.Instance.isPlayerDead && !wallJumpon && !isDodge && !GameManager.Instance.isTalking)
        {
            Char_Vec.x = Input.GetAxisRaw("Horizontal");
            Rb.AddForce(Char_Vec * Char_Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        //구르기
        if (Input.GetKey(KeyCode.LeftControl) && !isDodge && !JumpOn && !Iswall && !DJumpOn)
        {
            if(GameManager.Instance.Player_CurSP < 15)
            {
                return;
            }
            else if (GameManager.Instance.Player_CurSP > 15)
            {
                GameManager.Instance.Player_CurSP -= 15;
                Rb.velocity = Vector2.zero;
                isDodge = true;

                if (!Sr.flipX)
                {
                    Rb.AddForce(new Vector3(1, 0) * DodgeSpeed, ForceMode2D.Impulse);
                    gameObject.layer = 10;
                    Invoke("F_ReturnLayer", 0.5f);
                    Ani.SetTrigger("Dodge");
                    weapon1.gameObject.SetActive(false);
                }
                else if (Sr.flipX)
                {
                    Rb.AddForce(new Vector3(-1, 0) * DodgeSpeed, ForceMode2D.Impulse);
                    gameObject.layer = 10;
                    Invoke("F_ReturnLayer", 0.5f);
                    Ani.SetTrigger("Dodge");
                    weapon1.gameObject.SetActive(false);
                }
            }
        }
        //최대속력 제어
        if (Rb.velocity.x > Char_MaxSpeed && !wallJumpon && !isDodge)
        {
            Rb.velocity = new Vector2(Char_MaxSpeed, Rb.velocity.y);
        }
        else if (Rb.velocity.x < Char_MaxSpeed * (-1) && !wallJumpon && !isDodge)
        {
            Rb.velocity = new Vector2(Char_MaxSpeed * (-1), Rb.velocity.y);
        }

        //캐릭터 스프라이트 방향전환
        if (Char_Vec.x < 0 && !Iswall)
        {
            Sr.flipX = true;
        }
        else if (Char_Vec.x > 0)
        {
            Sr.flipX = false;
        }
    }

    private void F_ReturnLayer()
    {
        gameObject.layer = 6;
        isDodge = false;
        weapon1.gameObject.SetActive(true);
    }
    ////캐릭터 일반공격
    //private void F_MeleeAttack()
    //{
    //    Ani.SetTrigger("Attack");
    //}

    //캐릭터 벽점프
    public bool wallJumpon;
    private void F_WallJump()
    {
        if (Iswall)
        {
            

                //벽에 붙으면 속도제어
                Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * SliedSpeed);
            if (CastDir == Vector2.right)
            {
                Sr.flipX = false;
            }
            else if(CastDir == Vector2.left)
            {
                 Sr.flipX= true;
            }

            if (Input.GetButtonDown("Jump") && Iswall)
            {
                   // 벽 점프 
                if (CastDir == Vector2.right)
                {
                    wallJumpon = true;
                    Rb.velocity = Vector3.zero;
                    Rb.AddForce(new Vector2(-1 *  WalljumpPower, 1.6f * WalljumpPower), ForceMode2D.Impulse);
                    Invoke("F_WallJumpOff", 0.4f);
                }
                else if (CastDir == Vector2.left)
                {
                    wallJumpon = true;
                    Rb.velocity = Vector3.zero;
                    Rb.AddForce(new Vector2(1 * WalljumpPower, 1.6f * WalljumpPower), ForceMode2D.Impulse);
                    Invoke("F_WallJumpOff", 0.4f);
                }
            }
        }
     }
    private void F_WallJumpOff()
    {
        wallJumpon = false;
    }
    //벽 & 바닥 체크 RayCast

    private void F_WallCheaking()
    {
        //벽체크 레이캐스트 방향전환
        if(Rb.velocity.x > 0)
        {
            CastDir = Vector2.right;
        }
        else if(Rb.velocity.x < 0)
        {
            CastDir = Vector2.left;
        }
        //바닥체크
        Iswall = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, Wall_Layer); 
        //바닥체크
        isGround = Physics2D.Raycast(transform.position, Vector2.down, 0.65f, LayerMask.GetMask("Ground"));
    }



   //캐릭터 이동하다가 정지시 이동밀림제어
  
    private void F_CharMoveStop()
    {
        if (Input.GetButtonUp("Horizontal") && !wallJumpon)
        {
          Rb.velocity = new Vector2(Rb.velocity.normalized.x * 0.1f, Rb.velocity.y);
        }
    }

    //캐릭터 점프
    [Header("Jump")]
    [SerializeField] float JumpPower;
    [SerializeField] int JumpCount;
    [SerializeField] bool JumpOn;
    [SerializeField] bool DJumpOn;
    
    private void F_CharJump()
    {
        if (Input.GetButtonDown("Jump") && JumpCount < 2&& !OnDMG & !Iswall && !isflying && !GameManager.Instance.isTalking)
        {
            JumpOn = true;
            JumpCount++;
            Ani.SetBool("Jump", true) ;
             Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            
            //2단점프 제어
            if (JumpCount == 2)
            {
                DJumpOn = true;
             }
        }

        // 착지시 bool값 제어
        if (isGround && Rb.velocity.y < 0.3f)
        {
            JumpOn = false;
            DJumpOn = false;
            Ani.SetBool("Jump", false);
            JumpCount = 0;
            OnDMG = false;
          }

    }

    //캐릭터 무기발사
    float ShootTimer;
        
    private void F_ShootWeapone()
    {
        ShootTimer += Time.deltaTime;

        if (Input.GetMouseButton(1) && ShootTimer > 0.2f && !GameManager.Instance.isPlayerDead && !GameManager.Instance.isTalking)
        {
          
            PoolManager.Instance.F_GetObj("Weapone");
            ShootTimer = 0;
            
        }
    }

    //플레이어 피격
   public IEnumerator F_OnHit()
   {
        if (GameManager.Instance.Player_CurHP > 0 && !OnDMG)
        {
            OnDMG = true;
            GameManager.Instance.Player_CurHP--;

            if (GameManager.Instance.Player_CurHP <= 0)
            {
                StartCoroutine(GameOver());
            }
            else
            {
                // 잠시무적
                
                gameObject.layer = 10;
                Sr.color = new Color(1, 1, 1, 0.3f);

                //넉백
                if (Sr.flipX)
                {
                    Rb.AddForce(new Vector3(10, 1) * 8, ForceMode2D.Impulse);
                }
                else if (!Sr.flipX)
                {
                    Rb.AddForce(new Vector3(-10, 1) * 8, ForceMode2D.Impulse);
                }

                yield return new WaitForSeconds(1.5f);

                gameObject.layer = 6;
                Sr.color = new Color(1, 1, 1, 1);
                OnDMG = false;
            }
        }

    }
    

    IEnumerator GameOver()
    {
        GameManager.Instance.isPlayerDead = true;
        weapon1.gameObject.SetActive(false);
        Ani.SetBool("PlayerDead", GameManager.Instance.isPlayerDead);
        gameObject.layer = 14;
        Rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(5);
        //Time.timeScale = 0;
    }


    // 캐릭터 애니메이션
    private void F_CharAniParameter()
    {
        isCharMove = Mathf.Abs(Char_Vec.x) > 0;
        Ani.SetBool("Run", isCharMove);
        Ani.SetBool("DJump", DJumpOn);
        Ani.SetBool("Wall", Iswall);
        
        

    }

    private void F_JumpReset()
    {
        JumpOn = false;
        DJumpOn = false;
        Ani.SetBool("Jump", false);
        JumpCount = 0;
    }
    
    //기력회복
    private void F_SpRecovery()
    {
        if(GameManager.Instance.Player_CurSP > GameManager.Instance.Player_MaxSP)
        {
            GameManager.Instance.Player_CurSP = GameManager.Instance.Player_MaxSP;
        }

       else if(GameManager.Instance.Player_CurSP < GameManager.Instance.Player_MaxSP)
        {
            GameManager.Instance.Player_CurSP += 1 * Time.deltaTime * 4;
        }
            
    }
    private void F_HpRecovery()
    {
        if (GameManager.Instance.Player_CurHP > GameManager.Instance.Player_MaxHP)
        {
            GameManager.Instance.Player_CurHP = GameManager.Instance.Player_MaxHP;
        }

        else if (GameManager.Instance.Player_CurHP < GameManager.Instance.Player_MaxHP)
        {
            GameManager.Instance.Player_CurHP += 1 * Time.deltaTime * 0.05f;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            F_JumpReset();
        }

        if (collision.gameObject.CompareTag("Trap") && Rb.velocity.y < 0.2f)
        {
            F_JumpReset();
            StartCoroutine(F_OnHit());
        }
        if (collision.gameObject.CompareTag("Wall") && Rb.velocity.y < 0.2f)
        {
            F_JumpReset();
        }
        if (collision.gameObject.CompareTag("Saw"))
        {
            F_JumpReset();
            StartCoroutine(F_OnHit());
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Rb.velocity = Vector3.zero;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(F_OnHit());
            JumpOn = false;
            F_JumpReset();
        }
        if (collision.gameObject.CompareTag("Fan"))
        {
            F_JumpReset();
        }
        
    }

    [SerializeField] private Vector2 WindPower;
    [SerializeField] private Vector2 MaxWinY;
    [SerializeField] private float WindP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wind"))
        {
            Rb.velocity = Vector2.up * 3;
            if (!isflying)
            {
                isflying = true;
                
            }
        }
 
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wind"))
        {
            
            WindPower = Vector2.up * WindP;
            MaxWinY = new Vector2(Rb.velocity.x, 30);
            
            if (Rb.velocity.y > MaxWinY.y)
            {
                Rb.velocity = MaxWinY;
            }

            Rb.AddForce(WindPower, ForceMode2D.Force);
        }
    }
    private Vector2 DropVelo;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wind"))
        {
            DropVelo = new Vector2(Rb.velocity.x, 7);
            Rb.velocity = DropVelo;
            if(isflying)
            {
                isflying = false;
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(WallCheck.position, CastDir * WallCheakDis);
    }

}
