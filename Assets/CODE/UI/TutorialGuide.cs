using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialGuide : MonoBehaviour
{
    public static TutorialGuide instance;

    [SerializeField] List<VideoClip> videoList = new List<VideoClip>();
    VideoPlayer video;
    Button boxExitBtn;
    TMP_Text nameText;
    TMP_Text infoText;
    TMP_Text press;

    int npcCount;
    public int _GetItemNum;

    private void Awake()
    {
        if(instance == null)
        { 
            instance = this; 
        }
        else
        {
             Destroy(gameObject);
        }

        video = GetComponentInChildren<VideoPlayer>();
        boxExitBtn = GetComponentInChildren<Button>();
        TMP_Text[] textar = GetComponentsInChildren<TMP_Text>();
        press = textar[0];
        nameText = textar[1];
        infoText = textar[2];
        gameObject.SetActive(false);
    }
    void Start()
    {
        boxExitBtn.onClick.AddListener(() => 
        {
            video.Stop();
            SoundManager.instance.F_SoundPlay(SoundManager.instance.WindowPopDown, 0.8f);
            GameManager.Instance.MovingStop = false;
            Animator ani = gameObject.GetComponent<Animator>();
            ani.SetTrigger("Close");
            
        });
    }

    private void Update()
    {
        if(gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape))
            {
                SoundManager.instance.F_SoundPlay(SoundManager.instance.WindowPopDown, 1f);
                video.Stop();
                GameManager.Instance.MovingStop = false;
                Animator ani = gameObject.GetComponent<Animator>();
                if (npcCount == 1)
                {
                    _GetItemNum = 1;
                }
                else if(npcCount == 2)
                {
                    _GetItemNum = 2;
                }
             
                ani.SetTrigger("Close");
                
            }
        }
    }
    /// <summary>
    /// 튜토리얼 가이드영상
    /// </summary>
    /// <param name="_value">0점프,1벽점프,2플랫폼,3각종트랩,4전사,5원거리</param>
    public void F_SetTutorialWindow(int _value)
    {
        gameObject.SetActive(true);
        press.text = $"< Press <color=yellow>[F]</color>  or <color=yellow>[Esc]</color> Key  >";
        SoundManager.instance.F_SoundPlay(SoundManager.instance.WindowPopup, 1f);
        Animator ani = gameObject.GetComponent<Animator>();
        ani.SetTrigger("Open");
         if(video == null)
        {
            video = GetComponentInChildren<VideoPlayer>();
            boxExitBtn = GetComponentInChildren<Button>();
            TMP_Text[] textar = GetComponentsInChildren<TMP_Text>();
            nameText = textar[0];
            infoText = textar[1];
        }
        

        string a = string.Empty;
        string b = string.Empty;

        switch ( _value )
        {
            case 1:
                
                video.clip = videoList[0];
                a = $"캐릭터 점프";
                b = $"점프키 <color=yellow><b><SpaceBar></b></color>\n\n2회 입력시 '<color=yellow><b>2단 점프</b></color>'";
                break;

            case 2:
                video.clip = videoList[1];
                a = $"벽 점프";
                b = $"벽을 향해 도약 후\n\n방향키를 누르지 않고\n\n <color=yellow><b><SpaceBar></b></color>키 입력'";
                break;   
            
            case 3:
                video.clip = videoList[2];
                a = $"플랫폼 이용";
                b = $"바람을 타고 공중에서\n\n이동하거나 플랫폼을 타고\n\n이동할수 있습니다. ";
                break;

            case 4:
                video.clip = videoList[3];
                a = $"각종 위험요소";
                b = $"게임에는 수많은\n\n위험요소가 있습니다.\n\n잘보고 대처해야 합니다.";
                break;

            case 5:
                video.clip = videoList[4];
                a = $"근접 모드";
                b = $"좌클릭 = 일반 공격\n\n우클릭 = 방패 막기\n\n <size=30><color=yellow><b>각각의 스킬설명은 단축바에서 \n< 마우스오버 > 해주세요.</b></color></size>";
                npcCount = 1;

               
                break;
            
            case 6:
                video.clip = videoList[5];
                a = $"레인지 모드";
                b = $"좌클릭 = 화살 발사\n\n우클릭 = 활 조준\n\n <size=30><color=yellow><b>각각의  스킬설명은 단축바에서 \n< 마우스오버 > 해주세요.</b></color></size>";
                npcCount = 2;
                break;

            case 7:
                video.clip = videoList[6];
                a = $"돌 옮기기";
                b = $"<size=40>좌클릭 = 집기\n\n드래그 후 클릭해제\n\n<color=yellow>오브젝트를 드래그해서\n움직일 수 있습니다.</color>";
                npcCount = 2;
                break;
        }

      
        video.Play();
        nameText.text = a;
        infoText.text = b;
        GameManager.Instance.once = false;
    }

    public void F_OffGameObject()
    {
        gameObject.SetActive( false );
    }
     

}
