using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public enum ObjectType
    {
        Weapone
        , Enemy
    }
    public ObjectType type;
    private Queue WeapQ;
    private Queue<GameObject> EnemyAQ;

    [SerializeField] GameObject Weapone;
    [SerializeField] GameObject[] EnemyA;
    EnemySpawn EnemySc;
    Transform SpawnPoint;
    

    private void Awake()
    {
        WeapQ = new Queue();
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

  
        for (int i = 0; i < 30; i++)
        {
            GameObject obj = Instantiate(Weapone, transform.position, Quaternion.identity,transform);
            WeapQ.Enqueue(obj);
            obj.SetActive(false);
                      
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



            case "Weapone":
                {
                    GameObject obj = (GameObject)WeapQ.Dequeue();

                    // 스트라이트 방향에 따른  발사위치 초기화
                    if (GameManager.Instance.player.Sr.flipX == true)
                    {
                        ShootRota = new Vector3(-0.6f, 0);
                    }
                    else if (GameManager.Instance.player.Sr.flipX == false)
                    {
                        ShootRota = new Vector3(0.6f, 0);
                    }

                    // 발사체 Enable시 위치 정보 초기화
                    obj.transform.position = GameManager.Instance.player.transform.position + ShootRota;

                    obj.SetActive(true);
                    return obj;
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
                _obj.SetActive(false);
                EnemyAQ.Enqueue(_obj);
                break;

            case "Bullet":
                WeapQ.Enqueue(_obj);
                break;
                

        }


    }

}
