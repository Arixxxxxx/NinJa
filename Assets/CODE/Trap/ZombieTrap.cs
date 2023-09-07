using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieTrap : MonoBehaviour
{
    [Header("# 좀비 스폰 횟수 및 시간")]
    [SerializeField] int SpawnCount;
    [SerializeField] private float SpawnTimer;
    [SerializeField] private bool SpawnStart;
    [SerializeField] private bool BlackHoleOpenBool;
    [SerializeField] private float curspawntime;
    [SerializeField] private float totalspawntime;
    private Transform SpawnPoint1;
    private Transform SpawnPoint2;
    private Transform SpawnPoint3;
    private float BlackHoleSpin;
    private float BlackHoleScale;
    private bool start; 
    float Tir; // 현재 스폰시간 줄여줄 타이머

    private void Awake()
    {
        totalspawntime = SpawnCount * SpawnTimer;
        curspawntime = totalspawntime;

        SpawnPoint1 = transform.GetChild(0).GetComponent<Transform>();
        SpawnPoint2 = transform.GetChild(1).GetComponent<Transform>();
        SpawnPoint3 = transform.GetChild(2).GetComponent<Transform>();
        

        SpawnPoint1.localScale = Vector3.zero;
        SpawnPoint2.localScale = Vector3.zero;
        SpawnPoint3.localScale = Vector3.zero;
    }
    private void Update()
    {
        BlackSpin();
        BlackHoleOpen();
        BlackHoleCloseheyo();
        SetEventBar();
    }
    
    private void SetEventBar()
    {
        if (GameManager.Instance.EventTimeBar.gameObject.activeSelf)
        {
            Tir += Time.deltaTime;
            if( Tir > 1 ) 
            { 
                curspawntime -= 1; 
                Tir = 0; 
            }
            if(curspawntime < 0)
            {
                curspawntime = 0;
            }
            GameManager.Instance.TimeBar.fillAmount = curspawntime / totalspawntime;
            GameManager.Instance.TimeText.text = $"공세 종료까지 남은시간 : {curspawntime.ToString("F0")}초";
        }
        
    }
    private void BlackSpin()
    {
        BlackHoleSpin += Time.deltaTime * 15;

        SpawnPoint1.transform.eulerAngles = new Vector3(0, 0, BlackHoleSpin);
        SpawnPoint2.transform.eulerAngles = new Vector3(0, 0, BlackHoleSpin);
        SpawnPoint3.transform.eulerAngles = new Vector3(0, 0, BlackHoleSpin);
    }

    //플레이어가 스타트 콜라이더를 밟았을떄 블랙홀 열림
    
    private void BlackHoleOpen()
    {
        if (BlackHoleOpenBool)
        {
                    
            BlackHoleScale += Time.deltaTime * 0.15f;

            if (SpawnPoint1.localScale.x > 1)
            {
                if (!start) 
                {
                    StartCoroutine(SpawnStartCount());
                }

                return; 
            }
            else if (SpawnPoint1.localScale.x <= 1)
            {
                SpawnPoint1.localScale = new Vector3(BlackHoleScale, BlackHoleScale, BlackHoleScale);
                SpawnPoint2.localScale = new Vector3(BlackHoleScale, BlackHoleScale, BlackHoleScale);
                SpawnPoint3.localScale = new Vector3(BlackHoleScale, BlackHoleScale, BlackHoleScale);
            }
        }
    }
    private float BlackHoleCloseScale = 1;
    private void BlackHoleCloseheyo()
    {
        if (SpawnCount == 0 && !SpawnStart) // 다 끝났으니 줄어들어줘
        {
            BlackHoleCloseScale -= Time.deltaTime * 0.3f; // 이제 구멍크기 줄어들게해줘

            if(SpawnPoint1.localScale.x <= 0.05f)// 블랙홀 작아졌으면 연출종료
            {
                GameManager.Instance.EventTimeBar.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
            else if (SpawnPoint1.localScale.x > 0)
            {
                SpawnPoint1.localScale = new Vector3(BlackHoleCloseScale, BlackHoleCloseScale, BlackHoleCloseScale);
                SpawnPoint2.localScale = new Vector3(BlackHoleCloseScale, BlackHoleCloseScale, BlackHoleCloseScale);
                SpawnPoint3.localScale = new Vector3(BlackHoleCloseScale, BlackHoleCloseScale, BlackHoleCloseScale);
            }
          
        }
    }
    //5초후 스폰 시작합니다
    IEnumerator SpawnStartCount()
    {
        start = true;
        GameManager.Instance.EventTimeBar.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnStart = true;
        Ing();
    }

    private void Ing()
    {   //리스폰 카운트 다했고 연출다끝남
        if (SpawnCount <= 0)
        {
            SpawnStart = false; // 블랙홀 종료
            BlackHoleOpenBool = false; // 블랙홀 커지기 너도 종료
        }
        
        else if (SpawnCount > 0 && SpawnStart)
        {
            //풀매니저에서 좀비가져오세요~
            GameObject obj = PoolManager.Instance.F_GetObj("Enemy");
            obj.transform.position = SpawnPoint1.position;
            obj.SetActive(true);

            GameObject obj1 = PoolManager.Instance.F_GetObj("Enemy");
            obj1.transform.position = SpawnPoint2.position;
            obj1.SetActive(true);

            GameObject obj2 = PoolManager.Instance.F_GetObj("Enemy");
            obj2.transform.position = SpawnPoint3.position;
            obj2.SetActive(true);

            SpawnCount--;

            //스폰카운트 거덜날때까지 재귀함수
            Invoke("Ing", SpawnTimer);
        }

    }

    // 트랩 시작
    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!once)
            {
                once = true;
                GameManager.Instance.ScreenText.F_SetMsg("적 공세가 시작되었습니다....");
            }
            
            BlackHoleOpenBool = true;
        }
    }
}
