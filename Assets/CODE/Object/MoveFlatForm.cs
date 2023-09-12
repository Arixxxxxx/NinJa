using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlatForm : MonoBehaviour
{
    //µµÂøÁöÁ¡ A,B
    private Transform point1, point2;
    [Range(0, 10)]
    [SerializeField] private float flatFormSpeed;
    Vector2 targetPos;


    private void Awake()
    {
        point1 = transform.parent.GetChild(1).GetComponent<Transform>();
        point2 = transform.parent.GetChild(2).GetComponent<Transform>();
        targetPos = point1.position;

    }
    void Update()
    {
        if(Vector2.Distance(transform.position, point1.position) < 0.1f)  targetPos = point2.position;
        if(Vector2.Distance(transform.position, point2.position) < 0.1f)  targetPos = point1.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, flatFormSpeed * Time.deltaTime);
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
