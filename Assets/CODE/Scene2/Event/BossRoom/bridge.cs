using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridge : MonoBehaviour
{
    BoxCollider2D Box;

    private void Awake()
    {
        Box = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(Player.instance.Rb.velocity.y > 0.1f)
            {
                Box.isTrigger = true;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Player.instance.Rb.velocity.y > 0.1f)
            {
                StartCoroutine(False());
            }
            
        }
    }

    IEnumerator False()
    {
        yield return new WaitForSeconds(0.3f);
        Box.isTrigger = false;
    }
}
