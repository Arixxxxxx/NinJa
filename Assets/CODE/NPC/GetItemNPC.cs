using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetItemNPC : MonoBehaviour
{
    public static GetItemNPC Instance;
    //소환문 소환
    public Animator aniGate;
    public ParticleSystem partiGate;
    public AudioSource Audio;


    Transform itemsSprite;
    Transform canvas;
    ParticleSystem itemLight;
    GuideManager guideManager;
    bool once;
    bool once1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        Audio = GetComponent<AudioSource>();
        guideManager = GameObject.Find("GameGuide").GetComponent<GuideManager>();
        itemLight = transform.Find("Light").GetComponent<ParticleSystem>();
        itemsSprite = transform.Find("KAL").GetComponent<Transform>();
        canvas = transform.Find("Canvas").GetComponent<Transform>(); //장비획득 표시창
        aniGate = transform.Find("GateWay/Gate").GetComponent<Animator>();
        partiGate = transform.Find("GateWay/Parti").GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        MeleeItem();
    }

    //테스트용
    public bool Itemoff;
    private void MeleeItem()
    {
        //if (!Itemoff)
        if (itemLight.gameObject.activeSelf)
        {
            if (GameManager.Instance.isGetMeleeItem)
            {
                GameManager.Instance.player.Itemget0 = true;
                //먹었으니 템주고 플레이어 전투기능 켜줌설명서 보여줌
                if (!once)
                {
                    once = true;
                    guideManager.StopCharacter();
                    itemsSprite.gameObject.SetActive(false);
                    canvas.gameObject.SetActive(false); // 장비획득표시창 off
                    StartCoroutine(itemShowMeleeMode());
                    //GameManager.Instance.player.ora.SetTrigger("Up");
                    Player.instance.Ps.gameObject.SetActive(true);
                    Player.instance.Ps.Play();

                    StartCoroutine(ShowAni3());

                    //테스트끝나면 삭제해야함
                    //Itemoff = true;
                }




                itemLight.startColor -= new Color(1, 1, 1, 0.02f) * Time.deltaTime;

                if (itemLight.startColor.a < 0.1f)
                {
                    itemLight.gameObject.SetActive(false);
                }
            }
        }
    }


    IEnumerator itemShowMeleeMode()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        GameManager.Instance.player.weapon1.gameObject.SetActive(true);
        GameManager.Instance.player.sheld.gameObject.SetActive(true);

        GameManager.Instance.player.Ani.SetBool("GetMelee", true);

    }


    public IEnumerator ririSpawn()
    {
        GameManager.Instance.npc.transform.Find("Byuk2").GetComponent<Transform>().gameObject.SetActive(true);
        GameManager.Instance.ririRB.gameObject.SetActive(true);
        GameManager.Instance.npc.ani.SetBool("Show", true);
        yield return new WaitForSecondsRealtime(1.7f);
        GameManager.Instance.npc.ani.SetBool("Show", false);
        GameManager.Instance.ririRB.gravityScale = 1;
        GameManager.Instance.npc.transform.Find("TalkCheak").gameObject.SetActive(true);
    }

    //장비 획득 후  <근접모드> 설명 On
    IEnumerator ShowAni3()
    {

        yield return new WaitForSecondsRealtime(2f); 
        Player.instance.Ps.Stop();
        Player.instance.Ps.gameObject.SetActive(false);
        GameManager.Instance.gameUI.transform.Find("GameGuide").gameObject.SetActive(true);
        TutorialGuide.instance.F_SetTutorialWindow(5);
        //guideManager.Audio.clip = SoundManager.instance.popup;
        //guideManager.Audio.Play();
        //guideManager.Ani3.gameObject.transform.position = GameManager.Instance.playerTR.transform.position + new Vector3(0, 1.5f);

        //guideManager.Ani3.gameObject.SetActive(true);
        //guideManager.Ani3.SetBool("Show", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance.isGetMeleeItem)
            {
                return;
            }

            else if (!GameManager.Instance.isGetMeleeItem)
            {
                canvas.gameObject.SetActive(true);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            canvas.gameObject.SetActive(false);
        }
    }


}
