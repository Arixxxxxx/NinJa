using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Bullet;

public class arrowAttack : MonoBehaviour
{
    public static arrowAttack Instance;
    



    [Header("일반화살")]
    [SerializeField] GameObject Arrow;
    public float normalShootSpeed;

    [SerializeField] GameObject boomArrow;
    [SerializeField] GameObject Boom;
    [SerializeField] GameObject tripleArrow;
    [SerializeField] GameObject powerShot;
    [SerializeField] GameObject playerTrap;
    [SerializeField] Transform m_Arrow;
    [SerializeField] Transform BowPos;
    [SerializeField] Transform ArrowTong;
    private Transform tong;
    Camera maincam;
    Queue<GameObject> ArrowBox = new Queue<GameObject>();
    Queue<GameObject> boomArrowQUE = new Queue<GameObject>();
    public Queue<GameObject> boomQUE = new Queue<GameObject>();
    Queue<GameObject> TripleArrowQUE = new Queue<GameObject>();
    public Queue<GameObject> trapQUE = new Queue<GameObject>();
    public Queue<GameObject> powerQUE = new Queue<GameObject>();

    private Transform powerGaugeBar;
    private Image bar;


    public float curTime;
    Animator FillAni;

    int originCamSize;

    Image skill1, skill2, skill3, skill4;
    TMP_Text Cool1, Cool2, Cool3, Cool4;
    Animator Ani;

    Image SpecialBuffBar;
    Image SpecialSide;
    Image skillCase;
    Animator Rkey;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        maincam = Camera.main;
        tong = transform.Find("Tong").GetComponent<Transform>();


