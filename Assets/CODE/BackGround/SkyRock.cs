using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRock : MonoBehaviour
{
    float v;
    float fv;
    float time;
    Vector3 verticalVec;
    Rigidbody2D Rb;
    [SerializeField] private float Speed;

    Vector3 aaa;

    float pingPongValue;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        aaa = transform.position;
        pingPongValue = Random.Range(3.0f, 4.0f);
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        v = Mathf.PingPong(time, pingPongValue);
        fv = v - (pingPongValue/2);
        verticalVec = new Vector3(0, fv);

        Rb.velocity = verticalVec * Speed;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
