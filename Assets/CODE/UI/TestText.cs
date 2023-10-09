using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestText : MonoBehaviour
{
    TMP_Text text;
    RectTransform Rect;
    RectTransform box;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        Rect = GetComponent<RectTransform>();
        box = transform.parent.GetComponent<RectTransform>();   
    }
    void Start()
    {
        ddd();
    }

    void ddd()
    {
        
        Vector3 origin = Rect.sizeDelta;
        origin.x = text.preferredWidth; 
        origin.y = text.preferredHeight; 
        Rect.sizeDelta = origin;

                box.sizeDelta = origin;

    }
}
