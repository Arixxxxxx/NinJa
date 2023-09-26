using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheld : MonoBehaviour
{
    AudioSource Audio;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("¤³¤·");
            Audio.clip = SoundManager.instance.block;
            Audio.Play();
        }
           
    }
    //[SerializeField] private float Power;
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Enemy"))
    //    {
    //        if (!GameManager.Instance.player.isLeft)
    //        {
    //            GameManager.Instance.player.KB = true;
    //            GameManager.Instance.player.Rb.AddForce(new Vector2(-2f, 0) * Power, ForceMode2D.Impulse);
    //        }

    //        else if (GameManager.Instance.player.isLeft)
    //        {
    //            GameManager.Instance.player.KB = true;
    //            GameManager.Instance.player.Rb.AddForce(new Vector2(2f, 0) * Power, ForceMode2D.Impulse);
    //        }
    //    }
    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        GameManager.Instance.player.KB = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        GameManager.Instance.player.KB = false;
    //    }
    //}
}
