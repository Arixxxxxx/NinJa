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
        if(gameObject.name == "����")
        {
            ani = GetComponent<Animator>();
            Sr = GetComponent<SpriteRenderer>();
        }
        if (gameObject.name == "��������")
        {
            ani = GetComponent<Animator>();
            Sr = GetComponent<SpriteRenderer>();
        }

        btn = transform.Find("Canvas").GetComponent<Transform>();
    }
    
    public void TelSoundOn()
    {
        SoundManager.instance.F_SoundPlay(SoundManager.instance.npcTeleport, 0.8f);
    }

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