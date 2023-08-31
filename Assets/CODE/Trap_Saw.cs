using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : MonoBehaviour
{
    [Header("Åé³¯¹ÙÄû")]
    [Space]
    Rigidbody2D Rb;
    private Vector2 saw_vec;
    [SerializeField] private float saw_speed;
    private Vector2 scanSaw_Vec;
    public bool isSawOk;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        scanSaw_Vec = Vector2.left;
    }

    private void FixedUpdate()
    {
        saw_vec = scanSaw_Vec * saw_speed * Time.fixedDeltaTime;

        RaycastHit2D sawhit = Physics2D.Raycast(transform.position, scanSaw_Vec, 0.5f, LayerMask.GetMask("Wall"));
        RaycastHit2D sawhit2 = Physics2D.Raycast(transform.position, scanSaw_Vec, 0.5f, LayerMask.GetMask("Ground"));
        if (sawhit)
        {
            scanSaw_Vec = Vector2.Reflect(scanSaw_Vec, sawhit.normal);
        }
        if (sawhit2)
        {
            scanSaw_Vec = Vector2.Reflect(scanSaw_Vec, sawhit2.normal);
        }

        Rb.MovePosition(Rb.position + saw_vec);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, scanSaw_Vec * 0.5f);
    }
}
