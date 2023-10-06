using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GateWayCollider : MonoBehaviour
{
    public enum GateType
    {
        RiRiGate, BattleNpcGate
    }
    public GateType type;
    float counter;
    
    [SerializeField] bool up;
    [SerializeField] bool down;
    [SerializeField] bool end;

    [Range(0f,1f)][SerializeField] private float speed = 0.2f;
    Image back;

    BoxCollider2D boxColl;
    SpriteRenderer GateSr;
    bool GateOff;


    
    private void Start()
    {
        back = GameManager.Instance.blackScreen.GetComponent<Image>();
        boxColl = GetComponent<BoxCollider2D>();
        GateSr = GetComponent<SpriteRenderer>();
    }
    
    private void LateUpdate()
    {
        colorup();
        colordown();
        SrOff();
        //Gatedelete();

    }

    private void colorup()
    {
        if(back.color.a > 0.95f && up)
        {
            up = false;
            down = true;

            //����ȭ�鶧 �����̵�
            switch (type)
            {
                case GateType.RiRiGate:
                    transform.parent.position = transform.parent.Find("Tellpon1").transform.position;
                    GetItemNPC.Instance.partiGate.gameObject.SetActive(true);
                    gateMoving = true;
                    //GameManager.Instance.playerTR.transform.position = GameManager.Instance.telPoint1.transform.position;
                    StartCoroutine(Step1());

                    break;

                case GateType.BattleNpcGate:
                    
                    transform.parent.position = transform.parent.Find("Tellpon1").transform.position;
                    GetItemNPC2.Instance.partiGate.gameObject.SetActive(true);
                    GameManager.Instance.worldLight.intensity = 1;
                    gateMoving = true;
                    //GameManager.Instance.playerTR.transform.position = GameManager.Instance.telPoint1.transform.position;
                    StartCoroutine(Step2());

                    break;

            }
            return;
        }
         if (up)
        {
            counter += Time.deltaTime * speed;
            back.color = new Color(0, 0, 0, counter);
        }
               
    }

    private void colordown()
    {
        if (end)
        {
            return;
        }

        if (down && !end)
        {
            if(back.color.a < 0.05f)
            {
                end = true;
                down = false;
                back.gameObject.SetActive(false);

            }
            counter -= Time.deltaTime * speed;
            back.color = new Color(0, 0, 0, counter);
            GameManager.Instance.player.MovingStop = false;
    
            
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.enterGate, 1);
            end = false;
            GameManager.Instance.MovingStop = true;
            GameManager.Instance.player.Rb.velocity = Vector3.zero;
            
            up = true;
            back.gameObject.SetActive(true);
            boxColl.enabled = false;
            SoundManager.instance.AudioChanger(SoundManager.instance.cityThema);
        }
    }
   
   
    IEnumerator Step1()
    {
        GetItemNPC.Instance.aniGate.SetBool("active", false);
        GameManager.Instance.player.Sr.enabled = false;
        GameManager.Instance.player.MeleeItemShow(1);
        GateSr.enabled = false;
        GameManager.Instance.playerTR.transform.position = GameManager.Instance.telPoint1.transform.position;
        Camera.main.transform.position
            =
        new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y, Camera.main.transform.position.z);

        yield return new WaitForSecondsRealtime(1.5f);

        
        GetItemNPC.Instance.partiGate.Play(); //������
        GetItemNPC.Instance.Audio.Play();
        GameManager.Instance.CameraShakeSwitch(0); //ī�޶�����
        yield return new WaitForSecondsRealtime(0.5f);
        transform.localScale = new Vector3(-1, 1, 1); //����ٲ��ְ�
        //GateSr.enabled = true;
        GetItemNPC.Instance.aniGate.SetTrigger("ShowUp"); // �ö����
        yield return new WaitForSecondsRealtime(0.2f);
        GateSr.enabled = true; //��������Ʈ Ű��
        yield return new WaitForSecondsRealtime(5.3f);

        //�÷��̾� Show �ִϸ��̼����� �̻簥����
        //GameManager.Instance.player.Sr.enabled = true;
        //GameManager.Instance.player.MeleeItemShow(0);
        //GetItemNPC.Instance.partiGate.gameObject.SetActive(false);
        //GameManager.Instance.MovingStop = false;
        
        // 
        //yield return new WaitForSecondsRealtime(0.2f);
        GameManager.Instance.CameraShakeSwitch(1);
        GetItemNPC.Instance.Audio.Stop();
        GameManager.Instance.gameUI.GetComponent<GameUI>().SetMapMoveBar("����");
        yield return new WaitForSecondsRealtime(2f);
        //GateOff = true;
        GetItemNPC.Instance.aniGate.SetBool("Hide",true);
        GetItemNPC2.Instance.aniGate.transform.position = GameManager.Instance.gateOriginPos;
    }
    bool gateMoving;

    private void SrOff()
    {
        if (!gateMoving)
        {
            return;
        }
        else if(gateMoving)
        {
            Debug.Log("����");
            GameManager.Instance.player.MeleeItemShow(1);
        }
    }
    IEnumerator Step2()
    {
        GetItemNPC2.Instance.aniGate.SetBool("active", false);
        GameManager.Instance.player.Sr.enabled = false;
        GameManager.Instance.player.MeleeItemShow(1);
        GateSr.enabled = false;
        GameManager.Instance.playerTR.transform.position = GameManager.Instance.telPoint1.transform.position;
        Camera.main.transform.position
            =
           new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y, Camera.main.transform.position.z);
        yield return new WaitForSecondsRealtime(2.5f);


        GetItemNPC2.Instance.partiGate.Play();
        GetItemNPC2.Instance.Audio.Play();
        GameManager.Instance.CameraShakeSwitch(0);
        yield return new WaitForSecondsRealtime(0.5f);
        transform.localScale = new Vector3(-1, 1, 1);
        //GateSr.enabled = true;
        GetItemNPC2.Instance.aniGate.SetTrigger("ShowUp");
        yield return new WaitForSecondsRealtime(0.2f);
        GateSr.enabled = true;
        yield return new WaitForSecondsRealtime(5.3f);
     
        //if (GameManager.Instance.meleeMode)
        //{
        //    GameManager.Instance.player.MeleeItemShow(0);
        //}
        //GameManager.Instance.player.Sr.enabled = true;
        //GetItemNPC2.Instance.partiGate.gameObject.SetActive(false);
        //GameManager.Instance.MovingStop = false;
        yield return new WaitForSecondsRealtime(0.2f);
        GameManager.Instance.CameraShakeSwitch(1);
        GetItemNPC2.Instance.Audio.Stop();
        GameManager.Instance.gameUI.GetComponent<GameUI>().SetMapMoveBar("����");
        yield return new WaitForSecondsRealtime(2f);
        GetItemNPC2.Instance.aniGate.SetBool("Hide", true);
        //GateOff = true;
    }

    //���� ������ Ȱ��ȭ
    public void GateActive1()
    {
        GetItemNPC.Instance.aniGate.SetBool("active", true);
        SoundManager.instance.F_SoundPlay(SoundManager.instance.gateUpComplete, 0.4f);
    }

    //��Ʋnpc ������ Ȱ��ȭ
    public void GateActive2()
    {
        GetItemNPC2.Instance.aniGate.SetBool("active", true);
        SoundManager.instance.F_SoundPlay(SoundManager.instance.gateUpComplete, 0.4f);
    }

    public int ShowCount = 0; // �ִϸ��̼����� ���ٲ��� �������� ó�����ö��� �۵��Ǹ�ȵǼ� �̶��� ��Ʈ���÷���
    public void ShowCharactor()
        
    {
        ShowCount++;
        if (ShowCount == 2)
        {
            GameManager.Instance.player.Sr.enabled = true;
            GameManager.Instance.player.MeleeItemShow(0);
            GetItemNPC.Instance.partiGate.gameObject.SetActive(false);
            GameManager.Instance.MovingStop = false;
            gateMoving = false;
        }
        
    }
    public int ShowCount1 = 0; // �ִϸ��̼����� ���ٲ��� �������� ó�����ö��� �۵��Ǹ�ȵǼ� �̶��� ��Ʈ���÷���
    public void ShowCharActer2()
    {
        ShowCount1++;

        if(ShowCount1 == 2)
        {
            GameManager.Instance.player.Sr.enabled = true;
            GetItemNPC2.Instance.partiGate.gameObject.SetActive(false);
            GameManager.Instance.MovingStop = false;
            gateMoving = false;
            if (GameManager.Instance.meleeMode)
            {
                GameManager.Instance.player.MeleeItemShow(0);
            }
           
        }
       
    }
}
