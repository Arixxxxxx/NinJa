using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# 인스턴스용(참조x)")]
    [Space]
    public Player player;
    public Enemys enemys;
    public DmgPooling dmgpooling;
    public DMGFont dmgfont;
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
    [SerializeField] private GameObject ScanNPC;
    [SerializeField] private GameObject TalkBox;
    [SerializeField] public bool isTalking;
    [SerializeField] private TalkManager talkmanager;

    private Image pressBtn;


    public void F_TalkSurch(GameObject _obj)
    {
         
            ScanNPC = _obj;
            SetNPCId sc = ScanNPC.GetComponent<SetNPCId>();
            TalkOn(sc.ID, sc.isNPC);

            TalkBox.gameObject.SetActive(isTalking);
    }

    public int TalkIndex;
    private void TalkOn(int _ID, bool _isNPC)
    {
             string talk =  talkmanager.F_GetMsg(_ID, TalkIndex);
        
        if(talk == null) 
        {
            isTalking = false;
            TalkIndex = 0;
            return; 
        }

        if (_isNPC)
        {
            
            TalkText.text = talk;
            pressBtn.gameObject.SetActive(true);
        }
        else
        {
            TalkText.text = talk;
            pressBtn.gameObject.SetActive(true);
        }

        isTalking = true;
        TalkIndex++;
    }

    private void Awake()
    {
        //float targetRatio = 16.0f / 9.0f // FHD 1920 1080 16:0
        //float ratio = (float)Screen.width / (float)Screen.height;
        //                                 //현재 스크린의 가로 , 세로
        //float scaleheight = ratio / targetRatio;
        //float fixedWidth = (float)Screen.width / scaleheight;
        //// 나누기
        //Screen.SetResolution((int)fixedWidth, Screen.height, true);
        /*Screen.SetResolution*/ // 프레임기능도있음 나중에 찾아봐야함
        /*Application.targetFrameRate = 120; /*///* 타겟 프레임*/

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
        talkmanager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
        player = FindObjectOfType<Player>();
        enemys = FindObjectOfType<Enemys>();
        dmgpooling = FindObjectOfType<DmgPooling>();
        dmgfont = FindObjectOfType<DMGFont>();
        pressBtn = TalkBox.transform.Find("PressEnter").GetComponent<Image>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
  
}
