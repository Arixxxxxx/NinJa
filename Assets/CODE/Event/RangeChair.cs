using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangeChair : MonoBehaviour
{

    RangeZone sc;
  

    private void Awake()
    {
        sc = transform.parent.GetComponent<RangeZone>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(ContactPoint2D a in collision.contacts)
            {
                if(a.normal == -Vector2.up)
                {
                    sc.StartEvent(collision);

                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
           
        }
    }
   
    public void boom()
    {
        transform.gameObject.SetActive(false);
    }
}
