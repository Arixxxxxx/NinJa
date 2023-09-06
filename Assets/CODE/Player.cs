using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//23³â 8¿ù 28ÀÏ ½ÃÀÛ
//23³â 8¿ù 30ÀÏ ÇÃ¶óÀ×ÆÒ, ½ºÆÄÀÌÅ©Æ®·¦,±¸¸£±â(È¸ÇÇ),±ÙÁ¢¹«±â ¾Ö´Ï¸ÞÀÌ¼Ç, ¿¡³Ê¹ÌBÁ¦ÀÛ
public class Player : MonoBehaviour
{

    public Rigidbody2D Rb;
    Animator Ani;
    public SpriteRenderer Sr;
   
    //Ä³¸¯ÅÍÀÌµ¿
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
    //ÇöÀç º¸´Â¹æÇâ
    public bool isLeft;

    //º®Ã¼Å©
    [Header("º®Ã¼Å©")]
    public Transform WallCheck;
    public float WallCheakDis;
    public LayerMask Wall_Layer;
    public bool Iswall;
    public float SliedSpeed;
    public float WalljumpPower;
    Vector2 CastDir;
    Transform weapon1;

    //¹Ù¶÷Å¸±â
    private bool isflying;

    //NPC °Ë»ö
    [Header("# NPC Å½»ö")]
    public GameObject ScanObject;
    RaycastHit2D Scanobj;

    // ¹«±â¹æÆÐ À§Ä¡ÀÌµ¿
    private Transform weapon;
    Vector3 weaponOriginPos;
    private Transform sheld;
    Vector3 sheldOriginPos;
    private SpriteRenderer sheldSR;
    Transform Bow;
    public Transform RealBow;
    Transform BowHat;
    

    // ¹«±â ±ÙÁ¢¼Óµµ
    [Header("# ±ÙÁ¢°ø°Ý")]
    [SerializeField] private float MeleeSpeed;
    [SerializeField] private float Timer;
    Animator SwordAni;
    Transform Sword;
    Transform Defence;

    //Ä³¸¯ÅÍ ¸»Ç³¼±
    Transform PlayerMSGUI;
   
    TMP_Text text;

