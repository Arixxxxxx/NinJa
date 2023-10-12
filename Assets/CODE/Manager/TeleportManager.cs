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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // P1 코루틴 뒤 숫자는 지역번호임 , 0 = 시작지역, 1= 마을,
    public void F_TelePort(TelePortPoint.PlacePointer _Value, GameObject _Obj)
    {
        switch (_Value)
        {
            case TelePortPoint.PlacePointer.P1:
                GameUI.instance.F_BlackScrrenOnOff(true);
                StartCoroutine(P1(_Obj, 1, _Value));
                break;

            case TelePortPoint.PlacePointer.P2:
                GameUI.instance.F_BlackScrrenOnOff(true);
                StartCoroutine(P1(_Obj, 0, _Value));
                break;
        }
    }
    
    IEnumerator P1(GameObject _Obj, int _value, TelePortPoint.PlacePointer _Value)
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.F_SetPlaceNum(_value);
        _Obj.transform.position = PointerTR[_value].transform.position;
        Vector3 PlayerVec = _Obj.transform.position;
        Camera.transform.position = new Vector3(PlayerVec.x, PlayerVec.y, Camera.transform.position.z);
        GameUI.instance.F_BlackScrrenOnOff(false);
        yield return new WaitForSeconds(1);

        switch (_Value)
        {
            case TelePortPoint.PlacePointer.P1:
                GameUI.instance.F_SetMapMoveBar("스톰윈드");
                Debug.Log("ㅁㅁ");
                break;

            case TelePortPoint.PlacePointer.P2:
                GameUI.instance.F_SetMapMoveBar("성문");
                break;
        }
        
    }




}
