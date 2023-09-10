using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
    //�ؿ� ���̵�� �ִϸ��̼ǹߵ�
    Transform gudiegurop;

    Animator Ani0;
    Animator ani1;
    bool ani1true;
    //2��Ű�� 2���ִ� 2������ 3��Ű�� 3�����̵� �ִ�

    //������ ���̵�
    Animator Ani2;
    Animator Ani2_1;
   
    bool ani2true;
    bool ani21true;
    bool ani3true;

    //�������̵� [��弳��3,��������3-1,���Ÿ�����3-2, ���Ÿ��� ����3-3] 
    Animator Ani3;
    Transform Ani3_1;
    Transform Ani3_2;
    Animator Ani3_3;
    public bool isBattleGuideStart;
    

    private void Awake()
    {
        gudiegurop = transform.GetChild(0).GetComponent<Transform>();
        Ani0 = transform.Find("GuideGroup/0").GetComponent<Animator>();

        //���������� ���̵�
        Ani2 = gudiegurop.transform.GetChild(2).GetComponent<Animator>();
        Ani2_1 = gudiegurop.transform.GetChild(3).GetComponent<Animator>();

        //�������̵�
        Ani3 = gudiegurop.transform.Find("3").GetComponent<Animator>();
        Ani3_1 = gudiegurop.transform.Find("3-1").GetComponent<Transform>();
        Ani3_2 = gudiegurop.transform.Find("3-2").GetComponent<Transform>();
        Ani3_3 = gudiegurop.transform.Find("3-3").GetComponent<Animator>();

    }
    // Ű�����

    //������ ����Ʈ �ޱ� �Լ�
    void Update()
    {
        GuideBoxOff();
    }

    private bool Action;
    string msg;
    public void F_GetColl(Pointer _value, Collider2D _collider)
    {
        switch (_value)
        {
            case Pointer.point0:
                Ani0.gameObject.SetActive(true);
                Ani0.SetBool("Show", true);
                StopCharacter();

                StartCoroutine(ActionShow0());
                break;

            case Pointer.point1:
                Transform JumpGuide = transform.Find("GuideGroup/1").GetComponent<Transform>();
                ani1 = JumpGuide.GetComponent<Animator>();
                StopCharacter();
                ani1true = true;
                ani1.gameObject.SetActive(true);
                ani1.SetBool("Show", ani1true);
               
                break;

            case Pointer.point2:
                StopCharacter();
                Ani2.gameObject.SetActive(true);
                Ani2.SetBool("Guide2", true);
                ani2true = true;
                break;
            
            case Pointer.point3:
                StopCharacter();
                Ani3.gameObject.SetActive(true);
                Ani3.SetBool("Show", true);
                break;
        }
    }

    private void StopCharacter()
    {
        GameManager.Instance.player.Rb.velocity = Vector2.zero;
        GameManager.Instance.MovingStop = true;
        GameManager.Instance.player.Char_Vec.x = 0;
    }

    private float nextbtnTimer;
    private float nextbtnTimer1;
    private float nextbtnTimer2;
    private float nextbtnTimer3;

    private void GuideBoxOff()
    {
        // 1������ [2������ ����]
        if (ani1true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameManager.Instance.MovingStop = false;
                ani1.SetBool("Show", false);
            }
        }
        //2�� ���� [ ������, �����̵� ����]
        if (ani2true) { 
            if (Input.GetKeyDown(KeyCode.F))
            {
                ani2true = false;
                Ani2_1.gameObject.SetActive(true);
                Ani2.gameObject.SetActive(false);
                Invoke("OffWindws", 2);
            }
        }
        
        if (Ani2_1.gameObject.activeSelf)
        {
            nextbtnTimer += Time.deltaTime; // �������� �ȴ����� ����
            if (Input.GetKeyDown(KeyCode.F) && nextbtnTimer > 1)
            {
                GameManager.Instance.MovingStop = false;
                Ani2_1.SetBool("Guide2", true);
                Invoke("OffWindws", 2);
                nextbtnTimer = 0;
            }
        }
        //3�� ���� [����/���Ÿ� ����]
        if (Ani3.gameObject.activeSelf) // �� ��
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
               Ani3.gameObject.SetActive(false);
               Ani3_1.gameObject.SetActive(true);
               
            }
        }
        if (Ani3_1.gameObject.activeSelf) //��
        {
            nextbtnTimer1 += Time.deltaTime; // �������� �ȴ����� ����
            if (Input.GetKeyDown(KeyCode.F) && nextbtnTimer1 > 0.25f)
            {
                
                Ani3_2.gameObject.SetActive(true);
                Ani3_1.gameObject.SetActive(false);

                nextbtnTimer1 -= 0;

            }
        }
        if (Ani3_2.gameObject.activeSelf) // ��
        {
            nextbtnTimer2 += Time.deltaTime; // �������� �ȴ����� ����
            if (Input.GetKeyDown(KeyCode.F) && nextbtnTimer2 > 0.25f)
            {
                
                Ani3_3.gameObject.SetActive(true);
                Ani3_2.gameObject.SetActive(false);

                nextbtnTimer2 -= 0;

            }
        }
        if (Ani3_3.gameObject.activeSelf) // ȭ��
        {
            nextbtnTimer3 += Time.deltaTime; // �������� �ȴ����� ����
            if (Input.GetKeyDown(KeyCode.F) && nextbtnTimer3 > 0.25f)
            {
                
                Ani3_3.SetBool("Hide", true);
                Invoke("OffWindws3", 2);
                nextbtnTimer3 -= 0;
                GameManager.Instance.MovingStop = false;

            }
        }

    }
    private void OffWindws()
    {
        ani1.gameObject.SetActive(false);
        Ani2_1.gameObject.SetActive(false);
        
    }

    private void OffWindws3()
    {
        Ani3_2.gameObject.SetActive(false );

    }
    IEnumerator ActionShow0()
    {
        yield return new WaitForSecondsRealtime(3);
        Ani0.SetBool("Show", false);
        yield return new WaitForSecondsRealtime(1.5f);
        GameManager.Instance.MovingStop = false;
        Ani0.gameObject.SetActive(false);

    }
}
