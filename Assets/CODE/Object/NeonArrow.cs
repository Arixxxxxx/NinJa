using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonArrow : MonoBehaviour
{
    Transform[] trs;
    SpriteRenderer[] Sr;
    Color BeforeColor;
    Color wantedColor;
    int count;
    [Range(0f,30f)][SerializeField] float Speed;
    [Range(0f, 30f)][SerializeField] float StartSpeed;
    private void Awake()
    {
        
        count = transform.childCount;
        trs = new Transform[count];
        Sr = new SpriteRenderer[count];

        for(int i = 0; i < count; i++)
        {
            trs[i] = transform.GetChild(i).GetComponent<Transform>();
            Sr[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
            //trs[i].gameObject.SetActive(false);


        }
        BeforeColor = Sr[0].color;
        wantedColor = new Color(0, 255, 0, 80);
    }
    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > StartSpeed)
        {
            StartCoroutine(StartArrow());
            timer = 0;
        }


    }

    IEnumerator StartArrow()
    {
        Sr[0].color = wantedColor;
        yield return new WaitForSeconds(Speed);
        Sr[0].color = BeforeColor;
        Sr[1].color = wantedColor;
        yield return new WaitForSeconds(Speed);
        Sr[1].color = BeforeColor;
        Sr[2].color = wantedColor;
        yield return new WaitForSeconds(Speed);
        Sr[2].color = BeforeColor;
        Sr[3].color = wantedColor;
        yield return new WaitForSeconds(Speed);
        Sr[3].color = BeforeColor;
        Sr[4].color = wantedColor;
        yield return new WaitForSeconds(Speed);
        Sr[4].color = BeforeColor;
        Sr[5].color = wantedColor;
        yield return new WaitForSeconds(Speed);
        Sr[5].color = BeforeColor;
        Sr[6].color = wantedColor;
        yield return new WaitForSeconds(Speed);
        Sr[6].color = BeforeColor;
        Sr[7].color = wantedColor;
        yield return new WaitForSeconds(Speed);
        Sr[7].color = BeforeColor;
       



    }

    
}
