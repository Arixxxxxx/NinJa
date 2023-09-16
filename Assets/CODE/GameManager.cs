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
    public TypeEffect text;
    private Image NpcSprite;
    public bool MovingStop;
    //��������� Sclae.x�� ��������
    public bool AimLeft;
    public bool AimRight;
    // ���̵� �Խ��� üũ��
    public bool once;

    //���� ĳ�������۳�Ʈ
    public NPC npc;
    public Rigidbody2D ririRB;
    
    //�ٸ� ��ũ��Ʈ ���ٿ� ����
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
         

            if (_objname == "��������")
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
                            script.ani.SetBool("Show", true);
                            StartCoroutine(RiRITel(_obj));

                            //��ȯ�� ��ȯ

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
        meleeMode = true;
        CurArrow = 100;
        MaxArrow = 100;

        
        //���� ������
        talkmanager = GameObject.Find("TalkManager").GetComponent<TalkManager>(); // NPC��ȭâ ����
        gameUI = GameObject.Find("GameUI").GetComponent<Transform>(); // ����UI ����
        backgroundTR = GameObject.Find("BackGround").GetComponent<Transform>(); //��׶��� ����
        playerTR = GameObject.Find("Player").GetComponent<Transform>();
        //��ũ��Ʈ �����
        player = FindObjectOfType<Player>(); //�÷��̾�
        enemys = FindObjectOfType<Enemys>(); //���� 1,2
        dmgpooling = FindObjectOfType<DmgPooling>(); // ������ �����
        dmgfont = FindObjectOfType<DMGFont>();
        floatform = FindObjectOfType<FloatForm>();

        // NPC��ȭâ ���ٿ�
        NpcSprite = TalkBox.transform.Find("NpcSprite").GetComponent<Image>(); // NPC ��������Ʈ
        TalkBowNPCName = TalkBox.transform.Find("Name").GetComponent<TMP_Text>(); // NPC �̸�
        text =FindObjectOfType<TypeEffect>(); // ��ȭâ Ÿ���� 
      
        //����UI ���ٿ�
        gameUiText = gameUI.transform.Find("EventText").GetComponent <TMP_Text>();  //// �̺�Ʈ�� MainText���ٿ�
        ScreenText = gameUiText.GetComponent<GameUiText>(); // �̺�Ʈ�� MainText
        EventTimeBar = gameUI.transform.Find("EventTimeBar").GetComponent<Transform>(); // �̺�Ʈ �ð���
        TimeBar = EventTimeBar.transform.GetChild(1).GetComponent<Image>();  // �̺�Ʈ �ð��� Fill�� ���ٿ�
        TimeText = EventTimeBar.transform.GetChild(2).GetComponent<TMP_Text>(); // �̺�Ʈ �ð��پȿ� �ؽ�Ʈ

        //���̵�UI ���ٿ�
        GameGuideTR = GameObject.Find("GameGuide").GetComponent<Transform>();
        GuideText0 = GameGuideTR.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MainUiText>();
        guideM = GameGuideTR.GetComponent<GuideManager>();

        npc = GameObject.Find("NPC/����").GetComponent<NPC>();
        ririRB = npc.transform.GetComponent<Rigidbody2D>();

        //���ѱ涧 ����� ���
        blackScreen = gameUI.transform.Find("BlackScreen").GetComponent<Image>();

        //�̺�Ʈ�� ���� ������
        gamebackground = backgroundTR.transform.Find("NoLight/Sky").GetComponent<Tilemap>();

        //�ڷ���Ʈ �����Ϳ�
        telPoint1 = transform.Find("TelPoint0").GetComponent<Transform>();

        //Ʃ�丮����
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
