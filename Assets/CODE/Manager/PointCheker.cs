using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pointer
{
    point0,point1,point2,point3,point4,point5,point6,point7,point8,point9,point10,point11,point12
}
public class PointCheker : MonoBehaviour
{
    public Pointer type;
    GuideManager guideManager;
    private bool once;

    private void Awake()
    {
        
    }
   
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!once && collision.gameObject.CompareTag("Player"))
        {
            guideManager = GameObject.Find("GameGuide").GetComponent<GuideManager>();
            guideManager.F_GetColl(type, collision);
            once = true;
        }
    }
}
