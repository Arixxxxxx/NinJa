using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    DMGFont DMG_Font;

    //좀비들 스크립트

    SpriteRenderer Sr;
    Rigidbody2D Rb;
    public float EnemySpeed;
    public float EnemyMaxSpeed;
    public float CurHP;
    public float MaxHp;
    public HpUi EnemyHpBar;
    AudioSource Audio;

    Transform[] Bloody;

    bool isEnemyDead;
    Animator Ani;

    Vector2 EnemyVec;
    Vector2 NextMove;

    DmgPooling dmp;
    DMGFont dmpText;

    private void Awake()
    {
        Sr = GetComponent<SpriteRenderer>();
        Rb = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
        Bloody = new Transform[6];
        EnemyHpBar = transform.GetChild(6).GetComponent<HpUi>();
        EnemyHpBar.gameObject.SetActive(false);
        DMG_Font = transform.GetComponentInChildren<DMGFont>(true);
        Audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        dmp = transform.GetChild(7).GetComponent<DmgPooling>();
        //dmpText = dmp.transform.GetComponentInChildren<DMGFont>();

    }
    void Update()
    {
        if (!GameManager.Instance.NpcSprite.gameObject.activeSelf)
        {
            F_FlipX();
            F_ToTargetMove();
            F_VeloLimit();
        }
        else if(GameManager.Instance.NpcSprite.gameObject.activeSelf)
        {
            Rb.velocity = Vector2.zero;
        }
       
    }


    //Enemy 팝업 실행
    private void OnEnable()
    {
        CurHP = MaxHp;
        gameObject.layer = 8;
        isEnemyDead = false;

    }

    //Enemy 피격
    bool Enemy_Hit;
    bool KB;
    public float KBSpeed;
    public void F_OnHIt(int _DMG)
    {
        if (CurHP > 0 && !Enemy_Hit)
        {
            StartCoroutine(HitOk());
            CurHP -= _DMG;
            
            GameObject obj = dmp.F_Get_FontBox();
            DMGFont sc = obj.GetComponent<DMGFont>();
            sc.F_FontPopup(_DMG);


            Ani.SetTrigger("Hit");

            if (!EnemyHpBar.gameObject.activeSelf)
            {
                EnemyHpBar.gameObject.SetActive(true);
            }
            
            EnemyHpBar.Ani.SetTrigger("Hit");

            //피가튐
            if (CurHP <= 0)
            {
                int R = Random.Range(0, 4);
                Audio.clip = SoundManager.instance.enemyDie[R];
                Audio.Play();
                if(gameObject.name == "Q1")
                {
                    GameManager.Instance.Q1++;
                }

                for (int i = 0; i < 6; i++)
                {
                    Bloody[i] = transform.GetChild(i).GetComponent<Transform>();
                    Bloody[i].gameObject.SetActive(true);
                }
                gameObject.layer = 13;
                isEnemyDead = true;
                Ani.SetBool("Dead", isEnemyDead);
                EnemyHpBar.gameObject.SetActive(false);

            }
        }
    }
    IEnumerator HitOk()
    {
        Enemy_Hit = true;
        yield return new WaitForSeconds(0.1f);
        Enemy_Hit = false;
        yield return new WaitForSeconds(0.2f);
        KB = false;
    }


    //Enemy 최대속력제어
    private void F_VeloLimit()
    {
        if (Rb.velocity.x > EnemyMaxSpeed && !KB)
        {
            Rb.velocity = new Vector2(EnemyMaxSpeed, Rb.velocity.y);
        }
        else if (Rb.velocity.x < EnemyMaxSpeed * (-1))
        {
            Rb.velocity = new Vector2(EnemyMaxSpeed * (-1), Rb.velocity.y);
        }
    }

    //플레이어대상 움직임함수
    private void F_ToTargetMove()
    {
        if (!isEnemyDead)
        {
            EnemyVec = GameManager.Instance.player.transform.position - transform.position;
            NextMove.x = Mathf.Sign(EnemyVec.x) * EnemySpeed * Time.deltaTime;

            Rb.AddForce(NextMove, ForceMode2D.Force);
        }

    }

    //회전 플립
    private void F_FlipX()
    {
        if (EnemyVec.x < 0)
        {
            Sr.flipX = true;
        }
        else if (EnemyVec.x > 0)
        {
            Sr.flipX = false;
        }
    }

    // 리턴
    public void ReturnQueue()
    {
        gameObject.layer = 8;
        PoolManager.Instance.F_ReturnObj(gameObject, "Enemy");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player sc = collision.gameObject.GetComponent<Player>();
            StartCoroutine(sc.F_OnHit());

        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (CurHP > 0 && !Enemy_Hit)
            {
                Audio.clip = SoundManager.instance.rangeHit;
                Audio.Play();
            }

        }

        if (collision.gameObject.CompareTag("Weapon"))
        {
            if (Sr.flipX && !KB)
            {
                KB = true;
                Rb.AddForce(new Vector2(1 * KBSpeed, 0), ForceMode2D.Impulse);

            }
            else if (!Sr.flipX && !KB)
            {
                KB = true;
                Rb.AddForce(new Vector2(-1 * KBSpeed, 0), ForceMode2D.Impulse);
            }
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            if (CurHP > 0 && !Enemy_Hit)
            {
                Audio.clip = SoundManager.instance.meleeAttack;
                Audio.Play();
            }
            F_OnHIt(1);
        }
    }
}

