using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HitBox;

public class WallJumpTraning : MonoBehaviour
{
    SpriteRenderer p1, p2, p3, p4, p5;
    public bool p1b, p2b, p3b, p4b, p5b;
    public bool start;
    [Range(0f, 15f)]
    public float ColorSpeed = 2;

  
    private void Awake()
    {
        
        p1 = transform.Find("1").GetComponent<SpriteRenderer>();   
        p2 = transform.Find("2").GetComponent<SpriteRenderer>();   
        p3 = transform.Find("3").GetComponent<SpriteRenderer>();   
        p4= transform.Find("4").GetComponent<SpriteRenderer>();   
        p5= transform.Find("5").GetComponent<SpriteRenderer>();


    }

    private void Update()
    {
        startAni();
    }

    private void startAni()
    {
        if (start)
        {
            p1.enabled = true;

            p2.enabled = true;
            p2.color = new Color(1, 1, 1, 0);

            p3.enabled = true;
            p3.color = new Color(1, 1, 1, 0);
            
            p4.enabled = true;
            p4.color = new Color(1, 1, 1, 0);

            p5.enabled = true;
            p5.color = new Color(1, 1, 1, 0);

        }
        if (p1b)
        {
            p1.enabled = false;
            p2.color += new Color(1, 1, 1, 0.1f) * ColorSpeed * Time.deltaTime;
         }
       if (p2b)
        {
            p2.enabled = false;
            p3.color += new Color(1, 1, 1, 0.1f) * ColorSpeed * Time.deltaTime;
        }
         if (p3b)
        {
            p3.enabled = false;
            p4.color += new Color(1, 1, 1, 0.1f) * ColorSpeed * Time.deltaTime;
        }
        if (p4b)
        {
            p4.enabled = false;
            p5.color += new Color(1, 1, 1, 0.1f) * ColorSpeed * Time.deltaTime;
        }

        if (p5b)
        {
            p5.enabled = false;
        }
    }
    public void F_SetArrow(BoxPoint num)
    {
        if(num == BoxPoint.p1)
        {
            p1b = true;
        }
        else if(num == BoxPoint.p2)
        {
            p2b = true;
        }
        else if (num == BoxPoint.p3)
        {
            p3b = true;
        }
        else if( num == BoxPoint.p4)
        {
            p4b = true;
        }
        else if (num == BoxPoint.p5)
        {
            p5b = true;
        }
        else if(num == BoxPoint.startpoint)
        {
            start = true;
            GameManager.Instance.player.F_CharText("WallJumpFail");
        }
        
    }
    
    public void F_TriggetExit(BoxPoint num)
    {
        if (num == BoxPoint.p1)
        {
            p1b = false; 
        }
        else if (num == BoxPoint.p2)
        {
            p2b = false;
        }
        else if (num == BoxPoint.p3)
        {
            p3b = false;
        }
        else if (num == BoxPoint.p4)
        {
            p4b = false;
        }
        else if (num == BoxPoint.p5)
        {
            p5b = false;
        }
        else if (num == BoxPoint.startpoint)
        {
            start = false;
        }
    }
   
}
