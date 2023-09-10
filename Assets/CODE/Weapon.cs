using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float SwordDMG;

      private void OnCollisionEnter2D(Collision2D collision)
      {
          if (collision.gameObject.CompareTag("Enemy"))
          {
            Enemys sc = collision.gameObject.GetComponent<Enemys>();
            sc.F_OnHIt((int)SwordDMG);
          }
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Ghost sc = collision.gameObject.GetComponent<Ghost>();
            sc.F_OnHIt((int)SwordDMG);
        }
    }


 }
