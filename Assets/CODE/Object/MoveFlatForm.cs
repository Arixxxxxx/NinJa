using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    [SerializeField] private float startTimer = 0.2f;
    [SerializeField] private float count;
    private Vector2 targetPos;
    public GameObject obj;
    private float exitTimeCounter;
    

    
    BoxCollider2D coll;
    RaycastHit2D box;


    private void Awake()
    {
        point1 = transform.parent.GetChild(1).GetComponent<Transform>();
        point2 = transform.parent.GetChild(2).GetComponent<Transform>();
        targetPos = point1.position;
        coll = GetComponent<BoxCollider2D>();
        




    }
    bool once, once2;
    public bool soundOn;
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

              
              
                box = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector3.up, checkDis, surchLayer);
                
                if (box.collider != null)
                {
                    count += Time.deltaTime;
                    if (count > startTimer)
                    {
                        if (transform.position.y == point1.position.y && !once)
                        {
                            once = true;
                            soundOn = false;

                            if(this.gameObject.name != "FlatForm")
                            {
                                SoundManager.instance.F_SoundPlay(SoundManager.instance.gateUpComplete, 0.5f);
                            }
                            
                        }
                        targetPos = point1.position;

                        if(Vector3.Distance(transform.position, point1.position) > 0.05f)
                        {
                            soundOn = true;
                        }
                        else
                        {
                            soundOn = false;
                        }
                       

                    }
                   
                    if(Vector3.Distance(targetPos, point1.position) > 5)
                    {
                        once = false;
                    }
                }
                else if(box.collider == null)
                {
                    exitTimeCounter += Time.deltaTime;
                    if(exitTimeCounter > 1.5f)
                    {
                        targetPos = point2.position;
                    }
                   
                    count = 0;
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
