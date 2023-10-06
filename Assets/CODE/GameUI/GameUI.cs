using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class GameUI : MonoBehaviour
{




    Image mapMoveBar;
    TMP_Text mapMoveText;

    // 시간바
    string ampm;
    int hour;
    int minute;
    TMP_Text timeText;

    Transform MeleeBar;
    Transform RangeBar;

    Image normalAttackFillFont, normalAttackFillBack;
    Image sideAttackBar;
    Transform RF, RB, MF, MB;
    Transform specialIconR, specialIconM;

    private void Awake()
    {



        //지역이동 알림바
        mapMoveBar = transform.Find("MapMoveBar").GetComponent<Image>();
        mapMoveText = mapMoveBar.transform.GetChild(0).GetComponent<TMP_Text>();
        mapMoveBar.fillAmount = 0;
        mapMoveText.color = new Color(1, 1, 1, 0);
        mapMoveText.text = string.Empty;

        //시간
        timeText = transform.Find("UnitFream/TimeBar/Time").GetComponent<TMP_Text>();

        //액션바
        MeleeBar = transform.Find("ActionBar/Melee").GetComponent<Transform>();
        MeleeBar.gameObject.SetActive(false);
        RangeBar = transform.Find("ActionBar/Range").GetComponent<Transform>();
        RangeBar.gameObject.SetActive(false);

        //평타UI
        normalAttackFillFont = transform.Find("ActionBar/AttackIcon/Circle/Front").GetComponent<Image>();
        normalAttackFillBack = transform.Find("ActionBar/AttackIcon/Circle/Back").GetComponent<Image>();
        RF = normalAttackFillFont.transform.Find("R").GetComponent<Transform>();
        RB = normalAttackFillBack.transform.Find("R").GetComponent<Transform>();

        MF = normalAttackFillFont.transform.Find("M").GetComponent<Transform>();
        MB = normalAttackFillBack.transform.Find("M").GetComponent<Transform>();

        //스페셜바 아이콘
        specialIconR = transform.Find("ActionBar/SpecialSkill/Circle/Front/R").GetComponent<Transform>();
        specialIconM = transform.Find("ActionBar/SpecialSkill/Circle/Front/M").GetComponent<Transform>();

        sideAttackBar = transform.Find("ActionBar/AttackIcon/Circle/SideBar").GetComponent<Image>();
    }



    private void Update()
    {
        AttackFill();
        normalAttackIcon();
        SkillBarSwap();
        WeaponeUIActive();
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetMapMoveBar("초원");

        }
    }

    private void LateUpdate()
    {
        TimeTexT();
    }

    bool once, once1;

    private void TimeTexT()
    {
        //ampm = DateTime.Now.ToString("t");
        hour = DateTime.Now.Hour;
        ampm = hour <= 12 ? "AM" : "PM";
        hour = hour % 12;
        minute = DateTime.Now.Minute;

        timeText.text = $"{ampm} {hour}:{minute}";
    }

    private void normalAttackIcon()
    {
        if (GameManager.Instance.isGetMeleeItem || GameManager.Instance.isGetRangeItem)
        {
            if (GameManager.Instance.rangeMode && !RF.gameObject.activeSelf)
            {
                RF.gameObject.SetActive(true);
                RB.gameObject.SetActive(true);
                specialIconR.gameObject.SetActive(true);

                MF.gameObject.SetActive(false);
                MB.gameObject.SetActive(false);
                specialIconM.gameObject.SetActive(false);
            }
            else if (GameManager.Instance.meleeMode && !MF.gameObject.activeSelf)
            {
                RF.gameObject.SetActive(false);
                RB.gameObject.SetActive(false);
                specialIconR.gameObject.SetActive(false);

                MF.gameObject.SetActive(true);
                MB.gameObject.SetActive(true);
                specialIconM.gameObject.SetActive(true);
            }
        }
     
    }

    private void AttackFill()
    {
        if (GameManager.Instance.rangeMode)
        {
            normalAttackFillFont.fillAmount = arrowAttack.Instance.curTime / arrowAttack.Instance.normalShootSpeed;
            sideAttackBar.fillAmount = arrowAttack.Instance.curTime / arrowAttack.Instance.normalShootSpeed;
        }
        else if (GameManager.Instance.meleeMode)
        {
            normalAttackFillFont.fillAmount = Player.instance.Timer / Player.instance.MeleeSpeed;
            sideAttackBar.fillAmount = Player.instance.Timer / Player.instance.MeleeSpeed;
        }
    }

    private void SkillBarSwap()
    {
        if (GameManager.Instance.meleeMode)
        {
            RangeBar.gameObject.SetActive(false);
            MeleeBar.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.rangeMode)
        {
            RangeBar.gameObject.SetActive(true);
            MeleeBar.gameObject.SetActive(false);
        }
    }
    private void WeaponeUIActive()
    {
        if (GameManager.Instance.isGetMeleeItem && !once)
        {
            once = true;

            GameManager.Instance.gameUI.Find("ActionBar").gameObject.SetActive(true);
            GameManager.Instance.gameUI.Find("ActionBar").GetComponent<Animator>().SetTrigger("Open");
        }
        if (GameManager.Instance.isGetRangeItem && !once1)
        {
            once1 = true;

            GameManager.Instance.gameUI.Find("ActionBar").GetComponent<Animator>().SetTrigger("Open");
            if (!GameManager.Instance.gameUI.Find("ActionBar").gameObject.activeSelf)
            {
                GameManager.Instance.gameUI.Find("ActionBar").gameObject.SetActive(true);
            }

            if (!GameManager.Instance.isGetMeleeItem)
            {

                GameManager.Instance.rangeMode = true;
            }
        }
    }



    // 맵이동 텍스트바 연출
    [SerializeField] float mapMoveToolSpeed;
    private string BoxMSG = "Test 입니다.";

    /// <summary>
    /// 맵이동 텍스트바
    /// </summary>
    /// <param name="_Value">초원, 점프, 플랫폼, 정글동글, 마을, 던전1</param>
    public void SetMapMoveBar(string _Value)
    {
        CancelInvoke();
        mapMoveBar.fillAmount = 0;
        mapMoveBar.color = new Color(1, 1, 1, 1);
        mapMoveText.color = new Color(1, 1, 1, 0);
        mapMoveText.text = string.Empty;

        switch (_Value)
        {
            case "초원":
                BoxMSG = "시작의 초원";
                break;

            case "점프":
                BoxMSG = "점프 훈련 Zone";
                break;

            case "플랫폼":
                BoxMSG = "플랫폼 훈련 Zone";
                break;

            case "정글동굴":
                BoxMSG = "정글 Cave";
                break;

            case "마을":
                BoxMSG = "시작의 빌리지";
                break;

            case "던전1":
                BoxMSG = "훈련 Cave";
                break;

            case "요정":
                BoxMSG = "요정나무의 동굴";
                break;

        }

        Invoke("StartingEffect", mapMoveToolSpeed);

    }

    private void StartingEffect()
    {


        if (mapMoveBar.fillAmount >= 0.98f)
        {
            mapMoveText.text = BoxMSG;
            Invoke("TextEffect", 0.1f);
        }
        else if (mapMoveBar.fillAmount < 0.98f)
        {
            mapMoveBar.fillAmount += Time.deltaTime;
            Invoke("StartingEffect", mapMoveToolSpeed);
        }
    }

    private void TextEffect()
    {
        if (mapMoveText.color.a >= 0.98f)
        {
            Debug.Log("진입00");
            Invoke("EffectEnd", 2.5f);
        }
        else if (mapMoveText.color.a <= 0.98f)
        {
            mapMoveText.color += new Color(1, 1, 1, 0.08f);
            Invoke("TextEffect", mapMoveToolSpeed);
        }
    }

    private void EffectEnd()
    {

        if (mapMoveBar.color.a < 0.05f)
        {

            mapMoveText.color = new Color(1, 1, 1, 0);
            mapMoveBar.color = new Color(1, 1, 1, 0);

        }
        else if (mapMoveBar.color.a > 0.05f)
        {

            mapMoveText.color -= new Color(0, 0, 0, 0.01f);
            mapMoveBar.color -= new Color(0, 0, 0, 0.01f); ;
            Invoke("EffectEnd", mapMoveToolSpeed);
        }

        //if (mapMoveBar.fillAmount <= 0.05f)
        //{
        //    mapMoveBar.fillAmount = 0;

        //}
        //else if (mapMoveBar.fillAmount > 0.05f)
        //{
        //    mapMoveBar.fillAmount -= Time.deltaTime;
        //    Invoke("EffectEnd", mapMoveToolSpeed);
        //}
    }

    public void SpawnZombie()
    {
        GameObject obj = PoolManager.Instance.F_GetObj("Enemy");
        obj.transform.position = PoolManager.Instance.SpawnPoint.position;


    }
}


