using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraZoom : MonoBehaviour
{
    Camera cam;
    Rigidbody2D playerRb;
    PixelPerfectCamera cams;
    bool zoomIn;

    [Range(2, 10)]
    public float zoomSize;
    [Range(0.01f, 0.1f)]
    public float zoomSpeed;
    [Range(1, 3)]
    public float waitTime;

    float waitCounter;
    private void Awake()
    {
        cam = Camera.main;
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        cams = cam.GetComponent<PixelPerfectCamera>();
    }

       
    void LateUpdate()
    {
        if (Mathf.Abs(playerRb.velocity.magnitude) < 8) // zoom in
        {
            waitCounter += Time.deltaTime;
            if (waitCounter > waitTime)
            {
                zoomIn = true;
            }
        }

        else // zoomout
        {
            zoomIn = false;
            waitCounter = 0;
        }

        if (zoomIn)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    private void ZoomIn()
    {
     
        cams.assetsPPU = (int)Mathf.Lerp(cam.orthographicSize, zoomSize, zoomSpeed);
    }

    private void ZoomOut()
    {
       
        cams.assetsPPU = (int)Mathf.Lerp(cam.orthographicSize,  18 , zoomSpeed);
    }
}
