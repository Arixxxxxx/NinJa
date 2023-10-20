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
    /// Ʃ�丮�� ���̵念��
    /// </summary>
    /// <param name="_value">0����,1������,2�÷���,3����Ʈ��,4����,5���Ÿ�</param>
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
                a = $"ĳ���� ����";
                b = $"����Ű <color=yellow><b><SpaceBar></b></color>\n\n2ȸ �Է½� '<color=yellow><b>2�� ����</b></color>'";
                break;

            case 2:
                video.clip = videoList[1];
                a = $"�� ����";
                b = $"���� ���� ���� ��\n\n����Ű�� ������ �ʰ�\n\n <color=yellow><b><SpaceBar></b></color>Ű �Է�'";
                break;   
            
            case 3:
                video.clip = videoList[2];
                a = $"�÷��� �̿�";
                b = $"�ٶ��� Ÿ�� ���߿���\n\n�̵��ϰų� �÷����� Ÿ��\n\n�̵��Ҽ� �ֽ��ϴ�. ";
                break;

            case 4:
                video.clip = videoList[3];
                a = $"���� ������";
                b = $"���ӿ��� ������\n\n�����Ұ� �ֽ��ϴ�.\n\n�ߺ��� ��ó�ؾ� �մϴ�.";
                break;

            case 5:
                video.clip = videoList[4];
                a = $"���� ���";
                b = $"��Ŭ�� = �Ϲ� ����\n\n��Ŭ�� = ���� ����\n\n <size=30><color=yellow><b>������ ��ų������ ����ٿ��� \n< ���콺���� > ���ּ���.</b></color></size>";
                npcCount = 1;

               
                break;
            
            case 6:
                video.clip = videoList[5];
                a = $"������ ���";
                b = $"��Ŭ�� = ȭ�� �߻�\n\n��Ŭ�� = Ȱ ����\n\n <size=30><color=yellow><b>������  ��ų������ ����ٿ��� \n< ���콺���� > ���ּ���.</b></color></size>";
                npcCount = 2;
                break;

            case 7:
                video.clip = videoList[6];
                a = $"�� �ű��";
                b = $"<size=40>��Ŭ�� = ����\n\n�巡�� �� Ŭ������\n\n<color=yellow>������Ʈ�� �巡���ؼ�\n������ �� �ֽ��ϴ�.</color>";
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
