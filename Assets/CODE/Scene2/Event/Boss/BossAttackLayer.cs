using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackLayer : MonoBehaviour
{
    Boss sc;
    private void Start()
    {
        sc = transform.parent.GetComponent<Boss>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sc.F_AttackLayerCheck(collision);
            gameObject.SetActive(false);
        }
      
        
    }
}
