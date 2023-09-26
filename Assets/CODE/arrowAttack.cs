using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowAttack : MonoBehaviour
{
    [Header("일반화살")]
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform m_Arrow;
    [SerializeField] Transform BowPos;
    [SerializeField] Transform ArrowTong;
    Camera maincam;
    Queue<GameObject> ArrowBox = new Queue<GameObject>();
    float curTime;
    Animator FillAni;
    

    private void Awake()
    { 
         maincam = Camera.main;
         

         for (int i = 0; i < 30; i++)
        {
            FillAni = GameObject.Find("GameUI").transform.Find("Btn2/ArrowFill").GetComponent<Animator>();
            GameObject obj = Instantiate(Arrow, transform.position, Quaternion.Euler(0,0,0), ArrowTong);
            obj.SetActive(false);
            ArrowBox.Enqueue(obj);
        }
    }

    
    private void LookAtMouse()
    {
        if (GameManager.Instance.isGetRangeItem)
        {
            Vector2 mousePos = maincam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = new Vector2(mousePos.x - m_Arrow.position.x, mousePos.y - m_Arrow.position.y);
            m_Arrow.right = dir;
            transform.position = GameManager.Instance.player.transform.position;

            //에임조준중일때 플레이어 캐릭터 보는방향 컨트롤해줄수잇게 마우스좌표x값으로 방향 bool 저장
            if (mousePos.x < GameManager.Instance.player.transform.position.x)
            {
                GameManager.Instance.AimLeft = true;
            }
            else if (mousePos.x > GameManager.Instance.player.transform.position.x)
            {
                GameManager.Instance.AimLeft = false;
            }
        }
          
    }

    private void ArrowFire()
    {
        if (GameManager.Instance.isGetRangeItem)
        {
            curTime += Time.deltaTime;

            if (curTime > 0.3f)
            {
                if (Input.GetMouseButton(0) && !GameManager.Instance.meleeMode && GameManager.Instance.player.RealBow.gameObject.activeSelf)
                {
                    if (GameManager.Instance.CurArrow <= 0)
                    {
                        GameManager.Instance.player.F_CharText("Arrow");
                        return;
                    }
                    GameObject obj = F_GetArrow();
                    obj.transform.GetComponent<Bullet>().Audio.clip = SoundManager.instance.rangeAttak;
                    obj.transform.GetComponent<Bullet>().Audio.Play();
                    obj.transform.position = BowPos.position;
                    obj.transform.rotation = m_Arrow.rotation;
                    obj.GetComponent<Rigidbody2D>().velocity = obj.transform.right * 15f;
                    curTime = 0;
                    GameManager.Instance.CurArrow--;
                    FillAni.SetTrigger("Ok");
                }
            }
        }
       
    }
    void Update()
    {
        LookAtMouse();
        ArrowFire();

    }

    public GameObject F_GetArrow()
    {
        GameObject arrow = null;
        arrow = ArrowBox.Dequeue();
        arrow.SetActive(true);
        return arrow;
    }
    public void F_SetArrow(GameObject obj)
    {
        
        obj.transform.position = transform.position;
        ArrowBox.Enqueue(obj);
    }
}
