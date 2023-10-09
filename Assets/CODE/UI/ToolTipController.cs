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
        //�ʱ�ȭ
        string boxtext = string.Empty;
        textBox.sizeDelta = originTextRectSize;
        Box.sizeDelta = originBoxRectSize;

        switch (type)
        {
            case ToolTipObejct.TipType.Hp:
                boxtext = $"���� ����� : <color=red>{GameManager.Instance.Player_CurHP} / {GameManager.Instance.Player_MaxHP}</color>\n�ʴ� <color=yellow>0.5</color>��ŭ �ڿ� ȸ��";
                break;

            case ToolTipObejct.TipType.Mp:
                boxtext = $"���� ���� : <color=blue>{GameManager.Instance.Player_CurMP} / {GameManager.Instance.Player_MaxMP}</color>\n�ʴ� <color=yellow>0.5</color>��ŭ �ڿ� ȸ��";
                break;

            case ToolTipObejct.TipType.Sp:
                boxtext = $"���� ��� : <color=green>{GameManager.Instance.Player_CurSP} / {GameManager.Instance.Player_MaxSP}</color>\n�ʴ� <color=yellow>{Player.instance.secRecerSP}</color>��ŭ �ڿ� ȸ��";
                break;

            case ToolTipObejct.TipType.Time:
                boxtext = $"���� �ð�";
                break;

            case ToolTipObejct.TipType.GM:
                boxtext = $"<color=red>GM Mode</color>";
                break;

            case ToolTipObejct.TipType.Menu:
                boxtext = $"Main Menu";
                break;
        }

        
        toolTipText.text = boxtext;

        //�ؽ�Ʈ ��Ʈ������ ����
        Vector3 origin = new Vector3(toolTipText.preferredWidth, toolTipText.preferredHeight); 
        textBox.sizeDelta = origin;

        //�ڽ� ������ ���� + ��������� ���ؼ�
        origin = new Vector3(origin.x + yubeckSizeX, origin.y + yubeckSizeY);
        Box.sizeDelta = origin;

    }
}
