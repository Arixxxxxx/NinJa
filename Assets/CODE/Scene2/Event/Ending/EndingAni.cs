using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAni : MonoBehaviour
{
    Animator Ani;
  

    private void Awake()
    {
        Ani = GetComponent<Animator>();
    }

    private void A_SoundEffect()
    {
        SoundManager.instance.F_SoundPlay(SoundManager.instance.gateUpComplete, 0.4f);
        StartCoroutine(FadeImage());
    }
    IEnumerator FadeImage()
    {
        yield return new WaitForSeconds(0.5f);
        Ani.SetTrigger("Hide");
    }

    private void A_StartSound()
    {
        SoundManager.instance.F_SoundPlay(SoundManager.instance.questComplete, 0.7f);
    }

    private void A_Ending_Act1()
    {
       GameManager.Instance.F_EndAct1(true);
        GameUI.instance.F_UIOff();
    }
}
