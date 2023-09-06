using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    
    Image ArrowFill;
    Transform FillBox;
 

    private void Awake()
    {
        ArrowFill = transform.Find("Btn2/ArrowFill/ArrowFill").GetComponent<Image>();
        FillBox = transform.Find("Btn2/ArrowFill").GetComponent <Transform>();
    }

    private void Update()
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

