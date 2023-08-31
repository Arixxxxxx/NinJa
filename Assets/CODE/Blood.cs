using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    Rigidbody2D Rb;
    float RandonX;
    float RandonY;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();   
    }
    private void OnEnable()
    {
        

           RandonX = Random.Range(-2f, 2f);
            RandonY = Random.Range(4, 8);

           Rb.AddForce(new Vector2(RandonX, RandonY), ForceMode2D.Impulse);

        Invoke("off", 3);
    }

    private void OnDisable()
    {
        transform.position = transform.parent.position;
    }
    private void off()
    {
       
        gameObject.SetActive(false);
    }
}
