using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{

    Transform cam; // 메인카메라
    Vector3 camstartpos;
    float distance;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backspeed;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxspeed;

    private void Start()
    {
        cam = Camera.main.transform;
        camstartpos = cam.position;

        int backcount = transform.childCount;
        mat = new Material[backcount];
        backspeed = new float[backcount];
        backgrounds = new GameObject[backcount];

        for (int i = 0; i < backcount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backcount);
    }

    void BackSpeedCalculate(int BackCount)
    {
        for (int i = 0; i < BackCount; i++)
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for(int i = 0; i <  BackCount; i++)
        {
            backspeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camstartpos.x;
        transform.position = new Vector3(cam.position.x + 2, cam.position.y + 0.5f);

        for(int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backspeed[i] * parallaxspeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }

}
