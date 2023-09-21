using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireTrap : MonoBehaviour
{

    Animator Ani;
    TrapScan trapscan;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        Ani = GetComponent<Animator>();
        trapscan = transform.GetChild(0).GetComponent<TrapScan>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
       
    }
    void Update()
    {
        F_Fire_Trap_OnOff();
    }
    private void F_Fire_Trap_OnOff()
    {
         Ani.SetBool("On", trapscan.Trap_on);
        if (Ani.GetBool("On"))
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.player.F_OnHit();
        }
    }


}
