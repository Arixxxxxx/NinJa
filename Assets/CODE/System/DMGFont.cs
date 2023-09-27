using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DMGFont : MonoBehaviour
{
    TextMeshProUGUI DMG_Font;
    Enemys sc;
    Ghost Ghostsc;
    Vector2 OriginVec;
    Vector2 targetpositiion;
    Vector2 startposition;
    bool isMoving;
    float moveTime;
    DmgPooling dmp;

    Transform B;
    Transform A;



    private void Awake()
    {
        DMG_Font = GetComponent<TextMeshProUGUI>();
        sc = transform.GetComponentInParent<Enemys>();
        Ghostsc = transform.GetComponentInParent<Ghost>();
        dmp = transform.GetComponentInParent<DmgPooling>();
        B = transform.GetComponentsInParent<Transform>(true)[1];
    }
    private void OnEnable()
    {
        OriginVec = transform.position;
        //startposition = sc.transform.position;
        startposition = startposition + Vector2.up;
        transform.position = startposition;
        targetpositiion = new Vector2(transform.position.x, transform.position.y + 0.7f);
    }

    private void FixedUpdate()
    {
        F_FontMove();
        
    }

    public void F_FontPopup(float _DMG)
    {

        if (B == null)
        {
            B = transform.GetComponentsInParent<Transform>(true)[2];
        }

            startposition = B.transform.position;

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
                transform.position = B.transform.position + Vector3.up * 1;
                gameObject.SetActive(false);
                dmp.F_In_FontBox(gameObject);
                
            }
        }
  
    }
    
}
