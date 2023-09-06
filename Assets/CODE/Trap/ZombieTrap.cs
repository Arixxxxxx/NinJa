using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ZombieTrap : MonoBehaviour
{
    [Header("# 좀비 스폰 횟수 및 시간")]
    [SerializeField][Tooltip("기본 4회")] int SpawnCount;
    [SerializeField] private float SpawnTimer;
   
    private Transform SpawnPoint1;
    private Transform SpawnPoint2;
    private Transform SpawnPoint3;
       
    private void Awake()
    {
        SpawnTimer = 10;
        SpawnPoint1 = transform.GetChild(0).GetComponent<Transform>();
        SpawnPoint2 = transform.GetChild(1).GetComponent<Transform>();
        SpawnPoint3 = transform.GetChild(2).GetComponent<Transform>();
        SpawnCount = 4;
    }

    private void Ing()
    {
        if (SpawnCount <= 0)
        {
            return;
        }

        if (SpawnCount > 0)
        {
            GameObject obj = PoolManager.Instance.F_GetObj("Enemy");
            obj.transform.position = SpawnPoint1.position;
            obj.SetActive(true);

            GameObject obj1 = PoolManager.Instance.F_GetObj("Enemy");
            obj1.transform.position = SpawnPoint2.position;
            obj1.SetActive(true);

            GameObject obj2 = PoolManager.Instance.F_GetObj("Enemy");
            obj2.transform.position = SpawnPoint3.position;
            obj2.SetActive(true);

            SpawnCount--;

            Invoke("Ing", SpawnTimer);
        }

    }

    bool onecall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!onecall) { Ing(); onecall = true; }
        }
    }
}
