using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameObject glodbalLight;

    // 라이트 !@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    public Light2D worldLight;

    [Header("# 무기활성화 ")]
    //밀리방어구 활성화
    public bool isGetMeleeItem;
    //원거리방어구 활성화
    public bool isGetRangeItem;

    [Header("# 캐릭터 전투관련")]
    [Space]
    public bool meleeMode;
    public bool rangeMode;
    public float CurArrow;
    public float MaxArrow;
    
    [Header("# 캐릭터 HP설정")]
    [Space]
    public float Player_CurHP;
     public  float Player_MaxHP;
    [Header("# 캐릭터 SP설정")]
    [Space]
    public float Player_CurSP;
    public float Player_MaxSP;
    [Header("# 캐릭터사망")]
    [Space]
    public bool isPlayerDead;

    [Header("# Talk")]
    [Space]
    [SerializeField] private TextMeshProUGUI TalkText;
    private TMP_Text TalkBowNPCName;
    [SerializeField] private GameObject ScanNPC;
    [SerializeField] private Animator TalkBox;
    [SerializeField] public bool isTalking;
    [SerializeField] private TalkManager talkmanager;
    [SerializeField] public bool isWaitTalking;


    public TypeEffect text;
    [HideInInspector] public Image NpcSprite;
    public bool MovingStop;
    //레인지모드 Sclae.x값 변경조건
    [HideInInspector] public bool AimLeft;
    [HideInInspector] public bool AimRight;
    // 가이드 게시판 체크용
    [HideInInspector] public bool once;

    //리리 캐릭터컴퍼넌트
    [HideInInspector] public NPC npc;
    [HideInInspector] public Rigidbody2D ririRB;

    //전투교관 캐릭터컴퍼넌트
    [HideInInspector] public NPC npc2;
    [HideInInspector] public Transform questMark; // 퀘스트창
    [HideInInspector] public Rigidbody2D battleNpcRb;
    [HideInInspector] public SetNPCId battleNPCiD;
    //좀비3마리 처치 퀘스트 시작 
    bool Qeust1Start;


    //다른 스크립트 접근용 변수
    [HideInInspector] public Player player;
    [HideInInspector] public Transform playerTR;

    [HideInInspector] public Enemys enemys;

    // 카메라 연출 컨트롤
    [Header("# 카메라 연출 확인용")]
    [Space]
    public bool normalCamera;
     public bool cameraShake;

    //고대차원문 위치기억
    public Vector3 gateOriginPos;

    private Enemys _enemy;
    public Enemys Enemy2
    { 
        get => _enemy;
        private set => _enemy = value;
    }

    //퀘스트용 좀비3마리
    public int Q1; // 좀비 킬횟수
   

    [HideInInspector] public DmgPooling dmgpooling;
    [HideInInspector] public DMGFont dmgfont;
    [HideInInspector] public Transform gameUI;
    [HideInInspector] public TMP_Text gameUiText;
    [HideInInspector] public GameUiText ScreenText;
    [HideInInspector] public Transform GameGuideTR;
    [HideInInspector] public MainUiText GuideText0;
    [HideInInspector] public Transform EventTimeBar;
    [HideInInspector] public Image TimeBar;
    [HideInInspector] public TMP_Text TimeText;
    [HideInInspector] public Transform backgroundTR;
    [HideInInspector] public Tilemap gamebackground;
    [HideInInspector] public GuideManager guideM;
    [HideInInspector] public FloatForm floatform;
    //텔포용 포인트
    [HideInInspector] public Transform telPoint1;


    // 검정화면 조절기능
    public Image blackScreen;

    //튜토리얼 이벤트[배틀존]
    [HideInInspector] public Transform tutorialEvent;
    [HideInInspector] public Transform battlezone;
    [HideInInspector] public Transform rangeZone;

    //배틀존 엘레베이터 사격중지
    [HideInInspector] public bool FireStop;

    //마지막 튜토용 이동만불가 불리언
    public bool legStop;
    public float deathEagleConter; // 킬카운터
    
    public float curEagle = 10; // 킬카운터
    public float totalDeathEagle = 10; // 잡아야하는 독수리양

    //NPC 대화종료후 다시 말거는 타이밍
    float talkingtimer;
    [SerializeField] float talkingWaitTime = 2.5f;

    //튜토리얼 씬 종료
    bool Act1End;

    //대화중일때 NPC 움직임 멈춤
    [HideInInspector] public bool curPlayerTalkingYouStop;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {//삭제 되었음.
            Debug.Log("삭제되엇음");
            Destroy(gameObject);
        }

      
        CameraShakeSwitch(1);

        meleeMode = true;
        CurArrow = 100;
        MaxArrow = 100;

        worldLight = glodbalLight.GetComponent<Light2D>();
        //접근 참조용
        talkmanager = GameObject.Find("TalkManager").GetComponent<TalkManager>(); // NPC대화창 폴더
        gameUI = GameObject.Find("GameUI").GetComponent<Transform>(); // 게임UI 폴더
        backgroundTR = GameObject.Find("BackGround").GetComponent<Transform>(); //백그라운드 폴더
        playerTR = GameObject.Find("Player").GetComponent<Transform>();

        //스크립트 연결용
        player = FindObjectOfType<Player>(); //플레이어
        enemys = FindObjectOfType<Enemys>(); //좀비 1,2
        _enemy = FindObjectOfType<Enemys>(); //좀비 1,2
        dmgpooling = FindObjectOfType<DmgPooling>(); // 몹위에 대미지
        dmgfont = FindObjectOfType<DMGFont>();
        floatform = FindObjectOfType<FloatForm>();

        // NPC대화창 접근용
        NpcSprite = TalkBox.transform.Find("NpcSprite").GetComponent<Image>(); // NPC 스프라이트
        TalkBowNPCName = TalkBox.transform.Find("Name").GetComponent<TMP_Text>(); // NPC 이름
        text = FindObjectOfType<TypeEffect>(); // 대화창 타이핑 

        //게임UI 접근용
        gameUiText = gameUI.transform.Find("EventText").GetComponent<TMP_Text>();  //// 이벤트용 MainText접근용
        ScreenText = gameUiText.GetComponent<GameUiText>(); // 이벤트용 MainText <화면중앙>
        EventTimeBar = gameUI.transform.Find("EventTimeBar").GetComponent<Transform>(); // 이벤트 시간바
        TimeBar = EventTimeBar.transform.GetChild(1).GetComponent<Image>();  // 이벤트 시간바 Fill값 접근용
        TimeText = EventTimeBar.transform.GetChild(2).GetComponent<TMP_Text>(); // 이벤트 시간바안에 텍스트

        //가이드UI 접근용
        GameGuideTR = GameObject.Find("GameGuide").GetComponent<Transform>();
        GuideText0 = GameGuideTR.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MainUiText>();
        guideM = GameGuideTR.GetComponent<GuideManager>();

        //리리npc 
        npc = GameObject.Find("NPC/리리").GetComponent<NPC>();
        ririRB = npc.transform.GetComponent<Rigidbody2D>();

        //전투교관npc
        npc2 = GameObject.Find("NPC/전투교관").GetComponent<NPC>();
        battleNpcRb = npc2.transform.GetComponent<Rigidbody2D>();
        questMark = npc2.transform.Find("TalkCheak").GetComponent<Transform>();
        battleNPCiD = npc2.GetComponent<SetNPCId>();


        //씬넘길때 사용할 배경
        blackScreen = gameUI.transform.Find("BlackScreen").GetComponent<Image>();

        //이벤트용 배경색 조절용
        gamebackground = backgroundTR.transform.Find("NoLight/Sky").GetComponent<Tilemap>();

        //텔레포트 포인터용
        telPoint1 = transform.Find("TelPoint0").GetComponent<Transform>();

        //튜토리얼존
        tutorialEvent = GameObject.Find("TutorialEvent").GetComponent<Transform>();
        battlezone = tutorialEvent.transform.Find("BattleTraning").GetComponent<Transform>();
        rangeZone = tutorialEvent.transform.Find("RangeZone").GetComponent<Transform>();
    }

    private void Start()
    {
        NpcSprite.gameObject.SetActive(false);
        if (rangeZone.gameObject.activeSelf)
        {
            rangeZone.gameObject.SetActive(true);   
        }
    }
    private void Update()
    {
        if (!meleeMode) { rangeMode = true; }
        else { rangeMode = false; }

        Act1EndBlackScreenOn();
        TalkOk();
        if(Qeust1Start)
        {
            ScUp();
        }
       
    }

    bool once2;
    private void Act1EndBlackScreenOn()
    {
        if (!Act1End)
        {
            return;
        }
       else if (Act1End)
        {
            blackScreen.gameObject.SetActive(true);
            blackScreen.color += new Color(0, 0, 0, 0.15f) * Time.deltaTime;
            if(blackScreen.color.a > 0.95f && !once2)
            {
                once2=true;
                SceneManager.LoadScene("Main");

            }
        }
      
    }
    public void F_TalkSurch(GameObject _obj)
    {
            
            ScanNPC = _obj;
            SetNPCId sc = ScanNPC.GetComponent<SetNPCId>();
            TalkOn(sc.ID, sc.isNPC, _obj.name, _obj);
            //TalkBox.gameObject.SetActive(true);
            TalkBox.SetBool("Show", isTalking);
    }

    
    public int TalkIndex;

    //대화 및 npc 연출기능
    private void TalkOn(int _ID, bool _isNPC, string _objname, GameObject _obj)
    {
             string talk =  talkmanager.F_GetMsg(_ID, TalkIndex);
        
        if(talk == null) 
        {
            isWaitTalking = true;
            curPlayerTalkingYouStop = false;

            if (_objname == "전투교관")
            {
             
                
                SetNPCId sc = _obj.GetComponent<SetNPCId>();

                switch(sc.ID)
                {
                    //인사하고 좀비꺼내줌
                    case 200:
                        Qeust1Start = true;

                        questMark.gameObject.SetActive(false);
                        StartCoroutine(Step1ZomeBieBoxOpen());
                   
                        break;

                   //산으로 가라그러고 텔탐
                    case 201:
                        sc.ID += 1;
                        Animator ZomebieBoxs = battlezone.transform.Find("ZombieBox").GetComponent<Animator>();
                        ZomebieBoxs.gameObject.SetActive(false);
                        _obj.transform.GetChild(2).GetComponent<Transform>().gameObject.SetActive(false);
                       
                        _obj.gameObject.layer = 12;
                        NPC script = _obj.GetComponent<NPC>();
                        script.ani.SetBool("Show", true);
                        

                        StartCoroutine(RiRITel(_obj));

                        break;

                     // 좋은활먹어서 ㅊㅋ한다하고 집으로돌아감
                    case 202:
                        sc.ID += 1;
                     
                        NPC script2 = _obj.GetComponent<NPC>();
                        Transform questionMark = _obj.transform.GetChild(0).GetComponent<Transform>();
                        questionMark.gameObject.SetActive(false); // 스캔기능 종료

                        script2.ani.SetBool("Show", true);
                       
                        StartCoroutine(BattleNpcGotoHome(_obj));
                        StartCoroutine(GatePlay(_obj));

                        break;

                        //뒤에 의자위에서 활쏘라고함
                    case 203:
                        _obj.transform.GetChild(2).GetComponent<Transform>().gameObject.SetActive(false);
                        rangeZone.gameObject.SetActive(true) ;

                        if (rangeZone.transform.Find("UI").GetComponent<Transform>().gameObject.activeSelf)
                        {
                            rangeZone.transform.Find("UI").GetComponent<Transform>().gameObject.SetActive(true);
                        }

                        break;

                    case 204:
                        questMark.gameObject.SetActive(false);
                        questMark.transform.parent.GetChild(0).gameObject.SetActive(false);
                        Act1End = true;
                        MovingStop = true;

                        break;


                }

            }
            if (_objname == "리리")
            {
                _obj.transform.GetChild(2).GetComponent<Transform>().gameObject.SetActive(false);
                SetNPCId sc = _obj.GetComponent<SetNPCId>();

                switch (sc.ID)
                {
                    case 100: //시작지점에서 동굴안으로
                        {
                            npc.transform.Find("Byuk").GetComponent<Transform>().gameObject.SetActive(false);
                            sc.ID += 1;
                            NPC script = _obj.GetComponent<NPC>();
                          
                            Transform questionMark =_obj.transform.GetChild(0).GetComponent<Transform>();
                            questionMark.gameObject.SetActive(false);
                            _obj.gameObject.layer = 12;
                            script.ani.SetBool("Show", true);
                            
                            StartCoroutine(RiRITel(_obj));
                            //transform.Find("Byuk").gameObject.SetActive(false);
                        }
                    break;

                    case 101: //동굴안에서 일단 사라지셈
                        {
                            npc.transform.Find("Byuk2").GetComponent<Transform>().gameObject.SetActive(false);
                            sc.ID += 1;
                            NPC script = _obj.GetComponent<NPC>();
                            Transform questionMark = _obj.transform.GetChild(0).GetComponent<Transform>();
                            questionMark.gameObject.SetActive(false); // 스캔기능 종료

                            script.ani.SetBool("Show", true);
                            
                            StartCoroutine(RiRITel(_obj));

                            //소환문 소환

                            StartCoroutine(GatePlay(_obj));
                        }
                        break;

                }
              
          
            }

            isTalking = false;
            TalkIndex = 0;
            Invoke("SpriteSetFalse", 0.3f);
            return; 
        }
     
        if (_isNPC)
        {
            TalkBowNPCName.text = $"< {_objname} >"; // 이름 = 오브젝트이름
            NpcSprite.gameObject.SetActive(true);
            text.F_SetMsg(talk.Split(':')[0]);
            NpcSprite.sprite = talkmanager.F_GetSprite(_ID, int.Parse(talk.Split(':')[1]));
           
        }

        else
        {
            TalkBowNPCName.text = $"< {_objname} >";
            NpcSprite.gameObject.SetActive(true);
            text.F_SetMsg(talk.Split(':')[0]);
            NpcSprite.sprite = talkmanager.F_GetSprite(_ID, int.Parse(talk.Split(':')[1]));
        }

        isTalking = true;
        TalkIndex++;
    }

    IEnumerator Step1ZomeBieBoxOpen()
    {
        Animator ZomebieBox = battlezone.Find("ZombieBox").GetComponent<Animator>();
        ZomebieBox.SetBool("Open", true);
        ZomebieBox.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameManager.Instance.MovingStop = false;
        yield return new WaitForSecondsRealtime(4);
        for(int i = 0; i < 6; i++)
        {
            ZomebieBox.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        ZomebieBox.SetTrigger("Hide");
        ZomebieBox.transform.Find("Door").GetComponent<BoxCollider2D>().enabled = false;
       
        
    }
    

    public void SpriteSetFalse()
    {
        NpcSprite.gameObject.SetActive(false);
    }

    //리리 대화이후 동굴끝으로 순간이동
   private IEnumerator RiRITel(GameObject _obj)
    {
        yield return new WaitForSecondsRealtime(1.7f);
        _obj.transform.position = _obj.transform.Find("TelPoint1").transform.position;
        Transform questionMark = _obj.transform.GetChild(0).GetComponent<Transform>();
        _obj.gameObject.layer = 18;
        questionMark.gameObject.SetActive(true);
        NPC sc = _obj.GetComponent<NPC>();

        sc.ani.SetBool("Show", false);
        ririRB.gravityScale = 0;
        _obj.gameObject.SetActive(false);
    }
    private IEnumerator BattleNpcGotoHome(GameObject _obj)
    {
        _obj.transform.Find("TalkCheak").gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1.7f);
        _obj.transform.position = transform.Find("TelPoint1").transform.position;
        Transform questionMark = _obj.transform.GetChild(0).GetComponent<Transform>();
        _obj.gameObject.layer = 18;
        questionMark.gameObject.SetActive(true);
        NPC sc = _obj.GetComponent<NPC>();

        sc.ani.SetBool("Show", false);

        _obj.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(18);
        _obj.gameObject.SetActive(true);
        sc.ani.SetBool("Show", true);
        yield return new WaitForSecondsRealtime(1);
        sc.ani.SetBool("Show", false);
        _obj.transform.Find("TalkCheak").gameObject.SetActive(true);

    }


    //동굴에서 게이트 소환씬
    private IEnumerator GatePlay(GameObject obj)
    {
        GameManager.Instance.MovingStop = true;
        yield return new WaitForSecondsRealtime(1.3f);
        GameManager.Instance.playerTR.localScale = new Vector3(3, 3, 3); //우측 바라봄

        //바닥진동과 이모티콘박스 궁금중
          if (obj.gameObject.name == "리리")
        {

            GetItemNPC.Instance.Audio.Play();
            GetItemNPC.Instance.partiGate.Play();
            Emoticon.instance.F_GetEmoticonBox("Question");
            CameraShakeSwitch(0);
            yield return new WaitForSecondsRealtime(1.5f);
            gateOriginPos = GetItemNPC.Instance.aniGate.transform.position;
            GetItemNPC.Instance.aniGate.SetTrigger("ShowUp");

            yield return new WaitForSecondsRealtime(7f);

            GameManager.Instance.MovingStop = false;

            CameraShakeSwitch(1);
            GetItemNPC.Instance.Audio.Stop();
            GetItemNPC.Instance.partiGate.gameObject.SetActive(false);

        }

        else if(obj.gameObject.name == "전투교관")
        {
            GetItemNPC2.Instance.partiGate.Play();
            GetItemNPC2.Instance.Audio.Play();
            Emoticon.instance.F_GetEmoticonBox("Question");
            CameraShakeSwitch(0);
            yield return new WaitForSecondsRealtime(1.5f);
            GetItemNPC2.Instance.aniGate.SetTrigger("ShowUp");

            yield return new WaitForSecondsRealtime(7f);

            GameManager.Instance.MovingStop = false;
            CameraShakeSwitch(1);
            GetItemNPC2.Instance.Audio.Stop();
            GetItemNPC2.Instance.partiGate.gameObject.SetActive(false);
        }

    }

  
    private void TalkOk()
    {
        if (!isWaitTalking) 
        {
            return;
        }

        else if (isWaitTalking)
        {
            talkingtimer += Time.deltaTime;
            if(talkingtimer > talkingWaitTime)
            {
                talkingtimer = 0;
                isWaitTalking = false;
            }

        }
    }

    /// <summary>
    /// 카메라 쉐이크
    /// </summary>
    /// <param name="_Value">0="켜기" , 1="끄기"</param>
    public void CameraShakeSwitch(int _Value)
    {
        switch (_Value)
        {
            case 0:
                normalCamera = false;
                cameraShake = true;
                break;
            case 1:
                normalCamera = true;
                cameraShake = false;
                break;
        }

    }

    //퀘스트 완료시
    bool once1;
    private void ScUp()
    {
        if (Q1 == 3 && !once1)
        {
            once1 = true;
            battleNPCiD.ID++;
            questMark.gameObject.SetActive(true);
            Qeust1Start = false;
        }
    }
}
