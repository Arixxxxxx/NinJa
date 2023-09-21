using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    private Vector3 p1, p2;
    private Vector3 move;
    float Dis;
    [SerializeField]  private float cloudspeed;
    void Start()
    {
        p1 = transform.parent.GetChild(0).position;
        p2 = transform.parent.GetChild(1).position;
        move = Vector3.left;
        
    }

    // Update is called once per frame
    void Update()
    {
        Dis = transform.position.x - p1.x;

        if (Dis < 0.1f)
        {
            transform.position = new Vector3 (p2.x, transform.position.y);
        }
      
        transform.position += move * cloudspeed * Time.deltaTime;

    }
}
