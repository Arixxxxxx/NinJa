using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;



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
        TMP_Text[] arrText = GetComponentsInChildren<TMP_Text>();
        //skillNamel = transform.Find("Text/SkillName").GetComponent<TMP_Text>();
        //skillInfo = transform.Find("Text/SkillInfo").GetComponent<TMP_Text>();
        skillNamel = arrText[0];
        skillInfo = arrText[1];
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void F_VideoChanger(ActionBarInfo.SkillType _Value) 
    {
        skillVideo.clip = videoList[(int)_Value];
        string Name = "";
        string Info = "";

        switch (_Value)
        {
            case ActionBarInfo.SkillType.waveShock:
                Name = "�� �� ��";
                Info = $"���濡 ����ĸ� ����� \n<b> [ <color=red>{SkillManager.instance.ShockWaveDmg} </color>] </b> �� ������� \n �����ϴ�.";
                break;

            case ActionBarInfo.SkillType.whilWind:
                Name = "�� �� ��";
                Info = $" <color=blue>< ��ư�� ������ �־�� �������� ></color=blue>\n�������ִ� ��� ������ \n [ <color=red>{SkillManager.instance.whilWindDmg} </color>] ��  �������\n [<color=red>{ SkillManager.instance.whilWindDmgInterval}</color>]�� �������� �����ϴ�. ";
                break;

            case ActionBarInfo.SkillType.dragonPier:
                Name = "���� ��ȿ";
                Info = $"������� ��ȿ�� ������ \n�ֺ��� <b> [ <color=red>{SkillManager.instance.dargonPierDmg} </color>] </b> �� �������  ������,\n ª���ð� ����ȿ���� �ɸ��ϴ�.";
                break;

            case ActionBarInfo.SkillType.warCry:
                Name = "������ �Լ�";
                Info = $" <color=blue>< ��ų�Է½� ���� On / Off ></color=blue>\n�Լ��� ������ �г밡 \n���������� ��� ���ݷ��� \n <b> [ <color=red>{SkillManager.instance.buffDmg} </color>] </b> ��ŭ �����մϴ�.";
                break;

            case ActionBarInfo.SkillType.powerShot:
                Name = "�Ϸ�Ʈ�δ� ���";
                Info = $"<color=red>< �Է¹�ư Ȧ���Ͽ� ��¡ ></color>\n�⸦��� ���������� ����� \n���濡 [<color=red>{SkillManager.instance.electronicShotDmg}~{SkillManager.instance.electronicShotDmg*2}</color>]</b>�� ������� �����ϴ�. \n<color=blue>< ������ ��� �����&���� ���� ></color=blue>";
                break;

            case ActionBarInfo.SkillType.tripleShot:
                Name = "Ʈ���� �ַο�";
                Info = $"���濡 3���� ȭ���� �ӻ��Ͽ�\n���� [<color=red>{SkillManager.instance.electronicShotDmg}</color>] </b> �� ������� �����ϴ�.";
                break;

            case ActionBarInfo.SkillType.boomShot:
                Name = "�������� ���";
                Info = $"<color=red>< �Է¹�ư Ȧ���Ͽ� ��¡ ></color>\nȭ�� ���������� [<color=red>{SkillManager.instance.electronicShotDmg}</color>] </b>�� ������� \n ������ ª���ð� ���Ͽ� �ɸ��ϴ�. \n<color=blue>< ������ ��� ȭ�� �Ÿ� ���� ></color=blue> ";
                break;

            case ActionBarInfo.SkillType.trap:
                Name = "���� ��";
                Info = $"���濡 ���� ���� �����ϴ�. \n���� ������ [<color=red>3</color>]�ʰ� \n�̵����� ���մϴ�. <color=blue>";
                break;

            case ActionBarInfo.SkillType.MelleR:
                Name = "���� �г�";
                Info = $"<color=blue><b><�Ϲݰ��� ���߽� ���� Ȯ�� Ȱ��ȭ></b></color>\n Ȱ��ȭ���¿��� ��ų ����\n<color=red>��� ��Ÿ���� �ʱ�ȭ</color>�ǰ� \n <color=red>������ �ִ�ġ�� ȸ��</color>�˴ϴ�.";
                break;

            case ActionBarInfo.SkillType.RangeR:
                Name = "�ӻ�";
                Info = $"<color=blue><b><�Ϲݰ��� ���߽� ���� Ȯ�� Ȱ��ȭ></b></color>\n Ȱ��ȭ���¿��� ��ų ����\n<color=red>{arrowAttack.Instance.buffMaxTime}�ʰ�  ���ݼӵ� 2��, ���ݷ� +1</color> ����";
                break;

        }

        skillNamel.text = Name;
        skillInfo.text = Info;
        skillVideo.Play();
    }
}
