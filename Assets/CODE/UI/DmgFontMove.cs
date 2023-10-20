using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgFontMove : MonoBehaviour
{
    TMP_Text text;
    Vector2 TargetPoint;
    [SerializeField] float EndTime = 0.3f;
    float Timer;
    private void OnEnable()
    {
        if(Timer != 0)
        {
            Timer = 0;
        }
    }
    public void F_DmgFont(float _value, Transform _obj)
    {
        transform.SetParent(_obj.transform);
        transform.position = _obj.position;
        if (text == null)   { text = GetComponent<TMP_Text>(); }
        TargetPoint = transform.parent.position + new Vector3(Random.Range(-0.6f,0.6f),1f);
        text.text = _value.ToString();

        Invoke("MoveFont",0.1f);
    }

    private void MoveFont()
    {
        if(text.text == null) { return; }

        Timer += Time.deltaTime;
        if (Timer < EndTime) 
        {
            transform.position = Vector2.Lerp(transform.position, TargetPoint, Time.deltaTime * 5);
            Invoke("MoveFont", 0.05f);
        }
        else if(Timer > EndTime)
        {
            Timer = 0;
            PoolManager.Instance.F_ReturnObj(gameObject, "Text");
        }
    }

}
