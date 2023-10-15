using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    

public class ActionBarInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public enum SkillType
    {
        waveShock, whilWind, dragonPier, warCry, powerShot, tripleShot, boomShot, trap, MelleR, RangeR
    }
    public SkillType type;
    
    Transform skillbar;
    SkillInfo sc;
    private void Start()
    {
        skillbar = GameManager.Instance.gameUI.Find("SkillInfoBar").GetComponent<Transform>();
        sc = skillbar.GetComponent<SkillInfo>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.cursorOnUi = true;
        skillbar.gameObject.SetActive(true);
        //skillbar.transform.position = new Vector2(transform.position.x - 270, skillbar.transform.position.y);
        skillbar.transform.position = new Vector2(transform.position.x, skillbar.transform.position.y);
        

        sc.F_VideoChanger(type);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.cursorOnUi = false;
        skillbar.gameObject.SetActive(false);
    }
}
