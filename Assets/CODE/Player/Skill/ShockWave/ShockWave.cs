using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [SerializeField] float interval;
    public int DMG;
     Animator A1;
     Animator A2;
     Animator A3;
    void Start()
    {
        A1  = transform.GetChild(0).GetComponent<Animator>();
        A2  = transform.GetChild(1).GetComponent<Animator>();
        A3  = transform.GetChild(2).GetComponent<Animator>();
        
    }

    public void F_ShockWave()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        if(A1 == null) 
        {
            A1 = transform.GetChild(0).GetComponent<Animator>();
            A2 = transform.GetChild(1).GetComponent<Animator>();
            A3 = transform.GetChild(2).GetComponent<Animator>();
        }
        A1.gameObject.SetActive(true);
        A1.SetTrigger("1");
        yield return new WaitForSeconds(interval);
        A2.gameObject.SetActive(true);
        A2.SetTrigger("1");
        yield return new WaitForSeconds(interval);
        A3.gameObject.SetActive(true);
        A3.SetTrigger("1");
        yield return new WaitForSeconds(2f);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(false);
        SkillManager.instance.ShockQUE.Enqueue(gameObject);
    }

   
}
