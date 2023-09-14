using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlatFormHitBox : MonoBehaviour
{
    public enum FloatFormHitBox
    {
        point1,point2,point3
    }
    public FloatFormHitBox hitBox;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           GameManager.Instance.floatform.GetTrigger(hitBox);
        }
    }

}
