using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShoot : MonoBehaviour
{
    Transform firePos;

    public void Shoot()
    {
        firePos = transform.parent.GetChild(2).GetComponent<Transform>();
        GameObject obj = PoolManager.Instance.F_GetObj("EB");
        obj.transform.position = firePos.position;

        EnemyBullet bullet = obj.GetComponent<EnemyBullet>();
        bullet.Shoot();
    }
}
