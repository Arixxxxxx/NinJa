using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
using Unity.VisualScripting;

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
    //생성 마리수
    int makeEagleEA;
    bool spawnStart;

    private void Awake()
    {
        spawnPoint1 = transform.Find("SpawnPoint1").GetComponent<Transform>();
        spawnPoint2 = transform.Find("SpawnPoint2").GetComponent<Transform>();
        EventUi = transform.Find("UI").GetComponent<Transform>();
        KillText = EventUi.transform.Find("KillCount").GetComponent<TMP_Text>();
        chairBoom = transform.Find("Chair").GetComponent<Animator>();


        makeEagleEA = 10;
  
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
        if (gameStart && makeEagleEA > 0)
        {
            GameManager.Instance.EventTimeBar.gameObject.SetActive(true);
            GameManager.Instance.TimeText.text = $"남은 마리수 : {GameManager.Instance.curEagle} / {GameManager.Instance.totalDeathEagle}";
            float value = GameManager.Instance.curEagle / GameManager.Instance.totalDeathEagle;
            if(value < GameManager.Instance.TimeBar.fillAmount)
            {
                GameManager.Instance.TimeBar.fillAmount -= Time.deltaTime * Speed;
            }

            countTimer += Time.deltaTime;
            total = count - countTimer;
            if(total > 0)
            {
                KillText.text = (total + 1).ToString("F0");
            }
            if(total < 0 && !once1)
            {
                once1 = true;
                StartCoroutine(StartGame());
            }
                        
            if (spawnStart)
            {
                //KillText.text = $"남은 마리수 : {GameManager.Instance.deathEagleConter} / {GameManager.Instance.totalDeathEagle}";
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
    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        KillText.text = "Start";
        yield return new WaitForSecondsRealtime(0.6f);
        spawnStart = true;
        KillText.text = string.Empty;
    }
  
    // 의자 콜라이더 정보 가져와서 시작
    public void StartEvent(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ReadyForEvent());
        }

    }
    // 이벤트 시작준비
    IEnumerator ReadyForEvent()
    {
        gameStart = true;
        GameManager.Instance.legStop = true;
        yield return new WaitForSecondsRealtime(1);
        EventUi.gameObject.SetActive(true);

    }
    
    // 이벤트 종료
    IEnumerator EndEvent()
    {
        GameManager.Instance.npc2.transform.Find("TalkCheak").gameObject.SetActive(true);
        //KillText.text = $"남은 마리수 : {GameManager.Instance.deathEagleConter} / {GameManager.Instance.totalDeathEagle}";
        yield return new WaitForSecondsRealtime(0.2f);
        KillText.text = "미션 완료!";
        yield return new WaitForSecondsRealtime(0.5f);
        chairBoom.SetTrigger("Boom");

        GameManager.Instance.EventTimeBar.gameObject.SetActive(false);
        GameManager.Instance.TimeBar.fillAmount = 1;
        GameManager.Instance.TimeText.text = string.Empty;



        chairBoom.transform.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSecondsRealtime(3);
        EventUi.gameObject.SetActive(false);
        
    }

}

