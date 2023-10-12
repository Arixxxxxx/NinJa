using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3 : MonoBehaviour
{


    bool once;
    [SerializeField] float BossPopupTimer;
    [SerializeField] float BossEffetInterval;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            StartCoroutine(Event3Start());
            transform.GetComponent<BoxCollider2D>().enabled = false;
            transform.Find("BossEffect").gameObject.SetActive(true);
        }
        
    }

    IEnumerator Event3Start()
    {
        Emoticon.instance.F_GetEmoticonBox("Question"); //물음표
        GameManager.Instance.F_MoveStop(0); //멈추고

        yield return new WaitForSeconds(BossPopupTimer); 
         transform.Find("Boss").gameObject.SetActive(true); //보스 팝업

        yield return new WaitForSeconds(BossEffetInterval);
         GameManager.Instance.gameUI.transform.Find("Boss").gameObject.SetActive(true); //보스소개창

        while (GameManager.Instance.gameUI.transform.Find("Boss").gameObject.activeSelf) //소개창꺼지면
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);

        transform.Find("Boss").GetComponent<Animator>().SetBool("Casting", true); //시전 모션
        SoundManager.instance.F_SoundPlay(SoundManager.instance.lougther, 0.8f);


        //공세시작
        while (transform.Find("Boss").gameObject.activeSelf)
        {
            yield return null;
        }
        GameManager.Instance.F_MoveStop(1); //캐릭터움직이세요

        transform.Find("Spawn").GetComponent<ZombieTrap>().BlackHoleOpenBool = true;

        while (transform.Find("Spawn").gameObject.activeSelf)
        {
            yield return null;
        }
        transform.Find("BossEffect").gameObject.SetActive(false);
        gameObject.gameObject.SetActive(false);
    }
}
