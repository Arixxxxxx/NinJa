using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap_Ground : MonoBehaviour
{
    public bool TrapOn;
    private Animator Ani;
    Transform[] SB;
   

    private void Awake()
    {
        Ani = GetComponent<Animator>();
        SB = new Transform[transform.childCount+1];
        for(int i = 0; i < SB.Length; i++)
        {
            SB[i] = transform.GetComponentsInChildren<Transform>(true)[i];
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && TrapOn == false)
        {
            TrapOn = true;
            Ani.SetBool("TrapOn", true);

            for (int i = 0; i < SB.Length; i++)
            {
                SB[i].gameObject.SetActive(true);
            }
        }
    }
}
