using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magic : MonoBehaviour
{
    BoxCollider2D Box;
    Boss sc;
    bool once;

    private void Awake()
    {
        Box = GetComponent<BoxCollider2D>();
        sc = FindAnyObjectByType<Boss>();
    }


    private void OnEnable()
    {
        if (Box.enabled)
        {
            Box.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            StartCoroutine(Player.instance.F_OnHit());
            
        }
    }

    private void A_CollSet()
    {
        if (!Box.enabled)
        {
            Box.enabled = true;
        }
        else if(Box.enabled)
        {
            Box.enabled = false;
            once = false;
        }
    }

    private void ReturnObject()
    {
        if(sc == null)
        {
            sc = FindAnyObjectByType<Boss>();
        }
        
        sc.F_SetMagic(gameObject);
    }
}
