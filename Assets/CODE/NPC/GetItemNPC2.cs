using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GetItemNPC2 : MonoBehaviour
{
    public static GetItemNPC2 Instance;
    //소환문 소환
    public Animator aniGate;
    public ParticleSystem partiGate;

  

    Transform itemsSprite;
    Transform canvas;
    ParticleSystem itemLight;
    GuideManager guideManager;
    BoxCollider2D noWay;
    public AudioSource Audio;
    bool once;
    bool once1;

    private void Awake()
    {
        if(Instance == null)
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
        aniGate = transform.Find("GateWay/Gate").GetComponent <Animator>();
        partiGate = transform.Find("GateWay/Parti").GetComponent<ParticleSystem>();
        noWay = transform.Find("NoWay").GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
                    
                RangeItem();
                
    }
    
    //테스트용
    public bool Itemoff;
    

    private void RangeItem()
    {
        if (itemLight.gameObject.activeSelf)
        {
            if (GameManager.Instance.isGetRangeItem)
            {
                GameManager.Instance.player.Itemget1 = true;

                //먹었으니 템주고 플레이어 전투기능 켜줌설명서 보여줌
                if (!once1)
                {
                    
                    once1 = true;
                    //guideManager.StopCharacter();
                    GameManager.Instance.MovingStop = true;
                    itemsSprite.gameObject.SetActive(false);
                    canvas.gameObject.SetActive(false);
                    StartCoroutine(itemShowRangeMode());
                    //GameManager.Instance.player.ora.SetTrigger("Up");
                    StartCoroutine(ShowAura());
                    noWay.enabled = true;
                    StartCoroutine(ShowAni4());
                }




                itemLight.startColor -= new Color(1, 1, 1, 0.02f) * Time.deltaTime;

                if (itemLight.startColor.a < 0.1f)
                {
                    itemLight.gameObject.SetActive(false);
                }
            }
        }
    }
   
    IEnumerator itemShowRangeMode()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        GameManager.Instance.player.MeleeItemShow(1);
        //GameManager.Instance.player.Ani.SetBool("GetMelee", true);
        //GameManager.Instance.meleeMode = false;
        GameManager.Instance.player.F_RangeMode();
    }

    IEnumerator ShowAura()
    {
        Player.instance.Ps2.gameObject.SetActive(true);
        Player.instance.Ps2.Play();
        yield return new WaitForSeconds(2.5f);
        Player.instance.Ps2.Stop();
        Player.instance.Ps2.gameObject.SetActive(false);
    }
    
    //장비 획득 후  <원거리> 모드 설명 On
    IEnumerator ShowAni4()
    {
      
        yield return new WaitForSecondsRealtime(2.5f);
        GameManager.Instance.gameUI.transform.Find("GameGuide").gameObject.SetActive(true);
        TutorialGuide.instance.F_SetTutorialWindow(6);

        //guideManager.Audio.clip = SoundManager.instance.popup;
        //guideManager.Audio.Play();
        //guideManager.Ani3_2.gameObject.transform.position = GameManager.Instance.playerTR.transform.position + new Vector3(0, 2.3f);
        //guideManager.Ani3_2.gameObject.SetActive(true);
        //guideManager.Ani3_2.SetBool("Show", true);
    }

    //산속에서 템먹고나면 텔타서 전투교관 나타남
    public IEnumerator ririSpawn()
    {
        GameManager.Instance.battleNpcRb.gameObject.SetActive(true);
        GameManager.Instance.npc2.ani.SetBool("Show", true);

        yield return new WaitForSecondsRealtime(1.7f);

        GameManager.Instance.npc2.ani.SetBool("Show", false);
        GameManager.Instance.npc2.transform.Find("TalkCheak").gameObject.SetActive(true);
        GameManager.Instance.battleNpcRb.gravityScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
                if (collision.CompareTag("Player"))
                {
                    if (GameManager.Instance.isGetRangeItem)
                    {
                        return;
                    }
                    else if (!GameManager.Instance.isGetRangeItem)
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
