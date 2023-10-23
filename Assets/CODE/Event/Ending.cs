using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{

    Image WhiteScrren;
    Image BlackScrren;
    [SerializeField] private float CuttonSpeed;
    [SerializeField] private Animator DengeonAni;
    [SerializeField] bool Action1;
    [SerializeField] bool Action2;
    [SerializeField] bool Action3;
    [SerializeField] private Transform[] Npcs;
    [SerializeField] private Transform[] EndingNpcPosPoint;
    bool once, once1;
    private void Awake()
    {

        WhiteScrren = transform.Find("WhiteScreen").GetComponent<Image>();
        BlackScrren = transform.Find("BlackScreen").GetComponent<Image>();

        int Count = Npcs.Length;
        for(int i = 0; i < Count; i++)
        {
            Npcs[i].transform.position = EndingNpcPosPoint[i].transform.position;

        }
    }

    private void Start()
    {
        Action1 = true;
    }

    private void Update()
    {
        Action1Start();
        Action2Start();
        Action3Start();
    }

    private void Action1Start()
    {
        if (!Action1) { return; }

        if (WhiteScrren.color.a > 0.01f)
        {
            WhiteScrren.color -= new Color(0, 0, 0, 0.1f) * CuttonSpeed * Time.deltaTime;
        }
        else if (WhiteScrren.color.a < 0.01)
        {
            WhiteScrren.color = new Color(1, 1, 1, 0f);
            Action1 = false;
            Action2 = true;
        }
    }


    private void Action2Start()
    {
        if (!Action2) { return; }
        if (Action2 && !once)
        {
            once = true;
            Debug.Log("11");
            StartCoroutine(Act2());
        }


    }

    IEnumerator Act2()
    {
        Emoticon.instance.F_GetEmoticonBox("Smile");
        yield return new WaitForSeconds(1);
        GameManager.Instance.CameraShakeSwitch(0);
        yield return new WaitForSeconds(1);
        DengeonAni.SetTrigger("Destory");
        SoundManager.instance.F_SoundPlay(SoundManager.instance.NPCHwanHo, 0.7f);
        yield return new WaitForSeconds(8f);
        GameManager.Instance.CameraShakeSwitch(1);
        yield return new WaitForSeconds(1);
        transform.Find("MissionEnd").gameObject.SetActive(true);
        transform.Find("MissionEnd").GetComponent<Animator>().SetTrigger("Ok");
        yield return new WaitForSeconds(5);
        Action3 = true;

    }


    private void Action3Start()
    {
        if (!Action3) { return; }
        if (Action3 && !once1)
        {
           
            if (BlackScrren.color.a >= 0.99f)
            {
                BlackScrren.color = new Color(0, 0, 0, 1);
                once1 = true;
                SceneManager.LoadScene("End");
            }
            else if (BlackScrren.color.a >= 0)
            {
                BlackScrren.color += new Color(0, 0, 0, 0.05f) * CuttonSpeed * Time.deltaTime;
            }

        }
    }
}
