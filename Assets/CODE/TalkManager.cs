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
    {// 스프라이트 0번 리리 / 1번 표지판 // 2번 전투교관
        TalkList.Add( 100 , new string[] { "안녕하세요. 오랫동안 용사님을 기다렸습니다.\n이곳을 헤쳐나가기위한 방법을 <안내판>을  적어 곳곳에 배치해놨습니다:0","길을 따라가시면  보이시는 <표지판> 을 꼭 읽어보시고  \n 이 마을의 동굴 끝에 있는 <전설의 장비> 를 획득해주세요!:0", "저는 그곳에가서 당신을 기다리겠습니다\n 행운을 빌겠습니다 용사님!!!:0" });
        TalkList.Add(101, new string[] { "앗!! < 전설의 방어구 > 를 획득하셨네요 \n 이제부터는 그 무기로 <몬스터>들과 싸울있는 <전투훈련>을 할 시간이네요:0","왔던 길로 나가면 너무 위험하니 뒤에 전투교관님에게로 가는 고대 차원문을\n소환해드리겠습니다! 그곳에서 전투교관님을 만나 전투를 익혀보세요!!:0" });
        TalkList.Add( 200 , new string[] { "어서와라. 나는 전투교관이다 \n 보아하니 높은 산 깊은 동굴안에있는 <전설의 방어구> 를 얻었구만... :0", "그럼 그 장비가 자네의 것이 맞나 시험할 기회를 주지\n뒤에 있는 오두막에 있는 좀비를 풀어주겠네.. :0","적들을 물리쳐보게나:0"});
        TalkList.Add( 201 , new string[] { "잘해치웠군!! 잘했어\n 하지만 아직 앞에있는 깊은동굴 속 <전설의 활> 이 남아있다 :0", "그 장비를 획득하고 산꼭대에서 다시 만나세\n행운을 빌겠네.. :0"});
        TalkList.Add( 202 , new string[] { "대단하군!! <전설의 활> 을 찾아내다니!!\n일단 이곳은 위험하니깐 이 산을 벗어나세:0","고대 차원문을 소환하겠네, 처음만났던 곳에서 다시 만나지:0"});
        TalkList.Add( 203 , new string[] { "<전설의 활>을 획득한 자네를 보니 너무 자랑스럽네\n이제 그 활로 전투를 연습해보지:0","저 뒤에 의자를 배치해놨네.. <의자> 위에 올라서서 날아드는 새를\n10마리 잡고 다시 말을 걸어주게나:0"});
        TalkList.Add( 204 , new string[] { "굉장한 솜씨야! 이제 모험을 나설 준비가 끝났군.. \n옆마을에 언데드 군단이 처들어오고있다네.. :0","자네가 가서 도와주게나..\n그럼 행운을 빌게:0"});
        TalkList.Add( 300 , new string[] { "용사님.. 던전의 입구가 큰 바위들로 막혀있습니다..\n바위를 치워주시면 던전으로 들어가는 문을 열어 드리겠습니다 :0"});
        TalkList.Add( 301 , new string[] { "던전으로 들어가는 입구를 확보해주셨군요\n약속대로 들어갈수있는 문을 열어 드리겠습니다:0","그리고 던전 1층에 있는 모든 몬스터들을 토벌해주세요\n 행운을 빌겠습니다.:0"});

        //TalkList.Add( 1000 , new string[] { "언덕이나 장애물을 넘기위해서 <SpaceBar> 키로 점프를 할수있습니다. \n또한 높은 지형을 넘기위해 <SpaceBar>를 2회 누르면 2단 점프가 가능합니다:0 ", "앞에 보이는 언덕을 지나 <두번째 표지판> 을 찾으세요!!.:0"});
        //TalkList.Add( 1001 , new string[] { "점프를 잘 익히셨네요! 좋습니다!\n그럼 이번에는 벽을 슬라이딩하고 또 벽을 차며 점프를 하는 <벽 점프>를 배워보겠습니다:0","벽을 향해 <점프>하여 벽슬라이딩을 하는도중 <SpaceBar>를 눌러 점프해보세요\n그럼 마치 닌자처럼 벽을 타며 점프를 할 수 있습니다.:0","그럼 벽점프를 활용해 앞을 앞의 절벽을 올라가보세요!\n그럼 <세번째 표지판> 에서 뵙겠습니다. !!:0"});
        //TalkList.Add( 1002, new string[] { "무사히 이곳에 도착하셨네요!\n이번에는 전투에 대해서 배워보겠습니다.:0","기본적으로 근접모드는 키보드 [1] 번을 눌러 변경가능하고\n 근접모드때는 <마우스 좌클릭> '일반공격' / <마우스 우클릭> '방패가드' 입니다:0","그리고 키보드 [2]번을 누르게 되면 원거리모드로 진입합니다 \n 이때는 [마우스 우클릭] 으로 <조준>을 하고 해당방향으로 \n [좌클릭] 하여 화살을 <발사>할수 있습니다.:0","다만 화살의 갯수는 무한이 아니니 잘 관리하면서 진행하시길 바랄께요.\n 그리고 [ L-Ctrl ] 키를 눌러 < 구르기 > 를 사용할수잇습니다. \n '구르기' 는 SP를 15소모하며 SP는 자동으로 회복 됩니다.:0","이 앞에 좀비가 있네요! 전투를 해봅시다!:0"});
        TalkBoxSpriteList.Add(100 + 0, BoxSprite[0]); //리리
        TalkBoxSpriteList.Add(101 + 0, BoxSprite[0]); //리리
        TalkBoxSpriteList.Add(200 + 0, BoxSprite[2]); //전투교관
        TalkBoxSpriteList.Add(201 + 0, BoxSprite[2]); //전투교관
        TalkBoxSpriteList.Add(202 + 0, BoxSprite[2]); //전투교관
        TalkBoxSpriteList.Add(203 + 0, BoxSprite[2]); //전투교관
        TalkBoxSpriteList.Add(204 + 0, BoxSprite[2]); //전투교관
        if (GameManager.Instance.SceneName == "Chapter2")
        {
            TalkBoxSpriteList.Add(300 + 0, BoxSprite[3]); //아줌마
            TalkBoxSpriteList.Add(301 + 0, BoxSprite[3]); //아줌마
        }
        
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
    public GameObject F_Getobj()
    {
        GameObject obj = this.gameObject;
        return obj;
    }
    public Sprite F_GetSprite(int _Id, int _SpriteIndex)
    {
        return TalkBoxSpriteList[_Id + _SpriteIndex];
    }
}
