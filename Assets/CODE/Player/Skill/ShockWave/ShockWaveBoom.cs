using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveBoom : MonoBehaviour
{

    Animation Ani;
    
        private void Awake()
    {
        Ani = GetComponent<Animation>();
        
    }

    public void F_OffGameObject()
    {
     
        gameObject.SetActive(false);
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.gameObject.CompareTag("Enemy"))
        {
          if(collision.gameObject.GetComponent<Enemys>() != null)
            {
                Enemys sc = collision.gameObject.GetComponent<Enemys>();
                sc.F_OnHIt(SkillManager.instance.ShockWaveDmg);
                sc.F_Stun_Enemy(1.5f);
            }

            else if (collision.gameObject.GetComponent<Enemis>() != null)
            {
                Enemis sc = collision.gameObject.GetComponent<Enemis>();
                sc.F_OnHIt(SkillManager.instance.ShockWaveDmg);
                sc.F_Stun_Enemy(1.5f);
            }

        }
        if (collision.CompareTag("Ghost"))
        {
            collision.GetComponent<Ghost>().F_OnHIt(SkillManager.instance.ShockWaveDmg);
        }

    }
}
