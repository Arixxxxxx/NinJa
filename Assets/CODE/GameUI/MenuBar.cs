using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MenuBar : MonoBehaviour
{
    Button menuColl;
    Transform mainMenu;
    Button continueBtn;
    Button soundOptionBtn;
    Button gameExitBtn;

    Transform soundOptionMenu;
    Button optionReturn;
    Slider BgSound;

    Transform reallyExit;
    Button yesBtn;
    Button noBtn;

    AudioSource Audio;

    // GM 모드 버튼
    Button gmBtn;
    Transform gmMenu;

    Button moveB;
    Button moveS;
    Button GetSkillPoint;
    Button GetExp;
    Vector3 originPos;
    Vector3 battleZonePos;

    Button getItem1;
    Button getItem2;
    public TMP_InputField passwardWindow;
    Button closeBtn;
    Transform gmModeMenu;

    //스킬트리 버튼
    Button skillTreeBtn;
    private void Awake()
    {


        //게임메뉴옵션열기
        menuColl = transform.Find("Btn/MenuColl").GetComponent<Button>();

        //메뉴옵션
        mainMenu = transform.Find("MenuBar").GetComponent<Transform>();

        //메뉴창
        continueBtn = mainMenu.transform.Find("InBoxPanel/Continue").GetComponent<Button>();
        soundOptionBtn = mainMenu.transform.Find("InBoxPanel/SoundOption").GetComponent<Button>();
        gameExitBtn = mainMenu.transform.Find("InBoxPanel/ExitGame").GetComponent<Button>();

        //사운드옵션
        soundOptionMenu = mainMenu.transform.Find("SoundOption").GetComponent<Transform>();
        optionReturn = soundOptionMenu.transform.Find("Exit").GetComponent<Button>();
        BgSound = soundOptionMenu.transform.Find("BG").GetComponent<Slider>();


        //정말 끌꺼야? 창
        reallyExit = mainMenu.transform.Find("RealExit").GetComponent<Transform>();
        yesBtn = reallyExit.GetChild(1).GetComponent<Button>();
        noBtn = reallyExit.GetChild(2).GetComponent<Button>();

        Audio = GetComponent<AudioSource>();


        //GM모드관련
        gmBtn = transform.Find("Btn/GmMode").GetComponent<Button>();
        gmModeMenu = transform.Find("GmModeMenu").GetComponent<Transform>();

        gmMenu = gmModeMenu.transform.GetChild(0).GetComponent<Transform>();
        getItem1 = gmMenu.transform.Find("GetItemMelee").GetComponent<Button>();
        getItem2 = gmMenu.transform.Find("GetItemRange").GetComponent<Button>();
        passwardWindow = gmModeMenu.transform.GetChild(1).GetComponent<TMP_InputField>();
        closeBtn = passwardWindow.transform.Find("X").GetComponent<Button>();
        moveB = gmMenu.transform.Find("MoveB").GetComponent<Button>();

        moveS = gmMenu.transform.Find("MoveS").GetComponent<Button>();
        moveS = gmMenu.transform.Find("MoveS").GetComponent<Button>();

        skillTreeBtn = transform.Find("Btn/SkillBtn").GetComponent<Button>();
        SkillPointWindow sc = GetComponent<SkillPointWindow>();
        skillTreeBtn.onClick.AddListener(() => { sc.F_SkillTreeWindowPopUp();});

    }

    private void Start()
    {
        if(GameManager.Instance.SceneName == "Chapter2")
        {
            GetSkillPoint = gmMenu.transform.Find("GetSkillPoint").GetComponent<Button>();
            GetSkillPoint.onClick.AddListener(() => { GameManager.Instance.gameUI.GetComponent<SkillPointWindow>().F_GetStatsPoint(1); });
            GetExp = gmMenu.transform.Find("GetEXP").GetComponent<Button>();
            GetExp.onClick.AddListener(() => { ExpManager.instance.F_GmModeGetExp(); });
        }

        //좌표저장
        originPos = GameObject.Find("Player").transform.position;
        battleZonePos = GameObject.Find("PoolManager").transform.Find("GMZone").transform.position;

        Audio.clip = SoundManager.instance.BtnClick;

        menuColl.onClick.AddListener(() => { MenuOpen(0); Audio.Play(); });
        continueBtn.onClick.AddListener(() => { MenuOpen(1); });
        soundOptionBtn.onClick.AddListener(() => { MenuOpen(2); Audio.Play(); });
        optionReturn.onClick.AddListener(() => { MenuOpen(3); Audio.Play(); });
        gameExitBtn.onClick.AddListener(() => { MenuOpen(4); Audio.Play(); });
        noBtn.onClick.AddListener(() => { MenuOpen(5); Audio.Play(); });
        yesBtn.onClick.AddListener(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("Main"); });

        moveB.onClick.AddListener(() =>
        {
            Player.instance.transform.position = battleZonePos;
            Camera.main.transform.position 
             =
            new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y, Camera.main.transform.position.z);
        });
        moveS.onClick.AddListener(() =>
        {
            Player.instance.transform.position = originPos;
            Camera.main.transform.position
             =
            new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y, Camera.main.transform.position.z);
        });

        gmBtn.onClick.AddListener(() =>
        {
            if (gmMenu.gameObject.activeSelf)
            {
                gmMenu.gameObject.SetActive(false);
                isPasswardPopup = false;
            }

            else if (!passwardWindow.gameObject.activeSelf)
            {
                passwardWindow.gameObject.SetActive(true);

            }


        }
       );

        closeBtn.onClick.AddListener(() => { passwardWindow.text = string.Empty; passwardWindow.gameObject.SetActive(false); });

        //gmBtn.onClick.AddListener(() =>
        //{
        //    if (!gmMenu.gameObject.activeSelf)
        //    {
        //        gmMenu.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        gmMenu.gameObject.SetActive(false);
        //    }
        //}
        //);

        getItem1.onClick.AddListener(() => { if (!GameManager.Instance.isGetMeleeItem) GameManager.Instance.isGetMeleeItem = true; });
        getItem2.onClick.AddListener(() => { if (!GameManager.Instance.isGetRangeItem) GameManager.Instance.isGetRangeItem = true; });

    }



    private void Update()
    {
        EscKeyMenuClose();
        PasswardWindowClose();


    }
    bool isPasswardPopup;
    private void PasswardWindowClose()
    {
        if (!isPasswardPopup)
        {


            if (passwardWindow.text == "1234")
            {
                isPasswardPopup = true;
                passwardWindow.gameObject.SetActive(false);
                gmMenu.gameObject.SetActive(true);
                passwardWindow.text = string.Empty;
            }
        }

    }
    private void EscKeyMenuClose()
    {
        if (mainMenu.gameObject.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenu.gameObject.activeSelf && !soundOptionMenu.gameObject.activeSelf && !reallyExit.gameObject.activeSelf && !GameManager.Instance.GuideWindow.gameObject.activeSelf)
        {   
            MenuOpen(0);
            Audio.Play();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && mainMenu.gameObject.activeSelf && !soundOptionMenu.gameObject.activeSelf && !reallyExit.gameObject.activeSelf))
        {
            MenuOpen(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && soundOptionMenu.gameObject.activeSelf)
        {
            MenuOpen(3);
            Audio.Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && reallyExit.gameObject.activeSelf)
        {
            MenuOpen(5);
            Audio.Play();
        }
    }

    /// <summary>
    /// 메뉴 열닫
    /// </summary>
    /// <param name="_value">메인메뉴 0열기 1닫기 , 사운드메뉴 열기2 닫기3, 진짜꺼 4열기 닫기5</param>
    private void MenuOpen(int _value)
    {
        switch (_value)
        {
            case 0:
                if (!mainMenu.gameObject.activeSelf)
                {
                    mainMenu.gameObject.SetActive(true);
                }
                break;


            case 1:
                if (mainMenu.gameObject.activeSelf)
                {
                    mainMenu.gameObject.SetActive(false);
                }
                break;

            case 2:
                if (!soundOptionMenu.gameObject.activeSelf)
                {
                    soundOptionMenu.gameObject.SetActive(true);
                }
                break;

            case 3:
                if (soundOptionMenu.gameObject.activeSelf)
                {
                    soundOptionMenu.gameObject.SetActive(false);
                }
                break;

            case 4:
                if (!reallyExit.gameObject.activeSelf)
                {
                    reallyExit.gameObject.SetActive(true);
                }
                break;

            case 5:
                if (reallyExit.gameObject.activeSelf)
                {
                    reallyExit.gameObject.SetActive(false);
                }
                break;
        }



    }

}
