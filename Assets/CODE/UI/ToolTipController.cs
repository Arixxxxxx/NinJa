using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ToolTipController : MonoBehaviour
{
    TMP_Text toolTipText;
    RectTransform Box;
    RectTransform textBox;

    Vector2 originTextRectSize;
    Vector2 originBoxRectSize;
    [SerializeField] float yubeckSizeX;
    [SerializeField] float yubeckSizeY;

    private void Awake()
    {
        toolTipText = transform.Find("ToolTipBox/Text").GetComponent<TMP_Text>();
        textBox = toolTipText.GetComponent<RectTransform>();

        Box = transform.Find("ToolTipBox/Box").GetComponent<RectTransform>();
    }

    private void Start()
    {
        originTextRectSize = textBox.sizeDelta;
        originBoxRectSize = Box.sizeDelta;

    }

    public void F_ToolTipTextSet(ToolTipObejct.TipType type)
    {
        //초기화
        string boxtext = string.Empty;
        textBox.sizeDelta = originTextRectSize;
        Box.sizeDelta = originBoxRectSize;

        switch (type)
        {
            case ToolTipObejct.TipType.Hp:
              string HP = GameManager.Instance.Player_CurHP.ToString("F1");

                boxtext = $"현재 생명력 : <color=red>{HP} / {GameManager.Instance.Player_MaxHP}</color>\n초당 <color=yellow>0.5</color>만큼 자연 회복";
                break;

            case ToolTipObejct.TipType.Mp:
                string MP = GameManager.Instance.Player_CurMP.ToString("F1");
                boxtext = $"현재 마나 : <color=#00ffffff>{MP} / {GameManager.Instance.Player_MaxMP}</color>\n초당 <color=yellow>0.5</color>만큼 자연 회복";
                break;

            case ToolTipObejct.TipType.Sp:
                
                boxtext = $"현재 기력 : <color=green>{GameManager.Instance.Player_CurSP} / {GameManager.Instance.Player_MaxSP}</color>\n초당 <color=yellow>{Player.instance.secRecerSP}</color>만큼 자연 회복";
                break;

            case ToolTipObejct.TipType.Time:
              
                boxtext = $"현재 시간";
                break;

            case ToolTipObejct.TipType.GM:
               
                boxtext = $"<color=red>GM Mode</color>";
                break;

            case ToolTipObejct.TipType.Menu:
                
                boxtext = $"Main Menu <color=yellow>(ESC)</color>";
                break;

            case ToolTipObejct.TipType.Lv:
                
                boxtext = $"현재 레벨";
                break;

            case ToolTipObejct.TipType.SkillTree:
                boxtext = $"스킬트리 창 <color=yellow>(K)</color>";
                break;

            case ToolTipObejct.TipType.StatsWindow:
                boxtext = $"캐릭터 스탯창<color=yellow>(C)</color>";
                break;

            case ToolTipObejct.TipType.M1:

                boxtext = $"<size=26><color=orange><b>[ 흡혈의 일격 ]</b></color></font>\n<size=24>일반공격 적중시 <color=yellow>30%확률</color>로 적의\n<color=green>생명력을 흡수</color>합니다.</size>\n<size=22>Lv.1  생명력 흡수 <color=green><b>+2</b></color>\nLv.2  생명력 흡수 <color=green><b>+4</b></color>\nLv.3  생명력 흡수 <color=green><b>+6</b></color></size>";
                break;

            case ToolTipObejct.TipType.M11:

                boxtext = $"<size=26><color=orange><b>[ 휠윈드 강화 ]</b></color></font>\n<size=24>휠윈드를 강화하여 적중시 <color=yellow>대미지 증가</color>하고\n<color=yellow>대미지 간격</color>이 감소합니다.</size>\n<size=22>Lv.1  대미지 증가 <color=green><b>+2</b></color> / 간격 감소 <color=red><b>-0.1초</b></color>\nLv.2  대미지 증가 <color=green><b>+4</b></color> / 간격 감소 <color=red><b>-0.2초</b></color>\nLv.3  대미지 증가 <color=green><b>+6</b></color> / 간격 감소 <color=red><b>-0.3초";
                break;

            case ToolTipObejct.TipType.M12:

                boxtext = $"<size=26><color=orange><b>[ 충격파 강화 ]</b></color></font>\n<size=24>충격파를 강화하여 적중시 <color=yellow>대미지 증가</color>하고\n<color=yellow>스턴 시간</color>이 증가합니다.</size>\n<size=22>Lv.1  대미지 증가 <color=green><b>+3</b></color> / 스턴시간 증가 <color=green><b>+0.25초</b></color>\nLv.2  대미지 증가 <color=green><b>+6</b></color> / 스턴시간 증가 <color=green><b>+0.5초</b></color>\nLv.3  대미지 증가 <color=green><b>+9</b></color> / 스턴시간 증가 <color=green><b>+0.75초</b></color>";
                break;

            case ToolTipObejct.TipType.M2:

                boxtext = $"<size=26><color=orange><b>[ 궁극기 강화 ]</b></color></font>\n<size=24>일반 공격 적중시 <color=yellow>발동 확률 증가</color>합니다.\n<size=22>Lv.1  발동확률 증가 <color=green><b>5%</b></color>";
                break;

            case ToolTipObejct.TipType.R1:

                boxtext = $"<size=26><color=orange><b>[ 마나재생 강화 ]</b></color></font>\n<size=24>일반공격시 생성되는 <color=yellow>마나</color>가\n<color=green>증가</color>합니다.</size>\n<size=22>Lv.1  마나 회복량 추가 <color=green><b>+2</b></color>\nLv.2  마나 회복량 추가 <color=green><b>+4</b></color>\nLv.3  마나 회복량 추가 <color=green><b>+4</b></color>";
                break;

            case ToolTipObejct.TipType.R11:

                boxtext = $"<size=26><color=orange><b>[ 일렉트로닉샷 강화 ]</b></color></font>\n<size=24>일렉트로닉샷이 강화되여 적중시 <color=yellow>대미지 증가</color>하고\n<color=yellow>충전 속도</color>가 <color=yellow>증가</color>합니다.</size>\n<size=22>Lv.1  대미지 증가 <color=green><b>+2</b></color> / 속도 증가율 <color=green><b>+10%</b></color>\nLv.2  대미지 증가 <color=green><b>+4</b></color> / 속도 증가율 <color=green><b>+20%</b></color>\nLv.3  대미지 증가 <color=green><b>+6</b></color> / 속도 증가율 <color=green><b>+30%";
                break;

            case ToolTipObejct.TipType.R12:

                boxtext = $"<size=26><color=orange><b>[ 트리플샷 강화 ]</b></color></font>\n<size=24>트리플샷이 강화되어 각 화살의 <color=yellow>대미지 증가</color>하고\n시전당 발사되는 투사체가 <color=yellow>증가</color>합니다.</size>\n<size=22>Lv.1  대미지 증가 <color=green><b>+1</b></color> / 투사체 증가 <color=green><b>+1발</b></color>\nLv.2  대미지 증가 <color=green><b>+2</b></color> / 투사체 증가 <color=green><b>+2발</b></color>\nLv.3  대미지 증가 <color=green><b>+3</b></color> / 투사체 증가 <color=green><b>+3발</b></color>";
                break;

            case ToolTipObejct.TipType.R2:

                boxtext = $"<size=26><color=orange><b>[ 궁극기 강화 ]</b></color></font>\n<size=24>일반 공격 적중시 <color=yellow>발동 확률 증가</color>합니다.\n<size=22>Lv.1  발동확률 증가 <color=green><b>5%</b></color>";
                break;

            case ToolTipObejct.TipType.Expbar:
                float[] temp = ExpManager.instance.F_GetCurexpAndNeedExp();

                boxtext = $"<color=yellow>경험치  : {temp[0]} / {temp[1]}</color> ";
                break;

              
        }

        
        toolTipText.text = boxtext;

        //텍스트 렉트사이즈 설정
        Vector3 origin = new Vector3(toolTipText.preferredWidth, toolTipText.preferredHeight); 
        textBox.sizeDelta = origin;

        //박스 사이즈 설정 + 여백사이즈 더해서
        origin = new Vector3(origin.x + yubeckSizeX, origin.y + yubeckSizeY);
        Box.sizeDelta = origin;

    }

 
}
