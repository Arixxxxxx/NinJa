using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNPCId : MonoBehaviour
{
    [Header("# NPC Stats")]
    public int ID;
    public bool isNPC;
    SpriteRenderer Sr;
    private void Awake()
    {
        Sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isNPC)
        {
            if(GameManager.Instance.player.transform.position.x < transform.position.x)
            {
                Sr.flipX = true;
            }
            else
            {
                Sr.flipX = false;
            }
        }
            
    }
}

