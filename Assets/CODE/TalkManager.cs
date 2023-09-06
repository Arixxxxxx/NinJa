using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //딕셔너리  int는 ID, 스트링[]은 output talk msg
    private Dictionary<int,string[]> TalkList;
    private Dictionary<int, Sprite> TalkBoxSpriteList;
    [SerializeField] private Sprite[] BoxSprite;
    private void Awake()
    {
        TalkList = new Dictionary<int, string[]>();
        TalkBoxSpriteList = new Dictionary<int, Sprite>();
        SetTalkMsg();
    }
    //대화가져있는 함수
    private void SetTalkMsg()
    {// 스프라이트 0번 리리 / 1번 표지판
        TalkList.Add( 100 , new string[] { "안녕하세요. 이곳에 오신걸 환영합니다~! \n이곳에서 용사님의 조작법을 알려드리기 위해 \n 제가 곳곳에 <표지판>을 설치해놨습니다!:0", "길을 따라가시면  보이시는 <표지판> 을 읽어보시고  \n이곳에서의 생존방식을 배울수 있을거예요!!:0"});
        TalkList.Add( 1000 , new string[] { "언덕이나 장애물을 넘기위해서 <SpaceBar> 키로 점프를 할수있습니다. \n또한 높은 지형을 넘기위해 <SpaceBar>를 2회 누르면 2단 점프가 가능합니다:0 ", "앞에 보이는 언덕을 지나 <두번째 표지판> 을 찾으세요!!.:0"});
        TalkList.Add( 1001 , new string[] { "점프를 잘 익히셨네요! 좋습니다!\n그럼 이번에는 벽을 슬라이딩하고 또 벽을 차며 점프를 하는 <벽 점프>를 배워보겠습니다:0","벽을 향해 <점프>하여 벽슬라이딩을 하는도중 <SpaceBar>를 눌러 점프해보세요\n그럼 마치 닌자처럼 벽을 타며 점프를 할 수 있습니다.:0","그럼 벽점프를 활용해 앞을 앞의 절벽을 올라가보세요!\n그럼 <세번째 표지판> 에서 뵙겠습니다. !!:0"});
        TalkList.Add( 1002, new string[] { "무사히 이곳에 도착하셨네요!\n이번에는 전투에 대해서 배워보겠습니다.:0","기본적으로 근접모드는 키보드 [1] 번을 눌러 변경가능하고\n 근접모드때는 <마우스 좌클릭> '일반공격' / <마우스 우클릭> '방패가드' 입니다:0","그리고 키보드 [2]번을 누르게 되면 원거리모드로 진입합니다 \n 이때는 [마우스 우클릭] 으로 <조준>을 하고 해당방향으로 \n [좌클릭] 하여 화살을 <발사>할수 있습니다.:0","다만 화살의 갯수는 무한이 아니니 잘 관리하면서 진행하시길 바랄께요.\n 그리고 [ L-Ctrl ] 키를 눌러 < 구르기 > 를 사용할수잇습니다. \n '구르기' 는 SP를 15소모하며 SP는 자동으로 회복 됩니다.:0","이 앞에 좀비가 있네요! 전투를 해봅시다!:0"});
        TalkBoxSpriteList.Add(100 + 0, BoxSprite[0]); //리리
        TalkBoxSpriteList.Add(1000 + 0, BoxSprite[1]); //1번쨰 표지판
        TalkBoxSpriteList.Add(1000 + 1, BoxSprite[1]); //2번째 표지판
        TalkBoxSpriteList.Add(1000 + 2, BoxSprite[1]); //3번째 표지판
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
    
    public Sprite F_GetSprite(int _Id, int _SpriteIndex)
    {
        return TalkBoxSpriteList[_Id + _SpriteIndex];
    }
}
