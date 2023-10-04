using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    
    public GameObject soundMan;
    private Queue<GameObject> audioQue;

    public static SoundManager instance;

    public AudioSource Audio;
    public AudioMixer audioMixer;



    [Range(0.01f,10f)][SerializeField] private float audioChangeSpeed;
    private void Awake()
    {
        audioQue = new Queue<GameObject>();

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
       if(SceneManager.GetActiveScene().name == "Chapter1")
       {
            for (int i = 0; i < 20; i++)
            {
                GameObject obj = Instantiate(soundMan, transform.position, Quaternion.identity, transform.Find("SoundMan"));
                obj.gameObject.SetActive(false);
                audioQue.Enqueue(obj);
            }
        }
       
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
    public AudioClip ItemGet; // ��� ȹ��
    public AudioClip ziZin; // �����Ҹ�
    public AudioClip npcTeleport; // ��
    public AudioClip gateUpComplete; // ����Ʈ ��
    public AudioClip enterGate; // ����Ʈ ��
    public AudioClip Elevator; // ����Ʈ ��
    public AudioClip questComplete; // ���Ϸ�
    public AudioClip zombieSpawn; // ������
    public AudioClip boomArrow; // BoomAroow



    [Header("# �÷��̾�")] 
    public AudioClip playerStep;
    public AudioClip jump; // �̰Ժ�������
    public AudioClip dodge; // ������
    public AudioClip normalJump;
    public AudioClip ground;
    public AudioClip onHIt;
    public AudioClip block;
    public AudioClip meleeAttack;
    public AudioClip rangeAttak;
    public AudioClip rangeHit;

    [Header("# �������")]
    public AudioClip shcokWave;
    public AudioClip dregonPier;
    public AudioClip sheildOn;
    public AudioClip cry;


    [Header("# Ȱ ����")]
    public AudioClip Start;
    public AudioClip Shoot;
    public AudioClip tripleShot;
    public AudioClip trapThrow;
    public AudioClip trapActive;
    public AudioClip elecSmall;
    public AudioClip elecLarge;


    [Header("# ���Ͱ���")] 
    public AudioClip[] enemyDie;
    public AudioClip[] enemyhit;
    public AudioClip onTrap;
    public AudioClip spikeball;
    public AudioClip firetrapOn;

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

    /// <summary>
    /// ���������
    /// </summary>
    /// <param name="_clip"> ���Ŭ��</param>
    /// <param name="_volume"> ����0f ~ 1.0f </param>
   
   
    public void F_SoundPlay(AudioClip _clip, float _volume)
    {
        GameObject obj;
       
        if (audioQue.Count == 0)
        {
            obj = Instantiate(soundMan, transform.position, Quaternion.identity, transform.Find("SoundMan"));

        }
        else
        {
            obj = audioQue.Dequeue();
            obj.SetActive(true);
        }

        AudioSource Audios = obj.GetComponent<AudioSource>();
       
        Audios.clip = _clip;
        Audios.volume = _volume;
        Audios.Play();

        StartCoroutine(EndCheak(Audios, obj));
    }

    IEnumerator EndCheak(AudioSource _Audio, GameObject _obj)
    {
        while (_Audio.isPlaying)
        {
            yield return null;
        }

        _obj.gameObject.SetActive(false);
        audioQue.Enqueue(_obj);

    }


    

}
