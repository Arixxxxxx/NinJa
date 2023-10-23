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
              string HP = GameManager.Instance.Player_CurHP.ToString("F1");

                boxtext = $"���� ����� : <color=red>{HP} / {GameManager.Instance.Player_MaxHP}</color>\n�ʴ� <color=yellow>0.5</color>��ŭ �ڿ� ȸ��";
                break;

            case ToolTipObejct.TipType.Mp:
                string MP = GameManager.Instance.Player_CurMP.ToString("F1");
                boxtext = $"���� ���� : <color=#00ffffff>{MP} / {GameManager.Instance.Player_MaxMP}</color>\n�ʴ� <color=yellow>0.5</color>��ŭ �ڿ� ȸ��";
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
                
                boxtext = $"Main Menu <color=yellow>(ESC)</color>";
                break;

            case ToolTipObejct.TipType.Lv:
                
                boxtext = $"���� ����";
                break;

            case ToolTipObejct.TipType.SkillTree:
                boxtext = $"��ųƮ�� â <color=yellow>(K)</color>";
                break;

            case ToolTipObejct.TipType.StatsWindow:
                boxtext = $"ĳ���� ����â<color=yellow>(C)</color>";
                break;

            case ToolTipObejct.TipType.M1:

                boxtext = $"<size=26><color=orange><b>[ ������ �ϰ� ]</b></color></font>\n<size=24>�Ϲݰ��� ���߽� <color=yellow>30%Ȯ��</color>�� ����\n<color=green>������� ���</color>�մϴ�.</size>\n<size=22>Lv.1  ����� ��� <color=green><b>+2</b></color>\nLv.2  ����� ��� <color=green><b>+4</b></color>\nLv.3  ����� ��� <color=green><b>+6</b></color></size>";
                break;

            case ToolTipObejct.TipType.M11:

                boxtext = $"<size=26><color=orange><b>[ ������ ��ȭ ]</b></color></font>\n<size=24>�����带 ��ȭ�Ͽ� ���߽� <color=yellow>����� ����</color>�ϰ�\n<color=yellow>����� ����</color>�� �����մϴ�.</size>\n<size=22>Lv.1  ����� ���� <color=green><b>+2</b></color> / ���� ���� <color=red><b>-0.1��</b></color>\nLv.2  ����� ���� <color=green><b>+4</b></color> / ���� ���� <color=red><b>-0.2��</b></color>\nLv.3  ����� ���� <color=green><b>+6</b></color> / ���� ���� <color=red><b>-0.3��";
                break;

            case ToolTipObejct.TipType.M12:

                boxtext = $"<size=26><color=orange><b>[ ����� ��ȭ ]</b></color></font>\n<size=24>����ĸ� ��ȭ�Ͽ� ���߽� <color=yellow>����� ����</color>�ϰ�\n<color=yellow>���� �ð�</color>�� �����մϴ�.</size>\n<size=22>Lv.1  ����� ���� <color=green><b>+3</b></color> / ���Ͻð� ���� <color=green><b>+0.25��</b></color>\nLv.2  ����� ���� <color=green><b>+6</b></color> / ���Ͻð� ���� <color=green><b>+0.5��</b></color>\nLv.3  ����� ���� <color=green><b>+9</b></color> / ���Ͻð� ���� <color=green><b>+0.75��</b></color>";
                break;

            case ToolTipObejct.TipType.M2:

                boxtext = $"<size=26><color=orange><b>[ �ñر� ��ȭ ]</b></color></font>\n<size=24>�Ϲ� ���� ���߽� <color=yellow>�ߵ� Ȯ�� ����</color>�մϴ�.\n<size=22>Lv.1  �ߵ�Ȯ�� ���� <color=green><b>5%</b></color>";
                break;

            case ToolTipObejct.TipType.R1:

                boxtext = $"<size=26><color=orange><b>[ ������� ��ȭ ]</b></color></font>\n<size=24>�Ϲݰ��ݽ� �����Ǵ� <color=yellow>����</color>��\n<color=green>����</color>�մϴ�.</size>\n<size=22>Lv.1  ���� ȸ���� �߰� <color=green><b>+2</b></color>\nLv.2  ���� ȸ���� �߰� <color=green><b>+4</b></color>\nLv.3  ���� ȸ���� �߰� <color=green><b>+4</b></color>";
                break;

            case ToolTipObejct.TipType.R11:

                boxtext = $"<size=26><color=orange><b>[ �Ϸ�Ʈ�δм� ��ȭ ]</b></color></font>\n<size=24>�Ϸ�Ʈ�δм��� ��ȭ�ǿ� ���߽� <color=yellow>����� ����</color>�ϰ�\n<color=yellow>���� �ӵ�</color>�� <color=yellow>����</color>�մϴ�.</size>\n<size=22>Lv.1  ����� ���� <color=green><b>+2</b></color> / �ӵ� ������ <color=green><b>+10%</b></color>\nLv.2  ����� ���� <color=green><b>+4</b></color> / �ӵ� ������ <color=green><b>+20%</b></color>\nLv.3  ����� ���� <color=green><b>+6</b></color> / �ӵ� ������ <color=green><b>+30%";
                break;

            case ToolTipObejct.TipType.R12:

                boxtext = $"<size=26><color=orange><b>[ Ʈ���ü� ��ȭ ]</b></color></font>\n<size=24>Ʈ���ü��� ��ȭ�Ǿ� �� ȭ���� <color=yellow>����� ����</color>�ϰ�\n������ �߻�Ǵ� ����ü�� <color=yellow>����</color>�մϴ�.</size>\n<size=22>Lv.1  ����� ���� <color=green><b>+1</b></color> / ����ü ���� <color=green><b>+1��</b></color>\nLv.2  ����� ���� <color=green><b>+2</b></color> / ����ü ���� <color=green><b>+2��</b></color>\nLv.3  ����� ���� <color=green><b>+3</b></color> / ����ü ���� <color=green><b>+3��</b></color>";
                break;

            case ToolTipObejct.TipType.R2:

                boxtext = $"<size=26><color=orange><b>[ �ñر� ��ȭ ]</b></color></font>\n<size=24>�Ϲ� ���� ���߽� <color=yellow>�ߵ� Ȯ�� ����</color>�մϴ�.\n<size=22>Lv.1  �ߵ�Ȯ�� ���� <color=green><b>5%</b></color>";
                break;

            case ToolTipObejct.TipType.Expbar:
                float[] temp = ExpManager.instance.F_GetCurexpAndNeedExp();

                boxtext = $"<color=yellow>����ġ  : {temp[0]} / {temp[1]}</color> ";
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
