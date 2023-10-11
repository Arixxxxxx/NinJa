using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUiText : MonoBehaviour
{
    public static GameUiText Instance;

    TextMeshProUGUI text;
    string Msg;
    int MsgIndex;
    public float TypeingSpeed;
    public bool NextTextOk;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        text = GetComponent<TextMeshProUGUI>();
        
    }
    public void F_SetMsg(string _MSG)
    {
        Msg = _MSG;
        EffectStart();
    }
    private void EffectStart()
    {
        gameObject.SetActive(true);
        NextTextOk =true;
        text.text = "";
        MsgIndex = 0;
        
        Invoke("Effecting", 1 / TypeingSpeed);
    }
    private void Effecting()
    {
        if(text.text== Msg && NextTextOk)
        {
            StartCoroutine(EffectEnd());
            return;
        }

        text.text += Msg[MsgIndex];
        MsgIndex++;

        Invoke("Effecting", 1 / TypeingSpeed);
    }

    IEnumerator EffectEnd()
    {
        NextTextOk = false;
        yield return new WaitForSecondsRealtime(3);
        gameObject.SetActive(false);
        
    }
}
