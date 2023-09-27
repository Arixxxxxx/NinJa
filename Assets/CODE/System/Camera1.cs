using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    private Transform target;
    private float minX = 0;
    private float MaX = 5000;
    [Range(0f, 30f)]
    [SerializeField] private float camVerticalValue;
    [SerializeField] float smooth;

    private Camera mainCam;
    Vector3 curCameraPos;
    [Range(0.01f, 0.1f)][SerializeField] private float shakeRange;
    [Range(0.01f, 2f)][SerializeField] private float shakeTimeInterval = 0.05f;
    float timer;
  
  
    private void Start()
    {
      
        target = GameManager.Instance.player.transform.GetComponent<Transform>();
        mainCam = Camera.main;
        
    }

    // 카메라의 x축이 0이하로 가지않게 조절
    void LateUpdate()
    {
        if (GameManager.Instance.normalCamera)
        {
            if (target != null)
            {
                Vector3 vec = transform.position;
                vec.y = target.position.y + camVerticalValue;
                vec.x = Mathf.Max(target.position.x, minX);
                vec.x = Mathf.Min(vec.x, MaX);
                vec.y = Mathf.Max(vec.y/*target.position.y*/, 1.54f);
                //vec.y = Mathf.Min(vec.y, 40);
                //transform.position = vec;

                transform.position = Vector3.Lerp(transform.position, vec, smooth * Time.deltaTime);
                curCameraPos = mainCam.transform.position;
            }
             }
        
        if (GameManager.Instance.cameraShake)
        {
             shake();
        }
       




    }


    private void shake()
    {
           
       
     
        timer += Time.deltaTime;
        if(timer > shakeTimeInterval)
        {
            timer = 0;
            StartCoroutine(SS());
            //StartShake();
        }
          
         
    
     }

    private void StartShake() 
    {
        float camX = Random.value * shakeRange * 2 - shakeRange;
        float camY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 camPos = mainCam.transform.position;
        camPos.x += camX;
        camPos.y += camY;
        mainCam.transform.position = camPos;
        
    }

    IEnumerator SS()
    {
        float camX = Random.value * shakeRange * 2 - shakeRange;
        float camY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 camPos = mainCam.transform.position;
        camPos.x += camX;
        camPos.y += camY;
        mainCam.transform.position = camPos;

        yield return new WaitForSecondsRealtime(shakeTimeInterval);
        mainCam.transform.position = curCameraPos;

    }
    //private void StopShake()
    //{
    //    CancelInvoke("StopShake");
    //    mainCam.transform.position = curCameraPos;
        
    //    Debug.Log("진입3");
    //}
}