        for (int i = 0; i < 30; i++)
        {

            GameObject obj = Instantiate(Arrow, transform.position, Quaternion.identity, ArrowTong);
            obj.SetActive(false);
            ArrowBox.Enqueue(obj);

            GameObject objs = Instantiate(tripleArrow, transform.position, Quaternion.identity, ArrowTong);
            objs.SetActive(false);
            TripleArrowQUE.Enqueue(objs);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(boomArrow, transform.position, Quaternion.identity, ArrowTong);
            obj.SetActive(false);
            boomArrowQUE.Enqueue(obj);

            GameObject objs = Instantiate(Boom, transform.position, Quaternion.identity, ArrowTong);
            objs.SetActive(false);
            boomQUE.Enqueue(objs);

            GameObject trapObj = Instantiate(playerTrap, transform.position, Quaternion.identity, ArrowTong);
            trapObj.SetActive(false);
            trapQUE.Enqueue(trapObj);

            GameObject powerObj = Instantiate(powerShot, transform.position, Quaternion.identity, ArrowTong);
            powerObj.SetActive(false);
            powerQUE.Enqueue(powerObj);

        }





    }
    private void Start()
    {
        // 파워바
        powerGaugeBar = Player.instance.transform.Find("PowerGaugeBar").GetComponent<Transform>();
        powerGaugeBar.gameObject.SetActive(false);
        bar = powerGaugeBar.transform.GetChild(1).GetComponent<Image>();

        skill1 = GameManager.Instance.gameUI.Find("ActionBar/Range/1/1").GetComponent<Image>();
        skill2 = GameManager.Instance.gameUI.Find("ActionBar/Range/2/2").GetComponent<Image>();
        skill3 = GameManager.Instance.gameUI.Find("ActionBar/Range/3/3").GetComponent<Image>();
        skill4 = GameManager.Instance.gameUI.Find("ActionBar/Range/4/4").GetComponent<Image>();

        Cool1 = skill1.transform.parent.Find("CoolTime").GetComponent<TMP_Text>();
        Cool2 = skill2.transform.parent.Find("CoolTime").GetComponent<TMP_Text>();
        Cool3 = skill3.transform.parent.Find("CoolTime").GetComponent<TMP_Text>();
        Cool4 = skill4.transform.parent.Find("CoolTime").GetComponent<TMP_Text>();

        Ani = GameManager.Instance.gameUI.Find("ActionBar/Range").GetComponent<Animator>();

        skill1Timer = SkillManager.instance.electronicShotCoolTime;
        skill2Timer = SkillManager.instance.tripleShotCoolTime;
        skill3Timer = SkillManager.instance.boomShotCoolTime;
        skill4Timer = SkillManager.instance.throwTrapCoolTime;

        //스페셜 스킬
        SpecialBuffBar = GameManager.Instance.gameUI.Find("ActionBar/SpecialSkill/Circle/SideBar/Color").GetComponent<Image>();
        SpecialBuffBar.gameObject.SetActive(false);
        SpecialSide = SpecialBuffBar.transform.parent.GetComponent<Image>();
        skillCase = GameManager.Instance.gameUI.Find("ActionBar/SpecialSkill/Circle/Front/R/Case").GetComponent<Image>();
        originAttackSpeed = normalShootSpeed;

        Rkey = GameManager.Instance.gameUI.Find("ActionBar/SpecialSkill/RKey").GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.Instance.MovingStop || !GameManager.Instance.GameAllStop)
        {
            LookAtMouse();
            ArrowFire();
            TripleArrow();
            ThrowTrap();
            PowerBarNoScale();
            PowerShot();
            RangeUnitFrame();
            SpecialSkill();
        }

    }

    private float skill1Timer;
    private float skill2Timer;
    private float skill3Timer;
    private float skill4Timer;

    private bool isSkill1Ok;
    private bool isSkill2Ok;
    private bool isSkill3Ok;
    private bool isSkill4Ok;

    private bool ani1, ani2, ani3, ani4;
    private void RangeUnitFrame()
    {
        skill1.fillAmount = skill1Timer / SkillManager.instance.electronicShotCoolTime;
        Cool1.text = (SkillManager.instance.electronicShotCoolTime - skill1Timer).ToString("F0");

        skill2.fillAmount = skill2Timer / SkillManager.instance.tripleShotCoolTime;
        Cool2.text = (SkillManager.instance.tripleShotCoolTime - skill2Timer).ToString("F0");

        skill3.fillAmount = skill3Timer / SkillManager.instance.boomShotCoolTime;
        Cool3.text = (SkillManager.instance.boomShotCoolTime - skill3Timer).ToString("F0");

        skill4.fillAmount = skill4Timer / SkillManager.instance.throwTrapCoolTime;
        Cool4.text = (SkillManager.instance.throwTrapCoolTime - skill4Timer).ToString("F0");

        if (skill1Timer >= SkillManager.instance.electronicShotCoolTime)
        {
            isSkill1Ok = true;
            skill1Timer = SkillManager.instance.electronicShotCoolTime;


            if (!ani1)
            {
                ani1 = true;
                Ani.SetTrigger("1");
                SkillManager.instance.electronicShotDmg = SkillManager.instance.originElectronicShotDmg;
                Cool1.gameObject.SetActive(false);
            }

        }
        else if (skill1Timer < SkillManager.instance.electronicShotCoolTime)
        {
            if (!Cool1.gameObject.activeSelf)
            {
                Cool1.gameObject.SetActive(true);
            }
            isSkill1Ok = false;
            skill1Timer += Time.deltaTime;
        }

        if (skill2Timer >= SkillManager.instance.tripleShotCoolTime)
        {
            isSkill2Ok = true;
            skill2Timer = SkillManager.instance.tripleShotCoolTime;
            if (!ani2)
            {
                ani2 = true;
                Ani.SetTrigger("2");

                Cool2.gameObject.SetActive(false);
            }
        }
        else if (skill2Timer < SkillManager.instance.tripleShotCoolTime)
        {
            if (!Cool2.gameObject.activeSelf)
            {
                Cool2.gameObject.SetActive(true);
            }
            isSkill2Ok = false;
            skill2Timer += Time.deltaTime;
        }

        if (skill3Timer >= SkillManager.instance.boomShotCoolTime)
        {
            isSkill3Ok = true;
            skill3Timer = SkillManager.instance.boomShotCoolTime;

            if (!ani3)
            {
                ani3 = true;
                Ani.SetTrigger("3");
                Cool3.gameObject.SetActive(false);
            }
        }
        else if (skill3Timer < SkillManager.instance.boomShotCoolTime)
        {
            if (!Cool3.gameObject.activeSelf)
            {
                Cool3.gameObject.SetActive(true);
            }
            isSkill3Ok = false;
            skill3Timer += Time.deltaTime;
        }

        if (skill4Timer >= SkillManager.instance.throwTrapCoolTime)
        {
            isSkill4Ok = true;
            skill4Timer = SkillManager.instance.throwTrapCoolTime;

            if (!ani4)
            {
                ani4 = true;
                Ani.SetTrigger("4");
                Cool4.gameObject.SetActive(false);
            }
        }
        else if (skill4Timer < SkillManager.instance.throwTrapCoolTime)
        {
            if (!Cool4.gameObject.activeSelf)
            {
                Cool4.gameObject.SetActive(true);
            }
            isSkill4Ok = false;
            skill4Timer += Time.deltaTime;
        }
    }
    private void PowerBarNoScale()
    {
        if (Player.instance.GetComponent<Transform>().transform.localScale.x < 0)
        {
            powerGaugeBar.localScale = new Vector3(-0.01f, 0.01f, 1);
        }
        else
        {
            powerGaugeBar.localScale = new Vector3(0.01f, 0.01f, 1);
        }
    }
    private void LookAtMouse()
    {
        if (GameManager.Instance.isGetRangeItem)
        {
            Vector2 mousePos = maincam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = new Vector2(mousePos.x - m_Arrow.position.x, mousePos.y - m_Arrow.position.y);
            m_Arrow.right = dir;
            transform.position = GameManager.Instance.player.transform.position;

            //에임조준중일때 플레이어 캐릭터 보는방향 컨트롤해줄수잇게 마우스좌표x값으로 방향 bool 저장
            if (mousePos.x < GameManager.Instance.player.transform.position.x)
            {
                GameManager.Instance.AimLeft = true;
            }
            else if (mousePos.x > GameManager.Instance.player.transform.position.x)
            {
                GameManager.Instance.AimLeft = false;
            }
        }

    }

    
    float dice;
    float originAttackSpeed;
    [SerializeField] private float buffPer;
    private void ArrowFire()
    {
        if (GameManager.Instance.isGetRangeItem)
        {
            curTime += Time.deltaTime;

            if (curTime > normalShootSpeed)
            {
                if (Input.GetMouseButton(0) && !GameManager.Instance.meleeMode && GameManager.Instance.rangeMode && GameManager.Instance.player.RealBow.gameObject.activeSelf)
                {
                    //특수기술 확률
                    dice = Random.Range(0, 100f);
                    if(dice < buffPer && !BuffOn)
                    {
                        BuffOn = true;
                        Rkey.SetBool("Active", true);
                        buffCounter = buffMaxTime;
                        SpecialSide.fillAmount = buffCounter / buffMaxTime;
                        skillCase.fillAmount = 1 - (buffCounter / buffMaxTime);
                        SpecialBuffBar.gameObject.SetActive(true);
                    }

                    GameObject obj = F_GetArrow(0);
                    SoundManager.instance.F_SoundPlay(SoundManager.instance.rangeAttak, 1f);

                    obj.transform.position = BowPos.position;
                    obj.transform.rotation = m_Arrow.rotation;
                    obj.GetComponent<Rigidbody2D>().velocity = obj.transform.right * 15f;
                    curTime = 0;

                }
            }

            if (Input.GetMouseButton(0) && !GameManager.Instance.player.RealBow.gameObject.activeSelf && GameManager.Instance.rangeMode)
            {
                Player.instance.F_CharText("ActiveBow");
            }

        }

    }

    float powerShotPower;
    public float powerMaxPower;
    bool Psonce;
    private void PowerShot()
    {
        if (GameManager.Instance.isGetRangeItem && GameManager.Instance.rangeMode)
        {
            if (Input.GetKey(KeyCode.Alpha1) && GameManager.Instance.player.RealBow.gameObject.activeSelf && isSkill1Ok)
            {
                if (!Psonce)
                {
                    Psonce = true;
                    Player.instance.powerShotPs.gameObject.SetActive(true);
                    Player.instance.powerShotPs.Play();
                }

                F_PowerGaugeBar(powerShotPower, powerMaxPower);
                powerShotPower += Time.deltaTime * 7;
                if (!soundOk)
                {
                    soundOk = true;
                    SoundManager.instance.F_SoundPlay(SoundManager.instance.Start, 1f);
                }

                if (powerShotPower > powerMaxPower)
                {
                    powerShotPower = powerMaxPower;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) && !Player.instance.isSkillStartOk && !isSkill1Ok)
            {
                Player.instance.F_CharText("CoolTime");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && !GameManager.Instance.player.RealBow.gameObject.activeSelf)
            {
                Player.instance.F_CharText("ActiveBow");
            }

            if (Input.GetKeyUp(KeyCode.Alpha1) && GameManager.Instance.player.RealBow.gameObject.activeSelf && isSkill1Ok)
            {
                StartCoroutine(Shake());
                Psonce = false;
                Player.instance.powerShotPs.Stop();
                Player.instance.powerShotPs.gameObject.SetActive(false);
                soundOk = false;
                ani1 = false;
                skill1Timer = 0;
                if (powerShotPower < powerMaxPower)
                {
                    SoundManager.instance.F_SoundPlay(SoundManager.instance.elecSmall, 1f);
                }
                else if (powerShotPower >= powerMaxPower)
                {
                    SkillManager.instance.electronicShotDmg *= 2;
                    SoundManager.instance.F_SoundPlay(SoundManager.instance.elecLarge, 1f);
                }




                powerGaugeBar.gameObject.SetActive(false);
                GameObject obj = powerQUE.Dequeue();
                PowerAdd(obj, powerShotPower); // Scale값 조정

                obj.transform.position = BowPos.position;
                obj.transform.rotation = m_Arrow.rotation;
                obj.gameObject.SetActive(true);
                ParticleSystem Ps = obj.GetComponent<ParticleSystem>();
                Ps.Play();
                StartCoroutine(EndPs(Ps));
                powerShotPower = 0;
            }
        }
    }

    bool isShaking;
    public IEnumerator Shake()
    {
        if (!isShaking)
        {
            isShaking = true;
            GameManager.Instance.CameraShakeSwitch(0);
            yield return new WaitForSeconds(0.2f);
            GameManager.Instance.CameraShakeSwitch(1);
        }
        isShaking = false;
    }
    Vector3 OriginPowerScale;
    private void PowerAdd(GameObject _obj, float _gauge)
    {
        OriginPowerScale = _obj.transform.localScale;
        Vector3 add = new Vector3(OriginPowerScale.x * (1 + (_gauge * 0.05f)), OriginPowerScale.y * (1 + (_gauge * 0.05f)));
        _obj.transform.localScale = add;
    }
    IEnumerator EndPs(ParticleSystem Ps)
    {
        while (Ps.isPlaying)
        {
            yield return null;
        }
        Ps.Stop();
        Ps.gameObject.transform.localScale = OriginPowerScale;
        Ps.gameObject.SetActive(false);
        powerQUE.Enqueue(Ps.gameObject);
    }


    public float shootPower;
    public float MaxPower;
    bool soundOk;

    private void TripleArrow()
    {
        if (GameManager.Instance.isGetRangeItem && GameManager.Instance.rangeMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && GameManager.Instance.player.RealBow.gameObject.activeSelf && isSkill2Ok)
            {
                skill2Timer = 0;
                ani2 = false;
                StartCoroutine(ArrowSpawn());
                GameManager.Instance.Player_CurMP -= 5;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && GameManager.Instance.player.RealBow.gameObject.activeSelf && !isSkill2Ok)
            {
                Player.instance.F_CharText("CoolTime");
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && GameManager.Instance.player.RealBow.gameObject.activeSelf && !isSkill3Ok)
            {
                Player.instance.F_CharText("CoolTime");
            }
            if (Input.GetKey(KeyCode.Alpha3) && GameManager.Instance.player.RealBow.gameObject.activeSelf && isSkill3Ok)
            {
                F_PowerGaugeBar(shootPower, MaxPower);
                shootPower += Time.deltaTime * 7;
                if (!soundOk)
                {
                    soundOk = true;
                    SoundManager.instance.F_SoundPlay(SoundManager.instance.Start, 1f);
                }

                if (shootPower > MaxPower)
                {
                    shootPower = MaxPower;
                }
            }


            if (Input.GetKeyUp(KeyCode.Alpha3) && GameManager.Instance.player.RealBow.gameObject.activeSelf && isSkill3Ok)
            {
                ani3 = false;
                skill3Timer = 0;
                soundOk = false;
                SoundManager.instance.F_SoundPlay(SoundManager.instance.Shoot, 1f);
                //GameManager.Instance.CameraShakeSwitch(1);
                powerGaugeBar.gameObject.SetActive(false);
                GameObject obj = F_GetArrow(ArrowType.boomArrow);
                //SoundManager.instance.F_SoundPlay(SoundManager.instance.rangeAttak, 1f);

                obj.transform.position = BowPos.position;
                obj.transform.rotation = m_Arrow.rotation;
                obj.GetComponent<Rigidbody2D>().velocity = obj.transform.right * shootPower;
                shootPower = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !GameManager.Instance.player.RealBow.gameObject.activeSelf)
            {
                Player.instance.F_CharText("ActiveBow");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && !GameManager.Instance.player.RealBow.gameObject.activeSelf)
            {
                Player.instance.F_CharText("ActiveBow");
            }

        }
    }

    IEnumerator ArrowSpawn()
    {
        ArrowOnsShot();
        yield return new WaitForSeconds(0.15f);
        ArrowOnsShot();
        yield return new WaitForSeconds(0.15f);
        ArrowOnsShot();
    }

    private void ThrowTrap()
    {
        if (GameManager.Instance.isGetRangeItem && GameManager.Instance.rangeMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4) && isSkill4Ok && GameManager.Instance.player.RealBow.gameObject.activeSelf)
            {
                ani4 = false;
                skill4Timer = 0;
                SoundManager.instance.F_SoundPlay(SoundManager.instance.trapThrow, 1f);
                GameObject trap = trapQUE.Dequeue();
                trap.gameObject.SetActive(true);
                trap.transform.position = BowPos.position;
                trap.transform.rotation = m_Arrow.rotation;
                trap.GetComponent<Animator>().SetTrigger("Throw");
                trap.GetComponent<Rigidbody2D>().velocity = trap.transform.right * 5;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && GameManager.Instance.player.RealBow.gameObject.activeSelf && !isSkill4Ok)
            {
                Player.instance.F_CharText("CoolTime");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && !GameManager.Instance.player.RealBow.gameObject.activeSelf)
            {
                Player.instance.F_CharText("ActiveBow");
            }
        }
    }
    private void ArrowOnsShot()
    {
        GameObject obj = F_GetArrow(ArrowType.triple);

        SoundManager.instance.F_SoundPlay(SoundManager.instance.tripleShot, 0.3f);

        obj.transform.position = BowPos.position;
        obj.transform.rotation = m_Arrow.rotation;
        //ParticleSystem aa = obj.transform.GetChild(0).GetComponent<ParticleSystem>();
        //aa.Play();
        obj.GetComponent<Rigidbody2D>().velocity = obj.transform.right * 18f;
    }

    bool BuffOn;
    [SerializeField] float buffMaxTime;
    [SerializeField] float buffCounter;
    bool once1;
    private void SpecialSkill()
    {
        if (GameManager.Instance.isGetRangeItem)
        {
            if (!GameManager.Instance.rangeMode || GameManager.Instance.meleeMode)
            {
                SpecialSide.fillAmount = 0;
                skillCase.fillAmount = 1;
                Rkey.SetBool("Active", false);
                normalShootSpeed = originAttackSpeed;
                buffCounter = 0;
                SkillManager.instance.RangeDmg -= 1;
                SpecialBuffBar.gameObject.SetActive(false);
                Player.instance.RangeBuff.Stop();
                Player.instance.RangeBuff.gameObject.SetActive(false);
                BuffOn = false;
                once1 = false;
            }
        }
      

        if (BuffOn)
        {
            if (Input.GetKeyDown(KeyCode.R) && GameManager.Instance.rangeMode)
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.cry, 0.8f);
                Player.instance.RangeBuff.gameObject.SetActive(true);
                Player.instance.RangeBuff.Play();
                Rkey.SetBool("Active", false);
                buffCounter = buffMaxTime;
             
                if (!once1)
                {
                    SkillManager.instance.RangeDmg += 1;
                    normalShootSpeed = normalShootSpeed / 2;
                }

                
                StartCoroutine(Timer());
                
              
            }
            
        }
    }

    IEnumerator Timer()
    {
        while (buffCounter > 0.05f)
        {
            
            buffCounter -= Time.deltaTime;
            SpecialSide.fillAmount = buffCounter / buffMaxTime;
            skillCase.fillAmount = 1 - (buffCounter / buffMaxTime);
            yield return null;
        }

        SpecialSide.fillAmount = 0;
        skillCase.fillAmount = 1;
        normalShootSpeed = originAttackSpeed;
        buffCounter = 0;
        SkillManager.instance.RangeDmg -= 1;
        SpecialBuffBar.gameObject.SetActive(false);
        Player.instance.RangeBuff.Stop();
        Player.instance.RangeBuff.gameObject.SetActive(false);
        BuffOn = false;
        once1 = false;
        
    }

   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_value">0=노말,1=트리블,2붐화살,3붐</param>
    /// <returns></returns>
    public GameObject F_GetArrow(ArrowType type)
    {
        GameObject arrow = null;

        switch (type)
        {
            case ArrowType.normal:

                arrow = ArrowBox.Dequeue();
                arrow.SetActive(true);
                return arrow;

                

            case ArrowType.triple:

                arrow = TripleArrowQUE.Dequeue();
                arrow.SetActive(true);
                return arrow;
               

            case ArrowType.boomArrow:
                arrow = boomArrowQUE.Dequeue();
                arrow.SetActive(true);
                return arrow;
              



            default: return null;
        }

    }
    public void AttackCameraShake()
    {
        StartCoroutine(Shake());
    }

    public GameObject F_Get_Boom()
    {
        GameObject obj = boomQUE.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public void F_Set_Boom(GameObject obj)
    {
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(false);
        boomQUE.Enqueue(obj);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="_value">0=노말,1=트리블,2붐화살,3붐</param>
    public void F_SetArrow(GameObject obj, ArrowType type)
    {
        switch (type)
        {
            case ArrowType.normal:
                obj.transform.position = transform.position;
                ArrowBox.Enqueue(obj);
                break;

            case ArrowType.triple:
                obj.transform.position = transform.position;
                TripleArrowQUE.Enqueue(obj);
                break;

            case ArrowType.boomArrow:
                obj.transform.position = transform.position;
                boomArrowQUE.Enqueue(obj);

                break;
            case ArrowType.boom:
                obj.transform.position = transform.position;
                boomQUE.Enqueue(obj);
                break;

        }

    }


    private void F_PowerGaugeBar(float _curPower, float _maxPower)
    {
        bar.fillAmount = 0;
        powerGaugeBar.gameObject.SetActive(true);
        bar.fillAmount = _curPower / _maxPower;

    }


}
