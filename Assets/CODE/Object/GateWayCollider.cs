using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GateWayCollider : MonoBehaviour
{
    float counter;
    
    [SerializeField] bool up;
    [SerializeField] bool down;
    [SerializeField] bool end;
    [SerializeField] int telePortNum = 0;
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
        Gatedelete();

    }

    private void colorup()
    {
        if(back.color.a > 0.95f && up)
        {
            up = false;
            down = true;

            //검정화면때 순간이동
            switch (telePortNum)
            {
                case 0:
                    telePortNum++;
                    transform.parent.position = transform.parent.Find("Tellpon1").transform.position;
                    GetItemNPC.Instance.partiGate.gameObject.SetActive(true);

                    //GameManager.Instance.playerTR.transform.position = GameManager.Instance.telPoint1.transform.position;
                    StartCoroutine(Step1());

                    break;

                case 1:
                    
                    transform.parent.position = transform.parent.Find("Tellpon1").transform.position;
                    GetItemNPC2.Instance.partiGate.gameObject.SetActive(true);
                    GameManager.Instance.worldLight.intensity = 1;
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
                telePortNum++;
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

            end = false;
            GameManager.Instance.MovingStop = true;
            GameManager.Instance.player.Rb.velocity = Vector3.zero;
            
            up = true;
            back.gameObject.SetActive(true);
            boxColl.enabled = false;
        }
    }
    float counter2 = 1;
    private void Gatedelete()
    {
        if (GateOff)
        {
            counter2 -= Time.deltaTime * speed;
            GateSr.color -= new Color(1, 1, 1, counter2);
            if(GateSr.color.a < 0.05f)
            {
                GateOff = false;
            }
        }
    }

    //float counter3 = 1;
    //private void Gatedelete2()
    //{
    //    if (GateOff)
    //    {
    //        counter3 -= Time.deltaTime * speed;
    //        GateSr.color -= new Color(1, 1, 1, counter3);
    //        if (GateSr.color.a < 0.05f)
    //        {
    //            GateOff = false;
    //        }
    //    }
    //}
    IEnumerator Step1()
    {
        GameManager.Instance.player.Sr.enabled = false;
        GameManager.Instance.player.MeleeItemShow(1);
        GateSr.enabled = false;
        GameManager.Instance.playerTR.transform.position = GameManager.Instance.telPoint1.transform.position;

        yield return new WaitForSecondsRealtime(1.5f);
      

        GetItemNPC.Instance.partiGate.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        transform.localScale = new Vector3(-1, 1, 1);
        //GateSr.enabled = true;
        GetItemNPC.Instance.aniGate.SetTrigger("ShowUp");
        yield return new WaitForSecondsRealtime(0.2f);
        GateSr.enabled = true;
        yield return new WaitForSecondsRealtime(5.3f);
        GameManager.Instance.player.Sr.enabled = true;
        GameManager.Instance.player.MeleeItemShow(0);
        GetItemNPC.Instance.partiGate.gameObject.SetActive(false);
        GameManager.Instance.MovingStop = false;
        yield return new WaitForSecondsRealtime(2f);
        GateOff = true;
    }

    IEnumerator Step2()
    {
        GameManager.Instance.player.Sr.enabled = false;
        GameManager.Instance.player.MeleeItemShow(1);
        GateSr.enabled = false;
        GameManager.Instance.playerTR.transform.position = GameManager.Instance.telPoint1.transform.position;

        yield return new WaitForSecondsRealtime(2.5f);


        GetItemNPC2.Instance.partiGate.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        transform.localScale = new Vector3(-1, 1, 1);
        //GateSr.enabled = true;
        GetItemNPC2.Instance.aniGate.SetTrigger("ShowUp");
        yield return new WaitForSecondsRealtime(0.2f);
        GateSr.enabled = true;
        yield return new WaitForSecondsRealtime(5.3f);
     
        if (GameManager.Instance.meleeMode)
        {
            GameManager.Instance.player.MeleeItemShow(0);
        }
        GameManager.Instance.player.Sr.enabled = true;
        GetItemNPC2.Instance.partiGate.gameObject.SetActive(false);
        GameManager.Instance.MovingStop = false;
        yield return new WaitForSecondsRealtime(2f);
        GateOff = true;
    }
}
