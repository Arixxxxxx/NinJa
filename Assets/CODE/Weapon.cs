using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    AudioSource Audio;
    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }

    float dice;
    public bool once;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && !once)
        {
            once = true;
            int R = Random.Range(0, 2);
            Audio.clip = SoundManager.instance.enemyhit[R];
            Audio.Play();

            if (collision.gameObject.GetComponent<Enemys>() != null)
            {
                Enemys sc = collision.gameObject.GetComponent<Enemys>();
                sc.F_OnHIt(SkillManager.instance.MeleeDmg);
            }

            else if (collision.gameObject.GetComponent<Enemis>() != null)
            {
                Enemis sc = collision.gameObject.GetComponent<Enemis>();
                sc.F_OnHIt(SkillManager.instance.MeleeDmg);
            }


            dice = Random.Range(0f, 100f);
            if (dice < SkillManager.instance.MeleePer)
            {
                Player.instance.meleeBuffOn = true;
                arrowAttack.Instance.Rkey.SetBool("Active", true);
            }
        }


        if (collision.gameObject.CompareTag("Ghost"))
        {
            int R = Random.Range(0, 2);
            Audio.clip = SoundManager.instance.enemyhit[R];
            Audio.Play();
            Ghost sc = collision.gameObject.GetComponent<Ghost>();
            sc.F_OnHIt(SkillManager.instance.MeleeDmg);

        }
    }
}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (!once)
    //    {
    //        once = true;
    //        if (collision.gameObject.CompareTag("Enemy"))
    //        {

    //            int R = Random.Range(0, 2);
    //            Audio.clip = SoundManager.instance.enemyhit[R];
    //            Audio.Play();

    //            if (collision.gameObject.GetComponent<Enemys>() != null)
    //            {
    //                Enemys sc = collision.gameObject.GetComponent<Enemys>();
    //                sc.F_OnHIt(SkillManager.instance.MeleeDmg);
    //            }

    //            else if (collision.gameObject.GetComponent<Enemis>() != null)
    //            {
    //                Enemis sc = collision.gameObject.GetComponent<Enemis>();
    //                sc.F_OnHIt(SkillManager.instance.MeleeDmg);
    //            }


    //            dice = Random.Range(0f, 100f);
    //            if (dice < SkillManager.instance.MeleePer)
    //            {
    //                Player.instance.meleeBuffOn = true;
    //                arrowAttack.Instance.Rkey.SetBool("Active", true);
    //            }
    //        }


    //        if (collision.gameObject.CompareTag("Ghost"))
    //        {
    //            int R = Random.Range(0, 2);
    //            Audio.clip = SoundManager.instance.enemyhit[R];
    //            Audio.Play();
    //            Ghost sc = collision.gameObject.GetComponent<Ghost>();
    //            sc.F_OnHIt(SkillManager.instance.MeleeDmg);

    //        }
    //    }
    //}






