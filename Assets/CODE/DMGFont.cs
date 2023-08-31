using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DMGFont : MonoBehaviour
{
    TextMeshProUGUI DMG_Font;
    Enemys sc;
    Vector2 OriginVec;
    Vector2 targetpositiion;
    Vector2 startposition;
    bool isMoving;
    float moveTime;
    DmgPooling dmp;

   
    private void Awake()
    {
        DMG_Font = GetComponent<TextMeshProUGUI>();
        sc = transform.GetComponentInParent<Enemys>();
        dmp = transform.GetComponentInParent<DmgPooling>();

    }
    private void OnEnable()
    {
        OriginVec = transform.position;
        //startposition = sc.transform.position;
        startposition =  Vector2.up;
        targetpositiion = new Vector2(transform.position.x, transform.position.y + 0.7f);
    }

    private void FixedUpdate()
    {
        F_FontMove();
        
    }

    public void F_FontPopup(float _DMG)
    {
        if (sc == null)
        {
            sc = transform.GetComponentInParent<Enemys>();
        }
        
        startposition = sc.transform.position;

        if (gameObject.activeSelf == false)
        {
            isMoving = true;
            gameObject.SetActive(true);
        }
        
        if (DMG_Font.text != _DMG.ToString("F0"))
        {
            DMG_Font.text = _DMG.ToString("F0");
        }
    }
    float CurTime;
    private void F_FontMove()
    {
    if(isMoving)
        {
            CurTime += Time.deltaTime;
            if (CurTime < 1)
            {
                transform.position = Vector2.Lerp(transform.position, targetpositiion, Time.deltaTime);
                
            }
           else if(CurTime > 1.0f)
            {
                CurTime = 0;
                isMoving = false;
                transform.position =sc.transform.position + Vector3.up * 1;
                gameObject.SetActive(false);
                dmp.F_In_FontBox(gameObject);
                
            }
        }
  
    }
    
}
