using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //��ųʸ�  int�� ID, ��Ʈ��[]�� output talk msg
    private Dictionary<int,string[]> TalkList;
    private Dictionary<int, Sprite> TalkBoxSpriteList;
    [SerializeField] private Sprite[] BoxSprite;
    private void Awake()
    {
        TalkList = new Dictionary<int, string[]>();
        TalkBoxSpriteList = new Dictionary<int, Sprite>();
        SetTalkMsg();
    }
    //��ȭ�����ִ� �Լ�
    private void SetTalkMsg()
    {
        TalkList.Add( 100 , new string[] {"�ȳ��ϼ��� \n�̰��� ó�����̱���:0", "���� ���󰡽ø� Ʃ�丮���� �����ϽǼ��ֽ��ϴ�\n���� ����Ǽ���!!:0"});
        TalkBoxSpriteList.Add(100 + 0, BoxSprite[0]);
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
    
    public Sprite F_GetSprite(int _Id, int _SpriteIndex)
    {
        return TalkBoxSpriteList[_Id + _SpriteIndex];
    }
}
