using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource Audio;
 

    [Range(0.01f,10f)][SerializeField] private float audioChangeSpeed;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        Audio = GetComponent<AudioSource>();
    }


    public AudioClip mainThema; //���� �׸���
    public AudioClip cityThema; // ���������ִ� ����
    public AudioClip jungleCaveThema; // ��������ִ� ����
    public AudioClip Deongen; // ��������ִ� ����
    public AudioClip CaveThema; //������ �׸���
    private AudioClip caseThema; // �Լ� ��� ���̽�

    [Header("# �÷��̾�")] public AudioClip playerStep;


    //�ܺο��� �̱������� �־���
    public void AudioChanger(AudioClip _clip)
    {
         if(caseThema == _clip)
        {
            return;
        }

            caseThema = _clip;
            VolumeDown();
    }
     
    private void VolumeDown()
    {
        if (Audio.volume <= 0.1f)
        {
            Audio.clip = caseThema;
            Audio.Play();
            Invoke("VolumeUp", 0.1f);
        }
        else if(Audio.volume > 0.1f) 
        {
            Audio.volume -= audioChangeSpeed * Time.deltaTime;
            Invoke("VolumeDown", 0.2f);
        }
    }

    private void VolumeUp()
    {
        if (Audio.volume == 1)
        {
              return;
        }
        else
        {
            Audio.volume += audioChangeSpeed * Time.deltaTime;
            Invoke("VolumeUp", 0.1f);
        }

    }




}
