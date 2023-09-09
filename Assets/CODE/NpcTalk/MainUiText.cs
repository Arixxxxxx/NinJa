using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUiText : MonoBehaviour
{
    TMP_Text text;
    string Msg;
    int MsgIndex;
    public float TypeingSpeed;
    public bool NextTextOk;
    public bool anigo;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        
    }
    public void F_SetMsg(string _MSG)
    {
        Msg = _MSG;
        EffectStart();
    }
    private void EffectStart()
    {
        NextTextOk=true;
        text.text = "";
        MsgIndex = 0;
        
        Invoke("Effecting", 1 / TypeingSpeed);
    }
    private void Effecting()
    {
        if(text.text== Msg)
        {
            EffectEnd();
            return;
        }

        text.text += Msg[MsgIndex];
        MsgIndex++;

        Invoke("Effecting", 1 / TypeingSpeed);
    }
    private void EffectEnd()
    {
        NextTextOk = false;
        anigo = true;
    }
}
