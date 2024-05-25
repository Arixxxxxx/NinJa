using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    private Queue<GameObject> EnemyAQ;
    [SerializeField] GameObject[] EnemyA;
    [SerializeField] GameObject Ghost;
    [SerializeField] GameObject Skele;
    [SerializeField] GameObject GhostPortal;
    [SerializeField] GameObject[] Bullet;
    [SerializeField] GameObject DmgText;
    EnemySpawn EnemySc;
    Queue<GameObject> DustQUE;
    Queue<GameObject> EnemyBullets;
    Queue<GameObject> DmgTextQue;
    Queue<GameObject> GhostQue;
    Queue<GameObject> SkeleQue;
    Queue<GameObject> PortalQue;

    public Transform SpawnPoint;


    private void Awake()
    {
        DmgTextQue = new Queue<GameObject>();
        EnemyBullets = new Queue<GameObject>();
        EnemyAQ = new Queue<GameObject>();
        GhostQue = new Queue<GameObject>();
        SkeleQue = new Queue<GameObject>();
        PortalQue = new Queue<GameObject>();
        EnemySc = FindAnyObjectByType<EnemySpawn>();
        SpawnPoint = EnemySc.transform.GetComponent<Transform>();
        DustQUE = new Queue<GameObject>();

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
            GameObject enemyobj = Instantiate(EnemyA[Rand], transform.position, Quaternion.identity, transform.Find("Enemy/Zombie"));
            EnemyAQ.Enqueue(enemyobj);
            enemyobj.SetActive(false);

        }

        for (int i = 0; i < 20; i++)
        {
            GameObject obj = Instantiate(EnemyA[2], transform.position, Quaternion.identity, transform.Find("Object"));
            DustQUE.Enqueue(obj);
            obj.SetActive(false);

            GameObject objs = Instantiate(DmgText, transform.position, Quaternion.identity, transform.Find("TextBox"));
            DmgTextQue.Enqueue(objs);
            objs.SetActive(false);

            GameObject objss = Instantiate(Ghost, transform.position, Quaternion.identity, transform.Find("Enemy/Ghost"));
            GhostQue.Enqueue(objss);
            objss.SetActive(false);

        }
          for (int i = 0;i < 40; i++)
        {
            GameObject obj = Instantiate(Bullet[0], transform.position, Quaternion.identity, transform.Find("Bullet/Enemy"));
            EnemyBullets.Enqueue(obj);
            obj.SetActive(false);
        }

         for(int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(Skele, transform.position, Quaternion.identity, transform.Find("Enemy/Skele"));
            SkeleQue.Enqueue(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(GhostPortal, transform.position, Quaternion.identity, transform.Find("Enemy/Portal"));
            PortalQue.Enqueue(obj);
            obj.SetActive(false);
        }

    }

    // 오브젝트 풀링
    Vector3 ShootRota;


    /// <summary>
    /// 오브젝트풀
    /// </summary>
    /// <param name="_Value">Enemy,Dust,Ghost,Skele,Portal,EB,Text </param>
    /// <returns></returns>
    public GameObject F_GetObj(string _Value)
    {
        switch (_Value)
        {
            case "Enemy":
                {
                    if(EnemyAQ.Count == 0)
                    {
                        int Rand = Random.Range(0, 2);
                        GameObject enemyobj = Instantiate(EnemyA[Rand], transform.position, Quaternion.identity, transform.Find("Enemy/Zombie"));
                        EnemyAQ.Enqueue(enemyobj);
                        enemyobj.SetActive(false);
                    }
                    GameObject objs = EnemyAQ.Dequeue();
                    objs.transform.position = SpawnPoint.transform.position;
                    objs.SetActive(true);
                    return objs;
                   
                }

            case "Skele":
                {
                    GameObject objs = SkeleQue.Dequeue();
                    objs.transform.position = SpawnPoint.transform.position;
                    objs.SetActive(true);
                    return objs;
                }

            case "Portal":
                {
                    GameObject objs = PortalQue.Dequeue();
                    objs.transform.position = SpawnPoint.transform.position;
                    objs.SetActive(true);
                    return objs;
                }

            case "Ghost":
                {
                    if(GhostQue.Count == 0)
                    {
                        GameObject objss = Instantiate(Ghost, transform.position, Quaternion.identity, transform.Find("Enemy/Ghost"));
                        objss.transform.position = SpawnPoint.transform.position;
                        objss.SetActive(true);
                        return objss;
                    }
                    else
                    {
                        GameObject objs = GhostQue.Dequeue();
                        objs.transform.position = SpawnPoint.transform.position;
                        objs.SetActive(true);
                        return objs;

                    }


                }

            case "Dust":
                {
                    if(DustQUE.Count == 0)
                    {
                        GameObject objs = Instantiate(EnemyA[2], transform.position, Quaternion.identity, transform.Find("Object"));
                        objs.transform.position = SpawnPoint.transform.position;
                        objs.SetActive(true);
                        return objs;
                    }
                    else
                    {
                        GameObject objs = DustQUE.Dequeue();
                        objs.transform.position = SpawnPoint.transform.position;
                        objs.SetActive(true);
                        return objs;
                    }
                    
                   
                }


            case "EB":
                {
                    GameObject objs = EnemyBullets.Dequeue();
                    objs.transform.position = SpawnPoint.transform.position;
                    objs.SetActive(true);
                    return objs;
                }

            case "Text":
                {
                    if(DmgTextQue.Count == 0)
                    {
                        GameObject objs = Instantiate(DmgText, transform.position, Quaternion.identity, transform.Find("TextBox"));
                        objs.transform.position = SpawnPoint.transform.position;
                        objs.SetActive(true);
                        return objs;
                    }
                    else
                    {
                        GameObject objs = DmgTextQue.Dequeue();
                        objs.transform.position = SpawnPoint.transform.position;
                        objs.SetActive(true);
                        return objs;
                    }
                    
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
    /// <param name="_Name">Enemy,Dust,Ghost,Skele,Portal,EB,Text</param>
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
                DustQUE.Enqueue(_obj);
                break;

            case "Ghost":
                _obj.SetActive(false);
                GhostQue.Enqueue(_obj);
                break;

            case "Skele":
                {
                     _obj.SetActive(false);
                    SkeleQue.Enqueue(_obj);
                    break;
                }

            case "Portal":
                {
                    _obj.SetActive(false);
                    PortalQue.Enqueue(_obj);
                    break;
                    
                }
            case "EB":
                _obj.SetActive(false);
                EnemyBullets.Enqueue(_obj);
                break;

            case "Text":
                _obj.SetActive(false);
                _obj.transform.SetParent(transform.Find("TextBox"));
                DmgTextQue.Enqueue(_obj);
                break;

        }

    }

}
