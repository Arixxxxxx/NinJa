using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePoint : MonoBehaviour
{
  
    public enum TileType
    {
        Jungle, Dengoen, TreeRoom
    }

    public TileType type;
    private TileFadeManagers fadeManager;
    private bool once;
    float ExitDir;

    private void Start()
    {
        fadeManager =transform.parent.GetComponent<TileFadeManagers>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    ExitDir = (collision.transform.position - transform.position).normalized;

    //    if (collision.gameObject.CompareTag("Player") && !once)
    //    {
    //        once = true;
            
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
            once = false;
            ExitDir = (collision.transform.position.x - transform.position.x);
            Debug.Log($"{collision.transform.position.x} / {transform.position.x} / {ExitDir} ");
            fadeManager.F_TileFadeOnOff(type, ExitDir);
        }
    }
}
