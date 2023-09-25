using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireStop : MonoBehaviour
{

    bool once;
    private void Update()
    {
        if (once)
        {
            if(GameManager.Instance.worldLight.intensity > 0.5f)
            {
                GameManager.Instance.worldLight.intensity -= Time.deltaTime * 0.8f;

                if (GameManager.Instance.worldLight.intensity <= 0.5f)
                {
                    once = false;
                }
            }
          
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            
            once = true;
            SoundManager.instance.AudioChanger(SoundManager.instance.CaveThema);
            GameManager.Instance.FireStop = true;
            GameManager.Instance.gameUI.GetComponent<GameUI>().SetMapMoveBar("ø‰¡§");
        }
    }
   
}
