using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DengenAniEvent : MonoBehaviour
{
    [SerializeField] ParticleSystem Ps;
    [SerializeField] ParticleSystem Ps2;
    [SerializeField] ParticleSystem Ps3;
    
    [SerializeField] float PsInterval;
    bool startPs;
    bool broken;
    private void Update()
    {
        if (broken)
        {
            StartCoroutine(DestoryFall());
        }
    }
    private void A_ParticleStart()
    {
       broken = true;
    }

    IEnumerator DestoryFall()
    {
        if (!startPs)
        {
            startPs = true;
            Ps.Play();
            yield return new WaitForSeconds(PsInterval/3);
            Ps2.Play();
            yield return new WaitForSeconds((PsInterval / 3)*2);
            Ps3.Play();
            yield return new WaitForSeconds(PsInterval);
            startPs = false;
        }
      
    }

    private void A_OffGmaeObJect()
    {
        broken = false;
        gameObject.SetActive(false);
    }
}
