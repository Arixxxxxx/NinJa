using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bridge : MonoBehaviour
{
    public enum FloorType
    {
        one, two
    }
    public FloorType type;
    BoxCollider2D Box;
    Vector3 PlayerPos;
    Vector3 PlayerVelo;
    [SerializeField] float Y;
    [SerializeField] float oneY;
    [SerializeField] bool DownOk;

    bool once2;
    private void Awake()
    {
        Box = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        PlayerPos = Player.instance.F_Get_PlayrPos();
        PlayerVelo = Player.instance.F_PlayerVelo();

        if (type == FloorType.one)
        {
            Y = transform.position.y - PlayerPos.y;

            if (Y < oneY && !once2 && PlayerVelo.y > 0)
            {
                once2 = true;
                Box.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.S) && !once && DownOk)
            {
                once = true;
                once2 = false;
                Box.enabled = false;
                StartCoroutine(DownStair());
            }
        }

        if (type == FloorType.two)
        {
            Y = transform.position.y - PlayerPos.y;

            if (Y < oneY && !once2 && PlayerVelo.y > 0)
            {
                once2 = true;
                Box.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.S) && !once && DownOk)
            {
                once = true;
                once2 = false;
                Box.enabled = false;
                StartCoroutine(DownStair());
            }
        }



    }
    bool once;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DownOk = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DownOk = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DownOk = false;
        }
    }

    IEnumerator DownStair()
    {
        yield return new WaitForSeconds(0.2f);
        once = false;
    }
}
