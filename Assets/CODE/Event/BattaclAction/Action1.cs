using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action1 : MonoBehaviour
{
    BoxCollider2D boxColl;
    Transform right, left;

    private void Awake()
    {
        right = transform.Find("Right").GetComponent<Transform>();
        left = transform.Find("Left").GetComponent<Transform>();
        
    }
    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            Emoticon.instance.F_GetEmoticonBox("Angry");
            SoundManager.instance.F_SoundPlay(SoundManager.instance.zombieSpawn, 0.8f);
            once = true;
            GameObject E1 = PoolManager.Instance.F_GetObj("Enemy");
            E1.transform.position = right.transform.position;
            
            GameObject E2 = PoolManager.Instance.F_GetObj("Enemy");
            E2.transform.position = left.transform.position;

            if(boxColl == null)
            {
                boxColl = GetComponent<BoxCollider2D>();
            }
            boxColl.enabled = false;
            StartCoroutine(TreeLayerReturn());
        }
    }
    IEnumerator TreeLayerReturn()
    {
        yield return new WaitForSecondsRealtime(2);

        right.GetComponent<SpriteRenderer>().sortingOrder = 5;
        left.GetComponent<SpriteRenderer>().sortingOrder = 5;

    }
}
