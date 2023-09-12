using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    Material mat;
    float dis;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }
       void Update()
    {
        dis += Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", Vector3.right * dis);
    }
}
