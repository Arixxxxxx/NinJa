using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;



public class Boss : MonoBehaviour
{
    Light2D BossLight;
    SpriteRenderer Sr;
    Animator Ani;
    Rigidbody2D Rb;
    Transform DmgBox;
    Transform AttackLayer;
    Transform[] BossTel;
    

    [SerializeField] GameObject MagicPrefab;
    [SerializeField] Transform MagicTong;
    Queue<GameObject> MagicRoom = new Queue<GameObject>();

    [Header("#보스 스탯 및 이동관련")]
    [SerializeField] private float bossAttackPower;
    [SerializeField] private float bossCurHP;
    [SerializeField] private float bossMaxHP;
    [SerializeField] private float moveSpeed;
    [Space]
    [Header("#보스 마법시전 관련")]
    float CastTimer;
    [SerializeField] float CastWaitTime;
    [SerializeField] private int CastMagicCount;
    [SerializeField] private float CastMagicInterval;
    [Space]
    [Header("#보스 및 플레이어 현재 위치 층수")]
    [SerializeField] private int curPlayerFloor;
    [SerializeField] private int curBossFloor;

    [Space]
    [Header("#보스 상태")]
    // 게임시작
    [SerializeField] private bool isGameStart;
    [SerializeField] private bool Run;
    [SerializeField] private bool isCurMagicCasting;
    [SerializeField] public bool isBossHide;
    [SerializeField] private float popupIntervalTime;
    private bool isAttking;

    //플레이어와의 거리
    Vector3 PlayerPos;
    private float toPlayerDis;
    [SerializeField] private float toPlayerDisABSValue;
    [SerializeField] private float toPlayerDir;
    [SerializeField] private float attackRangeDis;



    //시간
    private float stopWatch;

    bool once, once1, once2, Enemy_Hit;


