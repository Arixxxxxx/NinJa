using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePortPoint : MonoBehaviour
{
   public enum PlacePointer
    {
        P1,P2,P3
    }

    public PlacePointer Place;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("aa");
            TeleportManager sc = transform.parent.GetComponent<TeleportManager>();
            sc.F_TelePort(Place, collision.gameObject);
        }
    }

}
