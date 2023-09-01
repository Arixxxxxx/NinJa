using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    Image sprite;
    Image dead;
    Color origin;

    private void Awake()
    {
        sprite = transform.GetChild(0).GetComponent<Image>();
        dead = transform.GetChild(1).GetComponent<Image>();
        origin = sprite.color;
    }

    private void Update()
    {
        if (!GameManager.Instance.isPlayerDead)
        {
            sprite.color = origin;
            dead.gameObject.SetActive(false);
        }
        if (GameManager.Instance.isPlayerDead)
        {
            sprite.color = Color.red;
            dead.gameObject.SetActive(true);
        }
        

        
    }
}
