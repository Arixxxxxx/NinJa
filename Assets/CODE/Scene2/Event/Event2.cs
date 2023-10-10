using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : MonoBehaviour
{
    bool once;
    Transform[] Point;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            Point = new Transform[4];
            for (int i = 0; i < Point.Length; i++)
            {
                Point[i] = transform.GetChild(i).GetComponent<Transform>();
            }
            
            SkeletonSpawn();
        }
    }

    bool once2;
    
    private void SkeletonSpawn()
    {
        for (int i = 0; i < Point.Length; i++)
        {
            Point[i] = transform.GetChild(i).GetComponent<Transform>();
            GameObject obj = PoolManager.Instance.F_GetObj("Skele");
            obj.transform.position = Point[i].position;
        }

        gameObject.SetActive(false);
    }
}
