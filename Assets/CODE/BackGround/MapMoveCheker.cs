using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoveCheker : MonoBehaviour
{
    public enum MapType
    {
        �ʿ�, ����, �÷���, ���۵���, ����, ����, ����
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
                    case MapType.�ʿ�:
                        gameUI.SetMapMoveBar("�ʿ�");
                        sound.AudioChanger(sound.mainThema);
                        break;
                    case MapType.����:
                        gameUI.SetMapMoveBar("����");
                        break;
                    case MapType.�÷���:
                        gameUI.SetMapMoveBar("�÷���");
                        sound.AudioChanger(sound.mainThema);
                        break;
                    case MapType.���۵���:
                        gameUI.SetMapMoveBar("���۵���");
                        sound.AudioChanger(sound.jungleCaveThema);
                        break;
                    case MapType.����:
                        gameUI.SetMapMoveBar("����");
                        sound.AudioChanger(sound.cityThema);
                        break;
                    case MapType.����:
                        gameUI.SetMapMoveBar("����1");
                        sound.AudioChanger(sound.Deongen);
                        break;
                    case MapType.����:
                        gameUI.SetMapMoveBar("����");
                        break;

                }
            }
            else
            {
                switch (left)
                {
                    case MapType.�ʿ�:
                        gameUI.SetMapMoveBar("�ʿ�");
                        sound.AudioChanger(sound.mainThema);
                        break;

                    case MapType.����:
                        gameUI.SetMapMoveBar("����");
                        break;

                    case MapType.�÷���:
                       gameUI.SetMapMoveBar("�÷���");
                       sound.AudioChanger(sound.mainThema);
                       break;

                    case MapType.���۵���:
                        gameUI.SetMapMoveBar("���۵���");
                        sound.AudioChanger(sound.jungleCaveThema);
                        break;

                    case MapType.����:
                        gameUI.SetMapMoveBar("����");
                        sound.AudioChanger(sound.cityThema);
                        break;

                    case MapType.����:
                        gameUI.SetMapMoveBar("����1");
                        sound.AudioChanger(sound.Deongen);
                        break;

                    case MapType.����:
                        gameUI.SetMapMoveBar("����");
                        break;

                };
            }
        }
    }

}
