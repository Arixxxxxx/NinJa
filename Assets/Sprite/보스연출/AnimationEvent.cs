using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
   public void A_NameOpen()
    {
        if (transform.gameObject.name == "Bg")
        {
            StartCoroutine(PaintOpen());
        }
        if (transform.gameObject.name == "Middle")
        {
            StartCoroutine(NameOpen());
        }
        if (transform.gameObject.name == "Name")
        {
            StartCoroutine(BossOpen());
        }
    }

    IEnumerator PaintOpen()
    {
        yield return new WaitForSeconds(0.3f);
        transform.parent.transform.Find("Paint").gameObject.SetActive(true);
       
    }   IEnumerator NameOpen()
    {
        yield return new WaitForSeconds(0.3f);
        transform.parent.transform.Find("Name").GetComponent<Animator>().SetTrigger("Open");
       
    }
    public void A_MiddleSound()
    {
        SoundManager.instance.F_SoundPlay(SoundManager.instance.BossMiddle, 0.8f);
    }

    //public void A_NameSound()
    //{
    //    SoundManager.instance.F_SoundPlay(SoundManager.instance.BossMiddle, 0.8f);
    //}
    IEnumerator BossOpen()
    {
        yield return new WaitForSeconds(0.3f);
        transform.parent.transform.Find("Boss").gameObject.SetActive(true);
        //SoundManager.instance.F_SoundPlay(SoundManager.instance.BossName, 0.8f);
    }
}
