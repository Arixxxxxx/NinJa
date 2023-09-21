using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MainSceneUi : MonoBehaviour
{
    private Image whiteBackGround;
    private Image blackBackGround;
    private Image openingLogo;
    private Transform backGround;
    private Image btn1, btn2;
    private SpriteRenderer mainLogo;
    private TMP_Text mainLogoText;

    [SerializeField] private float alpahColorSpeed;


    bool step1, step2;
    bool step3, step4;
    TMP_Text btn1_T, btn2_T;

    private void Awake()
    {
        whiteBackGround = transform.Find("BGw").GetComponent<Image>();
        openingLogo = whiteBackGround.transform.GetChild(0).GetComponent<Image>();

        blackBackGround = transform.Find("BGb").GetComponent<Image>();
        
        backGround = GameObject.Find("BackGround").GetComponent<Transform>();
        mainLogo = backGround.Find("MainLogo").GetComponent <SpriteRenderer>();
        mainLogoText = mainLogo.transform.GetChild(0).GetChild(0).GetComponent <TMP_Text>();


        btn1 = transform.Find("btn1").GetComponent <Image> ();
        btn1_T = btn1.transform.GetChild(0).GetComponent<TMP_Text> ();
        
        btn2 = transform.Find("btn2").GetComponent <Image> ();
        btn2_T = btn2.transform.GetChild(0).GetComponent<TMP_Text>();


        MainScrrenInit();
    }

    private void Update()
    {
        OpeningLogoPopup();
        Stpe2Start();
        Step6Start();
        BlackScreenOn();
    }

    bool once;
    private void OpeningLogoPopup()
    {
        if (!whiteBackGround.gameObject.activeSelf)
        {
            return;
        }

        if (!step2)
        {
            if (openingLogo.color.a > 0.9f && !once)
            {
                once = true;
                StartCoroutine(Step1End());
            }
            else if (!step1)
            {
                openingLogo.color += new Color(1, 1, 1, 0.2f) * alpahColorSpeed * Time.deltaTime;
            }
            if (step1)
            {
                openingLogo.color -= new Color(1, 1, 1, 0.2f) * alpahColorSpeed * Time.deltaTime;
                if (openingLogo.color.a < 0.1f)
                {
                    whiteBackGround.color -= new Color(1, 1, 1, 0.1f) * alpahColorSpeed * Time.deltaTime;

                    if (whiteBackGround.color.a < 0.1f)
                    {
                        
                        step2 = true;

                    }
                }
            }
        }
        else if(step2)
        {
            step3= true;
            whiteBackGround.gameObject.SetActive(false);
            openingLogo.gameObject.SetActive(false);
        }

    }

    IEnumerator Step1End()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        step1 = true;
        
    }
    bool step5;
    private void Stpe2Start()
    {
        if (!step5)
        {
           
            if (step3 && !step4)
            {
               
                mainLogo.color += new Color(1, 1, 1, 0.2f) * alpahColorSpeed * Time.deltaTime;
                mainLogoText.color += new Color(0, 0, 0, 0.2f) * alpahColorSpeed * Time.deltaTime;

                if (mainLogo.color.a >= 0.98f)
                {
                    step4 = true;
                }
            }
            if (step4)  
            {
                step5= true;
                StartCoroutine(BtnOn());
            }
        }
        
    }

    bool step6;
    IEnumerator BtnOn()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        step6 = true;
    }

    private void Step6Start()
    {
        if (step6)
        {
            btn1.color += new Color(1, 1, 1, 0.2f) * alpahColorSpeed * Time.deltaTime;
            btn2.color += new Color(1, 1, 1, 0.2f) * alpahColorSpeed * Time.deltaTime;
            btn1_T.color += new Color(1, 1, 1, 0.2f) * alpahColorSpeed * Time.deltaTime;
            btn2_T.color += new Color(1, 1, 1, 0.2f) * alpahColorSpeed * Time.deltaTime;
            if(btn1.color.a > 0.95f)
            {
                step6 = false;
            }
        }
    }
    private void MainScrrenInit()
    {
        
        blackBackGround.gameObject.SetActive(false);

        whiteBackGround.gameObject.SetActive(true);
        openingLogo.color = new Color(1, 1, 1, 0);
        mainLogo.color = new Color(1, 1, 1, 0);
        mainLogoText.color = new Color(0, 0, 0, 0);

        //btn1.gameObject.SetActive(false);
        //btn2.gameObject.SetActive(false);

        btn1.color = new Color(1, 1, 1, 0);
        btn2.color = new Color(1, 1, 1, 0);
        btn1_T.color = new Color(1, 1, 1, 0);
        btn2_T.color = new Color(1, 1, 1, 0);
    }

    public void MouseOnEnterBtn()
    {
        switch (gameObject.name)
        {
            case "btn1T":
                btn1_T.color = new Color(0, 0, 0, 1);

                break;

            case "btn2T":
                btn2_T.color = new Color(0, 0, 0, 1);
                break;

        }
    }

    public void MouseOnExitBtn()
    {
        switch (gameObject.name)
        {
            case "btn1T":
                btn1_T.color = new Color(1, 1, 1, 1);

                break;

            case "btn2T":
                btn2_T.color = new Color(1, 1, 1, 1);
                break;

        }
    }


    // ���콺Ŭ����
    bool step7, step8;
    public void NexrScene()
    {
        blackBackGround.gameObject.SetActive(true);
        step7 = true;
    }
    private void BlackScreenOn()
    {
        if (step7)
        {
            blackBackGround.color += new Color(0, 0, 0, 0.2f) * alpahColorSpeed * Time.deltaTime;

            if(blackBackGround.color.a > 0.98f)
            {
                StartCoroutine(FinalCutton());
            }
        }

        if (step8)
        {
            step8 = false;
            SceneManager.LoadScene("Chapter1");
        }
        
    }

    IEnumerator FinalCutton()
    {
        yield return new WaitForSecondsRealtime(1);
        step7 = false;
        step8 = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}