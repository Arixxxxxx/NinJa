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
    Rigidbody2D Rb;
    bool isGround;
    public float beforeDropSpeed;

    bool ok;
    bool beforeDjump;
    private void Awake()
    {
        PlayerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        movePaticle = transform.Find("MovePaticle").GetComponent<ParticleSystem>();
        fallPaticle = transform.Find("FallPaticle").GetComponent<ParticleSystem>();
        wall = transform.Find("WallPaticle").GetComponent<ParticleSystem>();
        iswallPaticle = transform.Find("isWallPaticle").GetComponent<ParticleSystem>();
        Rb = GetComponent<Rigidbody2D>();
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
        if (GameManager.Instance.player.DJumpOn)
        {
            beforeDjump = true;
        }
        if (!isGround)
        {
            beforeDropSpeed += Time.deltaTime;
        }
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
        
            isGround = true;
            if (!ok)
            {
                if (beforeDropSpeed > fallvelo)
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
            beforeDropSpeed = 0;
        }
    }

}
