using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgPooling : MonoBehaviour
{
   
    private Queue<GameObject> Queue;
    public GameObject Font_DMG;
    private void Awake()
    {
        
        Queue = new Queue<GameObject>();

        for(int i = 0; i < 8; i++)
        {
            GameObject obj = Instantiate(Font_DMG, transform);
            Queue.Enqueue(obj);
            obj.SetActive(false);
        }
    }
    
    public GameObject F_Get_FontBox()
    {
        if(transform.childCount < 0)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject obj = Instantiate(Font_DMG, transform);
                Queue.Enqueue(obj);
                obj.SetActive(false);
            }
        }
        GameObject result;

        result = Queue.Dequeue();

        return result;
    }

    public void F_In_FontBox(GameObject _obj)
    {
        Queue.Enqueue(_obj);
    }
}
