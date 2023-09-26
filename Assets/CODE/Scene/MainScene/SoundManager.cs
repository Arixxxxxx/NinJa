using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource Audio;
    public AudioMixer audioMixer;


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
        Audio.volume = 0.5f;
    }


    public AudioClip mainThema; //���� �׸���
    public AudioClip cityThema; // ���������ִ� ����
    public AudioClip jungleCaveThema; // ��������ִ� ����
    public AudioClip Deongen; // ��������ִ� ����
    public AudioClip CaveThema; //������ �׸���
    private AudioClip caseThema; // �Լ� ��� ���̽�

    //ȿ����
    [Header("# ȿ����")]
    public AudioClip BtnClick; // ��ư Ŭ��



    [Header("# �÷��̾�")] 
    public AudioClip playerStep;
    public AudioClip jump;
    public AudioClip block;
    public AudioClip meleeAttack;
    public AudioClip rangeAttak;
    public AudioClip rangeHit;

    [Header("# ���Ͱ���")] 
    public AudioClip[] enemyDie;
    public AudioClip[] enemyhit;

    [Header("# UI")]
    public AudioClip popup;
    public AudioClip popdown;
    public AudioClip talkBoxChatSound;


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
        if (Audio.volume >= 0.5f)
        {
            Audio.volume = 0.5f;
             return;
        }
        else
        {
            Audio.volume += audioChangeSpeed * Time.deltaTime;
            Invoke("VolumeUp", 0.1f);
        }

    }

    public void SoundValueChanger(float _value)
    {
        audioMixer.SetFloat("MasterV", Mathf.Log10(_value) * 20);
    }


}
