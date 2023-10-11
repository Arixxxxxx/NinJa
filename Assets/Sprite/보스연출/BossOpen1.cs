using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BossOpen1 : MonoBehaviour
{

    Transform main, Boss, Paint;
    
    Animator middle, name, Bg;
    
    

    private void Awake()
    {
        main = transform.Find("Boss").GetComponent<Transform>();
        Bg = main.transform.Find("Bg").GetComponent<Animator>();
        middle = main.transform.Find("Middle").GetComponent<Animator>();
        name = main.transform.Find("Name").GetComponent<Animator>();
        Boss = main.transform.Find("Boss").GetComponent<Transform>();
        Paint = main.transform.Find("Paint").GetComponent<Transform>();
    }

    bool once2;
    bool once, once1;
    private void Update()
    {
        if(main.gameObject.activeSelf&& !once2)
        {
            Debug.Log("1");
            once2 = true;
            StartCoroutine(EventStart());

        }

        if (Paint.gameObject.activeSelf && !once1)
        {
            once1 = true;
            StartCoroutine(MiddleStart());

        }

        if (Boss.gameObject.activeSelf && !once)
        {
            once = true;
            StartCoroutine(EventEnd());
        }
    }
    
    [SerializeField] float EndTime;
    IEnumerator EventEnd()
    {
        yield return new WaitForSeconds(EndTime);
        once2 = false;
        main.gameObject.SetActive(false);
    } 
    IEnumerator MiddleStart()
    {
        yield return new WaitForSeconds(0.5f);
        once1 = false;
        middle.SetTrigger("Open");
    }

    IEnumerator EventStart()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("2");
        Bg.SetTrigger("Open");

    }

}
