using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrgonPier : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
     

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.GetComponent<Enemys>() != null)
            {
                Enemys sc = collision.gameObject.GetComponent<Enemys>();
                sc.F_OnHIt((int)SkillManager.instance.dargonPierDmg);
                sc.F_Stun_Enemy(1);
            }
            
            else if (collision.gameObject.GetComponent<Enemis>() != null)
            {
                Enemis sc = collision.gameObject.GetComponent<Enemis>();
                sc.F_OnHIt((int)SkillManager.instance.dargonPierDmg);
                sc.F_Stun_Enemy(1);

            }
            else if (collision.gameObject.GetComponent<Boss>() != null)
            {
                Boss sc = collision.gameObject.GetComponent<Boss>();
                sc.F_OnHIt((int)SkillManager.instance.dargonPierDmg);

            }
        }
        if (collision.CompareTag("Ghost"))
        {
            collision.GetComponent<Ghost>().F_OnHIt(SkillManager.instance.dargonPierDmg);
        }

    }
}
