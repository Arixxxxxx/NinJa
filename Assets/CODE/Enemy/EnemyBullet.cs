using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    Vector3 playerVec;
    Vector3 bulleVec;
    Vector3 dir;
    Rigidbody2D Rb;
    //SpriteRenderer Sr;

    [SerializeField] private float Speed = 5;
    private void Awake()
    {
       
    }
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        playerVec = GameManager.Instance.playerTR.transform.position;
        bulleVec = transform.position;
        dir = playerVec - bulleVec;
        Rb.velocity = dir * Speed;
  

    }
    //private void OnEnable()
    //{
    //    playerVec = GameManager.Instance.playerTR.transform.position;
    //    bulleVec = transform.position;
    //    dir = playerVec - bulleVec;
    //    Rb.velocity = dir * Speed;
    //    Sr.enabled = true;
    //}
    // Update is called once per frame
    void Update()
    {
        BulletMove();


    }

    private void BulletMove()
    {
        transform.right = Rb.velocity;


        Rb.velocity = transform.right * Speed;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            GameManager.Instance.player.F_OnHit();
            PoolManager.Instance.F_ReturnObj(gameObject, "EB");
        }

        //if (collision.gameObject.layer == LayerMask.GetMask("Ground", "Wall"))
        //{
        //    PoolManager.Instance.F_ReturnObj(gameObject, "EB");
        //}

    }
}

