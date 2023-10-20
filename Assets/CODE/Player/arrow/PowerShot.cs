using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bullet;

public class PowerShot : MonoBehaviour
{
    ParticleSystem Ps;

    private void Awake()
    {
        Ps = GetComponent<ParticleSystem>();
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            if(collision.GetComponent<Enemys>() != null)
            {
                collision.GetComponent<Enemys>().F_OnHIt(SkillManager.instance.electronicShotDmg);
               
            }

            else if (collision.GetComponent<Enemis>() != null)
            {
                collision.GetComponent<Enemis>().F_OnHIt(SkillManager.instance.electronicShotDmg);

            }

            else if (collision.GetComponent<Boss>() != null)
            {
                collision.GetComponent<Boss>().F_OnHIt(SkillManager.instance.electronicShotDmg);

            }

        }

        if (collision.CompareTag("Ghost"))
        {
            collision.GetComponent<Ghost>().F_OnHIt(SkillManager.instance.electronicShotDmg);
        }

        
    }
}

   

