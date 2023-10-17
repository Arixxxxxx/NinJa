using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class Player : MonoBehaviour
{
    public static Player instance;

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
    [SerializeField] LayerMask OnDMGLayer;

    public bool isDodge;
    public bool KB;
    RaycastHit2D hitPoint;
    RaycastHit2D GetItem;
    RaycastHit2D GetItemRange;




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
    public Transform weapon1;
    public bool noWallCheak;

    //�ٶ�Ÿ��
    private bool isflying;

    //NPC �˻�
    [Header("# NPC Ž��")]
    public GameObject ScanObject;
    public GameObject ScanGuideBox;
    RaycastHit2D Scanobj;

    // ������� ��ġ�̵�
    private Transform weapon;

    Vector3 weaponOriginPos;
    public Transform sheld;
    Vector3 sheldOriginPos;
    public SpriteRenderer sheldSR;
    public SpriteRenderer sheldOnSr;
    Transform Bow;
    [HideInInspector] public Transform RealBow;
    private bool ShieldOn;
    [HideInInspector] public bool isAttacking;

    //���Ÿ���� ��ġ����
    private bool isAiming;

    //��ƼŬ ����
    PaticleManager paticle;
    public ParticleSystem RangeBuff;

    // ���� �����ӵ�
    [Header("# ��������")]
    public float MeleeSpeed;
    public float Timer;
    public Animator SwordAni;
    private Transform Sword;
    public SpriteRenderer SwordSr;
    private Transform Defence;
    AudioSource meleeAtkAudio;

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

    Animator btn2;
    /*[HideInInspector] */
    public bool MovingStop;

    // ����ȹ�� ������
    public Animator ora;
    public ParticleSystem Ps;
    public ParticleSystem Ps2;
    public ParticleSystem powerShotPs;

    // ��������
    public bool isWhilWind;

    // �����
    private AudioSource Audio; // �߼Ҹ�
    //������� R��ų Ȯ��
    [SerializeField] float buffOnActivePercent;
    float buffDice;
    public bool meleeBuffOn;

    private bool isNoDodge;

    private void Awake()
    {



        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        Rb = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
        weapon1 = transform.GetChild(0).GetComponent<Transform>();
        Sword = transform.GetChild(0).GetChild(0).GetComponent<Transform>();
        SwordSr = Sword.GetComponent<SpriteRenderer>();
        meleeAtkAudio = transform.Find("Weapon").GetComponent<AudioSource>();
        sheld = transform.Find("Sheld").GetComponent<Transform>();
        sheldSR = sheld.GetComponent<SpriteRenderer>();
        sheldOnSr = transform.Find("SheldOn").GetComponent<SpriteRenderer>();
        SwordAni = weapon1.GetComponent<Animator>();
        Defence = transform.GetChild(2).GetComponent<Transform>();
        PlayerMSGUI = GameObject.Find("PlayerMSG").GetComponent<Transform>();
        text = PlayerMSGUI.transform.GetChild(0).GetComponent<TMP_Text>();
        weaponOriginPos = weapon1.transform.position;
        sheldOriginPos = sheld.transform.position;
        Bow = GameObject.Find("ArrowDir").GetComponent<Transform>();
        RealBow = Bow.transform.GetChild(0).GetComponent<Transform>();
        gameUiMain = GameObject.Find("GameUI").GetComponent<Transform>();
        //weaponBtn1 = gameUiMain.transform.Find("Btn1").GetComponent<Transform>();

        //weaponBtn2 = gameUiMain.transform.Find("Btn2").GetComponent<Transform>();


        textani = text.GetComponent<Animator>();

        paticle = transform.Find("Paticle").GetComponent<PaticleManager>();
        RangeBuff = paticle.transform.Find("RangeBuff").GetComponent<ParticleSystem>();
        ora = transform.Find("Up").GetComponent<Animator>();


        powerShotPs = transform.Find("Paticle/RangeCarge").GetComponent<ParticleSystem>();
        //����
        groundCheker = transform.Find("GroundCheker").GetComponent<Transform>();
        verGravity = new Vector2(0, -Physics2D.gravity.y);


    }

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
        Audio.clip = SoundManager.instance.playerStep;

        if (GameManager.Instance.SceneName == "Chapter1")
        {
            Ps = transform.Find("Paticle/GetMelee").GetComponent<ParticleSystem>();
            Ps2 = transform.Find("Paticle/GetRange").GetComponent<ParticleSystem>();
        }

        if (GameManager.Instance.SceneName == "Chapter2")
        {
            Ani.SetBool("GetMelee", true);
        }

    }

    void Update()
    {
        if (!GameManager.Instance.GameAllStop)
        {
            Wallok();
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
            SkillOk();

        }

    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.GameAllStop)
        {
            MovdChar();
            CharAniParameter();
            WallCheaking();
        }


    }
    public bool isSkillStartOk;
    private void SkillOk()
    {
        isSkillStartOk = isDodge || JumpOn || DJumpOn || ShieldOn;
    }

    bool once1;



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
            if (ModeChangeTimer < 0.45f)
            {
                ModeChangeTimer += Time.deltaTime;
            }

            if (GameManager.Instance.isGetMeleeItem)
            {
                if ((Input.GetAxis("Mouse ScrollWheel") > 0f) && !isDodge && !JumpOn && !isflying)
                {

                    if (!GameManager.Instance.meleeMode && GameManager.Instance.rangeMode)
                    {
                        if (ModeChangeTimer > 0.4f)
                        {

                            Ani.SetTrigger("ModeChange");
                            F_CharText("Melee");
                            GameManager.Instance.meleeMode = true;

                            ModeChangeTimer = 0;
                            if (GameManager.Instance.gameUI.transform.Find("ActionBar/ModeUpDown").gameObject.activeSelf)
                            {
                                GameManager.Instance.gameUI.transform.Find("ActionBar/ModeUpDown/Up").GetComponent<Animator>().SetTrigger("Up");
                            }

                        }
                    }
                }
                if ((Input.GetAxis("Mouse ScrollWheel") < 0f) && !isDodge && !JumpOn && !isflying)
                {
                    if (GameManager.Instance.meleeMode && !GameManager.Instance.rangeMode)
                    {
                        if (GameManager.Instance.isGetRangeItem)
                        {
                            F_RangeMode();
                            ModeChangeTimer = 0;
                            if (GameManager.Instance.gameUI.transform.Find("ActionBar/ModeUpDown").gameObject.activeSelf)
                            {
                                GameManager.Instance.gameUI.transform.Find("ActionBar/ModeUpDown/Down").GetComponent<Animator>().SetTrigger("Down");
                            }
                        }

                    }
                }


            }
            //if (GameManager.Instance.isGetRangeItem)
            //{
            //    if (Input.GetKeyDown(KeyCode.BackQuote) && !ShieldOn & !isDodge && !JumpOn && !isflying)
            //    {
            //        if (!GameManager.Instance.meleeMode)
            //        {
            //            return;
            //        }
            //        if (ModeChangeTimer > 0.4f)
            //        {
            //            F_RangeMode();

            //            //Ani.SetTrigger("ModeChange");
            //            //btn2.SetTrigger("Ok");
            //            //F_CharText("Range");
            //            //GameManager.Instance.meleeMode = false;
            //            ModeChangeTimer = 0;
            //        }
            //    }


            //}

            //
            if (GameManager.Instance.isGetMeleeItem)
            {
                if (GameManager.Instance.meleeMode)
                {
                    rangeitemshowok = false;
                    if (meleeitemshowok) { return; }


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
            }

            if (GameManager.Instance.isGetRangeItem)
            {

                // ���Ÿ����
                if (GameManager.Instance.rangeMode)
                {
                    meleeitemshowok = false;

                    // Ȱ ���콺 ��Ʈ��
                    if (Input.GetMouseButton(1))
                    {
                        isAiming = true;
                        RealBow.gameObject.SetActive(true);
                    }
                    if (Input.GetMouseButtonUp(1))
                    {
                        isAiming = false;
                        RealBow.gameObject.SetActive(false);
                    }

                    // �ѹ� ��庯���޴ٸ� ����ó��
                    if (rangeitemshowok) { return; }
                    //�����ϴ� ����UI�� �ƿ�����üũ Ȱ��ȭ

                    if (DJumpOn || JumpOn || isDodge || Iswall)
                    {
                        return;
                    }
                    //�������� ��Ȱ��ȭ
                    sheldSR.enabled = false;
                    SwordSr.enabled = false;
                    //weapon1.gameObject.SetActive(false);
                    //sheld.gameObject.SetActive(false);
                    rangeitemshowok = true;

                }

            }
        }

    }
    public void F_RangeMode()
    {

        Ani.SetTrigger("ModeChange");
        F_CharText("Range");
        GameManager.Instance.meleeMode = false;
    }
    IEnumerator mellemodeitemshow()
    {
        meleeitemshowok = true;

        yield return new WaitForSecondsRealtime(0.2f);
        sheldSR.enabled = true;
        SwordSr.enabled = true;
        //weapon1.gameObject.SetActive(true);
        //sheld.gameObject.SetActive(true);
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

            case "MP":
                text.gameObject.SetActive(true);
                text.color = Color.blue;
                text.text = "MP�� �����մϴ�...";
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
                text.text = "���������";
                textani.SetTrigger("Ok");

                break;

            case "Arrow":
                text.gameObject.SetActive(true);
                text.color = Color.red;
                text.text = "ȭ���� �����մϴ�..";
                textani.SetTrigger("Ok");
                break;

            case "CoolTime":
                text.gameObject.SetActive(true);
                text.color = Color.red;
                text.text = "���� �� ��ٷ����մϴ�";
                textani.SetTrigger("Ok");
                break;

            case "ActiveBow":
                text.gameObject.SetActive(true);
                text.color = Color.red;
                text.text = "Ȱ�� ��� �����ؾ� �մϴ�";
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
            if (GameManager.Instance.isGetMeleeItem)
            {
                if (GameManager.Instance.meleeMode)
                {
                    if (Timer < MeleeSpeed + 0.1f)
                    {
                        Timer += Time.deltaTime;
                    }
                    if (Input.GetMouseButton(0) && Timer > MeleeSpeed && !Iswall && !isDodge && !DJumpOn && !ShieldOn && !isWhilWind)
                    {
                        if (GameManager.Instance.SkillWindowPopup || GameManager.Instance.cursorOnUi)
                        {
                            return;
                        }

                        GameManager.Instance.Player_CurMP += SkillManager.instance.MeleeMpUp;
                        isAttacking = true;
                        SwordAni.SetTrigger("R");
                        Timer = 0;
                    }
                }
            }

        }
    }


    bool isSoundPlay;
    //���и���
    private void SheldOn()
    {
        if (!MovingStop)
        {
            if (GameManager.Instance.rangeMode)
            {
                ShieldOn = false;
                Defence.gameObject.SetActive(false);

            }
            else
            {
                RealBow.gameObject.SetActive(false);
            }

            if (GameManager.Instance.isGetMeleeItem)
            {
                if (GameManager.Instance.meleeMode)
                {

                    if (Input.GetMouseButtonDown(1) && !isAttacking && !isWhilWind)
                    {
                        if (GameManager.Instance.SkillWindowPopup) { return; }

                        ShieldOn = true;
                        if (!isSoundPlay)
                        {
                            isSoundPlay = true;
                            SoundManager.instance.F_SoundPlay(SoundManager.instance.sheildOn, 0.8f);
                        }

                        sheldSR.enabled = false;
                        SwordSr.enabled = false;
                        //weapon1.gameObject.SetActive(false);
                        //sheld.gameObject.SetActive(false);
                        Defence.gameObject.SetActive(true);
                    }
                    if (Input.GetMouseButtonUp(1) && !isWhilWind)
                    {
                        isSoundPlay = false;
                        ShieldOn = false;
                        sheldSR.enabled = true;
                        SwordSr.enabled = true;
                        Defence.gameObject.SetActive(false);

                    }
                }
            }

        }

    }
    //ĳ���͹��� �Ұ� ����
    private void SetCharDir()
    {
        if (transform.localScale.x == -3)
        {
            isLeft = true;
            SkillManager.instance.buffPs.transform.localScale = new Vector3(-0.25f, 0.3f);
        }
        else if (transform.localScale.x == 3)
        {
            isLeft = false;
            SkillManager.instance.buffPs.transform.localScale = new Vector3(0.25f, 0.3f);
        }
    }


    // Scan NPC && ������Ʈ
    Vector3 ScanDir;
    public bool Itemget0;
    public bool Itemget1;

    private void SuchTalk()
    {
        if (Rb.velocity.x < 0 && Char_Vec.x < 0)
        {
            ScanDir = Vector3.left;
        }
        else if (Rb.velocity.x > 0 && Char_Vec.x > 0)
        {
            ScanDir = Vector3.right;
        }
        Debug.DrawRay(transform.position, CastDir * 1.5f, Color.red);



        if (Input.GetKeyDown(KeyCode.F) && !GameManager.Instance.isWaitTalking)
        {

            //��ȣ�ۿ� �� �� �������� ���ѱ���� (�Խ����̳� npc)
            if (GameManager.Instance.text.NextTextOk)
            { return; }

            //NPCüũ
            Scanobj = Physics2D.Raycast(transform.position, ScanDir, 1.5f, LayerMask.GetMask("NPC"));
            if (Scanobj.collider != null)
            {
                GameManager.Instance.curPlayerTalkingYouStop = true;
                Rb.velocity = Vector2.zero;
                ScanObject = Scanobj.collider.gameObject;
                GameManager.Instance.F_TalkSurch(ScanObject);
            }


            GetItem = Physics2D.Raycast(transform.position, ScanDir, 1.5f, LayerMask.GetMask("GetItem"));
            if (GetItem.collider != null && !Itemget0)
            {
                GameManager.Instance.isGetMeleeItem = true;
                SoundManager.instance.F_SoundPlay(SoundManager.instance.ItemGet, 0.5f);
            }

            GetItemRange = Physics2D.Raycast(transform.position, ScanDir, 1.5f, LayerMask.GetMask("GetItem2"));
            if (GetItemRange.collider != null && !Itemget1)
            {
                GameManager.Instance.isGetRangeItem = true;
                SoundManager.instance.F_SoundPlay(SoundManager.instance.ItemGet, 0.5f);
            }

            //ã�Ƽ� �����
            hitPoint = Physics2D.Raycast(transform.position, ScanDir, 1.5f, LayerMask.GetMask("Point"));
            if (hitPoint.collider != null && !GameManager.Instance.once && !GameManager.Instance.GuideWindow.gameObject.activeSelf)
            {
                GameManager.Instance.MovingStop = true;
                Rb.velocity = Vector2.zero;
                GameManager.Instance.once = true;
                PointCheker sc = hitPoint.collider.GetComponent<PointCheker>();
                GameManager.Instance.GuideWindow.gameObject.SetActive(true);
                TutorialGuide.instance.F_SetTutorialWindow((int)sc.type);


                //���Ž� ���̵� ����
                //ScanGuideBox = hitPoint.collider.gameObject;
                //GameManager.Instance.guideM.F_GetColl(hitPoint.collider.gameObject);
            }

        }
    }

    float runConter;
    [SerializeField] float runTime = 0.4f;
    //ĳ���� �̵� �� ������
    private void MovdChar()

    {
        if (!GameManager.Instance.legStop)
        {
            if (!MovingStop)
            {
                //�̵�
                if (!OnDMG && !GameManager.Instance.isPlayerDead && !wallJumpon && !isDodge && !GameManager.Instance.isTalking)
                {
                    Char_Vec.x = Input.GetAxisRaw("Horizontal");
                    Rb.velocity = new Vector2(Char_Vec.x * Char_Speed, Rb.velocity.y);

                    //�߼Ҹ�
                    if (isCharMove)
                    {
                        runConter += Time.deltaTime;
                        if (runConter > runTime && isGround && !isWhilWind)
                        {
                            if (Audio.clip != SoundManager.instance.playerStep)
                            {
                                Audio.clip = SoundManager.instance.playerStep;
                            }

                            Audio.Play();
                            runConter = 0;

                        }

                    }
                    else
                    {

                        Audio.Stop();

                    }

                }
                //ĳ���� ���� ������
                if (!GameManager.Instance.rangeMode)
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

                if (GameManager.Instance.rangeMode)
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
                if (Input.GetKey(KeyCode.LeftControl) && !JumpOn && !Iswall && !DJumpOn && !isDodge && !isWhilWind)
                {
                    if (isNoDodge) { return; }
                    if (GameManager.Instance.Player_CurSP < 15)
                    {
                        F_CharText("SP");

                        return;
                    }
                    else if (GameManager.Instance.Player_CurSP > 15)
                    {

                        isDodge = true;
                        Rb.velocity = Vector2.zero;
                        sheldSR.enabled = false;
                        SwordSr.enabled = false;
                        //sheld.gameObject.SetActive(false);
                        //weapon1.gameObject.SetActive(false);
                        GameManager.Instance.Player_CurSP -= 15;


                        if (GameManager.Instance.rangeMode)
                        {
                            SoundManager.instance.F_SoundPlay(SoundManager.instance.dodge, 0.5f);
                            Rb.velocity = new Vector3(transform.localScale.x >= 0 ? -1 : 1, 0) * DodgeSpeed / 1.5f;
                            gameObject.layer = 29;
                            Invoke("F_ReturnLayer", 0.5f);
                            Ani.SetTrigger("Dodge");
                        }

                        else
                        {
                            if (!isLeft)
                            {
                                SoundManager.instance.F_SoundPlay(SoundManager.instance.dodge, 0.5f);
                                Rb.velocity = new Vector3(1, 0) * DodgeSpeed;
                                gameObject.layer = 29;
                                Invoke("F_ReturnLayer", 0.5f);
                                Ani.SetTrigger("Dodge");
                            }
                            else if (isLeft)
                            {
                                SoundManager.instance.F_SoundPlay(SoundManager.instance.dodge, 0.5f);
                                Rb.velocity = new Vector3(-1, 0) * DodgeSpeed;
                                gameObject.layer = 29;
                                Invoke("F_ReturnLayer", 0.5f);
                                Ani.SetTrigger("Dodge");

                            }

                        }
                    }
                }
            }

        }

    }
    private void F_ReturnLayer()
    {

        gameObject.layer = 6;
        isDodge = false;
        if (GameManager.Instance.meleeMode)
        {
            sheldSR.enabled = true;
            SwordSr.enabled = true;
        }
        else if (GameManager.Instance.rangeMode)
        {
            if (sheldSR.enabled == true)
            {
                sheldSR.enabled = false;
                SwordSr.enabled = false;
            }

        }
        //sheld.gameObject.SetActive(true);
        //weapon1.gameObject.SetActive(true);
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
            isNoDodge = true;
            JumpTime += Time.deltaTime;
            wallJumpon = false;
            RealBow.gameObject.SetActive(false);
            sheldSR.enabled = false;
            SwordSr.enabled = false;
            //sheld.gameObject.SetActive(false);
            //weapon1.gameObject.SetActive(false);

            Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * SliedSpeed);

            if (Input.GetButtonDown("Jump") && Iswall && JumpTime >= 0.1f)
            {

                // �� ���� 
                if (!isLeft)
                {
                    Audio.clip = SoundManager.instance.jump;
                    Audio.Play();
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
                    Audio.clip = SoundManager.instance.jump;
                    Audio.Play();
                    isLeft = false;
                    wallJumpon = true;
                    CastDir = Vector2.right;
                    Iswall = false;
                    Rb.velocity = new Vector2(1 * WalljumpPower, 1.5f * WalljumpPower);

                    if (Rb.velocity.x > 0)
                    {
                        transform.localScale = new Vector3(3, 3, 3);
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
        if (!isLeft/*Rb.velocity.x > 0*/)
        {
            CastDir = Vector2.right;
        }
        else if (isLeft/*Rb.velocity.x < 0*/)
        {
            CastDir = Vector2.left;
        }




        //�� Wall üũ
        Iswall = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, Wall_Layer);
        isFrontGround = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, LayerMask.GetMask("Ground"));
        noWallCheak = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, LayerMask.GetMask("NoWall"));

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
            if (Input.GetButtonDown("Jump") && JumpCount < 2 && !OnDMG & !Iswall && !isflying && !GameManager.Instance.isTalking && !wallJumpon && !noWallCheak && !isWhilWind)
            {

                SoundManager.instance.F_SoundPlay(SoundManager.instance.normalJump, 0.5f);

                isOneJump = true;
                JumpOn = true;
                Rb.velocity = new Vector2(Rb.velocity.x, JumpPower);

                JumpCount++;
                Ani.SetBool("Jump", true);

                jumpStayTime = 0;

                //2������ ����
                if (JumpCount == 2)
                {
                    if (GameManager.Instance.meleeMode)
                    {
                        //sheld.gameObject.SetActive(false);
                        //weapon1.gameObject.SetActive(false);
                        sheldSR.enabled = false;
                        SwordSr.enabled = false;

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

    /// <summary>
    /// �ڷ�ƾ �Դϴ�
    /// </summary>
    /// <returns>�ڷ�ƾ�̾� �ڷ�ƾ���� ����</returns>
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
                SoundManager.instance.F_SoundPlay(SoundManager.instance.onHIt, 0.8f);
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

                yield return new WaitForSecondsRealtime(1.5f);

                gameObject.layer = LayerMask.NameToLayer("Player");
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

        if (Iswall && !isGround)
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

      
        JumpOn = false;
        DJumpOn = false;
        Ani.SetBool("Jump", false);
        JumpCount = 0;
        wallJumpon = false;
        if (GameManager.Instance.meleeMode)
        {
            //sheldSR.enabled = true;
            //SwordSr.enabled = true;
        
            if (sheldOnSr.gameObject.activeSelf)
            {
                return;
            }
            MeleeItemShow(0);
        }
        else
        {

        }

        KB = false;
    }

    //���ȸ��
    public float secRecerSP = 4;
    private void SpRecovery()
    {
        if (GameManager.Instance.Player_CurSP > GameManager.Instance.Player_MaxSP)
        {
            GameManager.Instance.Player_CurSP = GameManager.Instance.Player_MaxSP;
        }

        else if (GameManager.Instance.Player_CurSP < GameManager.Instance.Player_MaxSP)
        {
            GameManager.Instance.Player_CurSP += 1 * Time.deltaTime * secRecerSP;
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

    public void F_LegGroundCheaker(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            F_JumpReset();
            isNoDodge = false;
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            F_JumpReset();
            isNoDodge = false;
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (GameManager.Instance.SceneName == "Chapter2")
            {
                F_JumpReset();
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("FlatForm"))
        {
            if (GameManager.Instance.SceneName == "Chapter2")
            {
                F_JumpReset();
                Debug.Log("aa");
            }
            if (collision.transform.GetComponent<BoxCollider2D>().gameObject.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {

                    StartCoroutine(ExitBrege(collision));
                }
            }

        }
    }

    public void LegStayCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("FlatForm"))
        {
           
            if (GameManager.Instance.SceneName == "Chapter2")
            {

                if (collision.transform.GetComponent<BoxCollider2D>().gameObject.activeSelf)
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {

                        StartCoroutine(ExitBrege(collision));
                    }
                }
              
            }
        }
    }

    float BregeInterval = 0.6f;
    IEnumerator ExitBrege(Collider2D collision)
    {
        collision.transform.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(BregeInterval);
        collision.transform.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {

                if (contact.normal == Vector2.up)
                {
                    F_JumpReset();
                }
            }
            //SoundManager.instance.F_SoundPlay(SoundManager.instance.ground, 0.5f);


        }


        if (collision.gameObject.CompareTag("Trap") && Rb.velocity.y < 0.2f)
        {
            if(gameObject.layer == OnDMGLayer) { return; }
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
            if (gameObject.layer == OnDMGLayer) { return; }
            F_JumpReset();
            StartCoroutine(F_OnHit());
        }
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    Rb.velocity = Vector3.zero;
        //}

        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (gameObject.layer == OnDMGLayer) { return; }
                StartCoroutine(F_OnHit());
                JumpOn = false;
                F_JumpReset();
            }
           
        }
        if (collision.gameObject.CompareTag("Ghost"))
        {
            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (gameObject.layer == OnDMGLayer) { return; }
                StartCoroutine(F_OnHit());
                JumpOn = false;
                F_JumpReset();
            }
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
            if (isflying)
            {
                isflying = false;
            }

        }
    }

    /// <summary>
    /// �и������� ��������Ʈ on/off
    /// </summary>
    /// <param name="_Value">0 = true, 1 = false</param>
    public void MeleeItemShow(int _Value)
    {
        switch (_Value)
        {
            case 0:
                {
                    sheldSR.enabled = true;
                    SwordSr.enabled = true;
                    sheldOnSr.enabled = true;
                }
                break;
            case 1:
                {
                    sheldSR.enabled = false;
                    SwordSr.enabled = false;

                    if (sheldOnSr.gameObject.activeSelf)
                    {
                        sheldOnSr.gameObject.SetActive(false);
                        isSoundPlay = false;
                        ShieldOn = false;
                        Defence.gameObject.SetActive(false);
                    }
                }
                break;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(WallCheck.position, CastDir * WallCheakDis);
    }

}


