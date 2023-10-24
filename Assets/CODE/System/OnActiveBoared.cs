using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnActiveBoared : MonoBehaviour
{
    Transform Btn;
    private void Awake()
    {
        Btn = transform.Find("Btn").GetComponent<Transform>();
    }

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Btn.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Btn.gameObject.SetActive(false);
        }
    }

}
