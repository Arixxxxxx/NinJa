using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;

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

    //�ٸ� ��ũ��Ʈ ���ٿ� ����
    [HideInInspector] public Player player;
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
    





    public void F_TalkSurch(GameObject _obj)
    {
         
            ScanNPC = _obj;
            SetNPCId sc = ScanNPC.GetComponent<SetNPCId>();
            TalkOn(sc.ID, sc.isNPC, _obj.name);
            //TalkBox.gameObject.SetActive(true);
            TalkBox.SetBool("Show", isTalking);
    }

    public int TalkIndex;
    private void TalkOn(int _ID, bool _isNPC, string _objname)
    {
             string talk =  talkmanager.F_GetMsg(_ID, TalkIndex);
        
        if(talk == null) 
        {
            isTalking = false;
            TalkIndex = 0;
            Invoke("SpriteSetFalse", 0.3f);
            return; 
        }

        if (_isNPC)
        {
            TalkBowNPCName.text = $"< {_objname} >";
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
        {
            Destroy(gameObject);
        }
        meleeMode = true;
        CurArrow = 100;
        MaxArrow = 100;

        //���� ������
        talkmanager = GameObject.Find("TalkManager").GetComponent<TalkManager>(); // NPC��ȭâ ����
        gameUI = GameObject.Find("GameUI").GetComponent<Transform>(); // ����UI ����
        backgroundTR = GameObject.Find("BackGround").GetComponent<Transform>(); //��׶��� ����
        //��ũ��Ʈ �����
        player = FindObjectOfType<Player>(); //�÷��̾�
        enemys = FindObjectOfType<Enemys>(); //���� 1,2
        dmgpooling = FindObjectOfType<DmgPooling>(); // ������ �����
        dmgfont = FindObjectOfType<DMGFont>();
        
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


        //�̺�Ʈ�� ���� ������
        gamebackground = backgroundTR.transform.Find("Sky").GetComponent<Tilemap>();
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

}
