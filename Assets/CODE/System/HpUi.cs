using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpUi : MonoBehaviour
{
    public enum PlayerType
    {
        Player,Enemy,Ghost,Enemis
    }
    public PlayerType Type;

    private Image Hp;
    private Image Effect;
    private Enemys Sc;
    private Ghost Ghostsc;
    public Animator Ani;

    private void Awake()
    {
        Hp = transform.Find("Hp").GetComponent<Image>();
        Effect = transform.Find("Effect").GetComponent<Image>();
        Ani = GetComponent<Animator>();

        switch (Type)
        {
            case PlayerType.Player:
                break;

                case PlayerType.Enemy:
                Sc = GetComponentInParent<Enemys>();
                break;

                case PlayerType.Ghost:
                Ghostsc = GetComponentInParent<Ghost>();
                break; 
            
               case PlayerType.Enemis:
                
                break;

        }
        
    }


    void Update()
    {
        F_HpUiPosition();
        F_FillAmount();
    }

    // HPBar Position
     Vector3 HVec = new Vector3(0, 0.6f);
    private void F_HpUiPosition()
    {
        switch (Type)
        {
            case PlayerType.Player:
                     Vector3 PVec = GameManager.Instance.player.transform.position;
                     transform.position = PVec + HVec;
                break;

                case PlayerType.Enemy:
                
                          transform.position = Sc.transform.position + HVec;
                break;

                case PlayerType.Ghost:
                transform.position = Ghostsc.transform.position + HVec;
                break;
        }
     
    }

    // FillAmount SetValue
    private void F_FillAmount()
    {
        switch (Type)
        {
            case PlayerType.Player:
                Hp.fillAmount = GameManager.Instance.Player_CurHP / GameManager.Instance.Player_MaxHP;

                if (Effect.fillAmount > Hp.fillAmount)
                {
                    Effect.fillAmount -= 0.2f * Time.deltaTime;
                }
                else if (Effect.fillAmount <= Hp.fillAmount)
                {
                    Effect.fillAmount = Hp.fillAmount;
                }
                break;

            case PlayerType.Enemy:
                Hp.fillAmount = Sc.CurHP / Sc.MaxHp;

                if (Effect.fillAmount > Hp.fillAmount)
                {
                    Effect.fillAmount -= 0.2f * Time.deltaTime;
                }
                else if (Effect.fillAmount <= Hp.fillAmount)
                {
                    Effect.fillAmount = Hp.fillAmount;
                }

                break;

                case PlayerType.Ghost:
                Hp.fillAmount = Ghostsc.CurHp / Ghostsc.MaxHp;

                if (Effect.fillAmount > Hp.fillAmount)
                {
                    Effect.fillAmount -= 0.2f * Time.deltaTime;
                }
                else if (Effect.fillAmount <= Hp.fillAmount)
                {
                    Effect.fillAmount = Hp.fillAmount;
                }

                break;
        }
       
    }
}
