using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FlatFormHitBox;

public class FloatForm : MonoBehaviour
{
    Transform reSpawnPoint;
    Transform arrow1,arrow2;
    private void Awake()
    {
        reSpawnPoint = transform.GetChild(0).GetComponent<Transform>();
        arrow1 = transform.Find("Arrow1").GetComponent<Transform>();
        arrow2 = transform.Find("Arrow2").GetComponent<Transform>();

        arrow1.gameObject.SetActive(false);
        arrow2.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = reSpawnPoint.position;
            Player sc = collision.gameObject.GetComponent<Player>();
            sc.Rb.velocity = Vector3.zero;
            Emoticon.instance.F_GetEmoticonBox("Angry");
        }
    }

    public void GetTrigger(FloatFormHitBox box)
    {
        switch(box)
        {
            case FloatFormHitBox.point1:
                arrow1.gameObject.SetActive(true);
                arrow2.gameObject.SetActive(false);
                break;
            case FloatFormHitBox.point2:
                arrow1.gameObject.SetActive(false);
                arrow2.gameObject.SetActive(true);
                break;
            case FloatFormHitBox.point3:
                arrow2.gameObject.SetActive(false);
                break;
        }
    }


}
