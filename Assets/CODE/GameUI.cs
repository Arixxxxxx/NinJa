using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    
    Image ArrowFill;
    Transform FillBox;
    TMP_Text ArrowEA;
 

    private void Awake()
    {
        ArrowFill = transform.Find("Btn2/ArrowFill/ArrowFill").GetComponent<Image>();
        FillBox = transform.Find("Btn2/ArrowFill").GetComponent <Transform>();
        ArrowEA = transform.Find("Btn2/ArrowFill/ArrowEA").GetComponent<TMP_Text>();
    }

    private void Update()
    {

        ArrowEaText();  // 화살갯수 text
        ArrowFillAmount(); // 화살갯수 게이지

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

