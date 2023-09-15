using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLayer : MonoBehaviour
{
    Transform Sword;
    
    private void Awake()
    {
        Sword = transform.GetChild(0).GetComponent<Transform>();
     
        
    }

    public void AttackOnlayer()
    {
        
        Sword.gameObject.layer = 15;
    }

    public void AttackOfflayer()
    {
        
   
        Sword.gameObject.layer = 16;
    }

    public void AttackEnd()
    {
        GameManager.Instance.player.isAttacking = false;
    }
  
    
}
