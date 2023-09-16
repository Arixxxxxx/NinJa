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


    
    private void Start()
    {
        back = GameManager.Instance.blackScreen.GetComponent<Image>();
    }
    
    private void LateUpdate()
    {
        colorup();
        colordown();
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
                    GameManager.Instance.playerTR.transform.position = GameManager.Instance.telPoint1.transform.position;
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
            GameManager.Instance.player.MovingStop = true;
            up = true;
            back.gameObject.SetActive(true);
        }
    }
}
