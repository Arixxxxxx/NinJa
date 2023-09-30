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

    // �ð���
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
        
        ArrowEaText();  // ȭ�찹�� text
        ArrowFillAmount(); // ȭ�찹�� ������
        WeaponeUIActive();
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetMapMoveBar("�ʿ�");

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

    // ���̵� �ؽ�Ʈ�� ����
    [SerializeField] float mapMoveToolSpeed;
    private string BoxMSG = "Test �Դϴ�.";

    /// <summary>
    /// ���̵� �ؽ�Ʈ��
    /// </summary>
    /// <param name="_Value">�ʿ�, ����, �÷���, ���۵���, ����, ����1</param>
    public void SetMapMoveBar(string _Value)
    {
        CancelInvoke();
        mapMoveBar.fillAmount = 0;
        mapMoveText.color = new Color(1, 1, 1, 0);
        mapMoveText.text = string.Empty;

        switch (_Value)
        {
            case "�ʿ�":
                BoxMSG = "������ �ʿ�";
                    break;

            case "����":
                BoxMSG = "���� �Ʒ� Zone";
                break;

            case "�÷���":
                BoxMSG = "�÷��� �Ʒ� Zone";
                break;

            case "���۵���":
                BoxMSG = "���� Cave";
                break;

            case "����":
                BoxMSG = "������ ������";
                break;

            case "����1":
                BoxMSG = "�Ʒ� Cave";
                break;

            case "����":
                BoxMSG = "���������� ����";
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


