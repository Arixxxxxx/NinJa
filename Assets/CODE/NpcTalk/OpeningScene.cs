using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningScene : MonoBehaviour
{
    public Image BackGround;
    public MainUiText openingText;
    Animator BackAni;

    private void Awake()
    {
        BackGround = transform.GetChild(0).GetComponent<Image>();
        openingText = BackGround.transform.GetChild(0).GetComponent<MainUiText>();
        BackGround.gameObject.SetActive(true);
        BackAni = BackGround.GetComponent<Animator>();

    }
    private void Start()
    {
     
        GameManager.Instance.gameUI.gameObject.SetActive(false);
        if(GameManager.Instance.SceneName == "Chapter1")
        {
            openingText.F_SetMsg("Chapter 1.  ¼­¸·ÀÇ ½ÃÀÛ");
        }
        else if(GameManager.Instance.SceneName == "Chapter2")
        {
            openingText.F_SetMsg("Chapter 2.  ¾îµÒÀÇ ±×¸²ÀÚ");
        }
      
        GameManager.Instance.MovingStop = true;
        StartCoroutine(ActionShow0());
    }

    IEnumerator ActionShow0()
    {
       

        yield return new WaitForSecondsRealtime(5);
        
        BackAni.SetBool("off", true);
        SoundManager.instance.AudioChanger(SoundManager.instance.Audio.clip = SoundManager.instance.mainThema);
        yield return new WaitForSecondsRealtime(1.5f);
        openingText.gameObject.SetActive(false);
        GameManager.Instance.MovingStop = false;
        
        

        GameManager.Instance.gameUI.gameObject.SetActive(true);

        if (GameManager.Instance.SceneName == "Chapter1")
        {
            GameManager.Instance.guideM.startTutorial = true;
        }
        if (GameManager.Instance.SceneName == "Chapter2")
        {
            GameUI.instance.F_SetMapMoveBar("¿¤À© ½£");
        }
        
        gameObject.SetActive(false);
    }
}
