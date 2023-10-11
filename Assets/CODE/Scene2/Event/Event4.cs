using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event4 : MonoBehaviour
{
    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            GameObject obj = PoolManager.Instance.F_GetObj("Portal");
            obj.transform.position = transform.Find("P1").transform.position;
        }
    }

}
