using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgFontCanvus : MonoBehaviour
{
    TMP_Text text;
    Vector3 OriginPos;
    Vector2 TargetPoint;
    [SerializeField] float EndTime = 0.3f;
    float Timer;
    private void OnEnable()
    {
        if (Timer != 0)
        {
            Timer = 0;
        }

     
    }

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<TMP_Text>();
    }
    
    public void F_DmgFont(float _value, Vector3 _obj)
    {
        transform.position = _obj;
        OriginPos = text.transform.position;
        if (text == null) { text = transform.GetChild(0).GetComponent<TMP_Text>(); }
        TargetPoint = transform.position + new Vector3(Random.Range(-0.6f, 0.6f), 1f);
        text.text = _value.ToString();

        Invoke("MoveFont", 0.1f);
    }

    private void MoveFont()
    {
        if (text.text == null) { return; }

        Timer += Time.deltaTime;
        if (Timer < EndTime)
        {
            text.transform.position = Vector2.Lerp(text.transform.position, TargetPoint, Time.deltaTime * 5);
            Invoke("MoveFont", 0.05f);
        }
        else if (Timer > EndTime)
        {
            Timer = 0;
            text.transform.position = OriginPos;
            PoolManager.Instance.F_ReturnObj(gameObject, "Text");
        }
    }
}
