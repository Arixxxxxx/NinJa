using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float SwordDMG;
    AudioSource Audio;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();


    }

    float dice;
    private void OnCollisionEnter2D(Collision2D collision)
      {
          if (collision.gameObject.CompareTag("Enemy"))
          {
            int R = Random.Range(0, 2);
            Audio.clip = SoundManager.instance.enemyhit[R];
            Audio.Play();
            Enemys sc = collision.gameObject.GetComponent<Enemys>();

            dice = Random.Range(0f, 100f);
            if (dice < SkillManager.instance.MeleePer)
            {
                Player.instance.meleeBuffOn = true;
                arrowAttack.Instance.Rkey.SetBool("Active", true);
            }

            sc.F_OnHIt(SkillManager.instance.MeleeDmg) ;
            
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
