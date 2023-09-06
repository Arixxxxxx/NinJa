using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    private Transform target;
    private float minX = 0;
    private float MaX = 5000;
    private void Start()
    {
        target = GameManager.Instance.player.transform.GetComponent<Transform>();
    }

    // 카메라의 x축이 0이하로 가지않게 조절
    void LateUpdate()
    {        
        if(target != null)
        {
            Vector3 vec = transform.position;
            vec.x = Mathf.Max(target.position.x, minX);
            vec.x = Mathf.Min(vec.x, MaX);
            vec.y = Mathf.Max(target.position.y, 1.54f);
            vec.y = Mathf.Min(vec.y, 40);
            transform.position = vec;
        }
        
    }
}
