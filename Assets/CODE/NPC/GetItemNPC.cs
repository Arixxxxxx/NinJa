using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemNPC : MonoBehaviour
{
  public enum ItemType
    {
        Melee, Range
    }  
    public ItemType type;

    Transform itemsSprite;
    Transform canvas;


    private void Awake()
    {
        itemsSprite = transform.Find("KAL").GetComponent<Transform>();
        canvas = transform.Find("Canvas").GetComponent<Transform>();
    }

    private void Update()
    {
        MeleeItem();
    }
    private void MeleeItem()
    {
        if (GameManager.Instance.isGetMeleeItem)
        {
            itemsSprite.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
