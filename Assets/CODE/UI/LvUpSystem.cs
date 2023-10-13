using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvUpSystem : MonoBehaviour
{
    [Range(0f, 5f)][SerializeField] float lvUpWindowDuration;
    public void A_SoundPlay()
    {
        SoundManager.instance.F_SoundPlay(SoundManager.instance.LvUp, 0.6f);
    }
    public void A_OffFade()
    {
        Invoke("off", lvUpWindowDuration);
    }
    private void off()
    {
        transform.GetComponent<Animator>().SetBool("Fade", false);
    }
    
    public void A_ObjectOff()
    {
        gameObject.gameObject.SetActive(false);
    }
}
