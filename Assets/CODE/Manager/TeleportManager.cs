using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    Transform[] PointerTR;
    Transform Camera;
    private void Awake()
    {
        PointerTR = new Transform[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            PointerTR[i]  = transform.GetChild(i).GetComponent<Transform>();
        }
        Camera = GameObject.Find("Camera").GetComponent<Transform>();
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
        }
    }
    
    IEnumerator P1(GameObject _Obj, int _PlaceNum, int _TrCounter,TelePortPoint.PlacePointer _Value)
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.F_SetPlaceNum(_PlaceNum);
        _Obj.transform.position = PointerTR[_TrCounter].transform.position;
        Vector3 PlayerVec = _Obj.transform.position;
        Camera.transform.position = new Vector3(PlayerVec.x, PlayerVec.y, Camera.transform.position.z);
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
        }
        
    }




}
