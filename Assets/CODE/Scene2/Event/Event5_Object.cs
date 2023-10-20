using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Event5_Object : MonoBehaviour
{
    Rigidbody2D Rb;
    RaycastHit2D Hit;
    Vector2 StartDrag;
    Vector2 EndDrag;
    Vector2 DragPower;
    PolygonCollider2D PolColl;
    BoxCollider2D BoxColl;
    float originMass, originGravityValue;
    float originBoxX, originBoxY;
    [SerializeField] float Power;
    ParticleSystem Ps;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        BoxColl = GetComponent<BoxCollider2D>();
        PolColl = GetComponent<PolygonCollider2D>();
        Ps = transform.Find("Ps").GetComponent<ParticleSystem>();
 
    }

    private void Update()
    {
        if (GameManager.Instance.RockQuest)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Default");
            Hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, ~layerMask);
            if (Hit.collider != null && Hit.collider.gameObject == gameObject)
            {
                GameManager.Instance.F_OnMouseObject(true);
                Debug.Log("켜짐");
                if (Input.GetMouseButtonDown(0))
                {
                    PolColl.enabled = false;
                    BoxColl.enabled = true;
                    Vector3 MousePos = Input.mousePosition;
                    StartDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RbChager(true);
                    Rb.mass = 0;
                    Rb.gravityScale = 0;
                    gameObject.layer = LayerMask.NameToLayer("EnemyDead");
                    BoxColl.size = new Vector2(BoxColl.size.x * 2f, BoxColl.size.y * 2f);
                    Vector3 WorldPos = Camera.main.ScreenToWorldPoint(MousePos);





                    transform.position = new Vector2(WorldPos.x, WorldPos.y);


                }

                if (Input.GetMouseButton(0))
                {
                    Vector3 MousePos = Input.mousePosition;
                    Vector3 WorldPos = Camera.main.ScreenToWorldPoint(MousePos);

                    transform.position = new Vector2(WorldPos.x, WorldPos.y);
                }

                if (Input.GetMouseButtonUp(0))
                {

                    RbChager(false);
                    PolColl.enabled = true;
                    BoxColl.enabled = false;
                    Rb.constraints = RigidbodyConstraints2D.None;
                    EndDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    BoxColl.size = new Vector2(originBoxX, originBoxY);
                    DragPower = EndDrag - StartDrag;
                    gameObject.layer = LayerMask.NameToLayer("Rock");
                    Rb.AddForce(DragPower * Power, ForceMode2D.Impulse);
                   

                }
            }
            else if(Hit.collider == null)
            {
                Debug.Log("꺼짐");
                GameManager.Instance.F_OnMouseObject(false);
            }
            
           
        }
       
    }
    public void OnPointerEnterDelegate(PointerEventData data)
    {
        Debug.Log("마우스가 오브젝트에 들어왔습니다.");
    }

    public void OnPointerExitDelegate(PointerEventData data)
    {
        Debug.Log("마우스가 오브젝트에서 나갔습니다.");
    }

    private void RbChager(bool _value)
    {
        switch (_value)
        {
            case true:
                originMass = Rb.mass;
                originGravityValue = Rb.gravityScale;
                originBoxX = BoxColl.size.x;
                originBoxY = BoxColl.size.y;

                break;

            case false:
                Rb.mass = originMass;
                Rb.gravityScale = originGravityValue;
                BoxColl.size = new Vector2(originBoxX, originBoxY);
                break;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Rock"))
        {
            Rb.velocity = Vector2.zero;
            Rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            StartCoroutine(PsPlay());
        }
    }

    IEnumerator PsPlay()
    {
        Ps.gameObject.SetActive(true);
        Ps.Play();
        while (Ps.isPlaying)
        {
            yield return null;
        }
        Ps.Stop();
        Ps.gameObject.SetActive(false);
    }

    public void ClickRock(bool _value)
    {
        switch(_value)
        {
            case true:
                GameManager.Instance.isMouseOnObject = true;
                break;

            case false:
                GameManager.Instance.isMouseOnObject = false;
                break;
        }

    }
}
