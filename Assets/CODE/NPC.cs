using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
   
    Transform btn;
    SpriteRenderer Sr;

    private void Awake()
    {
       Sr = GetComponent<SpriteRenderer>();
        btn = transform.GetChild(1).GetComponent<Transform>();
    }
    private void Update()
    {
        NpcFaceDir();
    }
    private void NpcFaceDir()
    {
        if(GameManager.Instance.player.transform.position.x < transform.position.x)
        {
            Sr.flipX = true;
        }
        else 
        {
            Sr.flipX = false; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            btn.gameObject.SetActive(true);
        }
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            btn.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            btn.gameObject.SetActive(false);
        }
    }
}
