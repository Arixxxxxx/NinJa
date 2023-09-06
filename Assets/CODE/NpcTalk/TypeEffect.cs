using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    TextMeshProUGUI text;
    string Msg;
    int MsgIndex;
    public float TypeingSpeed;
    public Image pressBtn;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        
    }
    public void F_SetMsg(string _MSG)
    {
        Msg = _MSG;
        EffectStart();
    }
    private void EffectStart()
    {
        text.text = "";
        MsgIndex = 0;
        pressBtn.gameObject.SetActive(false);

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

        pressBtn.gameObject.SetActive(true);
    }
}
