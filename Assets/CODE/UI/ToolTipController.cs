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
                boxtext = $"현재 생명력 : <color=red>{GameManager.Instance.Player_CurHP} / {GameManager.Instance.Player_MaxHP}</color>\n초당 <color=yellow>0.5</color>만큼 자연 회복";
                break;

            case ToolTipObejct.TipType.Mp:
                boxtext = $"현재 마나 : <color=blue>{GameManager.Instance.Player_CurMP} / {GameManager.Instance.Player_MaxMP}</color>\n초당 <color=yellow>0.5</color>만큼 자연 회복";
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
                boxtext = $"Main Menu";
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
