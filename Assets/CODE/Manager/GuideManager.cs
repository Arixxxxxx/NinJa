using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuideManager : MonoBehaviour
{

    //시작 알림 bool
    public bool startTutorial;
    //밑에 가이드들 애니매이션발동
    Transform gudiegurop;

    Animator Ani0;
    Animator Ani1;
    Animator Ani1_1;
    bool ani1true;
    //2번키고 2번애니 2번끄고 3번키고 3번하이드 애니

    //벽점프 가이드
    Animator Ani2;
    Animator Ani2_1;
   
    bool ani2true;
    bool ani21true;
    bool ani3true;

    //전투가이드 [모드설명3,근접설명3-1,원거리설명3-2, 원거리룰 설명3-3] 
    Animator Ani3;
    Transform Ani3_1;
    Transform Ani3_2;
    Animator Ani3_3;
    public bool isBattleGuideStart;

    //이동플랫폼 가이드
    Animator Ani4, Ani4_1;

    Transform player;
    PointCheker pc;

    //트랩 가이드
    Animator Ani5, Ani5_2;
    Transform Ani5_1;

    private void Awake()
    {
        player = GameManager.Instance.playerTR.GetComponent<Transform>();
        pc = GameObject.FindAnyObjectByType<PointCheker>();
        gudiegurop = transform.GetChild(0).GetComponent<Transform>();
        Ani0 = transform.Find("GuideGroup/0").GetComponent<Animator>();

        //점프 가이드
        Ani1 = gudiegurop.transform.Find("1").GetComponent<Animator>();
        Ani1_1 = gudiegurop.transform.Find("1-1").GetComponent<Animator>();

        //벽점프관련 가이드
        Ani2 = gudiegurop.transform.Find("2").GetComponent<Animator>();
        Ani2_1 = gudiegurop.transform.Find("2-1").GetComponent<Animator>();

        //전투가이드
        Ani3 = gudiegurop.transform.Find("3").GetComponent<Animator>();
        Ani3_1 = gudiegurop.transform.Find("3-1").GetComponent<Transform>();
        Ani3_2 = gudiegurop.transform.Find("3-2").GetComponent<Transform>();
        Ani3_3 = gudiegurop.transform.Find("3-3").GetComponent<Animator>();

        //이동플랫폼 가이드

        Ani4 = gudiegurop.transform.Find("4").GetComponent<Animator>();
        Ani4_1 = gudiegurop.transform.Find("4-1").GetComponent<Animator>();

        //트랩가이드
        Ani5 = gudiegurop.transform.Find("5").GetComponent<Animator>();
        Ani5_1 = gudiegurop.transform.Find("5-1").GetComponent<Transform>();
        Ani5_2 = gudiegurop.transform.Find("5-2").GetComponent<Animator>();

    }
    // 키고끄기

    //켜지는 포인트 받기 함수
    void Update()
    {
        GuideBoxoff_1();
        GuideBoxoff_2();
        GuideBoxoff_3();
        GuideBoxoff_4();
        GuideBoxoff_5();
        StartTutorial();
    }

    private void StartTutorial()
    {
        if (!startTutorial) { return; }
        else if (startTutorial)
        {
            startTutorial = false;
            //StopCharacter();
            Invoke("StartMSG", 0.1f);
            StartCoroutine(ActionShow0());
        }
       
    }
    //콜라이더로 서칭하다가 레이캐스트로 변경햇음
    //public void F_GetColl(Pointer _value)
    //public void F_GetColl(Pointer _value)
    //{
    //    switch (_value)
    //    {
    //        case Pointer.point0:
    //            StopCharacter();
    //            Invoke("StartMSG", 6.2f);
    //            StartCoroutine(ActionShow0());
    //            break;
    //    }
    //}


        public void F_GetColl(GameObject _obj)
        {
            string pointNum = _obj.name;
            switch (pointNum)
            {

                case "GuidePoint1":
                    StopCharacter();
                    GameManager.Instance.player.MovingStop = true;
                    Ani1.gameObject.transform.position = GameManager.Instance.playerTR.transform.position + new Vector3(0, 3.5f);
                    Ani1.gameObject.SetActive(true);
                    Ani1.SetBool("Show", true);

                    break;

                case "GuidePoint2":
                    StopCharacter();
                    Ani2.gameObject.transform.position = GameManager.Instance.playerTR.transform.position + new Vector3(0, 3.5f);
                    Ani2.gameObject.SetActive(true);
                    Ani2.SetBool("Show", true);
                    break;

                case "GuidePoint3":
                    StopCharacter();
                    Ani3.gameObject.SetActive(true);
                    Ani3.SetBool("Show", true);
                    break;

                case "GuidePoint4":
                    StopCharacter();
                Ani4.gameObject.transform.position = GameManager.Instance.playerTR.transform.position + new Vector3(0, 1.5f);
                Ani4.gameObject.SetActive(true);
                    Ani4.SetBool("Show", true);
                    break;

            case "GuidePoint5":
                StopCharacter();
                Ani5.gameObject.transform.position = GameManager.Instance.playerTR.transform.position + new Vector3(0, 1.5f);
                Ani5.gameObject.SetActive(true);
                Ani5.SetBool("Show", true);
                break;
        }
        }
        
    private void StartMSG()
    {
        Ani0.gameObject.SetActive(true);
        Ani0.SetBool("Show", true);
        
    }
    IEnumerator ActionShow0()
    {
       
        yield return new WaitForSecondsRealtime(6);
        GameManager.Instance.MovingStop = false;
        Ani0.SetBool("Show", false);
            
        yield return new WaitForSecondsRealtime(1.5f);
        Ani0.gameObject.SetActive(false);
        
    } 
  

    private float nextbtnTimer;
    private float nextbtnTimer1;
    private float nextbtnTimer2;
    private float nextbtnTimer3;

    float Next1, Next1_1;
    private void GuideBoxoff_1() // 1번연출 [점프 설명]
    {
       
        if (Ani1.gameObject.activeSelf)
        {
            Next1 += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.F) && Next1 > 0.25f)
            {
                Next1 = 0;
                Ani1.gameObject.SetActive(false);
                
                Ani1_1.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                Ani1_1.transform.position = Ani1.gameObject.transform.position;
                Ani1_1.gameObject.SetActive(true);

                

            }
        }

        else if (Ani1_1.gameObject.activeSelf)
        {
            Next1_1 += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.F) && Next1_1 > 0.25f)
            {
                Ani1_1.SetBool("Hide", true);
                GameManager.Instance.MovingStop = false;
                GameManager.Instance.player.MovingStop = false;


                Invoke("OffWindws", 0.2f);
            }
        }
    }

    float Next2, Next2_1;
    private void GuideBoxoff_2() // //2번 연출 [ 벽점프, 슬라이딩 설명]
    {
        if (Ani2.gameObject.activeSelf)
        {
            Next2 += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.F) && Next2 > 0.25f)
            {
                Next2 = 0;
                Ani2.gameObject.SetActive(false);
                Ani2_1.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                Ani2_1.gameObject.transform.position = Ani2.gameObject.transform.position;
                Ani2_1.gameObject.SetActive(true);
            }
        }

        if (Ani2_1.gameObject.activeSelf)
        {
            Next2_1 += Time.deltaTime; // 연속으로 안눌리게 조절
            if (Input.GetKeyDown(KeyCode.F) && Next2_1 > 0.25f)
            {
                GameManager.Instance.MovingStop = false;
                GameManager.Instance.player.MovingStop = false;
                Ani2_1.SetBool("Hide", true);
                Invoke("OffWindws", 0.35f);

            }
        }
    }

    private void GuideBoxoff_3()  //3번 연출 [근접/원거리 설명]
    {
        if (Ani3.gameObject.activeSelf) // 근 원
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Ani3.gameObject.SetActive(false);
                Ani3_1.gameObject.SetActive(true);

            }
        }
        if (Ani3_1.gameObject.activeSelf) //근
        {
            nextbtnTimer1 += Time.deltaTime; // 연속으로 안눌리게 조절
            if (Input.GetKeyDown(KeyCode.F) && nextbtnTimer1 > 0.25f)
            {

                Ani3_2.gameObject.SetActive(true);
                Ani3_1.gameObject.SetActive(false);

                nextbtnTimer1 -= 0;

            }
        }
        if (Ani3_2.gameObject.activeSelf) // 원
        {
            nextbtnTimer2 += Time.deltaTime; // 연속으로 안눌리게 조절
            if (Input.GetKeyDown(KeyCode.F) && nextbtnTimer2 > 0.25f)
            {

                Ani3_3.gameObject.SetActive(true);
                Ani3_2.gameObject.SetActive(false);

                nextbtnTimer2 -= 0;

            }
        }
        if (Ani3_3.gameObject.activeSelf) // 화살
        {
            nextbtnTimer3 += Time.deltaTime; // 연속으로 안눌리게 조절
            if (Input.GetKeyDown(KeyCode.F) && nextbtnTimer3 > 0.25f)
            {

                Ani3_3.SetBool("Hide", true);
                nextbtnTimer3 -= 0;
                GameManager.Instance.MovingStop = false;

            }
        }
    }

    public float Next4, Next4_1;
    private void GuideBoxoff_4() // //2번 연출 [ 벽점프, 슬라이딩 설명]
    {
        if (Ani4.gameObject.activeSelf)
        {
            Next4 += Time.deltaTime; // 연속으로 안눌리게 조절
            if (Input.GetKeyDown(KeyCode.F) && Next4 > 0.25f)
            {
                Next4 = 0;
                Ani4.gameObject.SetActive(false);
                Ani4_1.gameObject.transform.position = Ani4.gameObject.transform.position;
                Ani4_1.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                Ani4_1.gameObject.SetActive(true);
               
            }
        }

        if (Ani4_1.gameObject.activeSelf)
        {
            Next4_1 += Time.deltaTime; // 연속으로 안눌리게 조절
            if (Input.GetKeyDown(KeyCode.F) && Next4_1 > 0.255f)
            {
               
                GameManager.Instance.MovingStop = false;
                GameManager.Instance.player.MovingStop = false;
                Ani4_1.SetBool("Hide", true);
                

                Invoke("OffWindws", 0.3f);
              
                
            }
        }
    }

    public float Next5, Next5_1, Next5_2;
    private void GuideBoxoff_5() // //2번 연출 [ 벽점프, 슬라이딩 설명]
    {
        if (Ani5.gameObject.activeSelf)
        {
            Next5 += Time.deltaTime; // 연속으로 안눌리게 조절
            if (Input.GetKeyDown(KeyCode.F) && Next5 > 0.25f)
            {
                Next5 = 0;
                Ani5.gameObject.SetActive(false);

                Ani5_1.gameObject.transform.position = Ani5.gameObject.transform.position;
                Ani5_1.gameObject.SetActive(true);

            }
        }

        if (Ani5_1.gameObject.activeSelf)
        {
            Next5_1 += Time.deltaTime; // 연속으로 안눌리게 조절
            if (Input.GetKeyDown(KeyCode.F) && Next5_1 > 0.25f)
            {
                Next5_1 = 0;
                Ani5_1.gameObject.SetActive(false);
                Ani5_2.gameObject.transform.position = Ani5.gameObject.transform.position;
                Ani5_2.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                Ani5_2.gameObject.SetActive(true);

            }
        }
        if (Ani5_2.gameObject.activeSelf)
        {
            Next5_2 += Time.deltaTime; // 연속으로 안눌리게 조절
            if (Input.GetKeyDown(KeyCode.F) && Next5_2 > 0.255f)
            {

                GameManager.Instance.MovingStop = false;
                GameManager.Instance.player.MovingStop = false;
                Ani5_2.SetBool("Hide", true);


                Invoke("OffWindws", 0.3f);


            }
        }
    }
    private void StopCharacter() // 캐릭터 멈춤
    {
        GameManager.Instance.player.Rb.velocity = Vector2.zero;
        GameManager.Instance.MovingStop = true;
        GameManager.Instance.player.Char_Vec.x = 0;
    }
    private void OffWindws()
    {
        if(Ani1_1.gameObject.activeSelf)
        {
            Ani1_1.SetBool("Hide", false);
            GameManager.Instance.once = false;
            Next1_1 = 0;
            Ani1_1.gameObject.SetActive(false);
        }
        
        if(Ani2_1.gameObject.activeSelf)
        {
            Ani2_1.SetBool("Hide", false);
            GameManager.Instance.once = false;
            Next2_1 = 0;
            Ani2_1.gameObject.SetActive(false);
        }
        if (Ani4_1.gameObject.activeSelf)
        {
            Ani4_1.SetBool("Hide", false);
            GameManager.Instance.once = false;
            Next4_1 = 0;
            Ani4_1.gameObject.SetActive(false);
        }
        if (Ani5_2.gameObject.activeSelf)
        {
            Ani5_2.SetBool("Hide", false);
            GameManager.Instance.once = false;
            Next5_2 = 0;
            Ani5_2.gameObject.SetActive(false);
        }
    }

}
