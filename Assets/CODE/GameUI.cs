using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameUI : MonoBehaviour
{
    
    Image ArrowFill;
    Transform FillBox;
    TMP_Text ArrowEA;

    Transform meleeUI, rangeUi;
 

    private void Awake()
    {
        ArrowFill = transform.Find("Btn2/ArrowFill/ArrowFill").GetComponent<Image>();
        FillBox = transform.Find("Btn2/ArrowFill").GetComponent <Transform>();
        ArrowEA = transform.Find("Btn2/ArrowFill/ArrowEA").GetComponent<TMP_Text>();
        meleeUI = transform.Find("Btn1").GetComponent<Transform>();
        rangeUi = transform.Find("Btn2").GetComponent<Transform>();
    }

    private void Update()
    {

        ArrowEaText();  // 화살갯수 text
        ArrowFillAmount(); // 화살갯수 게이지
        WeaponeUIActive();
    }

    bool once, once1;

    private void WeaponeUIActive()
    {
        if (GameManager.Instance.isGetMeleeItem && !once)
        {
            once= true;
            meleeUI.gameObject.SetActive(true);
        }
        if (GameManager.Instance.isGetRangeItem && once1)
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
}

