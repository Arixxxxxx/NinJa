using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public GameObject ShockWave;
    public GameObject dragonPier;
    public Queue<GameObject> ShockQUE = new Queue<GameObject>();
    public Queue<GameObject> drgonPierQUE = new Queue<GameObject>();

    [Header("# 평타 데미지")]
    public float MeleeDmg;
    public float RangeDmg;

    [Header("# 근접스킬 데미지")]
    public float ShockWaveDmg;
    public float dargonPierDmg;
    public float whilWindDmg;
    public float whilWindDmgInterval;
    public float buffDmg;

    [Header("# 원거리 데미지")]
    public float originElectronicShotDmg;
    public float electronicShotDmg;
    public float tripleShotDmg;
    public float boomShotDmg;
    public float throwTrapDmg;

    [Header("# 스킬 쿨타임")]
    public float ShockWaveCoolTime;
    public float dargonPieCoolTimer;
    public float whilWindCoolTime;
    public float warCryCoolTimer;
    public float electronicShotCoolTime;
    public float tripleShotCoolTime;
    public float boomShotCoolTime;
    public float throwTrapCoolTime;


    private float skill1Timer;
    private float skill2Timer;
    private float skill3Timer;
    private float skill4Timer;
    bool isSkillStartOk;

    Image skill1;
    Image skill2;
    Image skill3, skill4;

    Animator Ani;

    TMP_Text Cool1;
    TMP_Text Cool2;
    TMP_Text Cool3;
    TMP_Text Cool4;


    public ParticleSystem buffPs;


    private void Awake()
    {
        originElectronicShotDmg = electronicShotDmg;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }



        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(ShockWave, transform.position, Quaternion.identity, transform);
            obj.gameObject.SetActive(false);
            ShockQUE.Enqueue(obj);

            GameObject objs = Instantiate(dragonPier, transform.position, Quaternion.identity, transform);
            objs.gameObject.SetActive(false);
            drgonPierQUE.Enqueue(objs);

        }
    }


    private void Start()
    {
       
        buffPs = Player.instance.transform.Find("Paticle/AtkBuff").GetComponent<ParticleSystem>();
        Ani = GameManager.Instance.gameUI.Find("ActionBar/Melee").GetComponent<Animator>();

        whilAudio = Player.instance.transform.Find("Skill").GetComponent<AudioSource>();
        skill1Timer = ShockWaveCoolTime;
        skill2Timer = whilWindCoolTime;
        skill3Timer = dargonPieCoolTimer;
        skill4Timer = warCryCoolTimer;

        skill1 = GameManager.Instance.gameUI.Find("ActionBar/Melee/1/1").GetComponent<Image>();
        skill2 = GameManager.Instance.gameUI.Find("ActionBar/Melee/2/2").GetComponent<Image>();
        skill3 = GameManager.Instance.gameUI.Find("ActionBar/Melee/3/3").GetComponent<Image>();
        skill4 = GameManager.Instance.gameUI.Find("ActionBar/Melee/4/4").GetComponent<Image>();

        Cool1 = GameManager.Instance.gameUI.Find("ActionBar/Melee/1/CoolTime").GetComponent<TMP_Text>();
        Cool2 = GameManager.Instance.gameUI.Find("ActionBar/Melee/2/CoolTime").GetComponent<TMP_Text>();
        Cool3 = GameManager.Instance.gameUI.Find("ActionBar/Melee/3/CoolTime").GetComponent<TMP_Text>();
        Cool4 = GameManager.Instance.gameUI.Find("ActionBar/Melee/4/CoolTime").GetComponent<TMP_Text>();

    }

    private void Update()
    {
        if (!GameManager.Instance.MovingStop || !GameManager.Instance.GameAllStop)
        {
            MeleeSkill1();
            MeleeSkill2();
            MeleeSkill3();
            MeleeSkill4();
        }
        MeleeUnitFrame();
    }

    public bool isSkill1Ok;
    public bool isSkill2Ok;
    public bool isSkill3Ok;
    public bool isSkill4Ok;

    private bool ani1 = true;
    private bool ani2 = true;
    private bool ani3 = true;
    private bool ani4 = true;
    private void MeleeUnitFrame()
    {
        skill1.fillAmount = skill1Timer / ShockWaveCoolTime;
        Cool1.text = (ShockWaveCoolTime - skill1Timer).ToString("F0");

        skill2.fillAmount = skill2Timer / whilWindCoolTime;
        Cool2.text = (whilWindCoolTime - skill2Timer).ToString("F0");

        skill3.fillAmount = skill3Timer / dargonPieCoolTimer;
        Cool3.text = (dargonPieCoolTimer - skill3Timer).ToString("F0");

        skill4.fillAmount = skill4Timer / warCryCoolTimer;
        Cool4.text = (warCryCoolTimer - skill4Timer).ToString("F0");

        if (skill1Timer >= ShockWaveCoolTime)
        {
            isSkill1Ok = true;
            skill1Timer = ShockWaveCoolTime;

            if (!ani1)
            {
                ani1 = true;
                Ani.SetTrigger("1");
                Cool1.gameObject.SetActive(false);
            }

        }
        else if (skill1Timer < ShockWaveCoolTime)
        {
            if (!Cool1.gameObject.activeSelf)
            {
                Cool1.gameObject.SetActive(true);
            }
            isSkill1Ok = false;
            skill1Timer += Time.deltaTime;
        }

        if (skill2Timer >= whilWindCoolTime)
        {
            isSkill2Ok = true;
            skill2Timer = whilWindCoolTime;
            if (!ani2)
            {
                ani2 = true;
                Ani.SetTrigger("2");
                Cool2.gameObject.SetActive(false);
            }
        }
        else if (skill2Timer < whilWindCoolTime)
        {
            if (!Cool2.gameObject.activeSelf)
            {
                Cool2.gameObject.SetActive(true);
            }
            isSkill2Ok = false;
            skill2Timer += Time.deltaTime;
        }

        if (skill3Timer >= dargonPieCoolTimer)
        {
            isSkill3Ok = true;
            skill3Timer = dargonPieCoolTimer;

            if (!ani3)
            {
                ani3 = true;
                Ani.SetTrigger("3");
                Cool3.gameObject.SetActive(false);
            }
        }
        else if (skill3Timer < dargonPieCoolTimer)
        {
            if (!Cool3.gameObject.activeSelf)
            {
                Cool3.gameObject.SetActive(true);
            }
            isSkill3Ok = false;
            skill3Timer += Time.deltaTime;
        }


        if (skill4Timer >= warCryCoolTimer)
        {
            isSkill4Ok = true;
            skill4Timer = warCryCoolTimer;

            if (!ani4)
            {
                ani4 = true;
                Ani.SetTrigger("4");
                Cool4.gameObject.SetActive(false);
            }
        }
        else if (skill4Timer < warCryCoolTimer)
        {
            if (!Cool4.gameObject.activeSelf)
            {
                Cool4.gameObject.SetActive(true);
            }
            isSkill4Ok = false;
            skill4Timer += Time.deltaTime;
        }

    }
    private void MeleeSkill1()
    {
        if (GameManager.Instance.isGetMeleeItem)
        {
            if (GameManager.Instance.meleeMode && !Player.instance.isSkillStartOk && isSkill1Ok)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    arrowAttack.Instance.AttackCameraShake();
                    skill1Timer = 0;
                    ani1 = false;
                    StartCoroutine(SkillPlay());
                    SoundManager.instance.F_SoundPlay(SoundManager.instance.shcokWave, 0.5f);

                    Player.instance.SwordAni.SetTrigger("R");
                    GameObject obj = ShockQUE.Dequeue();
                    obj.transform.position = Player.instance.transform.Find("Skill").transform.position;
                    if (Player.instance.isLeft)
                    {
                        obj.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    obj.gameObject.SetActive(true);
                    obj.GetComponent<ShockWave>().F_ShockWave();
                }
            }

            else if (GameManager.Instance.meleeMode && Input.GetKeyDown(KeyCode.Alpha1) && !isSkill1Ok)
            {
                Player.instance.F_CharText("CoolTime");
            }

        }
    }

    IEnumerator SkillPlay()
    {
        Player.instance.Rb.velocity = Vector3.zero;
        Player.instance.Ani.SetBool("Run", false);
        GameManager.Instance.MovingStop = true;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.MovingStop = false;
    }

    AudioSource whilAudio;
    bool once;
    private void MeleeSkill2()
    {
        if (GameManager.Instance.isGetMeleeItem)
        {
            if (GameManager.Instance.meleeMode)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2) && !Player.instance.isSkillStartOk && isSkill2Ok)
                {
                    if (!once)
                    {
                        once = true;
                        whilAudio.Play();
                    }

                    Player.instance.isWhilWind = true;
                    Player.instance.sheldSR.enabled = false;
                    Player.instance.SwordSr.enabled = false;
                    //Player.instance.gameObject.layer = 10;
                    Player.instance.transform.Find("Skill").GetComponent<BoxCollider2D>().enabled = true;
                    Player.instance.transform.Find("WhilWInd").gameObject.SetActive(true);
                    Player.instance.Ani.SetBool("WhilWind", true);

                }

                else if (Input.GetKeyDown(KeyCode.Alpha2) && !Player.instance.isSkillStartOk && !isSkill2Ok)
                {
                    Player.instance.F_CharText("CoolTime");
                }

            }

            if (GameManager.Instance.meleeMode && Input.GetKey(KeyCode.Alpha2) && !Player.instance.isSkillStartOk && isSkill2Ok)
            {
               
                Player.instance.isWhilWind = true;
                Player.instance.sheldSR.enabled = false;
                Player.instance.SwordSr.enabled = false;
                if (!whilAudio.isPlaying)
                {
                    whilAudio.Play();
                }
            }
            if (GameManager.Instance.meleeMode)
            {
                if (Input.GetKeyUp(KeyCode.Alpha2) && !Player.instance.isSkillStartOk && isSkill2Ok)
                {

                    skill2Timer = 0;
                    ani2 = false;
                    once = false;
                    whilAudio.Stop();
                    Player.instance.isWhilWind = false;
                    Player.instance.sheldSR.enabled = true;
                    Player.instance.SwordSr.enabled = true;
                    //Player.instance.gameObject.layer = 6;
                    Player.instance.Ani.SetBool("WhilWind", false);
                    Player.instance.transform.Find("WhilWInd").gameObject.SetActive(false);
                    Player.instance.transform.Find("Skill").GetComponent<BoxCollider2D>().enabled = false;
                }

            }
        }
    }





    private void MeleeSkill3()
    {
        if (GameManager.Instance.isGetMeleeItem)
        {
            if (GameManager.Instance.meleeMode)
            {
                if (Input.GetKeyDown(KeyCode.Alpha3) && !Player.instance.isSkillStartOk && isSkill3Ok)
                {
                    arrowAttack.Instance.AttackCameraShake();
                    ani3 = false;
                    skill3Timer = 0;
                    StartCoroutine(SkillPlay());
                    SoundManager.instance.F_SoundPlay(SoundManager.instance.dregonPier, 0.4f);
                    GameObject obj = drgonPierQUE.Dequeue();
                    obj.transform.position = Player.instance.transform.Find("Paticle/FallPaticle").transform.position;

                    StartCoroutine(PlayDragonPier(obj));
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3) && !Player.instance.isSkillStartOk && !isSkill3Ok)
                {
                    Player.instance.F_CharText("CoolTime");
                }
            }

        }
    }

    IEnumerator PlayDragonPier(GameObject _obj)
    {
        _obj.gameObject.SetActive(true);
        _obj.GetComponent<ParticleSystem>().Play();

        while (_obj.GetComponent<ParticleSystem>().isPlaying)
        {
            yield return null;
        }
        drgonPierQUE.Enqueue(_obj);
        _obj.gameObject.SetActive(false);
    }


    private void MeleeSkill4()
    {
        if (GameManager.Instance.isGetMeleeItem)
        {
            if (GameManager.Instance.meleeMode)
            {
                if (Input.GetKeyDown(KeyCode.Alpha4) && isSkill4Ok)
                {

                    buff();

                }
                else if (Input.GetKeyDown(KeyCode.Alpha4) && !Player.instance.isSkillStartOk && !isSkill4Ok)
                {
                    Player.instance.F_CharText("CoolTime");
                }


            }

        }
    }

    float o1, o2, o3, o4, o5, o6, o7, o8;
    bool buffOnOff;
    private void buff()
    {
        buffOnOff = !buffOnOff;

        switch (buffOnOff)
        {
            case true:
               
                buffPs.gameObject.SetActive(true);
                buffPs.Play();
                SoundManager.instance.F_SoundPlay(SoundManager.instance.cry, 0.7f);
                skill4.color = new Color(0.5f, 0.5f, 0.5f, 1);
                o1 = MeleeDmg;
                MeleeDmg += buffDmg;

                o2 = RangeDmg;
                RangeDmg += buffDmg;
                o3 = ShockWaveDmg;

                ShockWaveDmg += buffDmg;

                o4 = dargonPierDmg;
                dargonPierDmg += buffDmg;

                o5 = whilWindDmg;
                whilWindDmg += buffDmg;

                o6 = electronicShotDmg;
                electronicShotDmg += buffDmg;

                o7 = tripleShotDmg;
                tripleShotDmg += buffDmg;

                o8 = boomShotDmg;
                boomShotDmg += buffDmg;


                break;

            case false:
                ani4 = false;
                skill4Timer = 0;
                buffPs.gameObject.SetActive(false);
                buffPs.Stop();
                skill4.color = new Color(1, 1, 1, 1);
                MeleeDmg = o1;
                RangeDmg = o2;
                ShockWaveDmg = o3;
                dargonPierDmg = o4;
                whilWindDmg = o5;
                electronicShotDmg = o6;
                tripleShotDmg = o7;
                boomShotDmg = o8;

                break;
        }
    }
}
