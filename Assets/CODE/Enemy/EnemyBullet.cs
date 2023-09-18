using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    Vector3 playerVec;
    Transform playerTrs;//수정필요
    Vector3 bulleVec;
    Vector3 dir;
    private Rigidbody2D _Rb;
    private Rigidbody2D Rb
    {
        get
        {
            if (_Rb == null)
            {
                _Rb = GetComponent<Rigidbody2D>();
            }
            return _Rb;
        }
    }

    //SpriteRenderer Sr;

    [SerializeField] private float Speed = 5;
    private void Awake()
    {

    }
    //void Start()
    //{
    //    Rb = GetComponent<Rigidbody2D>();
    //    playerVec = GameManager.Instance.playerTR.transform.position;
    //    bulleVec = transform.position;
    //    dir = playerVec - bulleVec;
    //    Rb.velocity = dir * Speed;


    //}
    private void Start()
    {
        //playerVec = GameManager.Instance.playerTR.transform.position;
    }

    public void Shoot()
    {
        if (GameManager.Instance == null)
            return;

        bulleVec = transform.position;
        playerTrs = GameManager.Instance.playerTR;
        dir = playerTrs.position - bulleVec;
        //Rb.velocity = dir * Speed;
        float angle = Quaternion.FromToRotation(Vector3.up, dir).eulerAngles.z;
        //Debug.Log(dir);
        transform.localEulerAngles = new Vector3(0, 0, angle);
        Rb.velocity = transform.up * Speed;
    }

    void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        //transform.right = Rb.velocity;
        //Rb.velocity = transform.right * Speed;

        Rb.velocity = transform.up * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            StartCoroutine(collision.GetComponent<Player>().F_OnHit());
            PoolManager.Instance.F_ReturnObj(gameObject, "EB");
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Shield"))
        {
            GameManager.Instance.Player_CurSP -= 5;
            
            GameObject obj = PoolManager.Instance.F_GetObj("Dust");
            obj.transform.position = gameObject.transform.position;

            PoolManager.Instance.F_ReturnObj(gameObject, "EB");

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            
            PoolManager.Instance.F_GetObj("Dust");
            GameObject obj = PoolManager.Instance.F_GetObj("Dust");
            obj.transform.position = gameObject.transform.position;

            PoolManager.Instance.F_ReturnObj(gameObject, "EB");
        }

    }

    private void OnBecameInvisible()
    {
        PoolManager.Instance.F_ReturnObj(gameObject, "EB");
    }
}

