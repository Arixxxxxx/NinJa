using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MiddleBoss : MonoBehaviour
{
    Light2D BossLight;
    SpriteRenderer Sr;
    Animator Ani;
    private void Awake()
    {
        BossLight = GetComponent<Light2D>();
        Sr = GetComponent<SpriteRenderer>();
        Ani = GetComponent<Animator>();
    }

    private void Update()
    {
        BossLight.lightCookieSprite = Sr.sprite;
    }

    public void A_Bye()
    {
        Ani.SetTrigger("Bye");
    }

    public void A_Object_Off()
    {
        gameObject.SetActive(false);
    }
}
