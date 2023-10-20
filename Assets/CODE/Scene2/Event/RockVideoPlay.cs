using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockVideoPlay : MonoBehaviour
{
    bool once;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            GameManager.Instance.gameUI.transform.Find("GameGuide").gameObject.SetActive(true);
            TutorialGuide.instance.F_SetTutorialWindow(7);

        }
    }
}
