using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    Transform Effect;
    BoxCollider2D Box;
    [SerializeField] float DmgDur;

    private void Awake()
    {
        Effect = transform.parent.Find("Effect").GetComponent<Transform>();
        Box = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        if (Box.enabled)
        {
            Box.enabled = false;
        }
        
    }

    private void Update()
    {
        if (Effect.gameObject.activeSelf)
        {
            Box.enabled = true;
        }
        else if(!Effect.gameObject.activeSelf)
        {
            Box.enabled=false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Player.instance.F_OnHit());

        }
    }
    float Counter;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Counter += Time.deltaTime;
            if(Counter > DmgDur)
            {
                Counter = 0;
                StartCoroutine(Player.instance.F_OnHit());

            }

        }
    }
}
