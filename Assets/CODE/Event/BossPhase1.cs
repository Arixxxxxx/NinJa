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
    [Header("# ���� ���� Ƚ�� �� �ð�")]
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
    float Tir; // ���� �����ð� �ٿ��� Ÿ�̸�
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

    //�÷��̾ ��ŸƮ �ݶ��̴��� ������� ��Ȧ ����
    bool once1;
    private void BlackHoleOpen()
    {
        if (BlackHoleOpenBool)
        {
            if (!once1)
            {
                once1 = true;
                SoundManager.instance.F_SoundPlay(SoundManager.instance.lougther, 0.8f);
                GameUI.instance.F_CenterTextPopup("õ�Ѱ�.. ���Ͷ� ���� �ϼ��ε��̿�..");
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
        if (SpawnCount == 0 && !SpawnStart) // �� �������� �پ�����
        {
            skyreturn = true;
            BlackHoleCloseScale -= Time.deltaTime * 0.35f; // ���� ����ũ�� �پ�������


            if (SpawnPoint[3].localScale.x <= 0.05f)// ��Ȧ �۾������� ��������
            {
                //GameManager.Instance.EventTimeBar.gameObject.SetActive(false);
                for (int i = 2; i <4; i++)
                {
                    SpawnPoint[i].gameObject.SetActive(false);
                }

                if (!once4)
                {
                    once4 = true;
                    StartCoroutine(ReSpawnBoss());//���� �����
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
    //5���� ���� �����մϴ�
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
    {   //������ ī��Ʈ ���߰� ����ٳ���
        if (SpawnCount <= 0)
        {
            SpawnStart = false; // ��Ȧ ����
            BlackHoleOpenBool = false; // ��Ȧ Ŀ���� �ʵ� ����

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

            //Ǯ�Ŵ������� ������������~

            for (int i = 2; i < 4; i++)
            {
                GameObject obj = PoolManager.Instance.F_GetObj("Enemy");
                obj.transform.position = SpawnPoint[i].position;
                obj.SetActive(true);

            }


            SpawnCount--;

            //����ī��Ʈ �Ŵ��������� ����Լ�
            Invoke("Ing", SpawnTimer);
        }

    }
    

    public void F_Pahse1Start(bool _value)
    {
        BlackHoleOpenBool = _value;
    }
    // Ʈ�� ����
    
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
    //                    GameManager.Instance.ScreenText.F_SetMsg("�� ������ ���۵Ǿ����ϴ�....");
    //                }
    //            }
    //            break;

    //    }

    //}
}
