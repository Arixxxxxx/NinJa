using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class ToolTipObejct : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum TipType
    {
        Hp,Mp,Sp,Time, GM, Menu, Lv, M1, M11, M12, M2, R1, R11, R12, R2,Expbar, SkillTree, StatsWindow
    }

    public TipType Type;
    ToolTipController con;
    RectTransform tipBox;

    Transform toolTipBox;
    private void Start()
    {
        toolTipBox = GameManager.Instance.gameUI.Find("ToolTipBox").GetComponent<Transform>();
        con = GameManager.Instance.gameUI.GetComponent<ToolTipController>();
        tipBox = toolTipBox.Find("Box").GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.cursorOnUi = true;

        if (Type == TipType.Hp || Type == TipType.Mp || Type == TipType.Sp || Type == TipType.Time || Type == TipType.GM || Type == TipType.Menu || Type == TipType.Lv)
        {
            if (GameManager.Instance.SkillWindowPopup) { return; }
        }

        toolTipBox.gameObject.SetActive(true);
        Vector3 Pos = Camera.main.ScreenToViewportPoint(eventData.position);
        
        

        float X = 0;
        float Y = 0;

        Vector2 Edge =  new Vector3(tipBox.sizeDelta.x/2, tipBox.sizeDelta.y / 2, 0);

        if (Pos.x < 0.5f)
        {
            X = eventData.position.x + Edge.x;
        }
        else if(Pos.x > 0.5f)
        {
            X = eventData.position.x - Edge.x;
        }
        if(Pos.y < 0.5f)
        {
            Y = eventData.position.y + Edge.y;
        }
        else if(Pos.y > 0.5f)
        {
            Y = eventData.position.y - Edge.y;
        }

        toolTipBox.transform.position =  new Vector2(X, Y);

        con.F_ToolTipTextSet(Type);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        GameManager.Instance.cursorOnUi = false;
        toolTipBox.gameObject.SetActive(false);
        
    }
}
