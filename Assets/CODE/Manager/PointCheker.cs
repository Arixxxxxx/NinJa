using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pointer
{
    point0,point1,point2,point3,point4,point5,point6,point7,point8,point9
}
public class PointCheker : MonoBehaviour
{
    [SerializeField] LayerMask PlayerLayer;
    public Pointer type;
    Transform canvasBox;
    
    private void Awake()
    {
        canvasBox = transform.Find("PointCheker/Btn").GetComponent<Transform>();
    }
    private void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.gameObject.CompareTag("Player"))
        {
            canvasBox.gameObject.SetActive(true);
        }

        if (collision.gameObject.layer == PlayerLayer)
        {
            canvasBox.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
       {
            if (!canvasBox.gameObject.activeSelf)
            {
                canvasBox.gameObject.SetActive(true);  
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == PlayerLayer)
        {
            canvasBox.gameObject.SetActive(false);
        }
    }

}



   