using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Rigidbody2D Rb;
    private SpriteRenderer Sr;
    private Vector3 Right;
    private Vector3 RightRo;
    private Vector3 Left;
    private Vector3 LeftRo;
    private Animator Ani;
    private int Aex_DMG;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Sr = GetComponent<SpriteRenderer>();
        Ani = GetComponent<Animator>();

        Right = new Vector3(0.50f, 0.1f); //390도
        RightRo = new Vector3(0, 0, 390);
        Left = new Vector3(-0.6f, -0.08f); //360도
        LeftRo = new Vector3(0, 0, 352);

        //무기
        Aex_DMG = 3;
    }
    float CurTime;
    private void Update()
    {
        F_WeaponePosition();
    }
    private void F_WeaponePosition()
    {
        CurTime += Time.deltaTime;
        if (GameManager.Instance.player.Sr.flipX)
        {
            transform.position = GameManager.Instance.player.transform.position + Left;
            transform.rotation = Quaternion.Euler(LeftRo);
            Sr.flipX = true;
            Ani.SetBool("R", false);
            Ani.SetBool("L", true);


            if (Input.GetMouseButton(0) && CurTime > 0.4f)
            {
                gameObject.layer = 15;
                Ani.SetTrigger("LAttack");
                CurTime = 0;
                Invoke("F_LayerReturn", 0.4f);
            }

        }
        else if (!GameManager.Instance.player.Sr.flipX)
        {
            transform.position = GameManager.Instance.player.transform.position + Right ;
            transform.rotation = Quaternion.Euler(RightRo);
            Sr.flipX = false;
            Ani.SetBool("L", false);
            Ani.SetBool("R", true);
            if (Input.GetMouseButton(0)&& CurTime > 0.4f)
            {
                gameObject.layer = 15;
                Ani.SetTrigger("RAttack");
                CurTime = 0;
                Invoke("F_LayerReturn", 0.4f);
            }

        }
    }
   private void F_LayerReturn()
    {
        gameObject.layer = 16;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemys sc = collision.gameObject.GetComponent<Enemys>();
            sc.F_OnHIt(Aex_DMG);
        }
    }
}
