using System.Collections;
using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class RangeZone : MonoBehaviour
{
    [SerializeField] private GameObject Eagles;
    [SerializeField] private float Speed= 0.5f;
   private Transform spawnPoint1, spawnPoint2;
    private Transform EventUi;
   public bool gameStart;
    TMP_Text KillText;
    Animator chairBoom;
    float count = 3.5f;
    float countTimer;
    float total;
    //���� ������
    int makeEagleEA;
    bool spawnStart;
    AudioSource Audio;

    bool barOpen;
    bool isSoundOk;
    private void Awake()
    {
        spawnPoint1 = transform.Find("SpawnPoint1").GetComponent<Transform>();
        spawnPoint2 = transform.Find("SpawnPoint2").GetComponent<Transform>();

        EventUi = transform.Find("UI").GetComponent<Transform>();
        KillText = EventUi.transform.Find("KillCount").GetComponent<TMP_Text>();
        chairBoom = transform.Find("Chair").GetComponent<Animator>();

        Audio = GetComponent<AudioSource>();
        makeEagleEA = 10;
        Audio.volume = 0.5f;
    }

    private void Update()
    {
        Event();
    }
    bool once;
    bool once1;
    float Timer;
    [Range(1f,3f)][SerializeField] float reSapwnTime;
    public void Event()
    {
        if (gameStart && !barOpen)
        {
            GameManager.Instance.EventTimeBar.gameObject.SetActive(true);
            GameManager.Instance.TimeText.text = $"���� ������ : {GameManager.Instance.curEagle} / {GameManager.Instance.totalDeathEagle}";
            float value = GameManager.Instance.curEagle / GameManager.Instance.totalDeathEagle;
            if (value < GameManager.Instance.TimeBar.fillAmount)
            {
                GameManager.Instance.TimeBar.fillAmount -= Time.deltaTime * Speed;
            }
        }

        if (gameStart && makeEagleEA > 0)
        {
            //����ī��Ʈ

            
            countTimer += Time.deltaTime;
            total = count - countTimer;
            if(total > 0)
            {
                KillText.text = (total + 1).ToString("F0");
                if (!isSoundOk)
                {
                    isSoundOk = true;
                    StartCoroutine(SoundStart());
                }
            }
            if(total < 0 && !once1)
            {
                once1 = true;
                StartCoroutine(StartGame());  //���⼭ ���� ������
            }
                        
            //��������
            if (spawnStart )
            {
                Timer += Time.deltaTime;
                if (Timer > reSapwnTime)
                {
                    Timer = 0f;
                    makeEagleEA--;
                    GameObject obj = Instantiate(Eagles, spawnPoint1.position, Quaternion.identity, transform);
                    Eagle sc = obj.transform.GetChild(0).GetComponent<Eagle>();
                    if (sc.moveType == 0)
                    {
                        obj.transform.position = spawnPoint1.position;
                    }
                    else if (sc.moveType == 1)
                    {
                        obj.transform.position = spawnPoint2.position;
                    }
                }
            }
         
            
        }
        else if (GameManager.Instance.deathEagleConter == GameManager.Instance.totalDeathEagle)
        {
            gameStart = false;
            GameManager.Instance.legStop = false;
            if (!once)
            {
                once = true;
                StartCoroutine(EndEvent());
            }
            
        }
    }

    IEnumerator SoundStart()
    { 
        //�������� 1�� �ȸ¾Ƽ� �Ϸο���
        yield return new WaitForSecondsRealtime(1f);
        
        Audio.Play();
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        KillText.text = "Start";
        yield return new WaitForSecondsRealtime(0.6f);
        spawnStart = true;
        KillText.text = string.Empty;
    }
  
    // ���� �ݶ��̴� ���� �����ͼ� ����
    public void StartEvent(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ReadyForEvent());
        }

    }
    // �̺�Ʈ �����غ�
    IEnumerator ReadyForEvent()
    {
        gameStart = true;
        GameManager.Instance.legStop = true;
        yield return new WaitForSecondsRealtime(1);
        GameManager.Instance.player.Char_Vec.x = 0;
        GameManager.Instance.player.Rb.velocity = Vector3.zero;
        GameManager.Instance.player.Ani.SetBool("Run", false);
        EventUi.gameObject.SetActive(true);

    }
    
    // �̺�Ʈ ����
    IEnumerator EndEvent()
    {
        GameManager.Instance.npc2.transform.Find("TalkCheak").gameObject.SetActive(true);
        GameManager.Instance.battleNPCiD.ID++;
        
        yield return new WaitForSecondsRealtime(0.2f);
        SoundManager.instance.F_SoundPlay(SoundManager.instance.questComplete, 1f);
        KillText.text = "�̼� �Ϸ�!";
        Emoticon.instance.F_GetEmoticonBox("Smile");

        yield return new WaitForSecondsRealtime(0.5f);
        chairBoom.SetTrigger("Boom");
        chairBoom.transform.GetComponent<BoxCollider2D>().enabled = false;
       

        yield return new WaitForSecondsRealtime(1f);
        barOpen = true;
        GameManager.Instance.EventTimeBar.gameObject.SetActive(false);
        GameManager.Instance.TimeBar.fillAmount = 1;
        GameManager.Instance.TimeText.text = string.Empty;



      
        yield return new WaitForSecondsRealtime(1);
        EventUi.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        gameObject.SetActive(false);
        
    }

}

