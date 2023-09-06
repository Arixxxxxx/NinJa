using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum RiRiNPC
    {
        action1, action2, action3, action4, action5
    }

    public RiRiNPC npc;

    Transform btn;
   

    private void Awake()
    {
    
        btn = transform.Find("Canvas").GetComponent<Transform>();
    }
    private void Update()
    {
        
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
