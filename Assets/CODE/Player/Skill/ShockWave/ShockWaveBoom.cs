using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveBoom : MonoBehaviour
{

    Animation Ani;
    ShockWave DMG;
        private void Awake()
    {
        Ani = GetComponent<Animation>();
        DMG = transform.parent.GetComponent<ShockWave>();
    }

    public void F_OffGameObject()
    {
       gameObject.SetActive(false);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.gameObject.CompareTag("Enemy"))
        {
          
            Enemys sc = collision.gameObject.GetComponent<Enemys>();
            sc.F_OnHIt(DMG.DMG);
            sc.F_Stun_Enemy(1.5f);
        }


    }
}
