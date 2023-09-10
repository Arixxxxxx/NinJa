using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem movePaticle;
    [SerializeField] ParticleSystem fallPaticle;
    [SerializeField] public ParticleSystem wall;
    [SerializeField] public ParticleSystem iswallPaticle;
    [Range(0, 10)]
    [SerializeField] int createDustVelocity;
    [Range(0, 0.2f)]
    [SerializeField] float dustformation;
    [Range(-20, 20f)]
    [SerializeField] float fallvelo;

    Rigidbody2D PlayerRb;
    bool isGround;

    private void Awake()
    {
        PlayerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        movePaticle = transform.Find("MovePaticle").GetComponent<ParticleSystem>();
        fallPaticle = transform.Find("FallPaticle").GetComponent<ParticleSystem>();
        wall = transform.Find("WallPaticle").GetComponent<ParticleSystem>();
        iswallPaticle = transform.Find("isWallPaticle").GetComponent<ParticleSystem>();
    }
    float conter;

    private void Update()
    {
        isGround = GameManager.Instance.player.isGround;

        conter += Time.deltaTime;

        if (isGround && Mathf.Abs(PlayerRb.velocity.x) > createDustVelocity) 
        {
             if( conter > dustformation)
            {
            
                movePaticle.Play();
                conter = 0;
            }
        }

        
        //else if(Mathf.Abs(PlayerRb.velocity.x) < createDustVelocity)
        //{
        //    movePaticle.Pause();
        //}
    }
    bool ok;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            if (!ok)
            {
                if(PlayerRb.velocity.y < 0.5f)
                {
                    fallPaticle.Play();
                    ok = true;
                }
             
            }
          
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            ok = false;
        }
    }

}
