using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class GameUI : MonoBehaviour
{

    Image ArrowFill;
    Transform FillBox;
    TMP_Text ArrowEA;

    Transform meleeUI, rangeUi;
    Image mapMoveBar;
    TMP_Text mapMoveText;

    // 시간바
    string ampm;
    int hour;
    int minute;
    TMP_Text timeText;


    private void Awake()
    {
        ArrowFill = transform.Find("Btn2/ArrowFill/ArrowFill").GetComponent<Image>();
        FillBox = transform.Find("Btn2/ArrowFill").GetComponent<Transform>();
        ArrowEA = transform.Find("Btn2/ArrowFill/ArrowEA").GetComponent<TMP_Text>();
        meleeUI = transform.Find("Btn1").GetComponent<Transform>();
        rangeUi = transform.Find("Btn2").GetComponent<Transform>();
        mapMoveBar = transform.Find("MapMoveBar").GetComponent<Image>();
        mapMoveText = mapMoveBar.transform.GetChild(0).GetComponent<TMP_Text>();
        timeText = transform.Find("UnitFream/TimeBar/Time").GetComponent<TMP_Text>();
        mapMoveBar.fillAmount = 0;
        mapMoveText.color = new Color(1, 1, 1, 0);
        mapMoveText.text = string.Empty;
    }

   

    private void Update()
    {
        
        ArrowEaText();  // 화살갯수 text
        ArrowFillAmount(); // 화살갯수 게이지
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

    private void WeaponeUIActive()
    {
        if (GameManager.Instance.isGetMeleeItem && !once)
        {
            once = true;
            meleeUI.gameObject.SetActive(true);
        }
        if (GameManager.Instance.isGetRangeItem && !once1)
        {
            once1 = true;
            rangeUi.gameObject.SetActive(true);
        }
    }
    private void ArrowEaText()
    {
        ArrowEA.text = GameManager.Instance.CurArrow.ToString();
    }
    private void ArrowFillAmount()
    {
        ArrowFill.fillAmount = GameManager.Instance.CurArrow / GameManager.Instance.MaxArrow;
        if (GameManager.Instance.meleeMode)
        {
            FillBox.gameObject.SetActive(false);
        }
        else
        {
            FillBox.gameObject.SetActive(true);
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

            Invoke("EffectEnd",2.5f);
        }
        else if (mapMoveText.color.a <= 0.98f)
        {
            mapMoveText.color += new Color(1, 1, 1, 0.08f);
            Invoke("TextEffect", mapMoveToolSpeed);
        }
    }

    private void EffectEnd()
    {
        if (mapMoveBar.fillAmount <= 0.05f)
        {
            mapMoveBar.fillAmount = 0;

        }
        else if (mapMoveBar.fillAmount > 0.05f)
        {
            mapMoveBar.fillAmount -= Time.deltaTime;
            Invoke("EffectEnd", mapMoveToolSpeed);
        }
    }

    public void SpawnZombie()
    {
        GameObject obj = PoolManager.Instance.F_GetObj("Enemy");
        obj.transform.position = PoolManager.Instance.SpawnPoint.position;


    }
}


