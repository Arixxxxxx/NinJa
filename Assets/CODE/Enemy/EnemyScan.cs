using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScan : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.parent.GetComponent<Enemys>() != null)
            {
                Enemys sc = transform.parent.GetComponent<Enemys>();
                sc.F_AttackTrigger(true);
            }
            else if(transform.parent.GetComponent<Enemis>() != null)
            {

            }
        }
       
    }
}
