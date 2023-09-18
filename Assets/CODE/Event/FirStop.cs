using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireStop : MonoBehaviour
{

    bool once;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            
            once = true;
            GameManager.Instance.FireStop = true;
        }
    }
   
}
