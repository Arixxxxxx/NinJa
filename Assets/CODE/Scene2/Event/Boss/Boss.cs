using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;



public class Boss : MonoBehaviour
{
    Light2D BossLight;
    SpriteRenderer Sr;
    Animator Ani;
    Rigidbody2D Rb;
    BoxCollider2D Box;
    [SerializeField] Transform DmgBox;
    Transform AttackLayer;
    Transform[] BossTel;
    BossPhase1 phase1;

    [SerializeField] Transform[] Effect;
    [SerializeField] GameObject MagicPrefab;
    [SerializeField] Transform MagicTong;
    [SerializeField] TMP_Text UIText;
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
    [SerializeField] private bool isAttking;
    [SerializeField] private bool Dead;

    //플레이어와의 거리
    Vector3 PlayerPos;
    private float toPlayerDis;
    [SerializeField] private float toPlayerDisABSValue;
    [SerializeField] private float toPlayerDir;
    [SerializeField] private float attackRangeDis;
    [SerializeField] private float bossFightTime;



    //시간
    private float stopWatch;

    bool once, once1, once2, Enemy_Hit;


    private void Awake()
    {
        Box = GetComponent<BoxCollider2D>();
        BossLight = GetComponent<Light2D>();
        Rb = GetComponent<Rigidbody2D>();
        Sr = GetComponent<SpriteRenderer>();
        Ani = GetComponent<Animator>();
        AttackLayer = transform.Find("AttackBox").GetComponent<Transform>();
        BossTel = new Transform[transform.parent.Find("ReSpawnPoint").transform.childCount];
        phase1 = transform.parent.transform.Find("Phase1").GetComponent<BossPhase1>();
        for (int i = 0; i < BossTel.Length; ++i)
        {
            BossTel[i] = transform.parent.Find("ReSpawnPoint").GetChild(i).GetComponent<Transform>();
        }

        for (int i = 0; i < 5; ++i)
        {
            GameObject obj = Instantiate(MagicPrefab, transform.position, Quaternion.identity, MagicTong);
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

        //현재층 초기화
        //beforeFloor = curPlayerFloor;

    }




    private void Update()
    {
        if (bossCurHP < 0) { bossCurHP = 0; }

        if (!Dead)
        {
            if (isGameStart)
            {
            
                if (PhaseChange) { return; }
                CenterBossHpBar();
                PhaseChaker();
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
        }
        else if (Dead)
        {
            if (Effect[0].gameObject.activeSelf)
            {
                Effect[0].gameObject.SetActive(false);
            }
            if (Effect[1].gameObject.activeSelf)
            {
                Effect[1].gameObject.SetActive(false);
            }
            if (Effect[2].gameObject.activeSelf)
            {
                Effect[2].gameObject.SetActive(false);
            }

            CenterBossHpBar();
        }
        BossLight.lightCookieSprite = Sr.sprite;
    }
    bool SoundOnce;

    private void CenterBossHpBar()
    {
        if (bossCurHP > 0 && !Dead)
        {
            GameUI.instance.F_BossHPBar(true, bossCurHP, bossMaxHP);
        }
        if (Dead)
        {
            GameUI.instance.F_BossHPBar(false, bossCurHP, bossMaxHP);
        }
    }

    //페이즈 변환 및 증간 폐쇠
    bool phase1ok, phase2ok;
    [SerializeField] bool PhaseChange; // 페이즈전환중 불리언 변수
    bool PhaseTwo; // 2페이즈 일 경우
    bool Patten1, Patten2, Patten3, Patten4;
    [SerializeField] float effectChangeTime;
    private void PhaseChaker()
    {
        if (!PhaseChange)
        {
            bossFightTime += Time.deltaTime;

            if (bossFightTime > 3 && !Patten1)
            {
                Patten1 = true;
                StartCoroutine(CloseFloor(1));
            }
            if (bossFightTime > 15 && !Patten2)
            {
                Patten2 = true;
                StartCoroutine(CloseFloor(3));
            }

            if (bossFightTime > 8 && !Patten3 && PhaseTwo)
            {
                Patten3 = true;
                StartCoroutine(CloseFloor(3));
            }
            if (bossFightTime > 18 && !Patten4 && PhaseTwo)
            {
                Patten4 = true;
                StartCoroutine(CloseFloor(2));
            }
        }


        if (bossCurHP <= (bossMaxHP / 2) && !phase1ok)
        {
            StartCoroutine(PhaseOne());
        }
    }

    IEnumerator PhaseOne()
    {
        while (isBossHide)
        {
            yield return null;
        }

        PhaseChange = true;
        phase1ok = true;
        Rb.velocity = Vector3.zero;
        F_RbFreezX(true);
        CastTimer = 0;
        Ani.SetTrigger("1PhaseStart");
    }
    IEnumerator CloseFloor(int value)
    {

        switch (value)
        {
            case 1:
                UIText.text = $"5초 후 < 1층 > 이 독가스로 폐쇄 됩니다.";
                UIText.gameObject.SetActive(true);
                yield return new WaitForSeconds(7);
                Effect[0].gameObject.SetActive(true);
                UIText.gameObject.SetActive(false);
                break;
            case 2:
                UIText.text = $"5초 후 < 2층 > 이 독가스로 폐쇄 됩니다.";
                UIText.gameObject.SetActive(true);
                yield return new WaitForSeconds(7);
                Effect[1].gameObject.SetActive(true);
                UIText.gameObject.SetActive(false);
                break;
            case 3:
                UIText.text = $"5초 후 < 3층 > 이 독가스로 폐쇄 됩니다.";
                UIText.gameObject.SetActive(true);
                yield return new WaitForSeconds(7);
                Effect[2].gameObject.SetActive(true);
                UIText.gameObject.SetActive(false);
                break;

            case 4:
                UIText.text = $"3초후 모든 층의 독가스가 해제 됩니다";
                UIText.gameObject.SetActive(true);
                yield return new WaitForSeconds(5);
                Effect[0].gameObject.SetActive(false);
                Effect[1].gameObject.SetActive(false);
                Effect[2].gameObject.SetActive(false);
                UIText.gameObject.SetActive(false);
                break;

        }


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
            CastTimer = 0;

            if (!isAttking && !isCurMagicCasting && !isBossHide)
            {
                isAttking = true;
                StartCoroutine(Attack());
            }

        }
    }
    IEnumerator Attack()
    {
        while (isCurMagicCasting)
        {
            yield return null;
        }
        Ani.SetTrigger("Attack");
        AttackLayer.gameObject.SetActive(true);
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
        if (curBossFloor != curPlayerFloor)
        {
            counter += Time.deltaTime;
            if (counter > 2)
            {
                counter = 0;
                F_ExitFloor();
            }
        }
        else if (curBossFloor == curPlayerFloor)
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
            StartCoroutine(ExitFloor());
        }
    }

