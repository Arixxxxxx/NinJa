using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePortPoint : MonoBehaviour
{
   public enum PlacePointer
    {
        P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,P11,P12,P13
    }

    public PlacePointer Place;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TeleportManager sc = transform.parent.GetComponent<TeleportManager>();
            sc.F_TelePort(Place, collision.gameObject);
        }
    }

}
