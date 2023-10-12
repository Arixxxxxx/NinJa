using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TilemapObject : MonoBehaviour
{

    Animator Front;
    SpriteRenderer[] FrontSr;
    private void Start()
    {
        Front = GetComponent<Animator>();
        FrontSr = transform.GetComponentsInChildren<SpriteRenderer>();
    }

    int lastLength;
    [SerializeField] float FadeSpeed;
    private void F_FrontObjectSpriteColoADown()
    {
        Debug.Log("¤±¤±");
        for (int i = 0; i < FrontSr.Length; i++)
        {
            if (lastLength == 0)
            {
                lastLength = FrontSr.Length;
            }
            FrontSr[i].color -= new Color(0, 0, 0, 0.2f) * FadeSpeed * Time.deltaTime;
        }

        if (FrontSr[lastLength - 1].color.a > 0.65f)
        {
            Invoke("F_FrontObjectSpriteColoADown", 0.1f);
        }
        else if (FrontSr[lastLength - 1].color.a < 0.65f)
        {
            CancelInvoke();
            for (int i = 0; i < FrontSr.Length; i++)
            {
                FrontSr[i].color = new Color(1, 1, 1, 0.65f);
            }
        }
    }
    bool once;

    private void F_FrontObjectSpriteColoAUp()
    {
        Debug.Log("¤¡¤¡");
        for (int i = 0; i < FrontSr.Length; i++)
        {
            if (lastLength == 0)
            {
                lastLength = FrontSr.Length;
            }

            FrontSr[i].color += new Color(0, 0, 0, 0.2f) * FadeSpeed * Time.deltaTime;
        }

        if (FrontSr[lastLength - 1].color.a < 0.95f)
        {
            Invoke("F_FrontObjectSpriteColoAUp", 0.1f);
        }
        else if (FrontSr[lastLength - 1].color.a > 0.95f)
        {
            CancelInvoke();
            for (int i = 0; i < FrontSr.Length; i++)
            {
                FrontSr[i].color = new Color(1, 1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!once)
            {
                once = true;
                F_FrontObjectSpriteColoADown();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (once)
            {
                once = false;
                F_FrontObjectSpriteColoAUp();
            }
        }
    }
}