    //°ÔÀÓUI
    Transform gameUiMain;
    Transform weaponBtn1;
    Transform btnBoxOutLine1;
    Animator btn1;
    Transform weaponBtn2;
    Transform btnBoxOutLine2;
    Animator btn2;
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
        BowHat = transform.Find("BowHat").GetComponent<Transform>();
        RealBow = Bow.transform.GetChild (0).GetComponent<Transform>();
        gameUiMain = GameObject.Find("GameUI").GetComponent<Transform>();
        weaponBtn1 = gameUiMain.transform.Find("Btn1").GetComponent<Transform>();
        btnBoxOutLine1 = weaponBtn1.transform.Find("BoxOutLine").GetComponent< Transform > ();
        weaponBtn2 = gameUiMain.transform.Find("Btn2").GetComponent<Transform>();
        btnBoxOutLine2 = weaponBtn2.transform.Find("BoxOutLine").GetComponent<Transform>();
        btn1 = weaponBtn1.GetComponent<Animator>();
        btn2 = weaponBtn2.GetComponent<Animator>();
    }
    
   

    void Update()
    {
        SetCharDir();
        F_CharJump();
        //F_CharMoveStop(); // ¸Ø­ŸÀ»‹š ÀÌ¼Ó°¨¼Ò (ÁÖ¼®Ã³¸®)
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
    private void AttackModeShow()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            F_CharText("Melee");
            GameManager.Instance.meleeMode = true;
            btn1.SetTrigger("Ok");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            btn2.SetTrigger("Ok");
            F_CharText("Range");
            GameManager.Instance.meleeMode = false;
        }
        if (GameManager.Instance.meleeMode)
        {
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
            else if(!Defence.gameObject.activeSelf) 
            {
                BowHat.gameObject.SetActive(false);
                weapon1.gameObject.SetActive(true);
                sheld.gameObject.SetActive(true);
                RealBow.gameObject.SetActive(false);
            }
           
        }
        else if (!GameManager.Instance.meleeMode)
        {
            btnBoxOutLine1.gameObject.SetActive(false);
            btnBoxOutLine2.gameObject.SetActive(true);
            if (DJumpOn || JumpOn || isDodge || Iswall)
            {
                return;
            }

            BowHat.gameObject.SetActive(true);
            weapon1.gameObject.SetActive(false);
            sheld.gameObject.SetActive(false);

            if (Input.GetMouseButton(1))
            {
                RealBow.gameObject.SetActive(true);
            }
            if (Input.GetMouseButtonUp(1))
            {
                RealBow.gameObject.SetActive(false);
            }
        }
    }
    private void F_TextBoxPos()
    {
        PlayerMSGUI.transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f);
    }
    public void F_CharText(string _value)
    {
       
        switch (_value)
        {
            case "SP":
                if (text.gameObject.activeSelf) { return; }
                text.gameObject.SetActive(true);
                text.color = Color.blue;
                text.text = "SP°¡ ºÎÁ·ÇÕ´Ï´Ù...";
                Invoke("TextOff", 1.5f);
                break;

            case "Melee":
                if (text.gameObject.activeSelf) { return; }
                text.gameObject.SetActive(true);
                text.color = Color.white;
                text.text = "±ÙÁ¢¸ðµå";
                Invoke("TextOff", 1.5f);
                break;

            case "Range":
                if (text.gameObject.activeSelf) { return; }
                text.gameObject.SetActive(true);
                text.color = Color.white;
                text.text = "¿ø°Å¸®¸ðµå";
                Invoke("TextOff", 1.5f);
                break;

            case "Arrow":
                if (text.gameObject.activeSelf) { return; }
                text.gameObject.SetActive(true);
                text.color = Color.red;
                text.text = "È­»ìÀÌ ºÎÁ·ÇÕ´Ï´Ù..";
                Invoke("TextOff", 1.5f);
                break;


        }
    }
    private void TextOff()
    {
        text.gameObject.SetActive(false);
    }
    //±ÙÁ¢°ø°Ý
    private void F_MeleeAttack()
    {
        if (GameManager.Instance.meleeMode)
        {
            Timer += Time.deltaTime;
            if (Input.GetMouseButton(0) && Timer > MeleeSpeed && !Iswall && !isDodge && !DJumpOn)
            {
                StartCoroutine(IE_MeleeAttack());
            }
        }
        
    }

    IEnumerator IE_MeleeAttack()
    {
        Sword.gameObject.layer = 15;
        SwordAni.SetTrigger("R");
        Timer = 0;

        yield return new WaitForSecondsRealtime(0.5f);

        Sword.gameObject.layer = 16;
    }

    //¹æÆÐ¸·±â
    private void SheldOn()
    {
        if (GameManager.Instance.meleeMode)
        {
            if (Input.GetMouseButton(1))
            {
                weapon1.gameObject.SetActive(false);
                sheld.gameObject.SetActive(false);
                Defence.gameObject.SetActive(true);
            }
            if (Input.GetMouseButtonUp(1))
            {
                weapon1.gameObject.SetActive(true);
                sheld.gameObject.SetActive(true);
                Defence.gameObject.SetActive(false);
            }
        }
    }
    //Ä³¸¯ÅÍ¹æÇâ ºÒ°ª ÀúÀå
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
   

    // Scan
    Vector3 ScanDir;
    private void SuchTalk()
    { 
        if (Rb.velocity.x < 0)
        {
            ScanDir = Vector3.left;
        }
        else if(Rb.velocity.x > 0) 
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
  

    //Ä³¸¯ÅÍ ÀÌµ¿
    private void F_MovdChar()

    {  //Ä³¸¯ÅÍ ¹æÇâ ½ºÄÉÀÏ
        if (Rb.velocity.x > 0 && transform.localScale.x != 3 && !KB)
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
        else if (Rb.velocity.x < 0 && transform.localScale.x != -3&& !KB)
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }
        //ÀÌµ¿
        if (!OnDMG && !GameManager.Instance.isPlayerDead && !wallJumpon && !isDodge && !GameManager.Instance.isTalking)
        {
            Char_Vec.x = Input.GetAxisRaw("Horizontal");
            Rb.velocity = new Vector3(Char_Vec.x * Char_Speed,Rb.velocity.y);
        
        }
        //±¸¸£±â
        if (Input.GetKey(KeyCode.LeftControl) && !isDodge && !JumpOn && !Iswall && !DJumpOn)
        {
            if(GameManager.Instance.Player_CurSP < 15)
            {
                F_CharText("SP");
                return;
            }
            else if (GameManager.Instance.Player_CurSP > 15)
            {
               
                BowHat.gameObject.SetActive(false);
                sheld.gameObject.SetActive(false);
                weapon1.gameObject.SetActive(false);
                GameManager.Instance.Player_CurSP -= 15;
                Rb.velocity = Vector2.zero;
                isDodge = true;

                if (!isLeft)
                {
                    Rb.AddForce(new Vector3(1, 0) * DodgeSpeed, ForceMode2D.Impulse);
                    gameObject.layer = 10;
                    Invoke("F_ReturnLayer", 0.5f);
                    Ani.SetTrigger("Dodge");
                }
                else if (isLeft)
                {
                    Rb.AddForce(new Vector3(-1, 0) * DodgeSpeed, ForceMode2D.Impulse);
                    gameObject.layer = 10;
                    Invoke("F_ReturnLayer", 0.5f);
                    Ani.SetTrigger("Dodge");
                 
                }
            }
        }
    }

    private void F_ReturnLayer()
    {
        gameObject.layer = 6;
        isDodge = false;
        sheld.gameObject.SetActive(true);
        weapon1.gameObject.SetActive(true);
    }


    //º®Á¡ÇÁ
    public bool wallJumpon;
    private void F_WallJump()
    {
        if (Iswall)
        {
            RealBow.gameObject.SetActive(false);
            sheld.gameObject.SetActive(false);
            weapon1.gameObject.SetActive(false);
            Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * SliedSpeed);
       
            if (Input.GetButtonDown("Jump") && Iswall)
            {
                   // º® Á¡ÇÁ 
                if (CastDir == Vector2.right)
                {
                    wallJumpon = true;
                    Rb.velocity = Vector3.zero;
                    Rb.AddForce(new Vector2(-1 *  WalljumpPower, 1.6f * WalljumpPower), ForceMode2D.Impulse);
                    
                    sheld.gameObject.SetActive(false);
                    weapon1.gameObject.SetActive(false);
                    if (Rb.velocity.x < 0)
                    {
                        transform.localScale= new Vector3(-3, 3, 3);
                    }
                    Invoke("F_WallJumpOff", 0.4f);
                }
                else if (CastDir == Vector2.left)
                {
                    wallJumpon = true;
                    Rb.velocity = Vector3.zero;
                    Rb.AddForce(new Vector2(1 * WalljumpPower, 1.6f * WalljumpPower), ForceMode2D.Impulse);
                    
                    sheld.gameObject.SetActive(false);
                    weapon1.gameObject.SetActive(false);
                    if (Rb.velocity.x > 0)
                    {
                        transform.localScale = new Vector3(3, 3, 3);
                    }
                    Invoke("F_WallJumpOff", 0.4f);

                }
            }
        }
     }
    private void F_WallJumpOff()
    {
        wallJumpon = false;
    }
    //º® & ¹Ù´Ú Ã¼Å© RayCast

    private void F_WallCheaking()
    {
        //º®Ã¼Å© ·¹ÀÌÄ³½ºÆ® ¹æÇâÀüÈ¯
        if(Rb.velocity.x > 0)
        {
            CastDir = Vector2.right;
        }
        else if(Rb.velocity.x < 0)
        {
            CastDir = Vector2.left;
        }
        //¹Ù´ÚÃ¼Å©
        Iswall = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, Wall_Layer); 
        //¹Ù´ÚÃ¼Å©
        isGround = Physics2D.Raycast(transform.position, Vector2.down, 0.65f, LayerMask.GetMask("Ground"));
    }


    //Ä³¸¯ÅÍ Á¡ÇÁ
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
           

            //2´ÜÁ¡ÇÁ Á¦¾î
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
                    BowHat.gameObject.SetActive(false);
                }
                               
                DJumpOn = true;
             }
        }

        // ÂøÁö½Ã bool°ª Á¦¾î
        if (isGround && Rb.velocity.y < 0.3f)
        {
            JumpOn = false;
            DJumpOn = false;
            Ani.SetBool("Jump", false);
            JumpCount = 0;
            OnDMG = false;
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
                // Àá½Ã¹«Àû
                
                gameObject.layer = 10;
                Sr.color = new Color(1, 1, 1, 0.3f);

                //³Ë¹é
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


    // Ä³¸¯ÅÍ ¾Ö´Ï¸ÞÀÌ¼Ç
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
        if (GameManager.Instance.meleeMode)
        {
            sheld.gameObject.SetActive(true);
            weapon1.gameObject.SetActive(true);
        }
        else
        {
          BowHat.gameObject.SetActive(false);
        }
     
        KB = false;
    }
    
    //±â·ÂÈ¸º¹
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
        //if (collision.gameObject.CompareTag("Wall") && Rb.velocity.y > 0.05f)
        //{
        //    F_JumpReset();
        //}
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


