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

    //캐릭터이동
    [Header("# 캐릭터 이동관련")]
    [HideInInspector] public Vector2 Char_Vec;
    private Vector2 VZ = Vector2.zero;
    [SerializeField] private float Char_Speed;
    [SerializeField] private float Char_MaxSpeed;
    [SerializeField] private float KB_Power;
    [SerializeField] private float DodgeSpeed;
    private bool isCharMove;
    [Header("# 캐릭터 현재상태 관련")]
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




    //캐릭터 점프
    [Header("# Jump")]
    [Space]
    [SerializeField] float JumpPower;
    private int JumpCount;
    [SerializeField] bool JumpOn;
    [SerializeField] public bool DJumpOn;
    [Range(0f, 10f)][SerializeField] float dropSpeed;
    Vector2 verGravity;

    [SerializeField] Transform groundCheker;



    //현재 보는방향
    [HideInInspector] public bool isLeft;

    //벽체크
    [Header("# 벽체크")]
    public Transform WallCheck;
    public float WallCheakDis;
    public LayerMask Wall_Layer;
    public bool Iswall;
    public float SliedSpeed;
    public float WalljumpPower;
    Vector2 CastDir;
    public Transform weapon1;
    public bool noWallCheak;

    //바람타기
    private bool isflying;

    //NPC 검색
    [Header("# NPC 탐색")]
    public GameObject ScanObject;
    public GameObject ScanGuideBox;
    RaycastHit2D Scanobj;

    // 무기방패 위치이동
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

    //원거리모드 위치고정
    private bool isAiming;

    //파티클 참조
    PaticleManager paticle;
    public ParticleSystem RangeBuff;

    // 무기 근접속도
    [Header("# 근접공격")]
    public float MeleeSpeed;
    public float Timer;
    public Animator SwordAni;
    private Transform Sword;
    public SpriteRenderer SwordSr;
    private Transform Defence;
    AudioSource meleeAtkAudio;

    //캐릭터 말풍선
    private Transform PlayerMSGUI;
    private TMP_Text text;
    private Animator textani;

    //게임UI
    Transform gameUiMain;
    Transform weaponBtn1;
    Transform btnBoxOutLine1;
    Animator btn1;
    Transform weaponBtn2;

    Animator btn2;
    /*[HideInInspector] */
    public bool MovingStop;

    // 무기획득 오오라
    public Animator ora;
    public ParticleSystem Ps;
    public ParticleSystem Ps2;
    public ParticleSystem powerShotPs;

    // 휠윈드중
    public bool isWhilWind;

    // 오디오
    private AudioSource Audio; // 발소리
    //근접모드 R스킬 확률
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
        //점프
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



    //캐릭터 일시정지기능
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

                // 원거리모드
                if (GameManager.Instance.rangeMode)
                {
                    meleeitemshowok = false;

                    // 활 마우스 컨트롤
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

                    // 한번 모드변경햇다면 예외처리
                    if (rangeitemshowok) { return; }
                    //좌측하단 무기UI바 아웃라인체크 활성화

                    if (DJumpOn || JumpOn || isDodge || Iswall)
                    {
                        return;
                    }
                    //근접무기 비활성화
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

            case "MP":
                text.gameObject.SetActive(true);
                text.color = Color.blue;
                text.text = "MP가 부족합니다...";
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
                text.text = "레인지모드";
                textani.SetTrigger("Ok");

                break;

            case "Arrow":
                text.gameObject.SetActive(true);
                text.color = Color.red;
                text.text = "화살이 부족합니다..";
                textani.SetTrigger("Ok");
                break;

            case "CoolTime":
                text.gameObject.SetActive(true);
                text.color = Color.red;
                text.text = "아직 더 기다려야합니다";
                textani.SetTrigger("Ok");
                break;

            case "ActiveBow":
                text.gameObject.SetActive(true);
                text.color = Color.red;
                text.text = "활을 들고 조준해야 합니다";
                textani.SetTrigger("Ok");
                break;

                //case "WallJumpFail":
                //    text.gameObject.SetActive(true);
                //    text.color = Color.red;
                //    text.text = "'SpaceBar키만 사용하세요!";
                //    textani.SetTrigger("Ok");
                //    break;


        }
    }
    public void TextOff()
    {
        text.gameObject.SetActive(false);
    }
    //근접공격
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
    //방패막기
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
    //캐릭터방향 불값 저장
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


    // Scan NPC && 오브젝트
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

            //상호작용 글 다 안읽으면 못넘기게함 (게시판이나 npc)
            if (GameManager.Instance.text.NextTextOk)
            { return; }

            //NPC체크
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

            //찾아서 써놓기
            hitPoint = Physics2D.Raycast(transform.position, ScanDir, 1.5f, LayerMask.GetMask("Point"));
            if (hitPoint.collider != null && !GameManager.Instance.once && !GameManager.Instance.GuideWindow.gameObject.activeSelf)
            {
                GameManager.Instance.MovingStop = true;
                Rb.velocity = Vector2.zero;
                GameManager.Instance.once = true;
                PointCheker sc = hitPoint.collider.GetComponent<PointCheker>();
                GameManager.Instance.GuideWindow.gameObject.SetActive(true);
                TutorialGuide.instance.F_SetTutorialWindow((int)sc.type);


                //레거시 가이드 버전
                //ScanGuideBox = hitPoint.collider.gameObject;
                //GameManager.Instance.guideM.F_GetColl(hitPoint.collider.gameObject);
            }

        }
    }

    float runConter;
    [SerializeField] float runTime = 0.4f;
    //캐릭터 이동 및 구르기
    private void MovdChar()

    {
        if (!GameManager.Instance.legStop)
        {
            if (!MovingStop)
            {
                //이동
                if (!OnDMG && !GameManager.Instance.isPlayerDead && !wallJumpon && !isDodge && !GameManager.Instance.isTalking)
                {
                    Char_Vec.x = Input.GetAxisRaw("Horizontal");
                    Rb.velocity = new Vector2(Char_Vec.x * Char_Speed, Rb.velocity.y);

                    //발소리
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
                //캐릭터 방향 스케일
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

                //구르기
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


    //벽점프
    //bool 점프딜레이
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

                // 벽 점프 
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

    //벽 & 바닥 체크 RayCast
    private void WallCheaking()
    {
        //벽체크 레이캐스트 방향전환
        if (!isLeft/*Rb.velocity.x > 0*/)
        {
            CastDir = Vector2.right;
        }
        else if (isLeft/*Rb.velocity.x < 0*/)
        {
            CastDir = Vector2.left;
        }




        //벽 Wall 체크
        Iswall = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, Wall_Layer);
        isFrontGround = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, LayerMask.GetMask("Ground"));
        noWallCheak = Physics2D.Raycast(WallCheck.position, CastDir, WallCheakDis, LayerMask.GetMask("NoWall"));

        //바닥체크
        //isGround = Physics2D.OverlapCapsule(groundCheker.position, new Vector2(0.2f, 0.1f), CapsuleDirection2D.Horizontal, 0, LayerMask.GetMask("Ground"));
        isGround = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground"));

    }

    [SerializeField] float jumpStayTime; // 점프 유지시간
    [SerializeField] float jumpTime;
    [SerializeField] float stayPower;
    [SerializeField] bool isOneJump;
    private void CharJump()
    {
        if (!MovingStop)
        {
            //// 점프 길게 누르고잇으면 살짝 더 
            //if (Rb.velocity.y > 0 && isOneJump)
            //{
            //    jumpStayTime += Time.deltaTime;
            //    if (jumpStayTime > jumpTime)
            //    {
            //        isOneJump = false;
            //    }

            //    // 공중 지체시간의 절반이 넘어가면 상승속도 절반으로 ..
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

                //2단점프 제어
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
            //하강 중 속도 증가
            if (Rb.velocity.y < 0)
            {
                Rb.velocity -= verGravity * dropSpeed * Time.deltaTime;
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

    /// <summary>
    /// 코루틴 입니다
    /// </summary>
    /// <returns>코루틴이야 코루틴으로 꺼내</returns>
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
                SoundManager.instance.F_SoundPlay(SoundManager.instance.onHIt, 0.8f);
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

    // 캐릭터 애니메이션
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

    //기력회복
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

    //체력자연회복
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
            MaxWinY = new Vector2(Rb.velocity.x, 30); // 최대 올라가는 속도 제한

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
    /// 밀리아이템 스프라이트 on/off
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


