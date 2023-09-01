using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //딕셔너리  int는 ID, 스트링[]은 output talk msg
    private Dictionary<int,string[]> TalkList;
    private void Awake()
    {
        TalkList = new Dictionary<int, string[]>();
        SetTalkMsg();
    }
    //대화가져있는 함수
    private void SetTalkMsg()
    {
        TalkList.Add( 100 , new string[] {"안녕하세요 \n 이곳에 처음오셨군요", "길을 따라가시면 튜토리얼을 진행하실수있습니다\n좋은 모험되세요!!"});

    }
   //밖으로 내보내는 함수
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
