using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameObject glodbalLight;

    // ����Ʈ !@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    public Light2D worldLight;


    [Header("# ĳ���� ��������")]
    [Space]
    public bool meleeMode;
    public bool rangeMode;
    public float CurArrow;
    public float MaxArrow;
    
    [Header("# ĳ���� HP����")]
    [Space]
    public float Player_CurHP;
     public  float Player_MaxHP;
    [Header("# ĳ���� SP����")]
    [Space]
    public float Player_CurSP;
    public float Player_MaxSP;
    [Header("# ĳ���ͻ��")]
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
    private Image NpcSprite;
    public bool MovingStop;
    //��������� Sclae.x�� ��������
    public bool AimLeft;
    public bool AimRight;
    // ���̵� �Խ��� üũ��
    public bool once;

    //���� ĳ�������۳�Ʈ
    [HideInInspector] public NPC npc;
    [HideInInspector] public Rigidbody2D ririRB;

    //�������� ĳ�������۳�Ʈ
    [HideInInspector] public NPC npc2;
    [HideInInspector] public Transform questMark; // ����Ʈâ
    [HideInInspector] public Rigidbody2D battleNpcRb;

    //�ٸ� ��ũ��Ʈ ���ٿ� ����
    [HideInInspector] public Player player;
    [HideInInspector] public Transform playerTR;

    [HideInInspector] public Enemys enemys;

    // ī�޶� ���� ��Ʈ��
    [Header("# ī�޶� ���� Ȯ�ο�")]
    [Space]
    public bool normalCamera;
     public bool cameraShake;

    //��������� ��ġ���
    public Vector3 gateOriginPos;

    private Enemys _enemy;
    public Enemys Enemy2
    { 
        get => _enemy;
        private set => _enemy = value;
    }

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
    //������ ����Ʈ
    [HideInInspector] public Transform telPoint1;
    //�и��� Ȱ��ȭ
    public bool isGetMeleeItem;

     //���Ÿ��� Ȱ��ȭ
    public bool isGetRangeItem;

    // ����ȭ�� �������
    public Image blackScreen;

    //Ʃ�丮�� �̺�Ʈ[��Ʋ��]
    [HideInInspector] public Transform tutorialEvent;
    [HideInInspector] public Transform battlezone;
    [HideInInspector] public Transform rangeZone;

    //��Ʋ�� ���������� �������
    [HideInInspector] public bool FireStop;

    //������ Ʃ��� �̵����Ұ� �Ҹ���
    public bool legStop;
    public float deathEagleConter; // ųī����
    
    public float curEagle = 10; // ųī����
    public float totalDeathEagle = 10; // ��ƾ��ϴ� ��������

    //Ʃ�丮�� �� ����
    bool Act1End;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {//���� �Ǿ���.
            Debug.Log("�����Ǿ���");
            Destroy(gameObject);
        }

        CameraShakeSwitch(1);

        meleeMode = true;
        CurArrow = 100;
        MaxArrow = 100;

        worldLight = glodbalLight.GetComponent<Light2D>();
        //���� ������
        talkmanager = GameObject.Find("TalkManager").GetComponent<TalkManager>(); // NPC��ȭâ ����
        gameUI = GameObject.Find("GameUI").GetComponent<Transform>(); // ����UI ����
        backgroundTR = GameObject.Find("BackGround").GetComponent<Transform>(); //��׶��� ����
        playerTR = GameObject.Find("Player").GetComponent<Transform>();

        //��ũ��Ʈ �����
        player = FindObjectOfType<Player>(); //�÷��̾�
        enemys = FindObjectOfType<Enemys>(); //���� 1,2
        _enemy = FindObjectOfType<Enemys>(); //���� 1,2
        dmgpooling = FindObjectOfType<DmgPooling>(); // ������ �����
        dmgfont = FindObjectOfType<DMGFont>();
        floatform = FindObjectOfType<FloatForm>();

        // NPC��ȭâ ���ٿ�
        NpcSprite = TalkBox.transform.Find("NpcSprite").GetComponent<Image>(); // NPC ��������Ʈ
        TalkBowNPCName = TalkBox.transform.Find("Name").GetComponent<TMP_Text>(); // NPC �̸�
        text = FindObjectOfType<TypeEffect>(); // ��ȭâ Ÿ���� 

        //����UI ���ٿ�
        gameUiText = gameUI.transform.Find("EventText").GetComponent<TMP_Text>();  //// �̺�Ʈ�� MainText���ٿ�
        ScreenText = gameUiText.GetComponent<GameUiText>(); // �̺�Ʈ�� MainText <ȭ���߾�>
        EventTimeBar = gameUI.transform.Find("EventTimeBar").GetComponent<Transform>(); // �̺�Ʈ �ð���
        TimeBar = EventTimeBar.transform.GetChild(1).GetComponent<Image>();  // �̺�Ʈ �ð��� Fill�� ���ٿ�
        TimeText = EventTimeBar.transform.GetChild(2).GetComponent<TMP_Text>(); // �̺�Ʈ �ð��پȿ� �ؽ�Ʈ

        //���̵�UI ���ٿ�
        GameGuideTR = GameObject.Find("GameGuide").GetComponent<Transform>();
        GuideText0 = GameGuideTR.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MainUiText>();
        guideM = GameGuideTR.GetComponent<GuideManager>();

        //����npc 
        npc = GameObject.Find("NPC/����").GetComponent<NPC>();
        ririRB = npc.transform.GetComponent<Rigidbody2D>();

        //��������npc
        npc2 = GameObject.Find("NPC/��������").GetComponent<NPC>();
        battleNpcRb = npc2.transform.GetComponent<Rigidbody2D>();
        questMark = npc2.transform.Find("TalkCheak").GetComponent<Transform>();


        //���ѱ涧 ����� ���
        blackScreen = gameUI.transform.Find("BlackScreen").GetComponent<Image>();

        //�̺�Ʈ�� ���� ������
        gamebackground = backgroundTR.transform.Find("NoLight/Sky").GetComponent<Tilemap>();

        //�ڷ���Ʈ �����Ϳ�
        telPoint1 = transform.Find("TelPoint0").GetComponent<Transform>();

        //Ʃ�丮����
        tutorialEvent = GameObject.Find("TutorialEvent").GetComponent<Transform>();
        battlezone = tutorialEvent.transform.Find("BattleTraning").GetComponent<Transform>();
        rangeZone = tutorialEvent.transform.Find("RangeZone").GetComponent<Transform>();
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        if (!meleeMode) { rangeMode = true; }
        else { rangeMode = false; }

        Act1EndBlackScreenOn();
        TalkOk();
    }

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

    //��ȭ �� npc ������
    private void TalkOn(int _ID, bool _isNPC, string _objname, GameObject _obj)
    {
             string talk =  talkmanager.F_GetMsg(_ID, TalkIndex);
        
        if(talk == null) 
        {
            isWaitTalking = true;

            if (_objname == "��������")
            {
             
                
                SetNPCId sc = _obj.GetComponent<SetNPCId>();

                switch(sc.ID)
                {
                    //�λ��ϰ� ���񲨳���
                    case 200:
                        sc.ID += 1;
                        //Animator ZomebieBox = battlezone.Find("ZombieBox").GetComponent<Animator>();
                        //ZomebieBox.SetBool("Open", true);
                        //GameManager.Instance.MovingStop = false;

                        StartCoroutine(Step1ZomeBieBoxOpen());
                      //�ڸ�ƾ���� ó�� 
                        break;

                   //������ ����׷��� ��Ž
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
                     // ����Ȱ�Ծ �����Ѵ��ϰ� �����ε��ư�
                    case 202:
                        sc.ID += 1;
                        NPC script2 = _obj.GetComponent<NPC>();
                        Transform questionMark = _obj.transform.GetChild(0).GetComponent<Transform>();
                        questionMark.gameObject.SetActive(false); // ��ĵ��� ����

                        script2.ani.SetBool("Show", true);
                        StartCoroutine(BattleNpcGotoHome(_obj));
                        StartCoroutine(GatePlay(_obj));

                        break;

                        //�ڿ� ���������� Ȱ������
                    case 203:
                        _obj.transform.GetChild(2).GetComponent<Transform>().gameObject.SetActive(false);
                        sc.ID += 1;

                        rangeZone.gameObject.SetActive(true) ;
                        break;

                    case 204:
                        questMark.gameObject.SetActive(false);
                        questMark.transform.parent.GetChild(0).gameObject.SetActive(false);
                        Act1End = true;
                        MovingStop = true;

                        break;


                }

            }
            if (_objname == "����")
            {
                _obj.transform.GetChild(2).GetComponent<Transform>().gameObject.SetActive(false);
                SetNPCId sc = _obj.GetComponent<SetNPCId>();

                switch (sc.ID)
                {
                    case 100: //������������ ����������
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

                    case 101: //�����ȿ��� �ϴ� �������
                        {
                            sc.ID += 1;
                            NPC script = _obj.GetComponent<NPC>();
                            Transform questionMark = _obj.transform.GetChild(0).GetComponent<Transform>();
                            questionMark.gameObject.SetActive(false); // ��ĵ��� ����

                            script.ani.SetBool("Show", true);
                            StartCoroutine(RiRITel(_obj));

                            //��ȯ�� ��ȯ

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
            TalkBowNPCName.text = $"< {_objname} >"; // �̸� = ������Ʈ�̸�
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

    //���� ��ȭ���� ���������� �����̵�
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


    //�������� ����Ʈ ��ȯ��
    private IEnumerator GatePlay(GameObject obj)
    {
        GameManager.Instance.MovingStop = true;
        yield return new WaitForSecondsRealtime(1.3f);
        GameManager.Instance.playerTR.localScale = new Vector3(3, 3, 3); //���� �ٶ�

        //�ٴ������� �̸�Ƽ�ܹڽ� �ñ���
          if (obj.gameObject.name == "����")
        {
            GetItemNPC.Instance.partiGate.Play();
            Emoticon.instance.F_GetEmoticonBox("Question");
            CameraShakeSwitch(0);
            yield return new WaitForSecondsRealtime(1.5f);
            gateOriginPos = GetItemNPC.Instance.aniGate.transform.position;
            GetItemNPC.Instance.aniGate.SetTrigger("ShowUp");

            yield return new WaitForSecondsRealtime(7f);

            GameManager.Instance.MovingStop = false;

            CameraShakeSwitch(1);
            GetItemNPC.Instance.partiGate.gameObject.SetActive(false);

        }

        else if(obj.gameObject.name == "��������")
        {
            GetItemNPC2.Instance.partiGate.Play();
            Emoticon.instance.F_GetEmoticonBox("Question");
            CameraShakeSwitch(0);
            yield return new WaitForSecondsRealtime(1.5f);
            GetItemNPC2.Instance.aniGate.SetTrigger("ShowUp");

            yield return new WaitForSecondsRealtime(7f);

            GameManager.Instance.MovingStop = false;
            CameraShakeSwitch(1);
            GetItemNPC2.Instance.partiGate.gameObject.SetActive(false);
        }

    }

    float talkingtimer;
    [SerializeField] float talkingWaitTime = 2;
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
    /// ī�޶� ����ũ
    /// </summary>
    /// <param name="_Value">0="�ѱ�" , 1="����"</param>
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
}
