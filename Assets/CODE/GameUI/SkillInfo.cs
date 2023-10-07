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
    //0부터 근접 1,2,3,4 레인지 1,2,3,4

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
                Name = "충 격 파";
                Info = $"전방에 충격파를 만들어 \n<b> [ <color=red>{SkillManager.instance.ShockWaveDmg} </color>] </b> 의 대미지를 \n 입힙니다.";
                break;

            case ActionBarInfo.SkillType.whilWind:
                Name = "휠 윈 드";
                Info = $" <color=blue>< 버튼을 누르고 있어야 시전지속 ></color=blue>\n근접해있는 모든 적에게 \n [ <color=red>{SkillManager.instance.whilWindDmg} </color>] 의  대미지를\n [<color=red>{ SkillManager.instance.whilWindDmgInterval}</color>]초 간격으로 입힙니다. ";
                break;

            case ActionBarInfo.SkillType.dragonPier:
                Name = "용의 포효";
                Info = $"용과같은 포효를 내질러 \n주변에 <b> [ <color=red>{SkillManager.instance.dargonPierDmg} </color>] </b> 의 대미지를  입히고,\n 짧은시간 스턴효과에 걸립니다.";
                break;

            case ActionBarInfo.SkillType.warCry:
                Name = "전투의 함성";
                Info = $" <color=blue>< 스킬입력시 버프 On / Off ></color=blue>\n함성을 내질러 분노가 \n마를때까지 모든 공격력이 \n <b> [ <color=red>{SkillManager.instance.buffDmg} </color>] </b> 만큼 증가합니다.";
                break;

            case ActionBarInfo.SkillType.powerShot:
                Name = "일렉트로닉 사격";
                Info = $"<color=red>< 입력버튼 홀딩하여 차징 ></color>\n기를모아 전기파장을 만들어 \n전방에 [<color=red>{SkillManager.instance.electronicShotDmg}~{SkillManager.instance.electronicShotDmg*2}</color>]</b>의 대미지를 입힙니다. \n<color=blue>< 게이지 비례 대미지&범위 증가 ></color=blue>";
                break;

            case ActionBarInfo.SkillType.tripleShot:
                Name = "트리플 애로우";
                Info = $"전방에 3개의 화살을 속사하여\n각각 [<color=red>{SkillManager.instance.electronicShotDmg}</color>] </b> 의 대미지를 입힙니다.";
                break;

            case ActionBarInfo.SkillType.boomShot:
                Name = "전기폭발 사격";
                Info = $"<color=red>< 입력버튼 홀딩하여 차징 ></color>\n화살 낙하지점에 [<color=red>{SkillManager.instance.electronicShotDmg}</color>] </b>의 대미지를 \n 입히고 짧은시간 스턴에 걸립니다. \n<color=blue>< 게이지 비례 화살 거리 증가 ></color=blue> ";
                break;

            case ActionBarInfo.SkillType.trap:
                Name = "마비 덫";
                Info = $"전방에 마비 덫을 던집니다. \n적이 밟으면 [<color=red>3</color>]초간 \n이동하지 못합니다. <color=blue>";
                break;

            case ActionBarInfo.SkillType.MelleR:
                Name = "피의 분노";
                Info = $"<color=blue><b><일반공격 적중시 일정 확률 활성화></b></color>\n 활성화상태에서 스킬 사용시\n<color=red>모든 쿨타임이 초기화</color>되고 \n <color=red>마나가 최대치로 회복</color>됩니다.";
                break;

            case ActionBarInfo.SkillType.RangeR:
                Name = "속사";
                Info = $"<color=blue><b><일반공격 적중시 일정 확률 활성화></b></color>\n 활성화상태에서 스킬 사용시\n<color=red>{arrowAttack.Instance.buffMaxTime}초간  공격속도 2배, 공격력 +1</color> 증가";
                break;

        }

        skillNamel.text = Name;
        skillInfo.text = Info;
        skillVideo.Play();
    }
}
