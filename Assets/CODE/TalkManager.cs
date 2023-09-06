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
    {// ��������Ʈ 0�� ���� / 1�� ǥ����
        TalkList.Add( 100 , new string[] { "�ȳ��ϼ���. �̰��� ���Ű� ȯ���մϴ�~! \n�̰����� ������ ���۹��� �˷��帮�� ���� \n ���� ������ <ǥ����>�� ��ġ�س����ϴ�!:0", "���� ���󰡽ø�  ���̽ô� <ǥ����> �� �о�ð�  \n�̰������� ��������� ���� �����ſ���!!:0"});
        TalkList.Add( 1000 , new string[] { "����̳� ��ֹ��� �ѱ����ؼ� <SpaceBar> Ű�� ������ �Ҽ��ֽ��ϴ�. \n���� ���� ������ �ѱ����� <SpaceBar>�� 2ȸ ������ 2�� ������ �����մϴ�:0 ", "�տ� ���̴� ����� ���� <�ι�° ǥ����> �� ã������!!.:0"});
        TalkList.Add( 1001 , new string[] { "������ �� �����̳׿�! �����ϴ�!\n�׷� �̹����� ���� �����̵��ϰ� �� ���� ���� ������ �ϴ� <�� ����>�� ������ڽ��ϴ�:0","���� ���� <����>�Ͽ� �������̵��� �ϴµ��� <SpaceBar>�� ���� �����غ�����\n�׷� ��ġ ����ó�� ���� Ÿ�� ������ �� �� �ֽ��ϴ�.:0","�׷� �������� Ȱ���� ���� ���� ������ �ö󰡺�����!\n�׷� <����° ǥ����> ���� �˰ڽ��ϴ�. !!:0"});
        TalkList.Add( 1002, new string[] { "������ �̰��� �����ϼ̳׿�!\n�̹����� ������ ���ؼ� ������ڽ��ϴ�.:0","�⺻������ �������� Ű���� [1] ���� ���� ���氡���ϰ�\n ������嶧�� <���콺 ��Ŭ��> '�Ϲݰ���' / <���콺 ��Ŭ��> '���а���' �Դϴ�:0","�׸��� Ű���� [2]���� ������ �Ǹ� ���Ÿ����� �����մϴ� \n �̶��� [���콺 ��Ŭ��] ���� <����>�� �ϰ� �ش�������� \n [��Ŭ��] �Ͽ� ȭ���� <�߻�>�Ҽ� �ֽ��ϴ�.:0","�ٸ� ȭ���� ������ ������ �ƴϴ� �� �����ϸ鼭 �����Ͻñ� �ٶ�����.\n �׸��� [ L-Ctrl ] Ű�� ���� < ������ > �� ����Ҽ��ս��ϴ�. \n '������' �� SP�� 15�Ҹ��ϸ� SP�� �ڵ����� ȸ�� �˴ϴ�.:0","�� �տ� ���� �ֳ׿�! ������ �غ��ô�!:0"});
        TalkBoxSpriteList.Add(100 + 0, BoxSprite[0]); //����
        TalkBoxSpriteList.Add(1000 + 0, BoxSprite[1]); //1���� ǥ����
        TalkBoxSpriteList.Add(1000 + 1, BoxSprite[1]); //2��° ǥ����
        TalkBoxSpriteList.Add(1000 + 2, BoxSprite[1]); //3��° ǥ����
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
