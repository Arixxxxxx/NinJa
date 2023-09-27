using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    private float Timer;
    void Start()
    {
        
    }

 
    void Update()
    {
        //F_Spawn();
    }

    private void F_Spawn()
    {
        Timer += Time.deltaTime;
        if( Timer > 3f)
        {
            PoolManager.Instance.F_GetObj("Enemy");
            Timer = 0;
        }
    }
}
