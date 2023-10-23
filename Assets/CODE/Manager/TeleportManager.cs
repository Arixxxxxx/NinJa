using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    Transform[] PointerTR;
    Transform Camera;
    [SerializeField] Transform BossTr;
    Boss sc;
    private void Awake()
    {
        PointerTR = new Transform[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            PointerTR[i]  = transform.GetChild(i).GetComponent<Transform>();
        }
        Camera = GameObject.Find("Camera").GetComponent<Transform>();

    }
    private void Start()
    {
        sc = BossTr.GetComponent<Boss>();
    }
    // P1 코루틴 뒤 숫자는 지역번호임 , 0 = 시작지역, 1= 마을,
    public void F_TelePort(TelePortPoint.PlacePointer _Value, GameObject _Obj)
    {
        switch (_Value)
        {
            case TelePortPoint.PlacePointer.P1:
                GameManager.Instance.F_MoveStop(0);
                GameUI.instance.F_BlackScrrenOnOff(true);
                StartCoroutine(P1(_Obj, 1,1, _Value));
                break;

            case TelePortPoint.PlacePointer.P2:
                GameManager.Instance.F_MoveStop(0);
                GameUI.instance.F_BlackScrrenOnOff(true);
                StartCoroutine(P1(_Obj, 0, 0,_Value));
                break;

            case TelePortPoint.PlacePointer.P3:
                GameManager.Instance.F_MoveStop(0);
                GameUI.instance.F_BlackScrrenOnOff(true);
                StartCoroutine(P1(_Obj, 2,3, _Value));
                break;

            case TelePortPoint.PlacePointer.P4:
                GameManager.Instance.F_MoveStop(0);
                GameUI.instance.F_BlackScrrenOnOff(true);
                StartCoroutine(P1(_Obj, 1, 2,_Value));
                break;

            case TelePortPoint.PlacePointer.P5:
                GameManager.Instance.F_MoveStop(0);
                GameUI.instance.F_BlackScrrenOnOff(true);
                StartCoroutine(P5(_Obj, 3, 5, _Value));
                break;
        }
    }
    
    IEnumerator P1(GameObject _Obj, int _PlaceNum, int _TrCounter, TelePortPoint.PlacePointer _Value)
    {
        switch (_Value)
        {
            case TelePortPoint.PlacePointer.P1:
                SoundManager.instance.AudioChanger(SoundManager.instance.F_Get_Audio_List(1));
                break;
            case TelePortPoint.PlacePointer.P2:
                SoundManager.instance.AudioChanger(SoundManager.instance.mainThema);
                break;
            case TelePortPoint.PlacePointer.P3:
                SoundManager.instance.AudioChanger(SoundManager.instance.F_Get_Audio_List(2));
                break;
            case TelePortPoint.PlacePointer.P4:
                SoundManager.instance.AudioChanger(SoundManager.instance.F_Get_Audio_List(1));
                break;
        }

        yield return new WaitForSeconds(1);
        GameManager.Instance.F_SetPlaceNum(_PlaceNum);
        _Obj.transform.position = PointerTR[_TrCounter].transform.position;
        Vector3 PlayerVec = _Obj.transform.position;
        switch (_Value)
        {
            case TelePortPoint.PlacePointer.P1:
                Camera.transform.position = new Vector3(136, Camera.transform.position.y, Camera.transform.position.z);
                break;
                case TelePortPoint.PlacePointer.P2:
                Camera.transform.position = new Vector3(112, Camera.transform.position.y, Camera.transform.position.z);
                break;
            case TelePortPoint.PlacePointer.P3:
                Camera.transform.position = new Vector3(200, Camera.transform.position.y, Camera.transform.position.z);
                break;
            case TelePortPoint.PlacePointer.P4:
                Camera.transform.position = new Vector3(281, Camera.transform.position.y, Camera.transform.position.z);
                break;
            case TelePortPoint.PlacePointer.P5:
                
                break;
        }
        
        yield return new WaitForSeconds(0.3f);
        GameUI.instance.F_BlackScrrenOnOff(false);
        GameManager.Instance.F_MoveStop(1);
        yield return new WaitForSeconds(1);



        switch (_Value)
        {
            case TelePortPoint.PlacePointer.P1:
                GameUI.instance.F_SetMapMoveBar("스톰윈드");
                break;

            case TelePortPoint.PlacePointer.P2:
                GameUI.instance.F_SetMapMoveBar("성문");
                break;

            case TelePortPoint.PlacePointer.P3:
                GameUI.instance.F_SetMapMoveBar("던전1층");
                break;

            case TelePortPoint.PlacePointer.P4:
                GameUI.instance.F_SetMapMoveBar("스톰윈드");
                break;

            case TelePortPoint.PlacePointer.P5:
                GameUI.instance.F_SetMapMoveBar("보스");
                break;
        }

    }

    IEnumerator P5(GameObject _Obj, int _PlaceNum, int _TrCounter, TelePortPoint.PlacePointer _Value)
    {
        SoundManager.instance.AudioChanger(SoundManager.instance.F_Get_Audio_List(3));
        yield return new WaitForSeconds(1);
        GameManager.Instance.F_SetPlaceNum(_PlaceNum);
        _Obj.transform.position = PointerTR[_TrCounter].transform.position;
        Vector3 PlayerVec = _Obj.transform.position;
        Camera.transform.position = new Vector3(309.7f, Camera.transform.position.y, Camera.transform.position.z);
        yield return new WaitForSeconds(0.3f);
        GameUI.instance.F_BlackScrrenOnOff(false);

        switch (_Value)
        {
            case TelePortPoint.PlacePointer.P5:
                GameUI.instance.F_SetMapMoveBar("보스");
                break;
        }

        Emoticon.instance.F_GetEmoticonBox("Question");
        GameManager.Instance.CameraShakeSwitch(0);
        yield return new WaitForSeconds(3);
        GameManager.Instance.CameraShakeSwitch(1);
        yield return new WaitForSeconds(0.5f);
        GameUI.instance.F_CenterTextPopup("흐흐흐..게임을 시작하지...");
        SoundManager.instance.F_SoundPlay(SoundManager.instance.lougther, 0.8f);
        BossTr.gameObject.SetActive(true);
        yield return new WaitForSeconds(6);
        sc.F_GameStartBoss();
        GameManager.Instance.F_MoveStop(1);
        yield return new WaitForSeconds(1);



      

    }

}
