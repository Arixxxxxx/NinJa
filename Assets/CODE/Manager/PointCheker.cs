using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Pointer
{
    point0,point1,point2,point3,point4,point5,point6,point7,point8,point9,point10,point11,point12
}
public class PointCheker : MonoBehaviour
{
    public Pointer type;
    GuideManager guideManager;
    Transform btn;

    private void Awake()
    {
        if (this.type != Pointer.point0)
        {
            btn = transform.GetComponentsInChildren<Transform>(true)[4];
        }
           
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.type != Pointer.point0) // ���������ϰ��
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                btn.gameObject.SetActive(true);
            }
        }
        
       
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(this.type != Pointer.point0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                btn.gameObject.SetActive(true);
                //if (Input.GetKeyDown(KeyCode.F) && !GameManager.Instance.once)
                //{
                //    Debug.Log("����");
                //    GameManager.Instance.once = true;
                //    //guideManager = GameObject.Find("GameGuide").GetComponent<GuideManager>();
                //    GameManager.Instance.guideM.F_GetColl(type);
                //}
            }
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.type != Pointer.point0)
        {
            btn.gameObject.SetActive(false);
        }
    }




    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!once && collision.gameObject.CompareTag("Player"))
    //    {
    //        guideManager = GameObject.Find("GameGuide").GetComponent<GuideManager>();
    //        guideManager.F_GetColl(type, collision);
    //        once = true;
    //    }
    //}
}
