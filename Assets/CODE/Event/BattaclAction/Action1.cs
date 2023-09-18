using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action1 : MonoBehaviour
{
    BoxCollider2D boxColl;
    Transform right, left;

    private void Awake()
    {
        right = transform.Find("Right").GetComponent<Transform>();
        left = transform.Find("Left").GetComponent<Transform>();
    }
    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            GameObject E1 = PoolManager.Instance.F_GetObj("Enemy");
            E1.transform.position = right.transform.position;
            
            GameObject E2 = PoolManager.Instance.F_GetObj("Enemy");
            E2.transform.position = left.transform.position;

            boxColl.enabled = false;
        }
    }
}
