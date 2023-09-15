using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap_Ground : MonoBehaviour
{
    [SerializeField] public float DropMaxSpeed = 8.5f;
    public bool TrapOn;
    private Animator Ani;
    Transform[] SB;
    Transform spawnPosition;
    BoxCollider2D boxColl;
    

    private void Awake()
    {
        Ani = GetComponent<Animator>();
        SB = new Transform[transform.childCount];
        spawnPosition = transform.Find("SpawnPosition").GetComponent<Transform>();
        boxColl = GetComponent<BoxCollider2D>();


        for (int i = 1; i < SB.Length; i++)
        {
            SB[i] = transform.GetComponentsInChildren<Transform>(true)[i];
            SB[i].transform.position = spawnPosition.position;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && TrapOn == false)
        {
            TrapOn = true;
            Ani.SetBool("TrapOn", true);

            for (int i = 1; i < SB.Length; i++)
            {
                SB[i].gameObject.SetActive(true);
            }

            boxColl.enabled = false;
        }
    }
}
