using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WhilWIndTriger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int R = Random.Range(0, 2);
            SoundManager.instance.F_SoundPlay(SoundManager.instance.enemyhit[R], 0.5f);
            if (collision.gameObject.GetComponent<Enemys>() != null)
            {
                Enemys sc = collision.gameObject.GetComponent<Enemys>();
                sc.F_OnHIt(SkillManager.instance.whilWindDmg);
            }

            else if (collision.gameObject.GetComponent<Enemis>() != null)
            {
                Enemis sc = collision.gameObject.GetComponent<Enemis>();
                sc.F_OnHIt(SkillManager.instance.whilWindDmg);
            }
            else if (collision.gameObject.GetComponent<Boss>() != null)
            {
                Boss sc = collision.gameObject.GetComponent<Boss>();
                sc.F_OnHIt(SkillManager.instance.whilWindDmg);
            }
        }

        if (collision.CompareTag("Ghost"))
        {
            collision.GetComponent<Ghost>().F_OnHIt(SkillManager.instance.whilWindDmg);
        }

    }

    float WhilWindDmgTimer;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            WhilWindDmgTimer += Time.deltaTime;
            if (WhilWindDmgTimer > SkillManager.instance.whilWindDmgInterval)
            {

                int R = Random.Range(0, 2);
                SoundManager.instance.F_SoundPlay(SoundManager.instance.enemyhit[R], 0.5f);
                WhilWindDmgTimer = 0;

                if (collision.gameObject.GetComponent<Enemys>() != null)
                {
                    Enemys sc = collision.gameObject.GetComponent<Enemys>();
                    sc.F_OnHIt(SkillManager.instance.whilWindDmg);
                }

                else if (collision.gameObject.GetComponent<Enemis>() != null)
                {
                    Enemis sc = collision.gameObject.GetComponent<Enemis>();
                    sc.F_OnHIt(SkillManager.instance.whilWindDmg);
                }
                else if (collision.gameObject.GetComponent<Boss>() != null)
                {
                    Boss sc = collision.gameObject.GetComponent<Boss>();
                    sc.F_OnHIt(SkillManager.instance.whilWindDmg);
                }
            }
        }

        if (collision.CompareTag("Ghost"))
        {
            collision.GetComponent<Ghost>().F_OnHIt(SkillManager.instance.whilWindDmg);
        }
    }
}


