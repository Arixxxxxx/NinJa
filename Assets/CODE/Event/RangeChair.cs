using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangeChair : MonoBehaviour
{

    RangeZone sc;
  

    private void Awake()
    {
        sc = transform.parent.GetComponent<RangeZone>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sc.StartEvent(collision);
            GameManager.Instance.player.Rb.velocity = Vector3.zero;
            StartCoroutine(Runoff());
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    IEnumerator Runoff()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        GameManager.Instance.player.Ani.SetBool("Run", false);
    }
    public void boom()
    {
        transform.gameObject.SetActive(false);
    }
}
