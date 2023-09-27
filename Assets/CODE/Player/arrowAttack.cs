using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bullet;

public class arrowAttack : MonoBehaviour
{
    public static arrowAttack Instance;


    [Header("일반화살")]
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject boomArrow;
    [SerializeField] GameObject Boom;
    [SerializeField] GameObject tripleArrow;
    [SerializeField] Transform m_Arrow;
    [SerializeField] Transform BowPos;
    [SerializeField] Transform ArrowTong;
    private Transform tong;
    Camera maincam;
    Queue<GameObject> ArrowBox = new Queue<GameObject>();
    Queue<GameObject> boomArrowQUE = new Queue<GameObject>();
    public Queue<GameObject> boomQUE = new Queue<GameObject>();
    Queue<GameObject> TripleArrowQUE = new Queue<GameObject>();
    float curTime;
    Animator FillAni;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        maincam = Camera.main;
        tong = transform.Find("Tong").GetComponent<Transform>();
        FillAni = GameObject.Find("GameUI").transform.Find("Btn2/ArrowFill").GetComponent<Animator>();

        for (int i = 0; i < 30; i++)
        {

            GameObject obj = Instantiate(Arrow, transform.position, Quaternion.identity, ArrowTong);
            obj.SetActive(false);
            ArrowBox.Enqueue(obj);

            GameObject objs = Instantiate(tripleArrow, transform.position, Quaternion.identity, ArrowTong);
            objs.SetActive(false);
            TripleArrowQUE.Enqueue(objs);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(boomArrow, transform.position, Quaternion.identity, ArrowTong);
            obj.SetActive(false);
            boomArrowQUE.Enqueue(obj);

            GameObject objs = Instantiate(Boom, transform.position, Quaternion.identity, ArrowTong);
            objs.SetActive(false);
            boomQUE.Enqueue(objs);

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
                    GameObject obj = F_GetArrow(0);
                    SoundManager.instance.F_SoundPlay(SoundManager.instance.rangeAttak, 1f);

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

    public float shootPower;
    public float MaxPower;
    private void TripleArrow()
    {
        if (GameManager.Instance.isGetRangeItem && GameManager.Instance.rangeMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(ArrowSpawn());
                GameManager.Instance.Player_CurMP -= 5;
            }

            if (Input.GetKey(KeyCode.Alpha4))
            {
                shootPower += Time.deltaTime * 7;
                if(shootPower > MaxPower)
                {
                    shootPower = MaxPower;
                }
                
             
            }

            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                GameObject obj = F_GetArrow(ArrowType.boomArrow);
                SoundManager.instance.F_SoundPlay(SoundManager.instance.rangeAttak, 1f);

                obj.transform.position = BowPos.position;
                obj.transform.rotation = m_Arrow.rotation;
                obj.GetComponent<Rigidbody2D>().velocity = obj.transform.right * shootPower;
                shootPower = 0;
            }
        }
    }

    IEnumerator ArrowSpawn()
    {
        ArrowOnsShot();
        yield return new WaitForSeconds(0.15f);
        ArrowOnsShot();
        yield return new WaitForSeconds(0.15f);
        ArrowOnsShot();
    }

    private void ArrowOnsShot()
    {
        GameObject obj = F_GetArrow(ArrowType.triple);
        SoundManager.instance.F_SoundPlay(SoundManager.instance.rangeAttak, 1f);

        obj.transform.position = BowPos.position;
        obj.transform.rotation = m_Arrow.rotation;
        obj.GetComponent<Rigidbody2D>().velocity = obj.transform.right * 20f;
    }
    void Update()
    {
        LookAtMouse();
        ArrowFire();
        TripleArrow();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_value">0=노말,1=트리블,2붐화살,3붐</param>
    /// <returns></returns>
    public GameObject F_GetArrow(ArrowType type)
    {
        GameObject arrow = null;

        switch (type)
        {
            case ArrowType.normal:

                arrow = ArrowBox.Dequeue();
                arrow.SetActive(true);
                return arrow;

                break;

            case ArrowType.triple:

                arrow = TripleArrowQUE.Dequeue();
                arrow.SetActive(true);
                return arrow;
                break;

            case ArrowType.boomArrow:
                arrow = boomArrowQUE.Dequeue();
                arrow.SetActive(true);
                return arrow;
                break;



            default: return null;
        }

    }

    public GameObject F_Get_Boom()
    {
        GameObject obj = boomQUE.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public void F_Set_Boom(GameObject obj)
    {
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(false);
        boomQUE.Enqueue(obj);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="_value">0=노말,1=트리블,2붐화살,3붐</param>
    public void F_SetArrow(GameObject obj, ArrowType type)
    {
        switch (type)
        {
            case ArrowType.normal:
                obj.transform.position = transform.position;
                ArrowBox.Enqueue(obj);
                break;

            case ArrowType.triple:
                obj.transform.position = transform.position;
                TripleArrowQUE.Enqueue(obj);
                break;

            case ArrowType.boomArrow:
                obj.transform.position = transform.position;
                boomArrowQUE.Enqueue(obj);

                break;
            case ArrowType.boom:
                obj.transform.position = transform.position;
                boomQUE.Enqueue(obj);
                break;

        }

    }
}
