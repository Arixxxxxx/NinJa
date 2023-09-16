using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
   
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
    public TypeEffect text;
    private Image NpcSprite;
    public bool MovingStop;
    //레인지모드 Sclae.x값 변경조건
    public bool AimLeft;
    public bool AimRight;
    // 가이드 게시판 체크용
    public bool once;

    //리리 캐릭터컴퍼넌트
    public NPC npc;
    public Rigidbody2D ririRB;
    
    //다른 스크립트 접근용 변수
    [HideInInspector] public Player player;
    [HideInInspector] public Transform playerTR;

    [HideInInspector] public Enemys enemys;
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
    //밀리방어구 활성화
    public bool isGetMeleeItem;

     //원거리방어구 활성화
    public bool isGetRangeItem;

    // 검정화면 조절기능
    public Image blackScreen;

    //튜토리얼 이벤트[배틀존]
    [HideInInspector] public Transform tutorialEvent;
    [HideInInspector] public Transform battlezone;
    

    public void F_TalkSurch(GameObject _obj)
    {
            
            ScanNPC = _obj;
            SetNPCId sc = ScanNPC.GetComponent<SetNPCId>();
            TalkOn(sc.ID, sc.isNPC, _obj.name, _obj);
            //TalkBox.gameObject.SetActive(true);
            TalkBox.SetBool("Show", isTalking);
    }

    public int TalkIndex;
    private void TalkOn(int _ID, bool _isNPC, string _objname, GameObject _obj)
    {
             string talk =  talkmanager.F_GetMsg(_ID, TalkIndex);
        
        if(talk == null) 
        {
         

            if (_objname == "전투교관")
            {
             
                _obj.transform.GetChild(2).GetComponent<Transform>().gameObject.SetActive(false);
                SetNPCId sc = _obj.GetComponent<SetNPCId>();
                switch(sc.ID)
                {
                    case 200:
                        sc.ID += 1;
                        Animator ZomebieBox = battlezone.transform.GetChild(1).GetComponent<Animator>();
                        ZomebieBox.SetBool("Open", true);

                        break;

                    case 201:
                        //sc.ID += 1;

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
                            sc.ID += 1;
                            NPC script = _obj.GetComponent<NPC>();
                          
                            Transform questionMark =_obj.transform.GetChild(0).GetComponent<Transform>();
                            questionMark.gameObject.SetActive(false);
                            _obj.gameObject.layer = 12;
                            script.ani.SetBool("Show", true);
                            StartCoroutine(RiRITel(_obj));

                        }
                    break;

                    case 101: //동굴안에서 일단 사라지셈
                        {
                            sc.ID += 1;
                            NPC script = _obj.GetComponent<NPC>();
                            script.ani.SetBool("Show", true);
                            StartCoroutine(RiRITel(_obj));

                            //소환문 소환

                            StartCoroutine(GatePlay());
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
        meleeMode = true;
        CurArrow = 100;
        MaxArrow = 100;

        
        //접근 참조용
        talkmanager = GameObject.Find("TalkManager").GetComponent<TalkManager>(); // NPC대화창 폴더
        gameUI = GameObject.Find("GameUI").GetComponent<Transform>(); // 게임UI 폴더
        backgroundTR = GameObject.Find("BackGround").GetComponent<Transform>(); //백그라운드 폴더
        playerTR = GameObject.Find("Player").GetComponent<Transform>();
        //스크립트 연결용
        player = FindObjectOfType<Player>(); //플레이어
        enemys = FindObjectOfType<Enemys>(); //좀비 1,2
        dmgpooling = FindObjectOfType<DmgPooling>(); // 몹위에 대미지
        dmgfont = FindObjectOfType<DMGFont>();
        floatform = FindObjectOfType<FloatForm>();

        // NPC대화창 접근용
        NpcSprite = TalkBox.transform.Find("NpcSprite").GetComponent<Image>(); // NPC 스프라이트
        TalkBowNPCName = TalkBox.transform.Find("Name").GetComponent<TMP_Text>(); // NPC 이름
        text =FindObjectOfType<TypeEffect>(); // 대화창 타이핑 
      
        //게임UI 접근용
        gameUiText = gameUI.transform.Find("EventText").GetComponent <TMP_Text>();  //// 이벤트용 MainText접근용
        ScreenText = gameUiText.GetComponent<GameUiText>(); // 이벤트용 MainText
        EventTimeBar = gameUI.transform.Find("EventTimeBar").GetComponent<Transform>(); // 이벤트 시간바
        TimeBar = EventTimeBar.transform.GetChild(1).GetComponent<Image>();  // 이벤트 시간바 Fill값 접근용
        TimeText = EventTimeBar.transform.GetChild(2).GetComponent<TMP_Text>(); // 이벤트 시간바안에 텍스트

        //가이드UI 접근용
        GameGuideTR = GameObject.Find("GameGuide").GetComponent<Transform>();
        GuideText0 = GameGuideTR.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MainUiText>();
        guideM = GameGuideTR.GetComponent<GuideManager>();

        npc = GameObject.Find("NPC/리리").GetComponent<NPC>();
        ririRB = npc.transform.GetComponent<Rigidbody2D>();

        //씬넘길때 사용할 배경
        blackScreen = gameUI.transform.Find("BlackScreen").GetComponent<Image>();

        //이벤트용 배경색 조절용
        gamebackground = backgroundTR.transform.Find("NoLight/Sky").GetComponent<Tilemap>();

        //텔레포트 포인터용
        telPoint1 = transform.Find("TelPoint0").GetComponent<Transform>();

        //튜토리얼존
        tutorialEvent = GameObject.Find("TutorialEvent").GetComponent<Transform>();
        battlezone = tutorialEvent.transform.Find("BattleTraning").GetComponent<Transform>();
    }
    private void Update()
    {
        if (!meleeMode) { rangeMode = true; }
        else { rangeMode = false; }
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

    private IEnumerator GatePlay()
    {
        GameManager.Instance.MovingStop = true;
        GameManager.Instance.playerTR.localScale = new Vector3(3, 3, 3);
        GetItemNPC.Instance.aniGate.SetTrigger("ShowUp");
        GetItemNPC.Instance.partiGate.Play();
        Emoticon.instance.F_GetEmoticonBox("Question");

        yield return new WaitForSecondsRealtime(6.5f);

        GameManager.Instance.MovingStop = false;
        GetItemNPC.Instance.partiGate.gameObject.SetActive(false);
    }
}
