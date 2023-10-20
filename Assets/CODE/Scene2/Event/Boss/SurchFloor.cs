using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurchFloor : MonoBehaviour
{
    public enum FloorType
    {
        one,two, three
    }
    public FloorType type;
    [SerializeField] GameObject Boss;
    Boss sc;
    bool isBossAlive;
    private void Start()
    {
         sc = Boss.GetComponent<Boss>();
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            sc.F_SurchFloow(type,"P");
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            sc.F_SurchFloow(type, "B");
        }
        
    }
   
    float Timer;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBossAlive = sc.F_CheakBossAlive();
            if(!isBossAlive) 
            {
                sc.isBossHide = false;
                sc.F_ExitFloor();
            }
            
        }
    }
}
