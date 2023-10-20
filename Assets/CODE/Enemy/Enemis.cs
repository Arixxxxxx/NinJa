using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemis : MonoBehaviour
{
    private enum EnemyType
    {
        Sekeleton
    }

    [SerializeField] EnemyType enemyType;

    [Header("# 기본 정보")]
    [SerializeField] float CurHP;
    [SerializeField] float MaxHP;
    [SerializeField] float EnemySpeed;
    [SerializeField] float KBPower;
    [SerializeField] bool isStun;
    [SerializeField] bool isMoveStop;
    [SerializeField] bool Kb;
    [SerializeField] bool isAttackStart;

    [Space]

    //HpUi , 스턴
    Rigidbody2D Rb;
    SpriteRenderer Sr;
    ParticleSystem Ps;
    AudioSource Audio;
    Animator objectAni;
    Animator HpUiAni;
    Transform EnemyHpBar;
    HpUi Hpbar;
    Transform DmgBox;
    Image FrontHp, BackHp;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Ps = transform.Find("Stun").GetComponent<ParticleSystem>();
        Sr = GetComponent<SpriteRenderer>();
        Audio = GetComponent<AudioSource>();
        HpUiAni = transform.Find("EnemyHpUi").GetComponent<Animator>();
        objectAni = GetComponent<Animator>();
        FrontHp = HpUiAni.transform.Find("Hp").GetComponent<Image>();
        BackHp = HpUiAni.transform.Find("Effect").GetComponent<Image>();
        EnemyHpBar = HpUiAni.GetComponent<Transform>();
        Hpbar = EnemyHpBar.GetComponent<HpUi>();
        DmgBox = transform.Find("DmgBox").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        if (!EnemyHpBar.gameObject.activeSelf) { EnemyHpBar.gameObject.SetActive(true); }
        verGravity = new Vector2(Rb.velocity.x, -Physics2D.gravity.y);

        switch(enemyType)
        {
            case EnemyType.Sekeleton:
                CurHP = MaxHP;
                objectAni.SetTrigger("StandUp");
                objectAni.SetBool("Dead", false);
                break;

        }
    }

    Vector2 verGravity;
    [SerializeField] float dropSpeed;
    private void Update()
    {
        switch (enemyType)
        {
            case EnemyType.Sekeleton:
             
                DropSpeed();
                SetHpUi();
                if (isAttackStart)
                {
                    FlipX();
                    if (!isMoveStop)
                    {
                        EnemyMove();
                    }
                }
                CheckMove();
                break;
        }



    }

    Vector2 EnemyVec;

    float dir;

    private void DropSpeed()
    {
        if (Rb.velocity.y < 0)
        {
            Rb.velocity -= verGravity * dropSpeed * Time.deltaTime;
        }
    }
    private void CheckMove()
    {
        if (Enemy_Hit || isStun || Kb)
        {
            isMoveStop = true;


        }
        else if (!Enemy_Hit && !isStun && !Kb)
        {
            isMoveStop = false;
        }
    }
    private void EnemyMove()
    {

        EnemyVec = GameManager.Instance.player.transform.position - transform.position;
        dir = Mathf.Sign(EnemyVec.x);
        if (Kb)
        {
            dir = Rb.velocity.x;
        }
        Rb.velocity = new Vector2(dir, Rb.velocity.y) * EnemySpeed;

    }
    private void FlipX()
    {
        if (!Enemy_Hit)
        {
            if (dir < 0 && !Kb)
            {
                Sr.flipX = false;

            }
            else if (dir > 0 && !Kb)
            {
                Sr.flipX = true;
            }
        }

    }
    public bool Enemy_Hit;

    public void F_OnHIt(float _DMG)
    {
        if (!Enemy_Hit)
        {
            Enemy_Hit = true;
            Rb.velocity = new Vector2(0,Rb.velocity.y);

            if (CurHP > 0)
            {
                StartCoroutine(OneHit());
                HpUiAni.SetTrigger("Hit");
                CurHP -= _DMG;

                if (!Sr.flipX)
                {
                    Kb = true;
                    Rb.AddForce(new Vector3(2 * KBPower, 2), ForceMode2D.Impulse);
                }
                else if (Sr.flipX)
                {
                    Kb = true;
                    Rb.AddForce(new Vector3(-2 * KBPower, 2), ForceMode2D.Impulse);
                }

                GameObject obj = PoolManager.Instance.F_GetObj("Text");
                obj.GetComponent<DmgFontCanvus>().F_DmgFont(_DMG, DmgBox.position);

                Invoke("KbOff", 0.15f);

                if (CurHP <= 0)
                {
                    switch (enemyType)
                    {
                        case EnemyType.Sekeleton:
                            ExpManager.instance.F_SetExp(ExpManager.instance.EnemyExp["S"]);
                            SoundManager.instance.F_SoundPlay(SoundManager.instance.skeletonDead, 0.7f);
                            EnemyHpBar.gameObject.SetActive(false);
                            StartCoroutine(Dead());
                            break;
                    }
                  
                }

            }
        }
    }

    private void KbOff()
    {
        Kb = false;
    }
    IEnumerator Dead()
    {
        isAttackStart = false;
        Rb.velocity = Vector3.zero;
        objectAni.SetBool("Dead", true);
        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
        yield return new WaitForSeconds(1.5f);
        PoolManager.Instance.F_ReturnObj(gameObject, "Skele");


    }
    IEnumerator OneHit()
    {
        yield return new WaitForSeconds(0.2f);
        Enemy_Hit = false;
    }


    //스턴
    public void F_Stun_Enemy(float _duration)
    {
        StartCoroutine(Holding(_duration));
    }

    IEnumerator Holding(float _duration)
    {
        isStun = true;
        Ps.gameObject.SetActive(true);
        yield return new WaitForSeconds(_duration);
        isStun = false;
        Ps.gameObject.SetActive(false);
    }


    [Range(0f,5f)][SerializeField] float hpBackBarSpeed;
    private void SetHpUi()
    {
        FrontHp.fillAmount = CurHP / MaxHP;

        if (BackHp.fillAmount > FrontHp.fillAmount)
        {
            BackHp.fillAmount -= hpBackBarSpeed * Time.deltaTime;
        }
        else if (BackHp.fillAmount <= FrontHp.fillAmount)
        {
            BackHp.fillAmount = FrontHp.fillAmount;
        }
    }

    //해골맨 함수
    public void A_StartSound()
    {
        SoundManager.instance.F_SoundPlay(SoundManager.instance.skeletonPopup, 0.6f);
    }
    public void A_MoveEnemy()
    {
        objectAni.SetBool("Attack", true);
        isAttackStart = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (Kb)
            {
 
                Kb = false;
            }
        }
    }

}



