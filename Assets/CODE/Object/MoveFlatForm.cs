using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveFlatForm : MonoBehaviour
{
    public enum MoveType
    {
        FromAToBMove, onPlayerMove
    }
    public MoveType type;

    //µµÂøÁöÁ¡ A,B
    private Transform point1, point2;
    [Range(0, 10)]
    [SerializeField] private float flatFormSpeed;
    [Range(0.3f,1f)][SerializeField] private float checkDis = 0.5f;
    [SerializeField] private LayerMask surchLayer;
    private Vector2 targetPos;
    public GameObject obj;


    
    BoxCollider2D coll;
    RaycastHit2D box;


    private void Awake()
    {
        point1 = transform.parent.GetChild(1).GetComponent<Transform>();
        point2 = transform.parent.GetChild(2).GetComponent<Transform>();
        targetPos = point1.position;
        coll = GetComponent<BoxCollider2D>();
        




    }
    void Update()
    {
        switch (type)
        {
            case MoveType.FromAToBMove:

                    if (Vector2.Distance(transform.position, point1.position) < 0.1f) targetPos = point2.position;
                    if (Vector2.Distance(transform.position, point2.position) < 0.1f) targetPos = point1.position;

                    transform.position = Vector2.MoveTowards(transform.position, targetPos, flatFormSpeed * Time.deltaTime);

                break;


            case MoveType.onPlayerMove:

                //RaycastHit2D ray = Physics2D.CapsuleCast(transform.position, new Vector2(1.8f, 0.5f), CapsuleDirection2D.Horizontal, 0, Vector2.zero, LayerMask.GetMask("Player")) ;
                //RaycastHit2D ray = Physics2D.CapsuleCast(coll.bounds.center, coll.bounds.size, CapsuleDirection2D.Horizontal, 0 , Vector2.up, surchLayer);
                box = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector3.up, checkDis, surchLayer);
                
                if (box.collider != null)
                {
                    targetPos = point1.position;
                }
                else if(box.collider == null)
                {
                    targetPos = point2.position;
                }
                transform.position = Vector2.MoveTowards(transform.position, targetPos, flatFormSpeed * Time.deltaTime);

                break;
        }
            

       
  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (point1 ==null || point2 == null)
        {
            point1 = transform.parent.GetChild(1).GetComponent<Transform>();
            point2 = transform.parent.GetChild(2).GetComponent<Transform>();
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(point1.position, point2.position);
    }
}
