using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBox : MonoBehaviour
{
    bool once;
    Transform Guide;
    // Start is called before the first frame update
    void Start()
    {
        Guide = GameManager.Instance.gameUI.Find("GameGuide").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            Guide.gameObject.SetActive(true);
            TutorialGuide.instance.F_SetTutorialWindow(7);

        }
    }
}
