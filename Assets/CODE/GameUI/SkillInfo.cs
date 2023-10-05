using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using static ActionBarInfo;



public class SkillInfo : MonoBehaviour
{
 
    
    public List<VideoClip> videoList = new List<VideoClip>();
    //0���� ���� 1,2,3,4 ������ 1,2,3,4

    VideoPlayer skillVideo;
    TMP_Text skillNamel;
    TMP_Text skillInfo;
    ActionBarInfo sc;

    private void Awake()
    {
        skillVideo = transform.Find("Box/Bg/Box/Video").GetComponent<VideoPlayer>();
        skillNamel = transform.Find("Text/SkillName").GetComponent<TMP_Text>();
        skillInfo = transform.Find("Text/SkillInfo").GetComponent<TMP_Text>();
      
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void F_VideoChanger(SkillType _Value) 
    {
        skillVideo.clip = videoList[(int)_Value];
        string Name = "";
        string Info = "";

        switch (_Value)
        {
            case SkillType.waveShock:
                Name = "�� �� ��";
                Info = $"���濡 ����ĸ� ����� \n<b> [ <color=red>{SkillManager.instance.ShockWaveDmg} </color>] </b> �� ������� \n �����ϴ�.";
                break;

            case SkillType.whilWind:
                Name = "�� �� ��";
                Info = $"�������ִ� ��� ������ \n [ <color=red>{SkillManager.instance.whilWindDmg} </color>] ��  �������\n [<color=red>{ SkillManager.instance.whilWindDmgInterval}</color>]�� �������� �����ϴ�. \n <color=blue>< ��ư�� ������ �־�� �������� ></color=blue>";
                break;

            case SkillType.dragonPier:
                Name = "���� ��ȿ";
                Info = $"������� ��ȿ�� ������ \n�ֺ��� <b> [ <color=red>{SkillManager.instance.dargonPierDmg} </color>] </b> �� �������  \n������, ª���ð� ����ȿ���� �ɸ��ϴ�.";
                break;

            case SkillType.warCry:
                Name = "������ �Լ�";
                Info = $"�Լ��� ������ �г밡 \n���������� ��� ���ݷ��� \n <b> [ <color=red>{SkillManager.instance.buffDmg} </color>] </b> ��ŭ �����մϴ�.\n <color=blue>< ���� On/Off ��� ></color=blue>";
                break;

            case SkillType.powerShot:
                Name = "�Ϸ�Ʈ�δ� ���";
                Info = $"�⸦��� ���������� ����� \n���濡 [<color=red>{SkillManager.instance.electronicShotDmg}</color>] </b> �� ������� �����ϴ�. \n<color=blue>< ������ ��� �����&���� ���� ></color=blue> ";
                break;

            case SkillType.tripleShot:
                Name = "Ʈ���� �ַο�";
                Info = $"���濡 3���� ȭ���� �ӻ��Ͽ�\n���� [<color=red>{SkillManager.instance.electronicShotDmg}</color>] </b> �� ������� �����ϴ�.";
                break;

            case SkillType.boomShot:
                Name = "�������� ���";
                Info = $"�⸦��� ������ź�� ����� \n���������� [<color=red>{SkillManager.instance.electronicShotDmg}</color>] </b> �� ������� \n ������ ª���ð� ���Ͽ� �ɸ��ϴ�. \n<color=blue>< ������ ��� ȭ�� �Ÿ� ���� ></color=blue> ";
                break;

            case SkillType.trap:
                Name = "���� ��";
                Info = $"���濡 ���� ���� �����ϴ�. \n���� ������ [<color=red>3</color>]�ʰ� \n�̵����� ���մϴ�. <color=blue>";
                break;

        }

        skillNamel.text = Name;
        skillInfo.text = Info;
        skillVideo.Play();
    }
}
