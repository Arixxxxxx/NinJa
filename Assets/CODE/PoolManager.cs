using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    private Queue<GameObject> EnemyAQ;
    [SerializeField] GameObject[] EnemyA;
    [SerializeField] GameObject[] Bullet;
    EnemySpawn EnemySc;
    Queue<GameObject> Dust;
    Queue<GameObject> EnemyBullets;

    Transform SpawnPoint;


    private void Awake()
    {
        EnemyBullets = new Queue<GameObject>();
        EnemyAQ = new Queue<GameObject>();
        EnemySc = FindAnyObjectByType<EnemySpawn>();
        SpawnPoint = EnemySc.transform.GetChild(0).GetComponent<Transform>();
        Dust = new Queue<GameObject>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < 50; i++)
        {
            int Rand = Random.Range(0, 2);
            GameObject enemyobj = Instantiate(EnemyA[Rand], transform.position, Quaternion.identity, transform);
            EnemyAQ.Enqueue(enemyobj);
            enemyobj.SetActive(false);

        }

        for (int i = 0; i < 20; i++)
        {
            GameObject obj = Instantiate(EnemyA[2], transform.position, Quaternion.identity, transform);
            Dust.Enqueue(obj);
            obj.SetActive(false);
        }
          for (int i = 0;i < 20; i++)
        {
            GameObject obj = Instantiate(Bullet[0], transform.position, Quaternion.identity, transform);
            EnemyBullets.Enqueue(obj);
            obj.SetActive(false);
        }

    }

    // 오브젝트 풀링
    Vector3 ShootRota;
    

    /// <summary>
    /// 오브젝트풀
    /// </summary>
    /// <param name="_Value">EnemyAB="Enemy", 먼지 = "Dust", 적미사일 = "EB" </param>
    /// <returns></returns>
    public GameObject F_GetObj(string _Value)
    {
        switch (_Value)
        {
            case "Enemy":
                {
                    GameObject objs = EnemyAQ.Dequeue();
                    objs.transform.position = SpawnPoint.transform.position;
                    objs.SetActive(true);
                    return objs;
                   
                }

            case "Dust":
                {
                    GameObject objs = Dust.Dequeue();
                    objs.transform.position = SpawnPoint.transform.position;
                    objs.SetActive(true);
                    return objs;
                }


            case "EB":
                {
                    GameObject objs = EnemyBullets.Dequeue();
                    objs.transform.position = SpawnPoint.transform.position;
                    objs.SetActive(true);
                    return objs;
                }
            default: return null;   
          }


        
    }

    // 오브젝트 회수
    /// <summary>
    /// 
    /// 오브젝트회수
    /// </summary>
    /// <param name="_obj">gameObject</param>
    /// <param name="_Name">EnemyAB="Enemy", 먼지 = "Dust", 적미사일 = "EB"</param>
    public void F_ReturnObj(GameObject _obj, string _Name)
    {
        switch (_Name)
        {
            case "Enemy":
                _obj.SetActive(false) ;
                EnemyAQ.Enqueue(_obj);
                break;

            case "Dust":
                _obj.SetActive(false);
                Dust.Enqueue(_obj);
                break;

            case "EB":
                _obj.SetActive(false);
                EnemyBullets.Enqueue(_obj);
                break;

        }

    }

}
