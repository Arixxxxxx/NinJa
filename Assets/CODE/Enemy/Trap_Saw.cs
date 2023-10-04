using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap_Saw : MonoBehaviour
{
    public enum StartDir
    {
        Left,Right
    }
    public StartDir startdir;

    [Header("Åé³¯¹ÙÄû")]
    [Space]
    Rigidbody2D Rb;
    private Vector2 saw_vec;
    [SerializeField] private float saw_speed;
    private Vector2 Saw_Dir;
    public bool isSawOk;
    Transform point1, point2;
    int dir;
    
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        
        point1 = transform.parent.GetChild(1).GetComponent<Transform>();
        point2 = transform.parent.GetChild(2).GetComponent<Transform>();

       switch (startdir)
        { 
                case StartDir.Left:
                Saw_Dir = Vector2.left;
                dir = -1;
                break;

                case StartDir.Right:
                Saw_Dir = Vector2.right;
                dir = 1;
                break;
       }
    }

    private void FixedUpdate()
    {
        Rb.velocity = new Vector2(Saw_Dir.x * saw_speed, Rb.velocity.y);

        if (Vector2.Distance(transform.position, point1.position) < 0.1f)
        {
            Saw_Dir = Vector2.right;
            transform.localScale = new Vector3(-1, 1,1);
        }
        else if (Vector2.Distance(transform.position, point2.position) < 0.1f)
        {
            Saw_Dir = Vector2.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            point1 = parentTransform.childCount > 1 ? parentTransform.GetChild(1) : null;
            point2 = parentTransform.childCount > 2 ? parentTransform.GetChild(2) : null;
        }

        if (point1 != null && point2 != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(point1.position, point2.position);
        }
    }
}


        //    saw_vec = scanSaw_Vec * saw_speed * Time.fixedDeltaTime;

        //    RaycastHit2D sawhit = Physics2D.Raycast(transform.position, scanSaw_Vec, 0.5f, LayerMask.GetMask("Wall"));
        //    RaycastHit2D sawhit2 = Physics2D.Raycast(transform.position, scanSaw_Vec, 0.5f, LayerMask.GetMask("Ground"));
        //    if (sawhit)
        //    {
        //        scanSaw_Vec = Vector2.Reflect(scanSaw_Vec, sawhit.normal);
        //    }
        //    if (sawhit2)
        //    {
        //        scanSaw_Vec = Vector2.Reflect(scanSaw_Vec, sawhit2.normal);
        //    }

        //    Rb.MovePosition(Rb.position + saw_vec);
        //}


