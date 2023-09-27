using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    Vector3 playerVec;
    Transform playerTrs;//수정필요
    Vector3 bulleVec;
    Vector3 dir;
    private Rigidbody2D _Rb;
    private AudioSource Audio;
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
        Audio = GetComponent<AudioSource>();
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
        once = false;
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

    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            StartCoroutine(PlayerDMG(collision));
            //Player sc = collision.gameObject.GetComponent<Player>();
            //StartCoroutine(sc.F_OnHit());

            //PoolManager.Instance.F_ReturnObj(gameObject, "EB");
        }

          else if (collision.gameObject.layer == LayerMask.NameToLayer("Shield") && !once)
        {
            once = true;
            
            GameManager.Instance.Player_CurSP -= 5;
            
            GameObject obj = PoolManager.Instance.F_GetObj("Dust");
            obj.transform.position = gameObject.transform.position;

            PoolManager.Instance.F_ReturnObj(gameObject, "EB");

        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && !once)
        {
            once = true;
            PoolManager.Instance.F_GetObj("Dust");
            GameObject obj = PoolManager.Instance.F_GetObj("Dust");
            obj.transform.position = gameObject.transform.position;
            SoundManager.instance.F_SoundPlay(SoundManager.instance.block, 0.6f);
            PoolManager.Instance.F_ReturnObj(gameObject, "EB");
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("NoWall") && !once)
        {
            once = true;
            PoolManager.Instance.F_GetObj("Dust");
            GameObject obj = PoolManager.Instance.F_GetObj("Dust");
            obj.transform.position = gameObject.transform.position;

            PoolManager.Instance.F_ReturnObj(gameObject, "EB");
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") && !once)
        {
            once = true;
            PoolManager.Instance.F_GetObj("Dust");
            GameObject obj = PoolManager.Instance.F_GetObj("Dust");
            obj.transform.position = gameObject.transform.position;
            SoundManager.instance.F_SoundPlay(SoundManager.instance.block, 0.6f);
            PoolManager.Instance.F_ReturnObj(gameObject, "EB");
           
        }
    }
  
    private void OnBecameInvisible()
    {
        PoolManager.Instance.F_ReturnObj(gameObject, "EB");
    }
    
    IEnumerator PlayerDMG(Collider2D collision)
    {
        Player sc = collision.gameObject.GetComponent<Player>();
        StartCoroutine(sc.F_OnHit());
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSecondsRealtime(1.6f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        
        //PoolManager.Instance.F_ReturnObj(gameObject, "EB");
    }
}

