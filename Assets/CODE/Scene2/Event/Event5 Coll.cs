using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event5Coll : MonoBehaviour
{
    Event5 sc;
    private void Awake()
    {
        sc = transform.parent.GetComponent<Event5>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.gameObject.layer == LayerMask.NameToLayer("Rock") || (collision.gameObject.layer == LayerMask.NameToLayer("EnemyDead")))
        {
            sc.ExitRockCheker(collision);
        }
      
    }
}
