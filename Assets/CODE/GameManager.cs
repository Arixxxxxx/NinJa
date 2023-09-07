using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [Space]
    [Space]
    [Header("# 인스턴스용(참조x)")]
    [Space]
    public Player player;
    public Enemys enemys;
    public DmgPooling dmgpooling;
    public DMGFont dmgfont;
    public Transform gameUI;
    public TMP_Text gameUiText;
    public GameUiText ScreenText;
    public Transform EventTimeBar;
    public Image TimeBar;
    public TMP_Text TimeText;





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
        talkmanager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
        player = FindObjectOfType<Player>();
        enemys = FindObjectOfType<Enemys>();
        dmgpooling = FindObjectOfType<DmgPooling>();
        dmgfont = FindObjectOfType<DMGFont>();
        
        NpcSprite = TalkBox.transform.Find("NpcSprite").GetComponent<Image>();
        TalkBowNPCName = TalkBox.transform.Find("Name").GetComponent<TMP_Text>();
        text =FindObjectOfType<TypeEffect>();
        gameUI = GameObject.Find("GameUI").GetComponent<Transform>();
        gameUiText = gameUI.transform.Find("EventText").GetComponent <TMP_Text>();
        ScreenText = gameUiText.GetComponent<GameUiText>();
        EventTimeBar = gameUI.transform.Find("EventTimeBar").GetComponent<Transform>();
        TimeBar = EventTimeBar.transform.GetChild(1).GetComponent<Image>();
        TimeText = EventTimeBar.transform.GetChild(2).GetComponent<TMP_Text>();
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
