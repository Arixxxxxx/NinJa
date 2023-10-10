using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class Ghost : MonoBehaviour
{

    public enum AttackType
    {
        Patrol, OnlyAttack
    }
    public AttackType type;

    public Rigidbody2D Rb;
    SpriteRenderer Sr;
    SpriteRenderer attackSr;
    Animator Ani;

    //적이 없을때
    [Header("# 에너미 정보")]
    [SerializeField] public float CurHp;
    [SerializeField] public float MaxHp;
    [SerializeField] private float MoveSpeed;
    [Space]
    [Header("# 에너미 이동 정보")]
    [Space]
    [SerializeField][Header("# 이동거리")][Range(1f, 9f)] float dist = 7;
    [SerializeField][Header("# 이동속도")][Range(1f, 50f)] float speed = 5;
    [SerializeField][Header("# 파동빈도")][Range(1f, 40f)] float frequency = 20;
    [SerializeField][Header("# 파동높이")][Range(1f, 4f)] float waveheight = 0.5f;
    Vector3 pos;
    Vector3 localscale;
    public bool dirRight = true;

    Vector3 MyCurPos;
    Vector3 PlayerPos;

    [Header("# Gizmo")]
    [SerializeField] private bool ShowRange;
    [SerializeField][Range(0f, 10f)] private float Range = 5f;
    [SerializeField] private Color gizmoColor;

    public RaycastHit2D Target;
    public bool TargetOk;
    Vector2 WallCheakDir;

    DmgPooling dmp;
    HpUi EnemyHpBar;

    SpriteRenderer mark;
    public bool GhostDead;

    // 공격시작
    private bool attackstart;
    Animator Anis;
    bool onlyAttackReady;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Sr = GetComponent<SpriteRenderer>();
        pos = transform.position;
        localscale = transform.localScale;
        EnemyHpBar = transform.GetChild(0).GetComponent<HpUi>();
        mark = transform.Find("SurchPlayer").GetComponent<SpriteRenderer>();
        attackSr = mark.GetComponent<SpriteRenderer>();
        Ani = mark.GetComponent<Animator>();
        Anis = GetComponent<Animator>();

    }

    private void Start()
    {
        dmp = transform.GetComponentInChildren<DmgPooling>(true);
    }
    private void OnEnable()
    {
        this.gameObject.layer = 8;
        GhostDead = false;
        CurHp = MaxHp;
    }

    private void Update()
    {
        Ani.SetBool("Run", attackstart);

        Flipx();
        switch (type)
        {
            case AttackType.Patrol:
                F_SurchPlayer();
                F_EnemyMove();
                break;

            case AttackType.OnlyAttack:
                OnlyAttackFuntion();
                break;

        }



    }
    private void F_SurchPlayer()
    {
        Target = Physics2D.CircleCast(transform.position, Range, Vector2.zero, 0, LayerMask.GetMask("Player", "OnDMG"));
        TargetOk = Target.collider != null ? true : false;
        mark.gameObject.SetActive(TargetOk);

    }
    float onlyAttackCounter;
    private void OnlyAttackFuntion()
    {


        if (!GhostDead)
        {
            if (!onlyAttackReady)
            {
                onlyAttackCounter += Time.deltaTime;
                if (onlyAttackCounter > 2)
                {
                    onlyAttackReady = true;
                }
            }

            if (onlyAttackReady)
            {
                MyCurPos = transform.position;
                PlayerPos = GameManager.Instance.player.transform.position;
                Vector3 dir = PlayerPos - MyCurPos;

                if (Kb)
                {
                    dir = new Vector3(Rb.velocity.x, Rb.velocity.y);
                }

                Rb.velocity = dir.normalized * MoveSpeed;
            }
        }
    }
    private void F_EnemyMove()
    {
        if (!TargetOk && !GhostDead)
        {
            attackstart = false;

            if (transform.position.x > pos.x + dist)
            {
                dirRight = false;
            }
            else if (transform.position.x < pos.x + -dist)
            {
                dirRight = true;
            }

            switch (dirRight)
            {
                case true:
                    Rb.velocity = new Vector3(1 * speed, 1 * Mathf.Sin(Time.time * frequency) * waveheight);
                    break;

                case false:
                    Rb.velocity = new Vector3(-1 * speed, 1 * Mathf.Sin(Time.time * frequency) * waveheight);
                    break;

            }
        }
        else if (TargetOk && !GhostDead)
        {
            if (!attackstart)
            {
                StartCoroutine(AttackStart());
            }

            if (attackstart && !Kb)
            {
                MyCurPos = transform.position;
                PlayerPos = GameManager.Instance.player.transform.position;
                Vector3 dir = PlayerPos - MyCurPos;

              

                    Rb.velocity = dir.normalized * MoveSpeed;
                


            }

        }
    }


    //Player 발견시 딜레이 코르틴함수
    IEnumerator AttackStart()
    {
        Ani.SetTrigger("attack");

        Vector3 dir = PlayerPos - MyCurPos;
         Rb.velocity = dir.normalized;
       
        yield return new WaitForSecondsRealtime(0.7f);
        attackstart = true;
    }
    private void Flipx()
    {
        if (Rb.velocity.x < 0 && !Kb)
        {
            Sr.flipX = false;
            attackSr.flipX = false;

        }
        else if (Rb.velocity.x > 0 && !Kb)
        {
            Sr.flipX = true;
            attackSr.flipX = true;
        }

        if (Sr.flipX)
        {
            WallCheakDir = Vector2.left;
        }
        else if (!Sr.flipX)
        {
            WallCheakDir = Vector2.right;
        }
    }
    bool Enemy_Hit;
    bool Kb;
    [SerializeField] float KBPower;
    public void F_OnHIt(float _DMG)
    {
        if (CurHp > 0 && !Enemy_Hit)
        {
            StartCoroutine(HitOk());
            CurHp -= _DMG;

            GameObject obj = dmp.F_Get_FontBox();
            DMGFont sc = obj.GetComponent<DMGFont>();
            sc.F_FontPopup(_DMG);


            if (!EnemyHpBar.gameObject.activeSelf)
            {
                EnemyHpBar.gameObject.SetActive(true);
            }

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
            EnemyHpBar.Ani.SetTrigger("Hit");

        }

        if (CurHp <= 0)
        {
            Rb.velocity = Vector2.zero;
            EnemyHpBar.gameObject.SetActive(false);
            this.gameObject.layer = 13;
            GhostDead = true;
            Anis.SetBool("Dead", GhostDead);

        }
    }
    IEnumerator HitOk()
    {
        Enemy_Hit = true;
        yield return new WaitForSeconds(0.2f);
        Kb = false;
        Enemy_Hit = false;
        yield return new WaitForSeconds(0.2f);

    }

    public void SetGhostShowOff()
    {
        PoolManager.Instance.F_ReturnObj(gameObject, "Ghost");
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (ShowRange)
        {
            Handles.color = gizmoColor;
            Handles.DrawWireDisc(transform.position, Vector3.forward, Range);
        }

    }
#endif

}
