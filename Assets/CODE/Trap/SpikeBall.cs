using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    Rigidbody2D Rb;
    SpikeTrap_Ground parent;
    private float randomX;
    private Vector2 OriginPos;
    public bool AttackEnd;
    private void Awake()
    {
        parent = transform.GetComponentInParent<SpikeTrap_Ground>();
        Rb = GetComponent<Rigidbody2D>();
        randomX = Random.Range(-2f, 2f);
        OriginPos =  new Vector2(transform.position.x, transform.position.y);
    }
    private void Update()
    {
        if(Rb.velocity.y < -8.5f)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, -8.5f);
        }
    }
    private void OnEnable()
    {
        Rb.velocity = new Vector2(randomX,Rb.velocity.y);
        Invoke("F_OffSpike", 8);
    }

 
    private void F_OffSpike()
    {
        gameObject.SetActive(false);
        transform.position = OriginPos;
        AttackEnd=true;

    }
}
