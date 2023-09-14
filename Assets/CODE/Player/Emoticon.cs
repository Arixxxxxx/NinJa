using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoticon : MonoBehaviour
{  
    public static Emoticon instance;

    SpriteRenderer Sr; // �����ڽ�
    SpriteRenderer Sprite; // �����ڽ��ȿ� �̸�Ƽ��
    [Header("# �̸�Ƽ�� �ڽ� ����")]
    [Space]
    [SerializeField] private float EnalbeTime = 4;
    [SerializeField] Sprite Angry; // ȭ�����
    [SerializeField] Sprite Smile; // ��۶� ���¸��
    [SerializeField] Sprite Think; // ��۶� ���¸��
    [SerializeField] Sprite Question; // ��۶� ���¸��
    

    //[SerializeField][Range(0f,3f)] float OnOffSpeed = 1;
    //private bool isEffetingAction;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        Sr = GetComponent<SpriteRenderer>();
        Sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        QuestionFilpx();


    }

    public void F_GetEmoticonBox(string _Value)
    {
        switch(_Value)
        {
            case "Angry":
                Sprite.flipX = false;
                Sprite.sprite = Angry;
                StartCoroutine(Action());
                //isEffetingAction = true;
                //Sr.color = new Color(1, 1, 1, 0);
                //Effeting();
                break;

            case "Smile":
                Sprite.flipX = false;
                Sprite.sprite = Smile;
                StartCoroutine(Action());
                //Effeting();
                break;

            case "Think":
                Sprite.flipX = false;
                Sprite.sprite = Think;
                StartCoroutine(Action());
                break;

            case "Question":
                Sprite.sprite = Question;
                StartCoroutine(Action());
                break;


        }    
    }
    IEnumerator Action()
    {
        Sr.enabled = true;
        Sprite.enabled = true;
        yield return new WaitForSecondsRealtime(EnalbeTime);
        Sr.enabled = false;
        Sprite.enabled = false;
    }
    
    /// <summary>
    /// ����ǥ�̸�Ƽ���� ĳ�����̷� �����Ǹ� ����ǥ�� ���������� ����ǥ���� ��¤����
    /// </summary>
    private void QuestionFilpx()
    {
        if (Sprite.sprite == Question)
        {
            if (GameManager.Instance.playerTR.transform.localScale.x < 0)
            {
                Sprite.flipX = true;
            }
            else if (GameManager.Instance.playerTR.transform.localScale.x > 0)
            {
                Sprite.flipX = false;
            }
        }
        else
        {
            Sprite.flipX = false;
        }
    }

    //�����ϰ� ������ ������ ��� 

    //private void Effeting()
    //{
    //    if (isEffetingAction)
    //    {
    //        if(Sr.color.a >= 0.95f)
    //        {
    //            Sprite.enabled = true;
    //            EndEffeting();
    //        }

    //        Sr.color += new Color(1, 1, 1, 0.3f) * Time.deltaTime;

    //        Invoke("Effeting", OnOffSpeed);
    //    }
    // }


    //private void EndEffeting()
    //{
    //    if (Sr.color.a < 0.05f)
    //    {
    //        Sprite.enabled = false;
    //        Sr.color = new Color(1, 1, 1, 0);
    //        isEffetingAction = false;
    //        return;
    //    }

    //    Sr.color -= new Color(1, 1, 1, 0.3f) * OnOffSpeed * Time.deltaTime;
    //    Invoke("EndEffeting", OnOffSpeed);
    //}
}

