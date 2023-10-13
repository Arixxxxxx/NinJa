using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointWindow : MonoBehaviour
{

    Transform skillPointWindow;
    //종료버튼
    Button ExitBtn;

    //가져있는
    int haveAttackPoint, haveBodyPoint, haveSkillPoint;

    TMP_Text attackPointText, attackPointMelee, attackPointRange;
    TMP_Text meleeAttackLv, rangerAttackLv;

    Button meleeDownBtn, meleeUpBtn, rangerDownBtn, rangerUpBtn;

    // 근접포인트 변수
    public int meleePointCashe, meleePoint, rangePointCashe, rangerPoint;
    int meleeAtkLv = 1;
    int rangeAtkLv = 1;

    //건강 포인트 변수들
    Transform BodyPointBox;
    TMP_Text bodyPointText, hpPointText, mpPointText, hpLvText, mpLvText;
    Button hpUpBtn, hpDownBtn, mpUpbtn, mpDownbtn;
    int hpCashe, hpPoint, mpCashe, mpPoint, hpLv = 1, mpLv = 1;


   // 수락 초기화 버튼
   Button AcceptBtn, ResetBtn;

    private void Awake()
    {
        

        skillPointWindow = transform.Find("SkillPointWindow").GetComponent<Transform>();
        BodyPointBox = skillPointWindow.Find("BodyPoint/BodyPointBox").GetComponent<Transform>();
        ExitBtn = skillPointWindow.transform.Find("Bg/Bg/ExitBtn").GetComponent<Button>();
        

          //가져있는 포인트
          attackPointText = skillPointWindow.transform.Find("NormalAtk/AtkPointBox/Point/Point(Text)").GetComponent<TMP_Text>();
        bodyPointText = BodyPointBox.transform.Find("Point/Point(Text)").GetComponent <TMP_Text>();

      //공격포인트 투자
        attackPointMelee = skillPointWindow.transform.Find("NormalAtk/AtkPointBox/PointBox1/Point(Text)").GetComponent<TMP_Text>();
        meleeDownBtn = attackPointMelee.transform.parent.Find("L").GetComponent<Button>();
        meleeUpBtn = attackPointMelee.transform.parent.Find("R").GetComponent<Button>();
        meleeAttackLv = attackPointMelee.transform.parent.Find("Lv").GetComponent<TMP_Text>();

        attackPointRange = skillPointWindow.transform.Find("NormalAtk/AtkPointBox/PointBox2/Point(Text)").GetComponent<TMP_Text>();
        rangerDownBtn = attackPointRange.transform.parent.Find("L").GetComponent<Button>();
        rangerUpBtn = attackPointRange.transform.parent.Find("R").GetComponent<Button>();
        rangerAttackLv = attackPointRange.transform.parent.Find("Lv").GetComponent<TMP_Text>();
        AcceptBtn = skillPointWindow.transform.Find("acceptBtn").GetComponent<Button>();
        ResetBtn = skillPointWindow.transform.Find("Reset").GetComponent<Button>();

        //바디포인트
        hpPointText = BodyPointBox.transform.Find("HP/Point(Text)").GetComponent<TMP_Text>();
        hpLvText = BodyPointBox.transform.Find("HP/Lv").GetComponent<TMP_Text>();
        hpUpBtn = BodyPointBox.transform.Find("HP/R").GetComponent<Button>();
        hpDownBtn = BodyPointBox.transform.Find("HP/L").GetComponent<Button>();

        mpPointText = BodyPointBox.transform.Find("MP/Point(Text)").GetComponent<TMP_Text>();
        mpLvText = BodyPointBox.transform.Find("MP/Lv").GetComponent<TMP_Text>();
        mpUpbtn = BodyPointBox.transform.Find("MP/R").GetComponent<Button>();
        mpDownbtn = BodyPointBox.transform.Find("MP/L").GetComponent<Button>();

    }

    //구조 캐시 -> 포인트 -> 전송
    private void Update()
    {
        windowPopup();
        TextUpdater();
        LvLimitFuntion();

    }
    private void Start()
    {
        //근접공격력 관련 초기화
        meleeUpBtn.onClick.AddListener(() =>
        {
            if (haveAttackPoint > 0)
            {
                haveAttackPoint--;
                meleePointCashe++;
            }
            else
            {
                //스탯포인트가 부족합니다 창
            }
        });
        meleeDownBtn.onClick.AddListener(() => {
        
            if(meleePointCashe > 0)
            {
                meleePointCashe--;
                haveAttackPoint++;
            }
        });

        rangerUpBtn.onClick.AddListener(() =>
        {
            if (haveAttackPoint > 0)
            {
                haveAttackPoint--;
                rangePointCashe++;
            }
            else
            {
                //스탯포인트가 부족합니다 창
            }
        });
        rangerDownBtn.onClick.AddListener(() => {

            if (rangePointCashe > 0)
            {
                rangePointCashe--;
                haveAttackPoint++;
            }
        });

        hpUpBtn.onClick.AddListener(() => {
            if(haveBodyPoint > 0)
            {
                haveBodyPoint--;
                hpCashe++;
            }
        });
        hpDownBtn.onClick.AddListener(() =>
        {
            if (hpCashe > 0)
            {
                hpCashe--;
                haveBodyPoint++;
            }
        });

        mpUpbtn.onClick.AddListener(() =>
        {
            if (haveBodyPoint > 0)
            {
                haveBodyPoint--;
                mpCashe++;
            }
        });

        mpDownbtn.onClick.AddListener(() =>
        {
            if (mpCashe > 0)
            {
                mpCashe--;
                haveBodyPoint++;
            }
        });


        //결정, 리셋 버튼초기화
        AcceptBtn.onClick.AddListener(() =>
        { 
            //각각의 캐셔를 포인트로 넣고 스킬매니저에 반영

            //공격Point관련
            if (meleePointCashe != 0)
            {
                meleePoint += meleePointCashe;
                meleePointCashe = 0;
                meleeAtkLv = (1 + meleePoint);
                SkillManager.instance.F_SetLevupPointAdd("Attack", 0, meleePoint);
            }
            if (rangePointCashe != 0)
            {
                rangerPoint += rangePointCashe;
                rangePointCashe = 0;
                rangeAtkLv = (1 + rangerPoint);
                SkillManager.instance.F_SetLevupPointAdd("Attack", 1, rangerPoint);
            }
            if(hpCashe != 0)
            {
                hpPoint += hpCashe;
                hpCashe = 0;
                hpLv = (1 + hpPoint);
                SkillManager.instance.F_SetLevupPointAdd("Body", 0, hpPoint);
            }
            if(mpCashe != 0)
            {
                mpPoint += mpCashe;
                mpCashe = 0;
                mpLv = (1 + mpPoint);
                SkillManager.instance.F_SetLevupPointAdd("Body", 1, mpPoint);
            }

        });

        ResetBtn.onClick.AddListener(() =>
        {
            //공격point관련
            if(meleePoint > 0)
            {
                int temp = meleePoint;
                meleeAtkLv = (meleeAtkLv - meleePoint);
                meleePoint = 0;
                haveAttackPoint += temp;
                SkillManager.instance.F_SetLevupPointAdd("Attack", 0, meleePoint);
            }

            if(rangerPoint != 0)
            {
                int temp = rangerPoint;
                rangeAtkLv = (rangeAtkLv - rangerPoint);
                rangerPoint = 0;
                haveAttackPoint += temp;
                SkillManager.instance.F_SetLevupPointAdd("Attack", 1, rangerPoint);
            }

            if(hpPoint != 0)
            {
                int temp = hpPoint;
                hpLv = (hpLv - hpPoint);
                hpPoint = 0;
                haveBodyPoint += temp;
                SkillManager.instance.F_SetLevupPointAdd("Body", 0, hpPoint);
            }
           
            if(mpPoint != 0)
            {
                int temp = mpPoint;
                mpLv = (mpLv - mpPoint);
                mpPoint = 0;
                haveBodyPoint += temp;
                SkillManager.instance.F_SetLevupPointAdd("Body", 1, mpPoint);
            }
        });


        //종료 버튼
        ExitBtn.onClick.AddListener(() =>
        {
            if (skillPointWindow.gameObject.activeSelf)
            {
                skillPointWindow.gameObject.SetActive(false);
            }
        });
    }


    private void LvLimitFuntion()
    {
        if (meleeAtkLv < 1)
        {
            meleeAtkLv = 1;
        }
        if (rangeAtkLv < 1)
        {
            rangeAtkLv = 1;
        }

        if(hpLv < 1)
        {
            hpLv = 1;
        }
        if(mpLv < 1)
        {
            mpLv= 1;
        }
    }

    private void TextUpdater()
    {
        //렙업 포인트들
        attackPointText.text = haveAttackPoint.ToString();
        bodyPointText.text = haveBodyPoint.ToString();

        //공격 포인트
        attackPointMelee.text = meleePointCashe.ToString();
        meleeAttackLv.text = $"Lv. {meleeAtkLv.ToString("00")}";

        attackPointRange.text = rangePointCashe.ToString();
        rangerAttackLv.text = $"Lv. {rangeAtkLv.ToString("00")}";

        //바디 포인트

        hpPointText.text = hpCashe.ToString();
        hpLvText.text = $"Lv. {hpLv.ToString("00")}";

        mpPointText.text = mpCashe.ToString();
        mpLvText.text = $"Lv. {mpLv.ToString("00")}";


    }
    private void windowPopup()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!skillPointWindow.gameObject.activeSelf)
            {
                skillPointWindow.gameObject.SetActive(true);
            }
            else
            {
                skillPointWindow.gameObject.SetActive(false);
            }
        }
    }

    public void F_GetStatsPoint(int _Value)
    {
        haveAttackPoint += _Value;
        haveBodyPoint += _Value;
        haveSkillPoint += _Value;
    }


}
