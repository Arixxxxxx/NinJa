using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event5 : MonoBehaviour
{
    public int exitCounter;
    Animator Ani;
    [SerializeField] GameObject NPC;

    private void Awake()
    {
        Ani = GetComponent<Animator>(); 
    }

    public void ExitRockCheker(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Rock") || (collision.gameObject.layer == LayerMask.NameToLayer("EnemyDead")))
        {
            exitCounter++;
                if(exitCounter == 5)
            {
                Ani.SetBool("Fade", true);
            }
        }
    }

    private void A_offRock()
    {
        for(int i = 1; i<5; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        SetNPCId sc = NPC.GetComponent<SetNPCId>();
        sc.ID++;
        NPC.transform.Find("TalkCheak").gameObject.SetActive(true);

        gameObject.SetActive(false);
    }
}
