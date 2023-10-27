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
    [SerializeField] private BoxCollider2D coll1F, coll2F;
    private void Start()
    {
         sc = Boss.GetComponent<Boss>();
    }

    private void Update()
    {
        CheakCollider();
    }

    bool isCheakStart;
    bool NextCheak;
    private void CheakCollider()
    {
        if (isCheakStart)
        {
            switch (type)
            {
                case FloorType.one:
                    
                    if (NextCheak) { return; }
                    
                    NextCheak = true;

                    if (coll1F.enabled)
                    {
                        coll1F.enabled = false;
                    }
                    StartCoroutine(BoolFalseFuntion());
                    break;

                    case FloorType.two:

                    if (NextCheak) { return; }

                    NextCheak = true;

                    if (coll2F.enabled)
                    {
                        coll2F.enabled = false;
                    }
                    StartCoroutine(BoolFalseFuntion());
                    break;
            }
        }
    }

    IEnumerator BoolFalseFuntion()
    {
        yield return new WaitForSeconds(1.5f);
        NextCheak = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            sc.F_SurchFloow(type,"P");
            isCheakStart = true;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            sc.F_SurchFloow(type, "B");
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isCheakStart)
            {
                isCheakStart = true;
            }
          
             
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
            isCheakStart = false;
        }

    }
}
