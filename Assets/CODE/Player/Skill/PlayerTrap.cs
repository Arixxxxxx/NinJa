using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public Animator ani;
    public Rigidbody2D Rb;
 
   

    private void Awake()
    {
        ani = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
       
    }

    private void Update()
    {
        transform.right = Rb.velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.trapActive, 1f);
            Enemys sc = collision.transform.GetComponent<Enemys>();
            sc.F_Stun_Enemy(2);
            transform.position = collision.transform.position + new Vector3(0,-0.2f);
            ani.SetBool("Attack", true);

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.trapActive, 1f);
            Enemys sc = collision.transform.GetComponent<Enemys>();
            sc.F_Stun_Enemy(2);
            ani.SetBool("Attack", true);
            

        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Rb.constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    public void F_ReturnTrap()
    {
        ani.SetBool("Attack", false);
        Rb.constraints = RigidbodyConstraints2D.None;
        GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.SetActive(false);
        arrowAttack.Instance.trapQUE.Enqueue(gameObject);
    }
}
