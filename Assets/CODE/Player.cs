using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//23년 8월 28일 시작
//23년 8월 30일 플라잉팬, 스파이크트랩,구르기(회피),근접무기 애니메이션, 에너미B제작
public class Player : MonoBehaviour
{

    public Rigidbody2D Rb;
    public Animator Ani;
    public SpriteRenderer Sr;
   
    //캐릭터이동
    public Vector2 Char_Vec;
    private Vector2 VZ = Vector2.zero;
    [SerializeField] private float Char_Speed;
    [SerializeField] private float Char_MaxSpeed;
    [SerializeField] private float KB_Power;
    private bool isCharMove;
    bool OnDMG;
    private bool isGround;
    public bool isDodge;
    public float DodgeSpeed;
    public bool KB;
    //현재 보는방향
    public bool isLeft;

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

    // 무기방패 위치이동
    private Transform weapon;
    TrailRenderer weaponTrail;
    Vector3 weaponOriginPos;
    private Transform sheld;
    Vector3 sheldOriginPos;
    private SpriteRenderer sheldSR;
    Transform Bow;
    public Transform RealBow;
    private bool ShieldOn;

    //원거리모드 위치고정
    private bool isAiming;
    

    // 무기 근접속도
    [Header("# 근접공격")]
    [SerializeField] private float MeleeSpeed;
    [SerializeField] private float Timer;
    Animator SwordAni;
    Transform Sword;
    Transform Defence;

    //캐릭터 말풍선
    Transform PlayerMSGUI;
   
    TMP_Text text;
    Animator textani;
    //게임UI
    Transform gameUiMain;
    Transform weaponBtn1;
    Transform btnBoxOutLine1;
    Animator btn1;
    Transform weaponBtn2;
    Transform btnBoxOutLine2;
    Animator btn2;
    public bool MovingStop;
    private void Awake()
    {
       
        Rb = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
        weapon1 = transform.GetChild(0).GetComponent<Transform>();
        Sword = transform.GetChild(0).GetChild(0).GetComponent<Transform>();
        sheld = transform.Find("Sheld").GetComponent<Transform>();
        sheldSR = sheld.GetComponent<SpriteRenderer>();
        SwordAni = weapon1.GetComponent<Animator>();
        Defence = transform.GetChild(2).GetComponent<Transform>();
        PlayerMSGUI = GameObject.Find("PlayerMSG").GetComponent<Transform>();
        text = PlayerMSGUI.transform.GetChild(0).GetComponent<TMP_Text>();
       weaponOriginPos = weapon1.transform.position;
        sheldOriginPos = sheld.transform.position;
        Bow = GameObject.Find("ArrowDir").GetComponent <Transform>();
        RealBow = Bow.transform.GetChild (0).GetComponent<Transform>();
        gameUiMain = GameObject.Find("GameUI").GetComponent<Transform>();
        weaponBtn1 = gameUiMain.transform.Find("Btn1").GetComponent<Transform>();
        btnBoxOutLine1 = weaponBtn1.transform.Find("BoxOutLine").GetComponent< Transform > ();
        weaponBtn2 = gameUiMain.transform.Find("Btn2").GetComponent<Transform>();
        btnBoxOutLine2 = weaponBtn2.transform.Find("BoxOutLine").GetComponent<Transform>();
        btn1 = weaponBtn1.GetComponent<Animator>();
        btn2 = weaponBtn2.GetComponent<Animator>();
        textani = text.GetComponent<Animator>();
        weaponTrail = Sword.GetComponent<TrailRenderer>();
    }
    
   

    void Update()
    {
        MovingStop = GameManager.Instance.MovingStop;
        if( MovingStop )
        {
            RealBow.gameObject.SetActive(false);
        }
        SetCharDir();
        F_CharJump();
        F_WallJump();
        SuchTalk();
        F_SpRecovery();
        F_HpRecovery();
        F_MeleeAttack();
        SheldOn();
        F_TextBoxPos();
        AttackModeShow();
    }
    private void FixedUpdate()
    {
        
        F_MovdChar();
        F_CharAniParameter();
        F_WallCheaking();
        
    }

