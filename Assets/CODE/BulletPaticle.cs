using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPaticle : MonoBehaviour
{
     public enum PaticleType
    {
        Dust, Blood
    }
    ParticleSystem ArrowDust;
    public PaticleType type;

    private void Awake()
    {
        ArrowDust = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        Invoke("ReturnHome", 0.8f);
    }

    private void ReturnHome()
    {
        switch (type)
        {
                case PaticleType.Dust:
                PoolManager.Instance.F_ReturnObj(gameObject, "Dust");
                break;

                case PaticleType.Blood:
                PoolManager.Instance.F_ReturnObj(gameObject, "Blood");
                break;

        }
     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
                   ArrowDust.Play();
        }
    }
}
