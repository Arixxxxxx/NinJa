using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    private Queue<GameObject> EnemyAQ;
    [SerializeField] GameObject[] EnemyA;
    EnemySpawn EnemySc;
    Transform SpawnPoint;
   

    private void Awake()
    {
   
        EnemyAQ = new Queue<GameObject>();
        EnemySc = FindAnyObjectByType<EnemySpawn>();
        SpawnPoint = EnemySc.transform.GetChild(0).GetComponent<Transform>();
     
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
        
    }

    // 오브젝트 풀링
    Vector3 ShootRota;
    
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
                                default: return null;   
          }
        
    }

    // 오브젝트 회수
    public void F_ReturnObj(GameObject _obj, string _Name)
    {
        switch (_Name)
        {
            case "Enemy":
                _obj.SetActive(false) ;
                EnemyAQ.Enqueue(_obj);
                break;
        }

    }

}
