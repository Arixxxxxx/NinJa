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
        BoomDMG = 3;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(ps == null)
            {
                ps= gameObject.AddComponent<ParticleSystem>();
            }
                       
            
            Enemys sc = collision.gameObject.GetComponent<Enemys>();
            sc.F_OnHIt((int)BoomDMG);
            sc.F_Stun_Enemy(1);
        }

      
    }
}
