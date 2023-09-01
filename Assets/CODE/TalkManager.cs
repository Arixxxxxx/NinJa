using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //��ųʸ�  int�� ID, ��Ʈ��[]�� output talk msg
    private Dictionary<int,string[]> TalkList;
    private void Awake()
    {
        TalkList = new Dictionary<int, string[]>();
        SetTalkMsg();
    }
    //��ȭ�����ִ� �Լ�
    private void SetTalkMsg()
    {
        TalkList.Add( 100 , new string[] {"�ȳ��ϼ��� \n �̰��� ó�����̱���", "���� ���󰡽ø� Ʃ�丮���� �����ϽǼ��ֽ��ϴ�\n���� ����Ǽ���!!"});

    }
   //������ �������� �Լ�
   public string F_GetMsg(int _ID, int _TalkIndex)
    {
        if(_TalkIndex == TalkList[_ID].Length)
        {
            return null;
        }
        else
        {
            return TalkList[_ID][_TalkIndex];
        }
        
    }
    
}
