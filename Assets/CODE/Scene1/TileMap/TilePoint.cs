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

    private void Start()
    {
        fadeManager =transform.parent.GetComponent<TileFadeManagers>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            fadeManager.F_TileFadeOnOff(type);
        }  
    }
}