    private void Awake()
    {
        BossLight = GetComponent<Light2D>();
        Rb = GetComponent<Rigidbody2D>();
        Sr = GetComponent<SpriteRenderer>();
        Ani = GetComponent<Animator>();
        DmgBox = transform.Find("DmgBox").GetComponent<Transform>();
        AttackLayer = transform.Find("AttackBox").GetComponent<Transform>();
        BossTel = new Transform[transform.parent.Find("ReSpawnPoint").transform.childCount];
        for (int i = 0; i < BossTel.Length; ++i)
        {
            BossTel[i] = transform.parent.Find("ReSpawnPoint").GetChild(i).GetComponent<Transform>();
        }

        for(int i = 0; i < 5; ++i)
        {
            GameObject obj = Instantiate(MagicPrefab, transform.position, Quaternion.identity,MagicTong);
            obj.gameObject.SetActive(false);
            MagicRoom.Enqueue(obj);
        }
    }
    private void Start()
    {
        if (AttackLayer.gameObject.activeSelf)
        {
            AttackLayer.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        if (isGameStart)
        {
            Ani.SetBool("Run", Run);

            StopWatchStart();
            SurchPlayer();
            AttackAndMove();
            MagicCastTimer();

            if (!isBossHide)
            {
                if (!isCurMagicCasting) // 마법 캐스팅중아닐떄
                {
                    BossAction();
                    ReSurchPlayer();
                }
            }

        }

        if(Input.GetKeyDown(KeyCode.H)) 
        {
            A_CastingMagic();
        }

        BossLight.lightCookieSprite = Sr.sprite;
    }

    private void StopWatchStart()
    {
        if (isGameStart)
        {
            stopWatch += Time.deltaTime;

            if (!once)
            {
                once = true;
                Ani.SetBool("GameStart", true);
            }
        }
    }

    private void SurchPlayer()
    {
        PlayerPos = Player.instance.F_Get_PlayrPos();
        toPlayerDis = PlayerPos.x - transform.position.x;
        toPlayerDisABSValue = Mathf.Abs(toPlayerDis);
        toPlayerDir = Mathf.Sign(toPlayerDis);

        if (toPlayerDir > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (toPlayerDir < 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    // 근접공격 
    private void AttackAndMove()
    {
        
        if (toPlayerDisABSValue > attackRangeDis)
        {
            Run = true;
        }
        else if (toPlayerDisABSValue <= attackRangeDis)
        {
            Run = false;
            if (!isAttking && !isCurMagicCasting && !isBossHide)
            {
                isAttking = true;
                Ani.SetTrigger("Attack");
                AttackLayer.gameObject.SetActive(true);
            }

        }
    }

   
    //보스 이동
    private void BossAction()
    {
        if (Run)
        {
            if (isAttking) { return; }

            Rb.velocity = new Vector2(toPlayerDir, Rb.velocity.y) * moveSpeed;
        }
        if (!Run)
        {
            Rb.velocity = Vector2.zero;
        }
       
    }

   

    bool AttackTel;
    public void F_AttackLayerCheck(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(collision.GetComponent<Player>().F_OnHit());
            AttackTel = true;


        }
    }
    private void A_AttackOff()
    {
        isAttking = false;
        AttackLayer.gameObject.SetActive(false);
        if (AttackTel)
        {
            F_ExitFloor();
            AttackTel = false;
        }

    }


    float counter;
    //해당층에없다면 다시 찾기
    private void ReSurchPlayer()
    {
        if(curBossFloor != curPlayerFloor)
        {
            counter += Time.deltaTime;
            if (counter > 2)
            {
                counter = 0;
                F_ExitFloor();
            }
        }
        else if(curBossFloor == curPlayerFloor) 
        {
            counter = 0;
        }
    }

    public void F_SurchFloow(SurchFloor.FloorType type, string _SValue)
    {
        switch (_SValue)
        {
            case "P":

                switch (type)
                {
                    case SurchFloor.FloorType.one:
                        curPlayerFloor = 1;
                        break;

                    case SurchFloor.FloorType.two:
                        curPlayerFloor = 2;
                        break;

                    case SurchFloor.FloorType.three:
                        curPlayerFloor = 3;
                        break;
                }

                break;

            case "B":

                switch (type)
                {
                    case SurchFloor.FloorType.one:
                        curBossFloor = 1;
                        break;

                    case SurchFloor.FloorType.two:
                        curBossFloor = 2;
                        break;

                    case SurchFloor.FloorType.three:
                        curBossFloor = 3;
                        break;
                }
                break;

        }


        
    }

    // 순간이동 시작
    public void F_ExitFloor()
    {
        if (!isBossHide)
        {
            isBossHide = true;
            //Rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            Rb.velocity = Vector3.zero;
            Ani.SetTrigger("HideOn");
            CastTimer = 0;
        }

    }
    //순간이동 복귀
    private void A_HideExit()
    {
        StartCoroutine(FloorPosChange());
    }
    IEnumerator FloorPosChange()
    {
        Sr.enabled = false;
        yield return new WaitForSeconds(0.2f);

        switch (curPlayerFloor)
        {
            case 1:
                int Rand1 = Random.Range(0, 2);
                transform.position = BossTel[Rand1].position;
                yield return new WaitForSeconds(popupIntervalTime);
                Sr.enabled = true;
                Ani.SetTrigger("HideOff");
                isBossHide = false;
                isAttking = false;
                CastTimer = 0;
                
                break;

            case 2:
                int Rand2 = Random.Range(2, 4);
                transform.position = BossTel[Rand2].position;
                yield return new WaitForSeconds(popupIntervalTime);
                Sr.enabled = true;
                Ani.SetTrigger("HideOff");
                isBossHide = false;
                isAttking = false;
                CastTimer = 0;
              
                break;
            case 3:
                int Rand3 = Random.Range(4, 6);
                transform.position = BossTel[Rand3].position;
                yield return new WaitForSeconds(popupIntervalTime);
                Sr.enabled = true;
                Ani.SetTrigger("HideOff");
                isBossHide = false;
                isAttking = false;
                CastTimer = 0;
             
                break;
        }
    }

    //하이드 끝 애니함수
    private void A_isHideBoolFalse()
    {
        isBossHide = false;
    }

    // 마법구현
    private void A_CastingMagic()
    {
        StartCoroutine(Cast(CastMagicCount));
        Rb.velocity = Vector3.zero;
        
    }
    IEnumerator Cast(int value)
    {
        int count = value;
        while(count > 0)
        {
            count--;
            CastMagin();
            yield return new WaitForSeconds(CastMagicInterval);
        }
    }
    private void CastMagin()
    {
        if (MagicRoom.Count == 0)
        {
            GameObject objs = Instantiate(MagicPrefab, transform.position, Quaternion.identity, MagicTong);
            objs.gameObject.SetActive(false);
            MagicRoom.Enqueue(objs);
        }

        GameObject obj = MagicRoom.Dequeue();
        Vector3 Pos =  new Vector3(0.5f,2.6f);
        obj.transform.position = Player.instance.F_Get_PlayrPos() + Pos;
        obj.gameObject.SetActive(true);
    }

    public void F_SetMagic(GameObject obj)
    {
         obj.gameObject.SetActive(false);
         MagicRoom.Enqueue(obj);
    }

    //마법관련

  

    private void MagicCastTimer()
    {
        if (!isAttking && !isBossHide)
        {
            if (Run && !isCurMagicCasting)
            {
                CastTimer += Time.deltaTime;
                if (CastTimer > CastWaitTime)
                {
                    CastTimer = 0;
                    Ani.SetTrigger("Cast");
                }
            }
            else if (!Run)
            {
                CastTimer = 0;
            }
        }
    }
     
    private void A_MagicCasting()
    {
        A_CastingMagic();
    }
    private void A_CastingOnOff()
    {
        if (isCurMagicCasting)
        {
            isCurMagicCasting = false;
        }
        else if (!isCurMagicCasting)
        {
            isCurMagicCasting = true;
        }
    }

    //보스 피격
    public void F_OnHIt(float _DMG)
    {
        if (!Enemy_Hit)
        {
            Enemy_Hit = true;
            Rb.velocity = new Vector2(0, Rb.velocity.y);

            if (bossCurHP > 0)
            {
                Enemy_Hit = false;
                Ani.SetTrigger("DMG"); //' 애니 뒤에 히트 불 false 처리'
                bossCurHP -= _DMG;


                //데미지폰트
                GameObject obj = PoolManager.Instance.F_GetObj("Text");
                obj.GetComponent<DmgFontMove>().F_DmgFont(_DMG, DmgBox);

                if (bossCurHP <= 0)
                {
                    SoundManager.instance.F_SoundPlay(SoundManager.instance.skeletonDead, 0.7f);
                }

            }
        }
    }
}

