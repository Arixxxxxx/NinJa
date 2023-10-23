using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event5SpawnSC : MonoBehaviour
{
    public enum SpawnType
    {
        Spawn1, Spawn2, Spawn3, Spawn4, Spawn5
    }
    public SpawnType type;

    Transform[] Spawn1TR_Array;
    private void Awake()
    {
        switch (type)
        {
            case SpawnType.Spawn1:

                Spawn1TR_Array = new Transform[transform.childCount];
                for (int i = 0; i < transform.childCount; i++)
                {
                    Spawn1TR_Array[i] = transform.GetChild(i).GetComponent<Transform>();
                }

                break;

            case SpawnType.Spawn2:
                Spawn1TR_Array = new Transform[transform.childCount];
                for (int i = 0; i < transform.childCount; i++)
                {
                    Spawn1TR_Array[i] = transform.GetChild(i).GetComponent<Transform>();
                }

                break;
        }

    }
    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case SpawnType.Spawn1:

                    if (!once)
                    {
                        once = true;

                        for (int i = 0; i < Spawn1TR_Array.Length; i++)
                        {
                            GameObject _obj = PoolManager.Instance.F_GetObj("Skele");
                            _obj.transform.position = Spawn1TR_Array[i].position;
                        }
                    }

                    gameObject.SetActive(false);


                    break;

                case SpawnType.Spawn2:
                    if (!once)
                    {
                        once = true;

                        for (int i = 0; i < Spawn1TR_Array.Length; i++)
                        {
                            GameObject obj = PoolManager.Instance.F_GetObj("Portal");
                            obj.transform.position = Spawn1TR_Array[i].position;
                        }

                    }
                    gameObject.SetActive(false);
                    break;

                case SpawnType.Spawn3:
                    if (!once)
                    {
                        once = true;

                        StartCoroutine(StartZombieTrap());
                    }


                    break;

                case SpawnType.Spawn4:



                    break;

                case SpawnType.Spawn5:



                    break;
            }
        }

    }

    IEnumerator StartZombieTrap()
    {
        transform.Find("SpawnZ").GetComponent<ZombieTrap>().BlackHoleOpenBool = true;

        while (transform.Find("SpawnZ").gameObject.activeSelf)
        {
            yield return null;
        }

        gameObject.gameObject.SetActive(false);
    }
}