    bool meleeitemshowok;
    bool rangeitemshowok;
    private float ModeChangeTimer;
    private void AttackModeShow()
    {
        if (!MovingStop)
        {
        
            ModeChangeTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (GameManager.Instance.meleeMode)
                {
                    return;
                }
                if (ModeChangeTimer > 0.4f)
                {
                    Ani.SetTrigger("ModeChange");
                    F_CharText("Melee");
                    GameManager.Instance.meleeMode = true;
                    btn1.SetTrigger("Ok");
                    ModeChangeTimer = 0;
                }

            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !ShieldOn)
            {
                if (!GameManager.Instance.meleeMode)
                {
                    return;
                }
                if (ModeChangeTimer > 0.4f)
                {
                    Ani.SetTrigger("ModeChange");
                    btn2.SetTrigger("Ok");
                    F_CharText("Range");
                    GameManager.Instance.meleeMode = false;
                    ModeChangeTimer = 0;
                }

            }

            //근접모드
            if (GameManager.Instance.meleeMode)
            {
                rangeitemshowok = false;
                if (meleeitemshowok) { return; }
                //좌측하단 무기UI바 아웃라인체크 활성화
                btnBoxOutLine1.gameObject.SetActive(true);
                btnBoxOutLine2.gameObject.SetActive(false);
                if (Iswall || DJumpOn || JumpOn || isDodge)
                {
                    return;
                }

                if (Defence.gameObject.activeSelf)
                {
                    return;
                }
                else if (!Defence.gameObject.activeSelf)
                {
                    StartCoroutine(mellemodeitemshow());
                }
            }

            // 원거리모드
            else if (GameManager.Instance.rangeMode)
            {
                meleeitemshowok = false;
            
                    // 활 마우스 컨트롤
                    if (Input.GetMouseButton(1))
                    {
                        isAiming = true;
                        RealBow.gameObject.SetActive(true);
                    }
                    if (Input.GetMouseButtonUp(1) )
                    {
                        isAiming = false;
                        RealBow.gameObject.SetActive(false);
                    }
              
                

                // 한번 모드변경햇다면 예외처리
                if (rangeitemshowok) { return; }
                //좌측하단 무기UI바 아웃라인체크 활성화
                btnBoxOutLine1.gameObject.SetActive(false);
                btnBoxOutLine2.gameObject.SetActive(true);
                if (DJumpOn || JumpOn || isDodge || Iswall)
                {
                    return;
                }
                //근접무기 비활성화
                weapon1.gameObject.SetActive(false);
                sheld.gameObject.SetActive(false);
                rangeitemshowok = true;
            }
        }
     
    }

    IEnumerator mellemodeitemshow()
    {
        meleeitemshowok = true;

        yield return new WaitForSecondsRealtime(0.2f);
        weapon1.gameObject.SetActive(true);
        sheld.gameObject.SetActive(true);
        RealBow.gameObject.SetActive(false);
        

    }

         
    private void F_TextBoxPos()
    {
        PlayerMSGUI.transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f);
    }
    
    //캐릭터 상태 문구
    //애니메이션에서 SetActive false처리함
    public void F_CharText(string _value)
    {
       
        switch (_value)
        {
            case "SP":
                text.gameObject.SetActive(true);
                text.color = Color.blue;
                text.text = "SP가 부족합니다...";
                textani.SetTrigger("Ok");
                break;

            case "Melee":
                text.gameObject.SetActive(true);
                text.color = Color.white;
                text.text = "근접모드";
                textani.SetTrigger("Ok");
               
           
                break;

            case "Range":
                
                text.gameObject.SetActive(true);
                text.color = Color.white;
                text.text = "원거리모드";
                textani.SetTrigger("Ok");
               
                break;

            case "Arrow":
                text.gameObject.SetActive(true);
                text.color = Color.red;
                text.text = "화살이 부족합니다..";
                textani.SetTrigger("Ok");
                break;


        }
    }
    public void TextOff()
    {
        text.gameObject.SetActive(false);
    }
    //근접공격
    private void F_MeleeAttack()
    {
        if (!MovingStop)
        {
            if (GameManager.Instance.meleeMode)
            {
                Timer += Time.deltaTime;
                if (Input.GetMouseButton(0) && Timer > MeleeSpeed && !Iswall && !isDodge && !DJumpOn & !JumpOn)
                {
                    SwordAni.SetTrigger("R");
                    Timer = 0;
                    //StartCoroutine(IE_MeleeAttack());
                }
            }
        }
    }

    //공격레이어를 애니메이션 함수로 옮겻음 [23.09.10]
    //IEnumerator IE_MeleeAttack()
    //{
    //    //Sword.gameObject.layer = 15;
    //    //SwordAni.SetTrigger("R");
    //    //Timer = 0;

    //    //yield return new WaitForSecondsRealtime(0.5f);

    //    //Sword.gameObject.layer = 16;
    //}

    //방패막기
    private void SheldOn()
    {
        if (!MovingStop)
        {
            if (GameManager.Instance.meleeMode)
            {
                if (Input.GetMouseButton(1))
                {
                    ShieldOn = true;
                    weapon1.gameObject.SetActive(false);
                    sheld.gameObject.SetActive(false);
                    Defence.gameObject.SetActive(true);
                }
                if (Input.GetMouseButtonUp(1))
                {
                    ShieldOn = false;
                    weapon1.gameObject.SetActive(true);
                    sheld.gameObject.SetActive(true);
                    Defence.gameObject.SetActive(false);
                }
            }
        }
       
    }
    //캐릭터방향 불값 저장
    private void SetCharDir()
    {
        if(transform.localScale.x == -3)
        {
            isLeft = true;
        }
        else if(transform.localScale.x == 3)
        {
            isLeft = false;
        }
    }
   

    // Scan NPC && 오브젝트
    Vector3 ScanDir;
    private void SuchTalk()
    { 
        if (Rb.velocity.x < 0 && Char_Vec.x < 0)
        {
            ScanDir = Vector3.left;
        }
        else if(Rb.velocity.x > 0 && Char_Vec.x > 0)
        {
            ScanDir = Vector3.right;
        }
        Debug.DrawRay(transform.position, CastDir * 1.5f,Color.red);

        if (Input.GetKeyDown(KeyCode.F))
        {
             //상호작용 글 다 안읽으면 못넘기게함 (게시판이나 npc)
            if(GameManager.Instance.text.NextTextOk)
            { return; }

             Scanobj = Physics2D.Raycast(transform.position, ScanDir, 1.5f, LayerMask.GetMask("NPC"));

            if(Scanobj.collider != null)
            {
                Rb.velocity = Vector2.zero;
                ScanObject = Scanobj.collider.gameObject;
                GameManager.Instance.F_TalkSurch(ScanObject);
            }
        }
    }
  

    //캐릭터 이동
    private void F_MovdChar()

    {
        if (!MovingStop)
        {
            //이동
            if (!OnDMG && !GameManager.Instance.isPlayerDead && !wallJumpon && !isDodge && !GameManager.Instance.isTalking)
            {
                Char_Vec.x = Input.GetAxisRaw("Horizontal");
                Rb.velocity = new Vector3(Char_Vec.x * Char_Speed, Rb.velocity.y);
            }
            //캐릭터 방향 스케일
            if (GameManager.Instance.meleeMode)
            {
                if (Rb.velocity.x > 0 && Char_Vec.x > 0 && !KB)
                {
                    transform.localScale = new Vector3(3, 3, 1);
                }
                else if (Rb.velocity.x < 0 && Char_Vec.x < 0 && !KB)
                {
                    transform.localScale = new Vector3(-3, 3, 1);
                }
            }

            if(GameManager.Instance.rangeMode) 
            {
                if (!isAiming)
                {
                    if (Rb.velocity.x > 0 && Char_Vec.x > 0 && !KB)
                    {
                        transform.localScale = new Vector3(3, 3, 1);
                    }
                    else if (Rb.velocity.x < 0 && Char_Vec.x < 0 && !KB)
                    {
                        transform.localScale = new Vector3(-3, 3, 1);
                    }
                }
                else
                {
                    if (GameManager.Instance.AimLeft)
                    {
                        transform.localScale = new Vector3(-3, 3, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(3, 3, 1);
                    }
                }
              
            }
           
            //구르기
            if (Input.GetKey(KeyCode.LeftControl) && !isDodge && !JumpOn && !Iswall && !DJumpOn)
            {
                if (GameManager.Instance.Player_CurSP < 15)
                {
                    F_CharText("SP");
                    return;
                }
                else if (GameManager.Instance.Player_CurSP > 15)
                {
                    weaponTrail.Clear();
                    isDodge = true;
                    Rb.velocity = Vector2.zero;
                    sheld.gameObject.SetActive(false);
                    weapon1.gameObject.SetActive(false);
                    GameManager.Instance.Player_CurSP -= 15;
                   
                   

                    if (!isLeft)
                    {
                        Rb.velocity = new Vector3(1, 0) * DodgeSpeed;
                        gameObject.layer = 10;
                        Invoke("F_ReturnLayer", 0.5f);
                        Ani.SetTrigger("Dodge");
                    }
                    else if (isLeft)
                    {
                        Rb.velocity = new Vector3(-1, 0) * DodgeSpeed;
                        gameObject.layer = 10;
                        Invoke("F_ReturnLayer", 0.5f);
                        Ani.SetTrigger("Dodge");

                    }
                }
            }
        }
      
    }

    private void F_ReturnLayer()
    {
        weaponTrail.Clear();
        gameObject.layer = 6;
        isDodge = false;
        sheld.gameObject.SetActive(true);
        weapon1.gameObject.SetActive(true);
    }


    //벽점프
    //bool 점프딜레이
    float JumpTime;
    public bool wallJumpon;
    private void F_WallJump()
    {
        if (Iswall)
        {
            JumpTime += Time.deltaTime;
            wallJumpon = false;
            RealBow.gameObject.SetActive(false);
            sheld.gameObject.SetActive(false);
            weapon1.gameObject.SetActive(false);
            Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * SliedSpeed);

            if (Input.GetButtonDown("Jump") && Iswall && JumpTime >= 0.1f)
            {
                                
                // 벽 점프 
                if (!isLeft)
                {
                    isLeft = true;
                    wallJumpon = true;
                    CastDir = Vector2.left;
                    Iswall = false;
                    Rb.velocity = new Vector2(-1 * WalljumpPower, 1.5f * WalljumpPower);

                    if (Rb.velocity.x < 0) { transform.localScale = new Vector3(-3, 3, 3); }
                    //Invoke("F_WallJumpOff", 0.5f);
                }
                else if (isLeft)
                {
                    isLeft = false;
                    wallJumpon = true;
                    CastDir = Vector2.right;
                    Iswall = false;
                    Rb.velocity = new Vector2(1* WalljumpPower, 1.5f * WalljumpPower);

                    if (Rb.velocity.x > 0) { transform.localScale = new Vector3(3, 3, 3); 
                    }
                    //Invoke("F_WallJumpOff", 0.5f);

                }

                JumpTime = 0;
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
        if(!isLeft/*Rb.velocity.x > 0*/)
        {
            CastDir = Vector2.right;
        }
        else if(isLeft/*Rb.velocity.x < 0*/)
        {
            CastDir = Vector2.left;
        }
        
        //벽 Wall 체크
        Iswall = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, Wall_Layer);
                
        //바닥체크
        isGround = Physics2D.Raycast(transform.position, Vector2.down, 0.65f, LayerMask.GetMask("Ground"));
    }


    //캐릭터 점프
    [Header("Jump")]
    [SerializeField] float JumpPower;
    [SerializeField] int JumpCount;
    [SerializeField] bool JumpOn;
    [SerializeField] bool DJumpOn;
    
    private void F_CharJump()
    {
        if(!MovingStop)
        {
            if (Input.GetButtonDown("Jump") && JumpCount < 2 && !OnDMG & !Iswall && !isflying && !GameManager.Instance.isTalking && !wallJumpon)
            {
                JumpOn = true;
                weaponTrail.Clear();
                JumpCount++;
                Ani.SetBool("Jump", true);
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);


                //2단점프 제어
                if (JumpCount == 2)
                {
                    if (GameManager.Instance.meleeMode)
                    {
                        sheld.gameObject.SetActive(false);
                        weapon1.gameObject.SetActive(false);
                    }
                    else
                    {
                        RealBow.gameObject.SetActive(false);

                    }

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
      

    }
    
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
                if (isLeft)
                {
                    Rb.AddForce(new Vector3(3 * KB_Power, 6), ForceMode2D.Impulse);
                    KB = true;
                }
                else if (!isLeft)
                {
                    Rb.AddForce(new Vector3(-3 * KB_Power, 6), ForceMode2D.Impulse);
                    KB = true;
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
        Ani.SetBool("RangeMode", GameManager.Instance.rangeMode);
        Ani.SetBool("MeleeMode", GameManager.Instance.meleeMode);

    }

    private void F_JumpReset()
    {
        weaponTrail.Clear();
        JumpOn = false;
        DJumpOn = false;
        Ani.SetBool("Jump", false);
        JumpCount = 0;
        wallJumpon = false;
        if (GameManager.Instance.meleeMode)
        {
            sheld.gameObject.SetActive(true);
            weapon1.gameObject.SetActive(true);
        }
        else
        {
          
        }
     
        KB = false;
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

    //체력자연회복
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
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    Rb.velocity = Vector3.zero;
        //}

        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(F_OnHit());
            JumpOn = false;
            F_JumpReset();
        }
        if (collision.gameObject.CompareTag("Ghost"))
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