    IEnumerator ExitFloor() // 순간이동 시작
    {
        while (isCurMagicCasting || isAttking)
        {
            yield return null;
        }

        while (PhaseChange)
        {
            yield return null;
        }

        Rb.velocity = Vector3.zero;
        Ani.SetTrigger("HideOn");
        F_RbFreezX(true);
        Box.enabled = false;
        CastTimer = 0;

        StartCoroutine(FreezXOff());

    }

    [SerializeField] float FreezXFalseTimimng;
    IEnumerator FreezXOff()
    {
        yield return new WaitForSeconds(FreezXFalseTimimng);
        F_RbFreezX(false);
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
                HideOff();

                break;

            case 2:
                int Rand2 = Random.Range(2, 4);
                transform.position = BossTel[Rand2].position;
                yield return new WaitForSeconds(popupIntervalTime);
                HideOff();
                break;
            case 3:
                int Rand3 = Random.Range(4, 6);
                transform.position = BossTel[Rand3].position;
                yield return new WaitForSeconds(popupIntervalTime);
                HideOff();
                break;
        }
    }
    private void HideOff()
    {
        if (!Sr.enabled)
        {
            Sr.enabled = true;
        }
        if (!Box.enabled)
        {
            Box.enabled = true;
        }
        if (!PhaseChange)
        {
            Ani.SetTrigger("HideOff");
        }

        else
        {
            Ani.SetBool("PhaseEnd", true);
        }
        Box.enabled = true;
        isBossHide = false;
        isAttking = false;
        
        CastTimer = 0;


    }

    //하이드 끝 애니함수
    private void A_isHideBoolFalse()
    {
        isBossHide = false;
        if (!Box.enabled)
        {
            Box.enabled = true;
        }
        isCurMagicCasting = false;
        CastTimer = 0;

    }

    //층간이동 이펙트
    //층을 벗어나면 이펙트 생김  (사용안함)
    [SerializeField] int beforeFloor;

    private void FloorChangerEffect()
    {
        if (beforeFloor == curPlayerFloor)
        {
            return;
        }

        else if (beforeFloor != curPlayerFloor && !once1)
        {
            once1 = true;
            StartCoroutine(OnEffect());
        }

    }
    IEnumerator OnEffect()
    {
        yield return new WaitForSeconds(1f);


        switch (beforeFloor)
        {
            case 1:
                beforeFloor = curPlayerFloor;
                once1 = false;
                Effect[0].gameObject.SetActive(true);

                yield return new WaitForSeconds(8);

                Effect[0].gameObject.SetActive(false);
                break;

            case 2:
                beforeFloor = curPlayerFloor;
                once1 = false;
                Effect[1].gameObject.SetActive(true);

                yield return new WaitForSeconds(8);

                Effect[1].gameObject.SetActive(false);
                break;

            case 3:
                beforeFloor = curPlayerFloor;
                once1 = false;
                Effect[2].gameObject.SetActive(true);

                yield return new WaitForSeconds(8);

                Effect[2].gameObject.SetActive(false);
                break;
        }

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
        while (count > 0)
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
        Vector3 Pos = new Vector3(0.5f, 2.6f);
        obj.transform.position = Player.instance.F_Get_PlayrPos() + Pos;
        obj.gameObject.SetActive(true);
    }

    public void F_SetMagic(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        MagicRoom.Enqueue(obj);
    }

    RigidbodyConstraints2D RbFreez;
    private void F_RbFreezX(bool _value)
    {
        switch (_value)
        {
            case true:

                RbFreez = RigidbodyConstraints2D.FreezeAll;
                Rb.constraints = RbFreez;
                break;

            case false:
                RbFreez = RigidbodyConstraints2D.FreezeRotation;
                Box.enabled = true;
                Rb.constraints = RbFreez;
                break;
        }

    }

    //마법관련

    private void MagicCastTimer()
    {
        if (!isAttking && !isBossHide && !PhaseChange)
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
        if (PhaseChange)
        {
            CastTimer = 0;
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
            StartCoroutine(OnDMGColorChanger());

            if (bossCurHP > 0)
            {
                Enemy_Hit = false;
                bossCurHP -= _DMG;


                //데미지폰트
                GameObject obj = PoolManager.Instance.F_GetObj("Text");
                obj.GetComponent<DmgFontCanvus>().F_DmgFont(_DMG, DmgBox.position);

                if (bossCurHP <= 0)
                {
                    //사운드//애니
                    Dead = true;
                    Ani.SetTrigger("Dead");
                    SoundManager.instance.F_SoundPlay(SoundManager.instance.BossHp0, 0.8f);
                    F_RbFreezX(true);
                    GameUI.instance.F_CenterTextPopup("내가 지다니.. 끄...윽...");
                    StartCoroutine(RealDead());
                }

            }
        }
    }
    [SerializeField] float onDmgColorDur;
    IEnumerator OnDMGColorChanger()
    {
        Sr.color = new Color(0.5f, 0.5f, 1, 1);
        yield return new WaitForSeconds(onDmgColorDur);
        Sr.color = new Color(1, 1, 1, 1);

    }
    IEnumerator RealDead()
    {
        yield return new WaitForSeconds(6);
        Ani.SetBool("RealDead", true);
        SoundManager.instance.F_SoundPlay(SoundManager.instance.BossDead, 0.8f);
        Effect[0].gameObject.SetActive(false);
        Effect[1].gameObject.SetActive(false);
        Effect[2].gameObject.SetActive(false);
        Box.enabled = false;
        F_RbFreezX(true);
        yield return new WaitForSeconds(2.5f);
        Sr.enabled = false;
        BossLight.enabled = false;
        transform.parent.Find("Act2EndPortal").gameObject.SetActive(true);

    }

    private void A_EndGame()
    {
        gameObject.SetActive(false);
    }
    private void A_1PhaseStart()
    {
        F_RbFreezX(true);
        Sr.enabled = false;
        BossLight.enabled = false;
        Box.enabled = false;
        StartCoroutine(StartGames());
    }

    public void F_ReSpawnBoss()
    {

        bossFightTime = 0;
        Box.enabled = true;
        moveSpeed += 1.5f;
        CastMagicCount += 2;
        PhaseTwo = true;
        isCurMagicCasting = false;
        BossLight.enabled = true;
        StartCoroutine(Pa());
        StartCoroutine(CloseFloor(4));

        A_HideExit();

    }
    IEnumerator Pa()
    {
        yield return new WaitForSeconds(1.25f);
        F_RbFreezX(false);
    }
    IEnumerator StartGames()
    {
        yield return new WaitForSeconds(1.5f);
        phase1.F_Pahse1Start(true);
    }

    private void A_PhaseEnd()
    {
        PhaseChange = false;
        F_RbFreezX(false);
    }

    private void EndBoss()
    {
        F_RbFreezX(true);
        Sr.enabled = false;
        BossLight.enabled = false;
        Box.enabled = false;
    }

    public bool F_CheakBossAlive()
    {
        return Dead;
    }

    public void F_GameStartBoss()
    {
        isGameStart = true;
    }

    private void HideOffStart()
    {
        Box.enabled = true;
    }
}

