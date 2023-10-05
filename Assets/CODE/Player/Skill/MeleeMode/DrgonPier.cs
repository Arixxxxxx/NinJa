using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrgonPier : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
     

        if (collision.gameObject.CompareTag("Enemy"))
        {
      
            Enemys sc = collision.gameObject.GetComponent<Enemys>();
            sc.F_OnHIt((int)SkillManager.instance.dargonPierDmg);
            sc.F_Stun_Enemy(1);
        }


    }
}
