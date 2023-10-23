using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoveCheker : MonoBehaviour
{
    public enum MapType
    {
        초원, 점프, 플랫폼, 정글동굴, 마을, 던전, 요정,엘윈,성문
    }

    public MapType Right;
    public MapType left;
    GameUI gameUI;
    SoundManager sound;

    private void Start()
    {
        gameUI = GameManager.Instance.gameUI.GetComponent<GameUI>();
        sound = SoundManager.instance;
    }

    Vector3 exitDir;
    float angle;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exitDir = (collision.transform.position - transform.position).normalized;
            //angle = Vector3.Angle(Vector3.right, exitDir);

            if(exitDir.x > 0) 
            {
                switch (Right)
                {
                    case MapType.초원:
                        gameUI.F_SetMapMoveBar("초원");
                        sound.AudioChanger(sound.mainThema);
                        break;
                    case MapType.점프:
                        gameUI.F_SetMapMoveBar("점프");
                        break;
                    case MapType.플랫폼:
                        gameUI.F_SetMapMoveBar("플랫폼");
                        sound.AudioChanger(sound.mainThema);
                        break;
                    case MapType.정글동굴:
                        gameUI.F_SetMapMoveBar("정글동굴");
                        sound.AudioChanger(sound.jungleCaveThema);
                        break;
                    case MapType.마을:
                        gameUI.F_SetMapMoveBar("마을");
                        sound.AudioChanger(sound.cityThema);
                        break;
                    case MapType.던전:
                        gameUI.F_SetMapMoveBar("던전1");
                        sound.AudioChanger(sound.Deongen);
                        break;
                    case MapType.요정:
                        gameUI.F_SetMapMoveBar("요정");
                        break;
                    case MapType.성문:
                        gameUI.F_SetMapMoveBar("성문");
                        
                        break;

                }
            }
            else
            {
                switch (left)
                {
                    case MapType.초원:
                        gameUI.F_SetMapMoveBar("초원");
                        sound.AudioChanger(sound.mainThema);
                        break;

                    case MapType.점프:
                        gameUI.F_SetMapMoveBar("점프");
                        break;

                    case MapType.플랫폼:
                       gameUI.F_SetMapMoveBar("플랫폼");
                       sound.AudioChanger(sound.mainThema);
                       break;

                    case MapType.정글동굴:
                        gameUI.F_SetMapMoveBar("정글동굴");
                        sound.AudioChanger(sound.jungleCaveThema);
                        break;

                    case MapType.마을:
                        gameUI.F_SetMapMoveBar("마을");
                        sound.AudioChanger(sound.cityThema);
                        break;

                    case MapType.던전:
                        gameUI.F_SetMapMoveBar("던전1");
                        sound.AudioChanger(sound.Deongen);
                        break;

                    case MapType.요정:
                        gameUI.F_SetMapMoveBar("요정");
                        break;
                    case MapType.엘윈:
                        gameUI.F_SetMapMoveBar("엘윈 숲");
                        break;
                   

                };
            }
        }
    }

}
