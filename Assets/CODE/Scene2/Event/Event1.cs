using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Event1 : MonoBehaviour
{

    Rigidbody2D[] NpcRb = new Rigidbody2D[4];
    public float[] Dis;
    Transform endPos;

    private void Awake()
    {
        for (int i = 0; i < NpcRb.Length; i++)
        {
            NpcRb[i] = transform.GetChild(i).GetComponent<Rigidbody2D>();
        }

        endPos = transform.Find("EndPos").GetComponent<Transform>();
    }
    private void Start()
    {
        Dis = new float[NpcRb.Length];
    }

    bool once;
    bool startRunNpc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            for (int i = 0; i < NpcRb.Length; i++)
            {
                NpcRb[i].gameObject.SetActive(true);
            }

            GameManager.Instance.F_MoveStop(0);
            Eventing();
            
        }
    }


    [SerializeField] float NpcMoveSpeed;
    bool once1;

    public bool Action;
    void Eventing()
    {
        
        for( int i = 0; i <NpcRb.Length; i++)
        {
            Action = false;

            if (NpcRb[i].gameObject.activeSelf)
            {
                Dis[i] = NpcRb[i].position.x - endPos.position.x;

                if (Dis[i] > 0.1f)
                {
                    Action = true;
                    NpcRb[i].velocity = Vector2.left * NpcMoveSpeed;
                }
                else if (Dis[i] < 0.1f)
                {
                    NpcRb[i].gameObject.SetActive(false);
                }
            }
           
        }

        if(Action)
        {
            Invoke("Eventing", 0.05f);
        }
        if (!once1)
        {
            once1 = true;
            Invoke("OnGame", 3);
        }
    }

    private void OnGame()
    {
        GameManager.Instance.F_MoveStop(1);
        GameUI.instance.F_SetMapMoveBar("¿Ü°û");
    }
   
}

