using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointWindow : MonoBehaviour
{

    Transform skillPointWindow;
    //�����ư
    Button ExitBtn;

    //�����ִ�
    int haveAttackPoint, haveBodyPoint, haveSkillPoint;

    TMP_Text attackPointText, attackPointMelee, attackPointRange;
    TMP_Text meleeAttackLv, rangerAttackLv;

    Button meleeDownBtn, meleeUpBtn, rangerDownBtn, rangerUpBtn;

    // ��������Ʈ ����
    public int meleePointCashe, meleePoint, rangePointCashe, rangerPoint;
    int meleeAtkLv = 1;
    int rangeAtkLv = 1;

    //�ǰ� ����Ʈ ������
    Transform BodyPointBox;
    TMP_Text bodyPointText, hpPointText, mpPointText, hpLvText, mpLvText;
    Button hpUpBtn, hpDownBtn, mpUpbtn, mpDownbtn;
    int hpCashe, hpPoint, mpCashe, mpPoint, hpLv = 1, mpLv = 1;


   // ���� �ʱ�ȭ ��ư
   Button AcceptBtn, ResetBtn;

    private void Awake()
    {
        

        skillPointWindow = transform.Find("SkillPointWindow").GetComponent<Transform>();
        BodyPointBox = skillPointWindow.Find("BodyPoint/BodyPointBox").GetComponent<Transform>();
        ExitBtn = skillPointWindow.transform.Find("Bg/Bg/ExitBtn").GetComponent<Button>();
        

          //�����ִ� ����Ʈ
          attackPointText = skillPointWindow.transform.Find("NormalAtk/AtkPointBox/Point/Point(Text)").GetComponent<TMP_Text>();
        bodyPointText = BodyPointBox.transform.Find("Point/Point(Text)").GetComponent <TMP_Text>();

      //��������Ʈ ����
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

        //�ٵ�����Ʈ
        hpPointText = BodyPointBox.transform.Find("HP/Point(Text)").GetComponent<TMP_Text>();
        hpLvText = BodyPointBox.transform.Find("HP/Lv").GetComponent<TMP_Text>();
        hpUpBtn = BodyPointBox.transform.Find("HP/R").GetComponent<Button>();
        hpDownBtn = BodyPointBox.transform.Find("HP/L").GetComponent<Button>();

        mpPointText = BodyPointBox.transform.Find("MP/Point(Text)").GetComponent<TMP_Text>();
        mpLvText = BodyPointBox.transform.Find("MP/Lv").GetComponent<TMP_Text>();
        mpUpbtn = BodyPointBox.transform.Find("MP/R").GetComponent<Button>();
        mpDownbtn = BodyPointBox.transform.Find("MP/L").GetComponent<Button>();

    }

    //���� ĳ�� -> ����Ʈ -> ����
    private void Update()
    {
        windowPopup();
        TextUpdater();
        LvLimitFuntion();

    }
    private void Start()
    {
        //�������ݷ� ���� �ʱ�ȭ
        meleeUpBtn.onClick.AddListener(() =>
        {
            if (haveAttackPoint > 0)
            {
                haveAttackPoint--;
                meleePointCashe++;
            }
            else
            {
                //��������Ʈ�� �����մϴ� â
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
                //��������Ʈ�� �����մϴ� â
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


        //����, ���� ��ư�ʱ�ȭ
        AcceptBtn.onClick.AddListener(() =>
        { 
            //������ ĳ�Ÿ� ����Ʈ�� �ְ� ��ų�Ŵ����� �ݿ�

            //����Point����
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
            //����point����
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


        //���� ��ư
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
        //���� ����Ʈ��
        attackPointText.text = haveAttackPoint.ToString();
        bodyPointText.text = haveBodyPoint.ToString();

        //���� ����Ʈ
        attackPointMelee.text = meleePointCashe.ToString();
        meleeAttackLv.text = $"Lv. {meleeAtkLv.ToString("00")}";

        attackPointRange.text = rangePointCashe.ToString();
        rangerAttackLv.text = $"Lv. {rangeAtkLv.ToString("00")}";

        //�ٵ� ����Ʈ

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
