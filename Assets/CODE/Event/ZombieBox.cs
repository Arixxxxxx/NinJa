using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBox : MonoBehaviour
{
    Animator Ani;

    private void Awake()
    {
        Ani = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
  
    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !once)
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.zombieSpawn, 0.8f);
            Emoticon.instance.F_GetEmoticonBox("Angry");
            Ani.SetTrigger("Open");
        }
        
    }

   
}
