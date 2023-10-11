using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUISprite : MonoBehaviour
{
    Image IM;
    Animator Ani;
    SpriteRenderer Sr;
    private void Awake()
    {
        Sr = GetComponent<SpriteRenderer>();
        IM = GetComponent<Image>();
        Ani = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Ani.SetTrigger("Ok");
    }
    // Update is called once per frame
    void Update()
    {
        IM.sprite = Sr.sprite;
        
    }
}
