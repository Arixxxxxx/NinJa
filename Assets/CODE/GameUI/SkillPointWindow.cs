using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointWindow : MonoBehaviour
{
    [SerializeField] GameObject SkillTreeBtn;
    [SerializeField] TMP_Text BtnText;

    Transform skillPointWindow;
    //종료버튼
    Button ExitBtn;

    //가져있는
    int haveAttackPoint, haveBodyPoint, haveSkillPoint;

    TMP_Text attackPointText, attackPointMelee, attackPointRange;
    TMP_Text meleeAttackLv, rangerAttackLv;

    Button meleeDownBtn, meleeUpBtn, rangerDownBtn, rangerUpBtn;

    // 근접포인트 변수
     int meleePointCashe, meleePoint, rangePointCashe, rangerPoint;
    int meleeAtkLv = 1;
    int rangeAtkLv = 1;

    //건강 포인트 변수들
    Transform BodyPointBox;
    TMP_Text bodyPointText, hpPointText, mpPointText, hpLvText, mpLvText;
    Button hpUpBtn, hpDownBtn, mpUpbtn, mpDownbtn;
    int hpCashe, hpPoint, mpCashe, mpPoint, hpLv = 1, mpLv = 1;

    //스킬 트리 변수들
    Transform M, R;
    TMP_Text haveSkillPointText;
    Button skillMelee1, skillMelee11, skillMelee12, skillMelee2;
    Button skillRange1, skillRange11, skillRange12, skillRange2;
    TMP_Text[] M_text;
    TMP_Text[] R_text;
    bool isM2treeOK, isR2treeOK, isM3treeOK, isR3treeOK;

    public int M1Cashe_int, M11Cashe_int, M12Cashe_int, M2Cashe_int;

    int R1Cashe_int, R11Cashe_int, R12Cashe_int, R2Cashe_int;

    public int M1Point, M11Point, M12Point, M2Point;
    int M1MaxPoint = 3, M11MaxPoint = 3, M12MaxPoint = 3, M2MaxPoint = 1;

    int R1Point, R11Point, R12Point, R2Point;  
    int R1MaxPoint = 3, R11MaxPoint = 3, R12MaxPoint = 3, R2MaxPoint = 1;  

   // 수락 초기화 버튼
   Button AcceptBtn, ResetBtn, TreeResetBtn;

    Transform CheakPoint;

    private void Awake()
    {

        CheakPoint = transform.Find("Btn/CeahkPoint").GetComponent<Transform>();
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
        TreeResetBtn = skillPointWindow.transform.Find("SkillTreeReset").GetComponent<Button>();

        //바디포인트
        hpPointText = BodyPointBox.transform.Find("HP/Point(Text)").GetComponent<TMP_Text>();
        hpLvText = BodyPointBox.transform.Find("HP/Lv").GetComponent<TMP_Text>();
        hpUpBtn = BodyPointBox.transform.Find("HP/R").GetComponent<Button>();
        hpDownBtn = BodyPointBox.transform.Find("HP/L").GetComponent<Button>();

        mpPointText = BodyPointBox.transform.Find("MP/Point(Text)").GetComponent<TMP_Text>();
        mpLvText = BodyPointBox.transform.Find("MP/Lv").GetComponent<TMP_Text>();
        mpUpbtn = BodyPointBox.transform.Find("MP/R").GetComponent<Button>();
        mpDownbtn = BodyPointBox.transform.Find("MP/L").GetComponent<Button>();

        //스킬트리 변수초기화

        M = transform.Find("SkillPointWindow/SkillTree/M").GetComponent<Transform>();
        R = transform.Find("SkillPointWindow/SkillTree/R").GetComponent<Transform>();
        haveSkillPointText = transform.Find("SkillPointWindow/SkillTree/PB/T").GetComponent <TMP_Text>();

        skillMelee1 = M.transform.Find("1/btn").GetComponent<Button>();
        skillMelee11 = M.transform.Find("1-1/btn").GetComponent<Button>();
        skillMelee12 = M.transform.Find("1-2/btn").GetComponent<Button>();
        skillMelee2 = M.transform.Find("2/btn").GetComponent<Button>();

        skillRange1 = R.transform.Find("1/btn").GetComponent<Button>();
        skillRange11 = R.transform.Find("1-1/btn").GetComponent<Button>();
        skillRange12 = R.transform.Find("1-2/btn").GetComponent<Button>();
        skillRange2 = R.transform.Find("2/btn").GetComponent<Button>();

        M_text = M.transform.Find("LvText").GetComponentsInChildren<TMP_Text>();
        R_text = R.transform.Find("LvText").GetComponentsInChildren<TMP_Text>();
    }

    //구조 캐시 -> 포인트 -> 전송
  
    private void Start()
    {
        skillMelee1.onClick.AddListener(() =>
        {
            if (haveSkillPoint > 0)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
                haveSkillPoint--;
                M1Point++;
                SkillManager.instance.F_SkillTreeSysTem("M", 1, M1Point);

                if (haveSkillPoint > 0 && M1Point == M1MaxPoint)
                {
                    skillMelee1.transform.parent.gameObject.SetActive(false);
                }
                if(haveSkillPoint > 0 && M1Point > 0)
                {
                    if(M11Point != M11MaxPoint)
                    {
                        skillMelee11.transform.parent.gameObject.SetActive(true);
                    }
                    
                    if(M12Point != M12MaxPoint)
                    {
                        skillMelee12.transform.parent.gameObject.SetActive(true);
                    }
                  
                }
                else if(haveSkillPoint == 0)
                {
                    skillMelee1.transform.parent.gameObject.SetActive(false);
                    skillMelee11.transform.parent.gameObject.SetActive(false);
                    skillMelee12.transform.parent.gameObject.SetActive(false);

                    skillRange1.transform.parent.gameObject.SetActive(false);
                    skillRange11.transform.parent.gameObject.SetActive(false);
                    skillRange12.transform.parent.gameObject.SetActive(false);
                }
            }
        });
        skillMelee11.onClick.AddListener(() =>
        {
            if (haveSkillPoint > 0)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
                haveSkillPoint--;
                M11Point++;
                SkillManager.instance.F_SkillTreeSysTem("M", 11, M11Point);
                if ( haveSkillPoint > 0 && M11Point == M11MaxPoint)
                {
                    skillMelee11.transform .parent.gameObject.SetActive(false);
                }
                if (haveSkillPoint > 0 && M11Point == M11MaxPoint && M2Point < 1)
                {
                    skillMelee2.transform.parent.gameObject.SetActive(true);
                }
                
                else if (haveSkillPoint == 0)
                {
                    skillMelee1.transform.parent.gameObject.SetActive(false);
                    skillMelee11.transform.parent.gameObject.SetActive(false);
                    skillMelee12.transform.parent.gameObject.SetActive(false);
                    skillMelee2.transform.parent.gameObject.SetActive(false);

                    skillRange1.transform.parent.gameObject.SetActive(false);
                    skillRange11.transform.parent.gameObject.SetActive(false);
                    skillRange12.transform.parent.gameObject.SetActive(false);
                    skillRange2.transform.parent.gameObject.SetActive(false);
                }
            }
        });

        skillMelee12.onClick.AddListener(() =>
        {
            if (haveSkillPoint > 0)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
                haveSkillPoint--;
                M12Point++;
                SkillManager.instance.F_SkillTreeSysTem("M", 12, M12Point);
                if (haveSkillPoint > 0 && M12Point == M12MaxPoint)
                {
                    skillMelee12.transform.parent.gameObject.SetActive(false);
                }
                if (haveSkillPoint > 0 && M12Point == M12MaxPoint && M2Point < 1) 
                {
                    skillMelee2.transform.parent.gameObject.SetActive(true);
                }
                else if (haveSkillPoint == 0)
                {
                    skillMelee1.transform.parent.gameObject.SetActive(false);
                    skillMelee11.transform.parent.gameObject.SetActive(false);
                    skillMelee12.transform.parent.gameObject.SetActive(false);
                    skillMelee2.transform.parent.gameObject.SetActive(false);

                    skillRange1.transform.parent.gameObject.SetActive(false);
                    skillRange11.transform.parent.gameObject.SetActive(false);
                    skillRange12.transform.parent.gameObject.SetActive(false);
                    skillRange2.transform.parent.gameObject.SetActive(false);
                }
            }
        });

        skillMelee2.onClick.AddListener(() =>
        {
            if (haveSkillPoint > 0)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
                haveSkillPoint--;
                M2Point++;
                SkillManager.instance.F_SkillTreeSysTem("M", 2, M2Point);
                if (M2Point == 1)
                {
                    skillMelee2.transform.parent.gameObject.SetActive(false);
                }
                if (haveSkillPoint == 0)
                {
                    skillMelee1.transform.parent.gameObject.SetActive(false);
                    skillMelee11.transform.parent.gameObject.SetActive(false);
                    skillMelee12.transform.parent.gameObject.SetActive(false);
                    skillMelee2.transform.parent.gameObject.SetActive(false);

                    skillRange1.transform.parent.gameObject.SetActive(false);
                    skillRange11.transform.parent.gameObject.SetActive(false);
                    skillRange12.transform.parent.gameObject.SetActive(false);
                    skillRange2.transform.parent.gameObject.SetActive(false);
                }
            }
        });

        skillRange1.onClick.AddListener(() =>
        {
            if (haveSkillPoint > 0)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
                haveSkillPoint--;
                R1Point++;
                SkillManager.instance.F_SkillTreeSysTem("R", 1, R1Point);
                if (haveSkillPoint > 0 && R1Point == R1MaxPoint)
                {
                    skillRange1.transform.parent.gameObject.SetActive(false);
                }
                if (haveSkillPoint > 0 && R1Point > 0)
                {
                    if(R11Point != R11MaxPoint)
                    {
                        skillRange11.transform.parent.gameObject.SetActive(true);
                    }
                    if(R12Point != R12MaxPoint)
                    {
                        skillRange12.transform.parent.gameObject.SetActive(true);
                    }
                   
                }
                else if (haveSkillPoint == 0)
                {
                    skillMelee1.transform.parent.gameObject.SetActive(false);
                    skillMelee11.transform.parent.gameObject.SetActive(false);
                    skillMelee12.transform.parent.gameObject.SetActive(false);

                    skillRange1.transform.parent.gameObject.SetActive(false);
                    skillRange11.transform.parent.gameObject.SetActive(false);
                    skillRange12.transform.parent.gameObject.SetActive(false);
                }
            }
        });
        skillRange11.onClick.AddListener(() =>
        {
            if (haveSkillPoint > 0)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
                haveSkillPoint--;
                R11Point++;
                SkillManager.instance.F_SkillTreeSysTem("R", 11, R11Point);
                if (haveSkillPoint > 0 && R11Point == R11MaxPoint)
                {
                    skillRange11.transform.parent.gameObject.SetActive(false);
                }
                if (haveSkillPoint > 0 && R11Point == R11MaxPoint && R2Point < 1)
                {
                    skillRange2.transform.parent.gameObject.SetActive(true);
                }

                else if (haveSkillPoint == 0)
                {
                    skillMelee1.transform.parent.gameObject.SetActive(false);
                    skillMelee11.transform.parent.gameObject.SetActive(false);
                    skillMelee12.transform.parent.gameObject.SetActive(false);
                    skillMelee2.transform.parent.gameObject.SetActive(false);

                    skillRange1.transform.parent.gameObject.SetActive(false);
                    skillRange11.transform.parent.gameObject.SetActive(false);
                    skillRange12.transform.parent.gameObject.SetActive(false);
                    skillRange2.transform.parent.gameObject.SetActive(false);
                }
            }
        });

        skillRange12.onClick.AddListener(() =>
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
            if (haveSkillPoint > 0)
            {
                haveSkillPoint--;
                R12Point++;
                SkillManager.instance.F_SkillTreeSysTem("R", 12, R12Point);
                if (haveSkillPoint > 0 && R12Point == R12MaxPoint)
                {
                    skillRange12.transform.parent.gameObject.SetActive(false);
                }
                if (haveSkillPoint > 0 && R12Point == R12MaxPoint && R2Point < 1)
                {
                    skillRange2.transform.parent.gameObject.SetActive(true);
                }
                else if (haveSkillPoint == 0)
                {
                    skillMelee1.transform.parent.gameObject.SetActive(false);
                    skillMelee11.transform.parent.gameObject.SetActive(false);
                    skillMelee12.transform.parent.gameObject.SetActive(false);
                    skillMelee2.transform.parent.gameObject.SetActive(false);

                    skillRange1.transform.parent.gameObject.SetActive(false);
                    skillRange11.transform.parent.gameObject.SetActive(false);
                    skillRange12.transform.parent.gameObject.SetActive(false);
                    skillRange2.transform.parent.gameObject.SetActive(false);
                }
            }
        });

        skillRange2.onClick.AddListener(() =>
        {
            if (haveSkillPoint > 0)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
                haveSkillPoint--;
                R2Point++;
                SkillManager.instance.F_SkillTreeSysTem("R", 2, R2Point);
                if (R2Point == R2MaxPoint)
                {
                    skillRange2.transform.parent.gameObject.SetActive(false);
                }
                if (haveSkillPoint == 0)
                {
                    skillMelee1.transform.parent.gameObject.SetActive(false);
                    skillMelee11.transform.parent.gameObject.SetActive(false);
                    skillMelee12.transform.parent.gameObject.SetActive(false);
                    skillMelee2.transform.parent.gameObject.SetActive(false);

                    skillRange1.transform.parent.gameObject.SetActive(false);
                    skillRange11.transform.parent.gameObject.SetActive(false);
                    skillRange12.transform.parent.gameObject.SetActive(false);
                    skillRange2.transform.parent.gameObject.SetActive(false);
                }
            }
        });

        //근접공격력 관련 초기화
        meleeUpBtn.onClick.AddListener(() =>
        {
            if (haveAttackPoint > 0)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
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
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
                meleePointCashe--;
                haveAttackPoint++;
            }
        });

        rangerUpBtn.onClick.AddListener(() =>
        {
            if (haveAttackPoint > 0)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
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
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
                rangePointCashe--;
                haveAttackPoint++;
            }
        });

        hpUpBtn.onClick.AddListener(() => {
            if(haveBodyPoint > 0)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
                haveBodyPoint--;
                hpCashe++;
            }
        });
        hpDownBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
            if (hpCashe > 0)
            {
                hpCashe--;
                haveBodyPoint++;
            }
        });

        mpUpbtn.onClick.AddListener(() =>
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
            if (haveBodyPoint > 0)
            {
                haveBodyPoint--;
                mpCashe++;
            }
        });

        mpDownbtn.onClick.AddListener(() =>
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
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
            SoundManager.instance.F_SoundPlay(SoundManager.instance.BtnClick, 0.8f);
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
        TreeResetBtn.onClick.AddListener(() => { SkillTreeReset(); });

        //종료 버튼
        ExitBtn.onClick.AddListener(() =>
        {
            if (skillPointWindow.gameObject.activeSelf)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.skillWindowClose, 0.8f);
                skillPointWindow.gameObject.SetActive(false);
            }
        });
    }

    private void SkillTreeReset()
    {
        int MSum = 0;
        int RSum = 0;
        int Total = 0;

        MSum = M1Point + M11Point + M12Point + M2Point;
        RSum = R1Point + R11Point + R12Point + R2Point;
        
        M1Point = 0;
        M11Point = 0;
        M12Point = 0;
        M2Point = 0;
        R1Point = 0;
        R11Point = 0;
        R12Point = 0;
        R2Point = 0;

        SkillManager.instance.F_SkillTreeSysTem("M", 1, M1Point);
        SkillManager.instance.F_SkillTreeSysTem("M", 11, M11Point);
        SkillManager.instance.F_SkillTreeSysTem("M", 12, M12Point);
        SkillManager.instance.F_SkillTreeSysTem("M", 2, M2Point);

        SkillManager.instance.F_SkillTreeSysTem("R", 1, R1Point);
        SkillManager.instance.F_SkillTreeSysTem("R", 11, R11Point);
        SkillManager.instance.F_SkillTreeSysTem("R", 12, R12Point);
        SkillManager.instance.F_SkillTreeSysTem("R", 2, R2Point);

        Total = MSum + RSum;
        haveSkillPoint += Total;

        if (Total > 0)
        {
            skillMelee1.transform.parent.gameObject.SetActive(true);
            skillMelee11.transform.parent.gameObject.SetActive(false);
            skillMelee12.transform.parent.gameObject.SetActive(false);
            skillMelee2.transform.parent.gameObject.SetActive(false);

            skillRange1.transform.parent.gameObject.SetActive(true);
            skillRange11.transform.parent.gameObject.SetActive(false);
            skillRange12.transform.parent.gameObject.SetActive(false);
            skillRange2.transform.parent.gameObject.SetActive(false);
        }
        else if(Total == 0 && haveSkillPoint ==0)
        {
            skillMelee1.transform.parent.gameObject.SetActive(false);
            skillMelee11.transform.parent.gameObject.SetActive(false);
            skillMelee12.transform.parent.gameObject.SetActive(false);
            skillMelee2.transform.parent.gameObject.SetActive(false);

            skillRange1.transform.parent.gameObject.SetActive(false);
            skillRange11.transform.parent.gameObject.SetActive(false);
            skillRange12.transform.parent.gameObject.SetActive(false);
            skillRange2.transform.parent.gameObject.SetActive(false);
        }
     
        
            
        
        
    }
    
    private void Update()
    {
        WindowPopup();
        TextUpdater();
        LvLimitFuntion();
        SkillTreePointTextUpdater();
        CheakingWindow();
        PleaseSkillPointUse();
    }

    int sum;
    private void PleaseSkillPointUse()
    {
        sum = haveAttackPoint + haveBodyPoint+ haveSkillPoint;
        if(sum >0)
        {
            CheakPoint.gameObject.SetActive(true);
            SkillTreeBtn.gameObject.SetActive(true);
            BtnText.text = sum.ToString();
        }
        else if(sum == 0)
        {
            CheakPoint.gameObject.SetActive(false);
            SkillTreeBtn.gameObject.SetActive(false);

        }
    }
    private void CheakingWindow()
    {
        if (!skillPointWindow.gameObject.activeSelf)
        {
            GameManager.Instance.SkillWindowPopup = false;
        }
        else if(skillPointWindow.gameObject.activeSelf)
        {
            GameManager.Instance.SkillWindowPopup = true;
        }
       
    }
    public void F_SetActiveSkillTree()
    {
        if (haveSkillPoint > 0)
        {
            if(M1Point == 3)
            {
                skillMelee1.transform.parent.gameObject.SetActive(false);
            }
            else if(M1Point < 3)
            {
                skillMelee1.transform.parent.gameObject.SetActive(true);
            }
            
            if(R1Point == 3)
            {
                skillRange1.transform.parent.gameObject.SetActive(false);
            }
            else if(R1Point < 3)
            {
                skillRange1.transform.parent.gameObject.SetActive(true);
            }
           

            if(M1Point > 0)
            {
                if(M11Point != 3)
                {
                    skillMelee11.transform.parent.gameObject.SetActive(true);
                }
                if(M12Point != 3)
                {
                    skillMelee12.transform.parent.gameObject.SetActive(true);
                }
            }
            if(R1Point > 0)
            {
                if(R11Point != 3)
                {
                    skillRange11.transform.parent.gameObject.SetActive(true);
                }
               
                if(R12Point != 3)
                {
                    skillRange12.transform.parent.gameObject.SetActive(true);
                }
            }

            if((M11Point == 3) ||(M12Point == 3))
            {
                if(M2Point != 1)
                {
                    skillMelee2.transform.parent.gameObject.SetActive(true);
                }
                
            }

            if ((R11Point == 3) || (R12Point == 3))
            {
                if(R2Point != 1)
                {
                    skillRange2.transform.parent.gameObject.SetActive(true);
                }
               
            }
        }
    }
    private void SkillTreePointTextUpdater()
    {
        M_text[0].text = $"[{M1Point}/{M1MaxPoint}]";
        M_text[1].text = $"[{M11Point}/{M11MaxPoint}]";
        M_text[2].text = $"[{M12Point}/{M12MaxPoint}]";
        M_text[3].text = $"[{M2Point}/{M2MaxPoint}]";

        R_text[0].text = $"[{R1Point}/{R1MaxPoint}]";
        R_text[1].text = $"[{R11Point}/{R11MaxPoint}]";
        R_text[2].text = $"[{R12Point}/{R12MaxPoint}]";
        R_text[3].text = $"[{R2Point}/{R2MaxPoint}]";
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
        haveSkillPointText.text = haveSkillPoint.ToString();
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
    private void WindowPopup()
    {
      
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!skillPointWindow.gameObject.activeSelf)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.skillWindowOpen, 0.5f);
                skillPointWindow.gameObject.SetActive(true);
            }
            else
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.skillWindowClose, 0.5f);
                skillPointWindow.gameObject.SetActive(false);
            }
        }
    }

    public void F_SkillTreeWindowPopUp()
    {
        if (!skillPointWindow.gameObject.activeSelf)
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.skillWindowOpen, 0.5f);
            skillPointWindow.gameObject.SetActive(true);
        }
        else
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.skillWindowClose, 0.5f);
            skillPointWindow.gameObject.SetActive(false);
        }
    }
  
    public void F_GetStatsPoint(int _Value)
    {
        haveAttackPoint += _Value;
        haveBodyPoint += _Value;
         haveSkillPoint += _Value;
    }
}
