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
            Ani.SetTrigger("Open");
        }
        
    }

   
}
