using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapObject : MonoBehaviour
{

    Animator Front;

    private void Start()
    {
        Front = transform.parent.transform.Find("Front").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Front.SetBool("Fade", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !Front.GetBool("Fade"))
        {
            Front.SetBool("Fade", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Front.SetBool("Fade", false);
        }
    }
}
