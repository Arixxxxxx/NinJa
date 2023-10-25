using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightMask : MonoBehaviour
{
    public enum ActiveMaskColor
    {
        Blue, Yellow, Green
    }

    public ActiveMaskColor type;
    [SerializeField] float ColorSpeed;


    Transform[] ColorMask;
    Light2D LightColor;
    bool StartLight;

    private void Awake()
    {
        int MaskCount = transform.childCount;
        ColorMask = new Transform[MaskCount];
       

        for(int i = 0; i < MaskCount; i++)
        {
            ColorMask[i] = transform.GetChild(i).GetComponent<Transform>();
        }

        LightColor = ColorMask[4].transform.GetComponent<Light2D>();
    }

    private void Update()
    {
        if (StartLight)
        {
            Debug.Log("1");
            if(LightColor.intensity >= 1.1f)
            {
                StartLight = false;
                Debug.Log("2");
            }
            else if (LightColor.intensity < 1.1f)
            {
                LightColor.intensity += ColorSpeed * Time.deltaTime;
                Debug.Log("3");
            }
                
        }
    }

    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            once = true;
            switch (type)
            {
                case ActiveMaskColor.Blue:
                    ColorMask[0].gameObject.SetActive(false);
                    ColorMask[1].gameObject.SetActive(true);
                    ColorMask[4].gameObject.SetActive(true);
                    LightColor.color = Color.cyan;
                    StartLight = true;
                    break;
                    case ActiveMaskColor.Yellow:
                    ColorMask[0].gameObject.SetActive(false);
                    ColorMask[2].gameObject.SetActive(true);
                    ColorMask[4].gameObject.SetActive(true);
                    LightColor.color = Color.yellow;
                    StartLight = true;
                    break;
                    case ActiveMaskColor.Green:
                    ColorMask[0].gameObject.SetActive(false);
                    ColorMask[3].gameObject.SetActive(true);
                    ColorMask[4].gameObject.SetActive(true);
                    LightColor.color = Color.green;
                    StartLight = true;
                    break;
            }

        }
    }

}
