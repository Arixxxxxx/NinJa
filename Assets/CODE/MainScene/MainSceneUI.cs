using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSceneUi : MonoBehaviour
{
    private Image whiteBackGround;
    private Image blackBackGround;
    private Image openingLogo;
    private Transform backGround;
    private Transform btn1, btn2;
    private SpriteRenderer mainLogo;
    private TMP_Text mainLogoText;

    [SerializeField] private float alpahColorSpeed;


    bool step1, step2;
    bool step3, step4;

    private void Awake()
    {
        whiteBackGround = transform.Find("BGw").GetComponent<Image>();
        openingLogo = whiteBackGround.transform.GetChild(0).GetComponent<Image>();

        blackBackGround = transform.Find("BGb").GetComponent<Image>();
        
        backGround = GameObject.Find("BackGround").GetComponent<Transform>();
        mainLogo = backGround.Find("MainLogo").GetComponent <SpriteRenderer>();
        mainLogoText = mainLogo.transform.GetChild(0).GetChild(0).GetComponent <TMP_Text>();


        btn1 = transform.Find("btn1").GetComponent < Transform> ();
        btn2 = transform.Find("btn2").GetComponent < Transform> ();
        MainScrrenInit();
    }

    private void Update()
    {
        OpeningLogoPopup();
        Stpe2Start();
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

    IEnumerator BtnOn()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
    }
    private void MainScrrenInit()
    {
        
        blackBackGround.gameObject.SetActive(false);

        whiteBackGround.gameObject.SetActive(true);
        openingLogo.color = new Color(1, 1, 1, 0);
        mainLogo.color = new Color(1, 1, 1, 0);
        mainLogoText.color = new Color(0, 0, 0, 0);

        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
    }

}
