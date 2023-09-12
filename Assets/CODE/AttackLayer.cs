using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLayer : MonoBehaviour
{
    Transform Sword;
    public TrailRenderer trail;

    private void Awake()
    {
        Sword = transform.GetChild(0).GetComponent<Transform>();
        trail = Sword.GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    public void AttackOnlayer()
    {
        trail.enabled = true;
        Sword.gameObject.layer = 15;
    }

    public void AttackOfflayer()
    {
        trail.enabled = false;
        trail.Clear();
        Sword.gameObject.layer = 16;
    }

    public void trailclear()
    {
        trail.Clear();
    }
    public void AttackEnd()
    {
        GameManager.Instance.player.isAttacking = false;
    }
  
    
}
