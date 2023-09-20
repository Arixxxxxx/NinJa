using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCanon : MonoBehaviour
{
    RaycastHit2D hit;

    [Range(1f, 20f)][SerializeField] float Radius = 1;
    [SerializeField] private LayerMask target;
    [SerializeField] private bool Attack;
    [SerializeField] private float AttackSpeed;
    Animator aniEye;

    

    private void Awake()
    {
       
        aniEye = transform.GetChild(0).GetComponent<Animator>();
    }



    private void Update()
    {
        AttackTarget();
        scanNPC();
        EyeDir();
    }
    float counter;

    private void EyeDir() 
    {
        bool left = GameManager.Instance.playerTR.position.x < transform.position.x;
        if (left)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void AttackTarget()
    {
        if (!GameManager.Instance.FireStop)
        {
            if (Attack)
            {
                counter += Time.deltaTime;
                if (counter >= AttackSpeed)
                {
                    aniEye.SetTrigger("Attack");
                    counter = 0;
                }
            }
        }
        
        
    }
    private void scanNPC()
    {
        hit = Physics2D.CircleCast(transform.position, Radius, Vector2.zero, 0, target);
        if(hit.collider != null)
        {
            Attack = true;
        }
        else
        {
            Attack =false;
        }
    }



}

