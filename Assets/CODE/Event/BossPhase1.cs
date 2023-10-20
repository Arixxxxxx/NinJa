using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase1 : MonoBehaviour
{
    public enum SpawnType
    {
        Phase1, Phase2

    }

    public SpawnType type;


    AudioSource Audio;
    [SerializeField] AudioClip[] Sfx;
    [Header("# 좀비 스폰 횟수 및 시간")]
    [SerializeField] int SpawnCount;
    [SerializeField] private float SpawnTimer;
    [SerializeField] private bool SpawnStart;
    [SerializeField] bool BlackHoleOpenBool;
    [SerializeField] private float curspawntime;
    [SerializeField] private float totalspawntime;
    [SerializeField] private Transform[] SpawnPoint;
 
    private float BlackHoleSpin;
    private float BlackHoleScale;
    private bool start;
    float Tir; // 현재 스폰시간 줄여줄 타이머
    bool lastSpawn;

        private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        totalspawntime = SpawnCount * SpawnTimer;
        curspawntime = totalspawntime;


        SpawnPoint[2].localScale = Vector3.zero;
        SpawnPoint[3].localScale = Vector3.zero;
    
        
    }
    private void Update()
    {
        BlackSpin();
        BlackHoleOpen();
        BlackHoleCloseheyo();
        

    }
    [SerializeField] float G = 1;
    [SerializeField] float B = 1;
    [SerializeField] bool skyreturn;
    [SerializeField] bool returnskycolorfinish;

    bool Event;

   
    private void BlackSpin()
    {
        BlackHoleSpin += Time.deltaTime * 15;

        for (int i = 2; i < 4; i++)
        {
            SpawnPoint[i].transform.eulerAngles = new Vector3(0, 0, BlackHoleSpin);
        }
        
    }

    //플레이어가 스타트 콜라이더를 밟았을떄 블랙홀 열림
    bool once1;
    private void BlackHoleOpen()
    {
        if (BlackHoleOpenBool)
        {
            if (!once1)
            {
                once1 = true;
                SoundManager.instance.F_SoundPlay(SoundManager.instance.lougther, 0.8f);
                GameUI.instance.F_CenterTextPopup("천한것.. 나와라 나의 하수인들이여..");
                Audio.clip = Sfx[0];
                Audio.Play();

                GameObject obj1 = PoolManager.Instance.F_GetObj("Portal");
                obj1.transform.position = SpawnPoint[0].position;

                GameObject obj2 = PoolManager.Instance.F_GetObj("Portal");
                obj2.transform.position = SpawnPoint[1].position;
            }

            BlackHoleScale += Time.deltaTime * 0.15f;

            if (SpawnPoint[3].localScale.x > 1)
            {
                if (!start)
                {
                    StartCoroutine(SpawnStartCount());
                }

                return;
            }
            else if (SpawnPoint[3].localScale.x <= 1)
            {
                for(int i = 2;i < 4; i++)
                {
                    SpawnPoint[i].localScale = new Vector3(BlackHoleScale, BlackHoleScale, BlackHoleScale);
                }
            
            }
        }
    }
    private float BlackHoleCloseScale = 1;
    bool once3, once4;
    private void BlackHoleCloseheyo()
    {
        if (SpawnCount == 0 && !SpawnStart) // 다 끝났으니 줄어들어줘
        {
            skyreturn = true;
            BlackHoleCloseScale -= Time.deltaTime * 0.35f; // 이제 구멍크기 줄어들게해줘


            if (SpawnPoint[3].localScale.x <= 0.05f)// 블랙홀 작아졌으면 연출종료
            {
                //GameManager.Instance.EventTimeBar.gameObject.SetActive(false);
                for (int i = 2; i <4; i++)
                {
                    SpawnPoint[i].gameObject.SetActive(false);
                }

                if (!once4)
                {
                    once4 = true;
                    StartCoroutine(ReSpawnBoss());//보스 재등장
                }
             }
            else if (SpawnPoint[3].localScale.x > 0.05f)
            {
                if (SpawnPoint[3].localScale.x < 0.5f && !once3)
                {
                    once3 = true;
                    Audio.clip = Sfx[2];
                    Audio.Play();
                }
                for (int i = 2; i < 4; i++)
                {
                    SpawnPoint[i].localScale = new Vector3(BlackHoleCloseScale, BlackHoleCloseScale, BlackHoleCloseScale);
                
                }
            }
        }
    }

    IEnumerator ReSpawnBoss()
    {
        yield return new WaitForSeconds(3);
        transform.parent.Find("Boss").GetComponent<Boss>().F_ReSpawnBoss();
    }
    //5초후 스폰 시작합니다
    bool once;
    IEnumerator SpawnStartCount()
    {
        start = true;
        //GameManager.Instance.EventTimeBar.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnStart = true;
        Audio.clip = Sfx[1];
        Audio.Play();
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
            if (Audio.clip != Sfx[1])
            {

                Audio.clip = Sfx[1];
                Audio.Play();
            }
            if (!Audio.isPlaying)
            {

                Audio.Play();
            }

            //풀매니저에서 좀비가져오세요~

            for (int i = 2; i < 4; i++)
            {
                GameObject obj = PoolManager.Instance.F_GetObj("Enemy");
                obj.transform.position = SpawnPoint[i].position;
                obj.SetActive(true);

            }


            SpawnCount--;

            //스폰카운트 거덜날때까지 재귀함수
            Invoke("Ing", SpawnTimer);
        }

    }
    

    public void F_Pahse1Start(bool _value)
    {
        BlackHoleOpenBool = _value;
    }
    // 트랩 시작
    
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    switch (type)
    //    {
    //        case SpawnType.triger:
    //            if (collision.gameObject.CompareTag("Player"))
    //            {
    //                if (!once)
    //                {
    //                    once = true;
    //                    BlackHoleOpenBool = true;
    //                    GameManager.Instance.ScreenText.F_SetMsg("적 공세가 시작되었습니다....");
    //                }
    //            }
    //            break;

    //    }

    //}
}
