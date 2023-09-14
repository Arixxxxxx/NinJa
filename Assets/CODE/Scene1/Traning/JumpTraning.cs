using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTraning : MonoBehaviour
{
    Transform reSpawnPoint;
    // Start is called before the first frame update
    private void Awake()
    {
        reSpawnPoint = transform.Find("RespawnPoint").GetComponent<Transform>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Emoticon.instance.F_GetEmoticonBox("Angry");
            collision.gameObject.transform.position = reSpawnPoint.position;
        }
    }
}
