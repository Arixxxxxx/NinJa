using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventButton : MonoBehaviour
{
    private TMP_Text text;
    
    AudioSource Audio;


    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        text = GetComponentInChildren<TMP_Text>();
        if (text == null)
        {
            Debug.Log($"<color=red>Error</color> = NotFound Text");
        }
    }

    public void MouseOn(bool OnEnter)
    {
        if (OnEnter == true)
        {
           
            Audio.Play();
            text.color = new Color(0, 0, 0, 1);
        }
        else
        {
            text.color = new Color(1, 1, 1, 1);
        }
    }
}
