using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    public Enemys enemys;
    public DmgPooling dmgpooling;
    public DMGFont dmgfont;

     public float Player_CurHP;
     public  float Player_MaxHP;
     public bool isPlayerDead;

    private void Awake()
    {
        //float targetRatio = 16.0f / 9.0f // FHD 1920 1080 16:0
        //float ratio = (float)Screen.width / (float)Screen.height;
        //                                 //���� ��ũ���� ���� , ����
        //float scaleheight = ratio / targetRatio;
        //float fixedWidth = (float)Screen.width / scaleheight;
        //// ������
        //Screen.SetResolution((int)fixedWidth, Screen.height, true);
        /*Screen.SetResolution*/ // �����ӱ�ɵ����� ���߿� ã�ƺ�����
        /*Application.targetFrameRate = 120; /*///* Ÿ�� ������*/

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

        }

        player = FindObjectOfType<Player>();
        enemys = FindObjectOfType<Enemys>();
        dmgpooling = FindObjectOfType<DmgPooling>();
        dmgfont = FindObjectOfType<DMGFont>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
