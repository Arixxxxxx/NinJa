using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieTrap : MonoBehaviour
{
    [Header("# ���� ���� Ƚ�� �� �ð�")]
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
    float Tir; // ���� �����ð� �ٿ��� Ÿ�̸�

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
            GameManager.Instance.TimeText.text = $"���� ������� �����ð� : {curspawntime.ToString("F0")}��";
        }
        
    }
    private void BlackSpin()
    {
        BlackHoleSpin += Time.deltaTime * 15;

        SpawnPoint1.transform.eulerAngles = new Vector3(0, 0, BlackHoleSpin);
        SpawnPoint2.transform.eulerAngles = new Vector3(0, 0, BlackHoleSpin);
        SpawnPoint3.transform.eulerAngles = new Vector3(0, 0, BlackHoleSpin);
    }

    //�÷��̾ ��ŸƮ �ݶ��̴��� ������� ��Ȧ ����
    
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
        if (SpawnCount == 0 && !SpawnStart) // �� �������� �پ�����
        {
            BlackHoleCloseScale -= Time.deltaTime * 0.3f; // ���� ����ũ�� �پ�������

            if(SpawnPoint1.localScale.x <= 0.05f)// ��Ȧ �۾������� ��������
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
    //5���� ���� �����մϴ�
    IEnumerator SpawnStartCount()
    {
        start = true;
        GameManager.Instance.EventTimeBar.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnStart = true;
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
            //Ǯ�Ŵ������� ������������~
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

            //����ī��Ʈ �Ŵ��������� ����Լ�
            Invoke("Ing", SpawnTimer);
        }

    }

    // Ʈ�� ����
    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!once)
            {
                once = true;
                GameManager.Instance.ScreenText.F_SetMsg("�� ������ ���۵Ǿ����ϴ�....");
            }
            
            BlackHoleOpenBool = true;
        }
    }
}
