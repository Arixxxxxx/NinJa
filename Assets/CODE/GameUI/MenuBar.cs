using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    private void Awake()
    {
        //���Ӹ޴��ɼǿ���
        menuColl = transform.Find("MenuColl").GetComponent<Button>();

        //�޴��ɼ�
        mainMenu = transform.Find("MenuBar").GetComponent<Transform>();

        //�޴�â
        continueBtn = mainMenu.transform.Find("InBoxPanel/Continue").GetComponent<Button>();
        soundOptionBtn = mainMenu.transform.Find("InBoxPanel/SoundOption").GetComponent<Button>();
        gameExitBtn = mainMenu.transform.Find("InBoxPanel/ExitGame").GetComponent<Button>();

        //����ɼ�
        soundOptionMenu = mainMenu.transform.Find("SoundOption").GetComponent<Transform>();
        optionReturn = soundOptionMenu.transform.Find("Exit").GetComponent<Button>();
        BgSound = soundOptionMenu.transform.Find("BG").GetComponent<Slider>();


        //���� ������? â
        reallyExit = mainMenu.transform.Find("RealExit").GetComponent<Transform>();
        yesBtn = reallyExit.GetChild(1).GetComponent<Button>();
        noBtn = reallyExit.GetChild(2).GetComponent<Button>();

        Audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
        Audio.clip = SoundManager.instance.BtnClick;

        menuColl.onClick.AddListener(() => { MenuOpen(0); Audio.Play(); });
        continueBtn.onClick.AddListener(() => { MenuOpen(1);});
        soundOptionBtn.onClick.AddListener(() => {MenuOpen(2); Audio.Play();});
        optionReturn.onClick.AddListener(() => {MenuOpen(3); Audio.Play(); });
        gameExitBtn.onClick.AddListener(() => {MenuOpen(4); Audio.Play(); });
        noBtn.onClick.AddListener(() => { MenuOpen(5); Audio.Play(); });
        yesBtn.onClick.AddListener(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("Main");  });


    }



    private void Update()
    {
        EscKeyMenuClose();
     
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
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenu.gameObject.activeSelf && !soundOptionMenu.gameObject.activeSelf && !reallyExit.gameObject.activeSelf)
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
    /// �޴� ����
    /// </summary>
    /// <param name="_value">���θ޴� 0���� 1�ݱ� , ����޴� ����2 �ݱ�3, ��¥�� 4���� �ݱ�5</param>
    private void MenuOpen(int _value)
    {
        switch(_value)
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