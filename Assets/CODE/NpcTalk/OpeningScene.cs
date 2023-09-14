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
        openingText.F_SetMsg("Chapter 1.  서막의 시작");
        GameManager.Instance.MovingStop = true;
        StartCoroutine(ActionShow0());
    }

    IEnumerator ActionShow0()
    {
        yield return new WaitForSecondsRealtime(5);
        BackAni.SetBool("off", true);

        yield return new WaitForSecondsRealtime(2);
        openingText.gameObject.SetActive(false);
        GameManager.Instance.MovingStop = false;
        gameObject.SetActive(false);
        GameManager.Instance.gameUI.gameObject.SetActive(true);
        GameManager.Instance.guideM.startTutorial = true;

    }
}
