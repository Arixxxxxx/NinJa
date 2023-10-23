using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class EndingManager : MonoBehaviour
{
    [SerializeField] private Animator boxTextAni;
    [SerializeField] private Animator LastTextAni;
    [SerializeField] private Image BoxSprite;
    [SerializeField] private Image SrCase;
    [SerializeField] private Image WhiteCutton;
    [SerializeField] private TMP_Text  BoxText;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private ParticleSystem Ps;
    [SerializeField] private AudioSource MainAudio;
    [Range(0f,10f)][SerializeField] private float popupSpeed;
    [Range(0f,10f)][SerializeField] private float nextSpeed;
    [Range(0f,10f)][SerializeField] private float PsColorFadeSpeed;
    [Range(0f, 10f)][SerializeField] private float endCuttonSpeed;
    [Range(0f, 10f)][SerializeField] private float endValumeSpeed;

    private bool isPsStart;
    private bool isEnd;
    private bool once;

    Vector2[] BoxPos = new Vector2[3];
    Vector2[] TextPos = new Vector2[3];

    private Transform MainCanvas;
    private void Awake()
    {
        MainCanvas = GameObject.Find("Canvas").GetComponent<Transform>();
        BoxPos[0] = MainCanvas.transform.Find("P1").position;
        BoxPos[1] = MainCanvas.transform.Find("P2").position;
        BoxPos[2] = MainCanvas.transform.Find("P3").position;

        TextPos[0] = MainCanvas.transform.Find("T1").position;
        TextPos[1] = MainCanvas.transform.Find("T2").position;
        TextPos[2] = MainCanvas.transform.Find("T3").position;
    }
    private void Start()
    {
        StartCoroutine(EngStart());
    }

    private void Update()
    {
        FadeOnPs();
        EndEnding();
    }

    private void EndEnding()
    {
        if (isEnd)
        {
            if(WhiteCutton.color.a > 0.98f && !once)
            {
                once = true;
                StartCoroutine(EndingFinish());
            }
            else if(WhiteCutton.color.a >= 0)
            {
                WhiteCutton.color += new Color(0, 0, 0, 0.05f) * endCuttonSpeed * Time.deltaTime;
                MainAudio.volume -= endValumeSpeed * Time.deltaTime;
            }
        }
    }

    IEnumerator EndingFinish()
    {
        WhiteCutton.color = Color.white;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main");
    }
    private void FadeOnPs()
    {
        if(Ps.startColor.a == 1)
        {
            return;
        }
       else if (isPsStart)
        {
            Ps.startColor += new Color(0, 0, 0, 0.2f) * PsColorFadeSpeed * Time.deltaTime;
        }
    }

    IEnumerator EngStart()
    {
        yield return new WaitForSeconds(8);
        isPsStart = true;
        yield return new WaitForSeconds(3);
        
        ChangeSrAndText(0);
        SrCase.rectTransform.position = BoxPos[0];
        BoxText.transform.position = TextPos[0];
        boxTextAni.SetTrigger("On");
        yield return new WaitForSeconds(popupSpeed);
        boxTextAni.SetTrigger("Off");
        yield return new WaitForSeconds(nextSpeed);

        ChangeSrAndText(1);
        SrCase.rectTransform.position = BoxPos[1];
        BoxText.transform.position = TextPos[1];
        boxTextAni.SetTrigger("On");
        yield return new WaitForSeconds(popupSpeed);
        boxTextAni.SetTrigger("Off");
        yield return new WaitForSeconds(nextSpeed);

        ChangeSrAndText(2);
        SrCase.rectTransform.position = BoxPos[2];
        BoxText.transform.position = TextPos[2];
        boxTextAni.SetTrigger("On");
        yield return new WaitForSeconds(popupSpeed);
        boxTextAni.SetTrigger("Off");
        yield return new WaitForSeconds(nextSpeed);

        ChangeSrAndText(3);
        SrCase.rectTransform.position = BoxPos[0];
        BoxText.transform.position = TextPos[0];
        boxTextAni.SetTrigger("On");
        yield return new WaitForSeconds(popupSpeed);
        boxTextAni.SetTrigger("Off");
        yield return new WaitForSeconds(nextSpeed);

        ChangeSrAndText(4);
        SrCase.rectTransform.position = BoxPos[1];
        BoxText.transform.position = TextPos[1];
        boxTextAni.SetTrigger("On");
        yield return new WaitForSeconds(popupSpeed);
        boxTextAni.SetTrigger("Off");
        yield return new WaitForSeconds(nextSpeed);

        ChangeSrAndText(5);
        SrCase.rectTransform.position = BoxPos[2];
        BoxText.transform.position = TextPos[2];
        boxTextAni.SetTrigger("On");
        yield return new WaitForSeconds(popupSpeed);
        boxTextAni.SetTrigger("Off");
        yield return new WaitForSeconds(nextSpeed);

        ChangeSrAndText(6);
        SrCase.rectTransform.position = BoxPos[0];
        BoxText.transform.position = TextPos[0];
        boxTextAni.SetTrigger("On");
        yield return new WaitForSeconds(popupSpeed);
        boxTextAni.SetTrigger("Off");
        yield return new WaitForSeconds(nextSpeed);

        ChangeSrAndText(7);
        SrCase.rectTransform.position = BoxPos[1];
        BoxText.transform.position = TextPos[1];
        boxTextAni.SetTrigger("On");
        yield return new WaitForSeconds(popupSpeed);
        boxTextAni.SetTrigger("Off");
        yield return new WaitForSeconds(nextSpeed);

        LastTextAni.SetTrigger("On");
        yield return new WaitForSeconds(popupSpeed);
        LastTextAni.SetTrigger("Off");
        yield return new WaitForSeconds(4);
        isEnd = true;

    }

    private void ChangeSrAndText(int value)
    {
        switch(value)
        {
            case 0:
                BoxSprite.sprite = sprites[value];
                BoxText.text = "<color=yellow>Unity 학습 기간</color> : 3개월\n\n<color=yellow>개발 기간</color> : 23.08.25 ~ 23.10.23 [59일]";

                break;

            case 1:
                BoxSprite.sprite = sprites[value];
                BoxText.text = "<color=yellow><b>기획</b></color>  =  Lee_Dong_Eun\n<color=yellow><b>개발</b></color>  =  Lee_Dong_Eun\n<color=yellow><b>디자인</b></color>  =  Lee_Dong_Eun";
                break;

            case 2:
                BoxSprite.sprite = sprites[1];
                BoxText.text = "캐릭터는 PhotoShap으로 <color=yellow>합성/편집</color> 하였고\n\n  <color=yellow>Asset Store</color>에서 참고 되었습니다.";
                break;

            case 3:
                BoxSprite.sprite = sprites[1];
                BoxText.text = "개발에 필요한 Resource는 대부분 <color=yellow>Youtube</color>와\n\n  <color=yellow>PngWing Site</color>에서 참고하였습니다.";
                break;

            case 4:
                BoxSprite.sprite = sprites[1];
                BoxText.text = "필요한 <color=yellow>ArtDesign Banner</color>는\n\n  <color=yellow>Adobe Express</color>로 직접 디자인하였습니다.";
                break;

            case 5:
                BoxSprite.sprite = sprites[1];
                BoxText.text = "대부분의 <color=yellow>SFX</color> / <color=yellow>Music Resource</color>는 \n\n  <color=yellow>월드오브워크래프트</color>에서 녹음/편집 하였습니다.";
                break;

            case 6:
                BoxSprite.sprite = sprites[1];
                BoxText.text = "대부분의 시스템은  원하는대로 \n\n  참고없이 <color=yellow>직접 구현</color>하였습니다.";
                break;
            
            case 7:
                BoxSprite.sprite = sprites[1];
                BoxText.text = "산이의 대모험의 <color=yellow><b><산이></b></color>는\n\n  5살 아들의 이름이였습니다 :)";
                break;
        }
    }
}
