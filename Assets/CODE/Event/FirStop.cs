using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireStop : MonoBehaviour
{

    bool once;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {

            GameManager.Instance.worldLight.intensity = 0.1f;
            SoundManager.instance.AudioChanger(SoundManager.instance.CaveThema);
            GameManager.Instance.FireStop = true;
            GameManager.Instance.gameUI.GetComponent<GameUI>().F_SetMapMoveBar("ø‰¡§");
        }
    }
   
}
