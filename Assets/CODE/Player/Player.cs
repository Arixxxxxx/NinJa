using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//23�� 8�� 28�� ����
//23�� 8�� 30�� �ö�����, ������ũƮ��,������(ȸ��),�������� �ִϸ��̼�, ���ʹ�B����
public class Player : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D Rb;
    [HideInInspector] public Animator Ani;
    [HideInInspector] public SpriteRenderer Sr;

    //ĳ�����̵�
    [Header("# ĳ���� �̵�����")]
    [HideInInspector] public Vector2 Char_Vec;
    private Vector2 VZ = Vector2.zero;
    [SerializeField] private float Char_Speed;
    [SerializeField] private float Char_MaxSpeed;
    [SerializeField] private float KB_Power;
    [SerializeField] private float DodgeSpeed;
    private bool isCharMove;
    [Header("# ĳ���� ������� ����")]
    [Space]
    bool OnDMG;
    public bool isGround;
    public bool isFrontGround;
    public bool isDodge;
    public bool KB;
    RaycastHit2D hitPoint;

    //ĳ���� ����
    [Header("# Jump")]
    [Space]
    [SerializeField] float JumpPower;
    private int JumpCount;
    [SerializeField] bool JumpOn;
    [SerializeField] public bool DJumpOn;
    [Range(0f, 10f)][SerializeField] float dropSpeed;
    Vector2 verGravity;

    [SerializeField] Transform groundCheker;



    //���� ���¹���
    [HideInInspector] public bool isLeft;

    //��üũ
    [Header("# ��üũ")]
    public Transform WallCheck;
    public float WallCheakDis;
    public LayerMask Wall_Layer;
    public bool Iswall;
    public float SliedSpeed;
    public float WalljumpPower;
    Vector2 CastDir;
    Transform weapon1;

    //�ٶ�Ÿ��
    private bool isflying;

    //NPC �˻�
    [Header("# NPC Ž��")]
    public GameObject ScanObject;
    RaycastHit2D Scanobj;

    // ������� ��ġ�̵�
    private Transform weapon;
    TrailRenderer weaponTrail;
    Vector3 weaponOriginPos;
    private Transform sheld;
    Vector3 sheldOriginPos;
    private SpriteRenderer sheldSR;
    Transform Bow;
    [HideInInspector] public Transform RealBow;
    private bool ShieldOn;
    [HideInInspector] public bool isAttacking;

    //���Ÿ���� ��ġ����
    private bool isAiming;

    //��ƼŬ ����
    PaticleManager paticle;

    // ���� �����ӵ�
    [Header("# ��������")]
    [SerializeField] private float MeleeSpeed;
    [SerializeField] private float Timer;
    private Animator SwordAni;
    private Transform Sword;
    private Transform Defence;

    //ĳ���� ��ǳ��
    private Transform PlayerMSGUI;
    private TMP_Text text;
    private Animator textani;

    //����UI
    Transform gameUiMain;
    Transform weaponBtn1;
    Transform btnBoxOutLine1;
    Animator btn1;
    Transform weaponBtn2;
    Transform btnBoxOutLine2;
    Animator btn2;
    [HideInInspector] public bool MovingStop;
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
        paticle = transform.Find("Paticle").GetComponent<PaticleManager>();

        //����
        groundCheker = transform.Find("GroundCheker").GetComponent<Transform> ();
        verGravity = new Vector2(0, -Physics2D.gravity.y);
    }
    
   

    void Update()
    {
        Wallok(); // 
        MoveStopFuntion();
        SetCharDir();
        CharJump();
        WallJump();
        SuchTalk();
        SpRecovery();
        HpRecovery();
        MeleeAttack();
        SheldOn();
        F_TextBoxPos();
        AttackModeShow();
        
    }
    private void FixedUpdate()
    {
        
        MovdChar();
        CharAniParameter();
        WallCheaking();
        
    }
  

    //ĳ���� �Ͻ��������
    private void MoveStopFuntion()
    {
        MovingStop = GameManager.Instance.MovingStop;
        if (MovingStop)
        {
            RealBow.gameObject.SetActive(false);
        }
    }
    bool meleeitemshowok;
    bool rangeitemshowok;
    private float ModeChangeTimer;
    private void AttackModeShow()
    {
        if (!MovingStop)
        {
        
            ModeChangeTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Alpha1) && !isDodge && !JumpOn && !isflying)
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
            if (Input.GetKeyDown(KeyCode.Alpha2) && !ShieldOn & !isDodge && !JumpOn && !isflying)
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

            //�������
            if (GameManager.Instance.meleeMode)
            {
                rangeitemshowok = false;
                if (meleeitemshowok) { return; }
                //�����ϴ� ����UI�� �ƿ�����üũ Ȱ��ȭ
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

            // ���Ÿ����
            else if (GameManager.Instance.rangeMode)
            {
                meleeitemshowok = false;
            
                    // Ȱ ���콺 ��Ʈ��
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
              
                

                // �ѹ� ��庯���޴ٸ� ����ó��
                if (rangeitemshowok) { return; }
                //�����ϴ� ����UI�� �ƿ�����üũ Ȱ��ȭ
                btnBoxOutLine1.gameObject.SetActive(false);
                btnBoxOutLine2.gameObject.SetActive(true);
                if (DJumpOn || JumpOn || isDodge || Iswall)
                {
                    return;
                }
                //�������� ��Ȱ��ȭ
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
    
    //ĳ���� ���� ����
    //�ִϸ��̼ǿ��� SetActive falseó����
    public void F_CharText(string _value)
    {
       
        switch (_value)
        {
            case "SP":
                text.gameObject.SetActive(true);
                text.color = Color.blue;
                text.text = "SP�� �����մϴ�...";
                textani.SetTrigger("Ok");
                break;

            case "Melee":
                text.gameObject.SetActive(true);
                text.color = Color.white;
                text.text = "�������";
                textani.SetTrigger("Ok");
               
           
                break;

            case "Range":
                
                text.gameObject.SetActive(true);
                text.color = Color.white;
                text.text = "���Ÿ����";
                textani.SetTrigger("Ok");
               
                break;

            case "Arrow":
                text.gameObject.SetActive(true);
                text.color = Color.red;
                text.text = "ȭ���� �����մϴ�..";
                textani.SetTrigger("Ok");
                break;

            //case "WallJumpFail":
            //    text.gameObject.SetActive(true);
            //    text.color = Color.red;
            //    text.text = "'SpaceBarŰ�� ����ϼ���!";
            //    textani.SetTrigger("Ok");
            //    break;


        }
    }
    public void TextOff()
    {
        text.gameObject.SetActive(false);
    }
    //��������
    private void MeleeAttack()
    {
        if (!MovingStop)
        {
            if (GameManager.Instance.meleeMode)
            {
                Timer += Time.deltaTime;
                if (Input.GetMouseButton(0) && Timer > MeleeSpeed && !Iswall && !isDodge && !DJumpOn & !JumpOn)
                {
                    isAttacking = true;
                    SwordAni.SetTrigger("R");
                    Timer = 0;
                }
            }
        }
    }
      

    //���и���
    private void SheldOn()
    {
        if (!MovingStop)
        {
            if (GameManager.Instance.meleeMode)
            {
               
                if (Input.GetMouseButton(1) && !isAttacking)
                {
                    ShieldOn = true;
                    weapon1.gameObject.SetActive(false);
                    sheld.gameObject.SetActive(false);
                    Defence.gameObject.SetActive(true);
                }
                if (Input.GetMouseButtonUp(1))
                {
                    weaponTrail.Clear();
                    ShieldOn = false;
                    weapon1.gameObject.SetActive(true);
                    sheld.gameObject.SetActive(true);
                    Defence.gameObject.SetActive(false);
                    
                }
            }
        }
       
    }
    //ĳ���͹��� �Ұ� ����
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
   

    // Scan NPC && ������Ʈ
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
             //��ȣ�ۿ� �� �� �������� ���ѱ���� (�Խ����̳� npc)
            if(GameManager.Instance.text.NextTextOk)
            { return; }

             Scanobj = Physics2D.Raycast(transform.position, ScanDir, 1.5f, LayerMask.GetMask("NPC"));
            //hitPoint = Physics2D.Raycast(transform.position, ScanDir, 1.5f, LayerMask.GetMask("Point"));
            if (Scanobj.collider != null)
            {
                Rb.velocity = Vector2.zero;
                ScanObject = Scanobj.collider.gameObject;
                GameManager.Instance.F_TalkSurch(ScanObject);
            }
            //if (hitPoint.collider != null)
            //{
            //    Rb.velocity = Vector2.zero;
            //    ScanObject = Scanobj.collider.gameObject;
            //    GameManager.Instance.F_TalkSurch(ScanObject);
            //}
        }
    }
  

    //ĳ���� �̵� �� ������
    private void MovdChar()

    {
        if (!MovingStop)
        {
            //�̵�
            if (!OnDMG && !GameManager.Instance.isPlayerDead && !wallJumpon && !isDodge && !GameManager.Instance.isTalking)
            {
                Char_Vec.x = Input.GetAxisRaw("Horizontal");
                Rb.velocity = new Vector2(Char_Vec.x * Char_Speed, Rb.velocity.y);
            }
            //ĳ���� ���� ������
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
           
            //������
            if (Input.GetKey(KeyCode.LeftControl) && !isDodge && !JumpOn && !Iswall && !DJumpOn && !isAttacking)
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


    //������
    //bool ����������
    float JumpTime;
    public bool wallJumpon;
    float dusttimer;
    private void WallJump()
    {
        if (Iswall && !isGround)
        {
            dusttimer += Time.deltaTime;
            if (dusttimer > 0.08f)
            {
                paticle.iswallPaticle.Play();
                dusttimer = 0;
            }
            
            JumpTime += Time.deltaTime;
            wallJumpon = false;
            RealBow.gameObject.SetActive(false);
            sheld.gameObject.SetActive(false);
            weapon1.gameObject.SetActive(false);

            Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * SliedSpeed);

            if (Input.GetButtonDown("Jump") && Iswall && JumpTime >= 0.1f)
            {
                                
                // �� ���� 
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
    
    //�� & �ٴ� üũ RayCast
    private void WallCheaking()
    {
        //��üũ ����ĳ��Ʈ ������ȯ
        if(!isLeft/*Rb.velocity.x > 0*/)
        {
            CastDir = Vector2.right;
        }
        else if(isLeft/*Rb.velocity.x < 0*/)
        {
            CastDir = Vector2.left;
        }
        
        //�� Wall üũ
        Iswall = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, Wall_Layer);
        isFrontGround = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, LayerMask.GetMask("Ground"));

        //�ٴ�üũ
        //isGround = Physics2D.OverlapCapsule(groundCheker.position, new Vector2(0.2f, 0.1f), CapsuleDirection2D.Horizontal, 0, LayerMask.GetMask("Ground"));
        isGround = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground"));
    }

    [SerializeField] float jumpStayTime; // ���� �����ð�
    [SerializeField] float jumpTime;
    [SerializeField] float stayPower;
    [SerializeField] bool isOneJump;
    private void CharJump()
    {
        if (!MovingStop)
        {
            //// ���� ��� ������������ ��¦ �� 
            //if (Rb.velocity.y > 0 && isOneJump)
            //{
            //    jumpStayTime += Time.deltaTime;
            //    if (jumpStayTime > jumpTime)
            //    {
            //        isOneJump = false;
            //    }

            //    // ���� ��ü�ð��� ������ �Ѿ�� ��¼ӵ� �������� ..
            //    float t = jumpStayTime / jumpTime;
            //    float currentJumpM = stayPower;

            //    if( t > 0.5f)
            //    {
            //        currentJumpM = stayPower * (1 * t);
            //    }

            //    Rb.velocity += verGravity * currentJumpM * Time.deltaTime;
            //}
            //if (Input.GetButtonUp("Jump") && isOneJump)
            //{
            //    isOneJump = false;
            //    jumpStayTime = 0;
            //    if(Rb.velocity.y > 0)
            //    {
            //        Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * 0.6f);
            //    }
            //
            if (Input.GetButtonDown("Jump") && JumpCount < 2 && !OnDMG & !Iswall && !isflying && !GameManager.Instance.isTalking && !wallJumpon)
            {
                isOneJump = true;
                JumpOn = true;
                Rb.velocity = new Vector2(Rb.velocity.x, JumpPower);
                weaponTrail.Clear();
                JumpCount++;
                Ani.SetBool("Jump", true);

                jumpStayTime = 0;

                //2������ ����
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
            //�ϰ� �� �ӵ� ����
            if (Rb.velocity.y < 0)
            {
                Rb.velocity -= verGravity * dropSpeed * Time.deltaTime;
            }


            // ������ bool�� ����
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
                // ��ù���
                
                gameObject.layer = 10;
                Sr.color = new Color(1, 1, 1, 0.3f);

                //�˹�
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

    bool isWllAcion;
    void Wallok()
    {

        if(Iswall && !isGround)
        {
            isWllAcion = true;
        }
        else
        {
            isWllAcion = false;
        }

    }

    // ĳ���� �ִϸ��̼�
    private void CharAniParameter()
    {
        isCharMove = Mathf.Abs(Char_Vec.x) > 0;
      
        Ani.SetBool("Run", isCharMove);
        Ani.SetBool("DJump", DJumpOn);
        Ani.SetBool("Wall", isWllAcion);
        Ani.SetBool("RangeMode", GameManager.Instance.rangeMode);
        Ani.SetBool("MeleeMode", GameManager.Instance.meleeMode);

    }

    private void F_JumpReset()
    {
        MovingStop = false;
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
    
    //���ȸ��
    private void SpRecovery()
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

    //ü���ڿ�ȸ��
    private void HpRecovery()
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
            paticle.wall.Play();
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
            MaxWinY = new Vector2(Rb.velocity.x, 30); // �ִ� �ö󰡴� �ӵ� ����
            
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(WallCheck.position, CastDir * WallCheakDis);
    }

}


