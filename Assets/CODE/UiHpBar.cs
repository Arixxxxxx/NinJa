using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiHpBar : MonoBehaviour
{
    public enum UiBarType
    {
        Hp, SP
    }
    [Header("# 체크하세요!")]
    [Space]
    public UiBarType type;

    Image Front;
    Image Back;
    TextMeshProUGUI HpText;

    private void Awake()
    {
        Front = transform.GetChild(1).GetComponent<Image>();
        Back = transform.GetChild(0).GetComponent<Image>();
        HpText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }
          void Update()
    {
        switch (type)
        {
            case UiBarType.Hp:
                Front.fillAmount = GameManager.Instance.Player_CurHP / GameManager.Instance.Player_MaxHP;

                if (Front.fillAmount <= Back.fillAmount)
                {
                    Back.fillAmount -= 0.2f * Time.deltaTime;
                }
                else if (Front.fillAmount >= Back.fillAmount)
                {
                    Back.fillAmount = Front.fillAmount;
                }
                if (GameManager.Instance.isPlayerDead)
                {
                    HpText.text = $"  D E A D";
                }
                else if (!GameManager.Instance.isPlayerDead)
                {
                    HpText.text = $"HP : {(int)GameManager.Instance.Player_CurHP} / {(int)GameManager.Instance.Player_MaxHP}";
                }
                break;

            case UiBarType.SP:
                Front.fillAmount = GameManager.Instance.Player_CurSP / GameManager.Instance.Player_MaxSP;

                //if (Front.fillAmount <= Back.fillAmount)
                //{
                //    Back.fillAmount -= 0.2f * Time.deltaTime;
                //}
                //else if (Front.fillAmount >= Back.fillAmount)
                //{
                    Back.fillAmount = Front.fillAmount;
                //}
                
                
                  HpText.text = $"SP : {(int)GameManager.Instance.Player_CurSP} / {(int)GameManager.Instance.Player_MaxSP}";
                
                break;

        }
         
        
        
    }
}
