using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
   
    Transform btn;
    
    public Animator ani;
    SpriteRenderer Sr;

    private void Awake()
    {
        if(gameObject.name == "府府")
        {
            ani = GetComponent<Animator>();
            Sr = GetComponent<SpriteRenderer>();
        }
       
        btn = transform.Find("Canvas").GetComponent<Transform>();
    }
    private void Update()
    {
        
    }
  
    //public void MoivPoint1()
    //{
    //    if(gameObject.name == "府府")
    //    {
    //        gameObject.transform.position = transform.Find("TelPoint1").position;
    //        ani.SetBool("Show", false);
    //    }
        
    //}
    public void offSprite()
    {
        gameObject.SetActive(false);
        
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
