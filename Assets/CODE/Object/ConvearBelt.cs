using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvearBelt : MonoBehaviour
{
    [SerializeField] private float Speed = 1.0f;
    Rigidbody2D PlayerRb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            PlayerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
               if(PlayerRb != null)
               {
                //PlayerRb.AddForce(Vector3.right * Speed * Time.deltaTime, ForceMode2D.Force);
                //PlayerRb.MovePosition(PlayerRb.position + Vector2.right * Speed * Time.deltaTime) ;
                PlayerRb.velocity = Vector3.right * Speed;
               }
               
             
           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
