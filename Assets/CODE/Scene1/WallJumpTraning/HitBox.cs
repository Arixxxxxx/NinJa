using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public enum BoxPoint
    {
        p1,p2,p3,p4,p5,startpoint
    }
    public BoxPoint pointNum;

    WallJumpTraning WJTsc;

    private void Awake()
    {
        WJTsc = transform.GetComponentInParent<WallJumpTraning>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WJTsc.F_SetArrow(pointNum);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WJTsc.F_TriggetExit(pointNum);
        }
    }
}
