using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RangeZone : MonoBehaviour
{
    [SerializeField] private GameObject Eagles;
   private Transform spawnPoint1, spawnPoint2;
    private Transform EventUi;
   public bool gameStart;
    TMP_Text KillText;
    Animator chairBoom;
 
    //생성 마리수
    int makeEagleEA;

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
    float Timer;
    [Range(1f,3f)][SerializeField] float reSapwnTime;
    public void Event()
    {
        if (gameStart && makeEagleEA > 0)
        {
            KillText.text = $"남은 마리수 : {GameManager.Instance.deathEagleConter} / {GameManager.Instance.totalDeathEagle}";
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
        KillText.text = $"남은 마리수 : {GameManager.Instance.deathEagleConter} / {GameManager.Instance.totalDeathEagle}";
        yield return new WaitForSecondsRealtime(0.2f);
        KillText.text = "미션 완료!";
        yield return new WaitForSecondsRealtime(0.5f);
        chairBoom.SetTrigger("Boom");
        yield return new WaitForSecondsRealtime(3);
        EventUi.gameObject.SetActive(false);
        
    }

}

