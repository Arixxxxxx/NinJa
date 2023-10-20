using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomArrow : MonoBehaviour
{
    private float BoomDMG = 3;
    bool once;
    ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        EndPs();
    }

    private void EndPs()
    {
        if (!ps.isPlaying)
        {
            ps.Stop();
            arrowAttack.Instance.F_Set_Boom(gameObject);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(ps == null)
            {
                ps= gameObject.AddComponent<ParticleSystem>();
            }
                       
            if(collision.gameObject.GetComponent<Enemys>() != null)
            {
                Enemys sc = collision.gameObject.GetComponent<Enemys>();
                sc.F_OnHIt(SkillManager.instance.boomShotDmg);
                sc.F_Stun_Enemy(1);
            }
            else if (collision.gameObject.GetComponent<Enemis>() != null)
            {
                Enemis sc = collision.gameObject.GetComponent<Enemis>();
                sc.F_OnHIt(SkillManager.instance.boomShotDmg);
                sc.F_Stun_Enemy(1);
            }
            else if (collision.gameObject.GetComponent<Boss>() != null)
            {
                Boss sc = collision.gameObject.GetComponent<Boss>();
                sc.F_OnHIt(SkillManager.instance.boomShotDmg);
                
            }
        }

        if (collision.CompareTag("Ghost"))
        {
            collision.GetComponent<Ghost>().F_OnHIt(SkillManager.instance.boomShotDmg);
        }
    }
}
