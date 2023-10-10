using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrap : MonoBehaviour
{

    public Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemys>() != null)
            {
                collision.GetComponent<Enemys>().F_Stun_Enemy(3f);
                ani.SetBool("Attack", true);
            }
            else if (collision.GetComponent<Enemis>() != null)
            {
                collision.GetComponent<Enemis>().F_Stun_Enemy(3f);
                ani.SetBool("Attack", true);
            }


        }
    }

    public void F_ReturnTrap()
    {
        gameObject.SetActive(false);
        arrowAttack.Instance.trapQUE.Enqueue(gameObject);
    }
}
