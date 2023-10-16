using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Camera1 : MonoBehaviour
{
    public static Camera1 Instance;
    private Transform target;
    private float minX = 0;
    private float MaX = 5000;
    [Range(0f, 30f)]
    [SerializeField] private float camVerticalValue;
    [SerializeField] float smooth;

    [Header("#씬2 장소별 카메라제한")]
    [SerializeField] float Place0XMin;
    [SerializeField] float Place0XMax;
    [SerializeField] float Place1XMin;
    [SerializeField] float Place1XMax;
    [SerializeField] float Place2XMin;
    [SerializeField] float Place2XMax;

    private Camera mainCam;
    public PixelPerfectCamera cam;
    Vector3 curCameraPos;
    [Range(0.01f, 0.1f)][SerializeField] private float shakeRange;
    [Range(0.01f, 2f)][SerializeField] private float shakeTimeInterval = 0.05f;
    float timer;

    private void Awake()
    {
        cam = GetComponent<PixelPerfectCamera>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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
            if (GameManager.Instance.SceneName == "Chapter1")
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

            if(GameManager.Instance.SceneName == "Chapter2")
            {
                switch (GameManager.Instance.PlaceNum)
                {
                    case 0:
                        Vector3 vec = transform.position;
                        vec.y = target.position.y + camVerticalValue;
                        vec.x = Mathf.Max(target.position.x, Place0XMin);
                        vec.x = Mathf.Min(vec.x, Place0XMax);
                        vec.y = Mathf.Max(vec.y/*target.position.y*/, 1.54f);
                        

                        transform.position = Vector3.Lerp(transform.position, vec, smooth * Time.deltaTime);
                        curCameraPos = mainCam.transform.position;
                        break;
                        
                    case 1:
                        vec = transform.position;
                        vec.y = target.position.y + camVerticalValue;
                        vec.x = Mathf.Max(target.position.x, Place1XMin);
                        vec.x = Mathf.Min(vec.x, Place1XMax);
                        vec.y = Mathf.Max(vec.y/*target.position.y*/, 1.54f);


                        transform.position = Vector3.Lerp(transform.position, vec, smooth * Time.deltaTime);
                        curCameraPos = mainCam.transform.position;
                        break;

                    case 2:
                        vec = transform.position;
                        vec.y = target.position.y + camVerticalValue;
                        vec.x = Mathf.Max(target.position.x, Place2XMin);
                        vec.x = Mathf.Min(vec.x, Place2XMax);
                        vec.y = Mathf.Max(vec.y/*target.position.y*/, 1.54f);


                        transform.position = Vector3.Lerp(transform.position, vec, smooth * Time.deltaTime);
                        curCameraPos = mainCam.transform.position;
                        break;

                }
            }
           
        }

        if (GameManager.Instance.cameraShake)
        {
            shake();
        }
    }
    /// <summary>
    /// 씬2 장소이동별 카메라x축 제한
    /// </summary>
    /// <param name="_value"></param>
   
    public void F_ZoomInCam(bool _value)
    {
        if (_value)
        {
            cam.assetsPPU += 1;
            if (cam.assetsPPU >= 20)
            {
                cam.assetsPPU = 20;
            }
        }
        else
        {
            cam.assetsPPU = 18;
        }
    }

    private void shake()
    {
        timer += Time.deltaTime;
        if (timer > shakeTimeInterval)
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

}
