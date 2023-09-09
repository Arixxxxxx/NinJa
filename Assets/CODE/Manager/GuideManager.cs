using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
    //밑에 가이드들 애니매이션발동
    Animator Ani0;
    Animator ani1;
    bool ani1true;
    private void Awake()
    {
        Ani0 = transform.Find("GuideGroup/0").GetComponent<Animator>();
    }
    // 키고끄기

    //켜지는 포인트 받기 함수
    void Update()
    {
        ani1off();
    }

    private bool Action;
    string msg;
    public void F_GetColl(Pointer _value, Collider2D _collider)
    {
        switch (_value)
        {
            case Pointer.point0:
                Ani0.SetBool("Show", true);
                StopCharacter();

                StartCoroutine(ActionShow0());
                break;

            case Pointer.point1:
                Transform JumpGuide = transform.Find("GuideGroup/1").GetComponent<Transform>();
                ani1 = JumpGuide.GetComponent<Animator>();
                StopCharacter();
                ani1true = true;
                ani1.SetBool("Show", ani1true);
               
                break;

            case Pointer.point2:
                break;
        }
    }

    private void StopCharacter()
    {
        GameManager.Instance.player.Rb.velocity = Vector2.zero;
        GameManager.Instance.MovingStop = true;
        GameManager.Instance.player.Char_Vec.x = 0;
    }
     
    private void ani1off()
    {
        if (ani1true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameManager.Instance.MovingStop = false;
                ani1.SetBool("Show", false);
            }
        }
    }
    IEnumerator ActionShow0()
    {
        yield return new WaitForSecondsRealtime(3);
        Ani0.SetBool("Show", false);
        yield return new WaitForSecondsRealtime(1.5f);
        GameManager.Instance.MovingStop = false;

    }
}
