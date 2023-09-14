using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoticonPointer : MonoBehaviour
{
    public enum EmoticonPoint
    {
        Smile, Angry, Think, Question
    }


    [Header("#이모티콘 팝업 셋팅")]
    [Space]
    public EmoticonPoint type;
    [SerializeField] private bool once;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            switch (type)
            {
                case EmoticonPoint.Smile:
                    Emoticon.instance.F_GetEmoticonBox("Smile");
                    this.gameObject.SetActive(false);
                    once = true;
                    break;

                case EmoticonPoint.Angry:
                    Emoticon.instance.F_GetEmoticonBox("Angry");
                    once = true;
                    this.gameObject.SetActive(false);
                    break;

                case EmoticonPoint.Think:
                    Emoticon.instance.F_GetEmoticonBox("Think");
                    once = true;
                    this.gameObject.SetActive(false);
                    break;

                case EmoticonPoint.Question:
                    Emoticon.instance.F_GetEmoticonBox("Question");
                    once = true;
                    this.gameObject.SetActive(false);
                    break;
            }
        }
    }
}
