using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
   
    Transform btn;

    private void Awake()
    {
       
        btn = transform.GetChild(1).GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("¡¯¿‘");
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
