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


    public AudioClip mainThema; //메인 테마곡
    public AudioClip cityThema; // 전투교관있는 마을
    public AudioClip jungleCaveThema; // 전투장비있는 동굴
    public AudioClip Deongen; // 전투장비있는 던전
    public AudioClip CaveThema; //동굴안 테마곡
    private AudioClip caseThema; // 함수 사용 케이스

    //효과음
    [Header("# 효과음")]
    public AudioClip BtnClick; // 버튼 클릭
    public AudioClip ItemGet; // 장비 획득
    public AudioClip ziZin; // 지진소리
    public AudioClip npcTeleport; // 텔
    public AudioClip gateUpComplete; // 게이트 쿵
    public AudioClip enterGate; // 게이트 쿵
    public AudioClip Elevator; // 게이트 쿵
    public AudioClip questComplete; // 퀘완료
    public AudioClip zombieSpawn; // 좀비젠
    public AudioClip boomArrow; // BoomAroow



    [Header("# 플레이어")] 
    public AudioClip playerStep;
    public AudioClip jump; // 이게벽점프임
    public AudioClip dodge; // 구르기
    public AudioClip normalJump;
    public AudioClip ground;
    public AudioClip onHIt;
    public AudioClip block;
    public AudioClip meleeAttack;
    public AudioClip rangeAttak;
    public AudioClip rangeHit;

    [Header("# 근접모드")]
    public AudioClip shcokWave;
    public AudioClip dregonPier;
    public AudioClip sheildOn;
    public AudioClip cry;


    [Header("# 활 관련")]
    public AudioClip Start;
    public AudioClip Shoot;
    public AudioClip tripleShot;
    public AudioClip trapThrow;
    public AudioClip trapActive;
    public AudioClip elecSmall;
    public AudioClip elecLarge;


    [Header("# 몬스터관련")] 
    public AudioClip[] enemyDie;
    public AudioClip[] enemyhit;
    public AudioClip onTrap;
    public AudioClip spikeball;
    public AudioClip firetrapOn;

    [Header("# UI")]
    public AudioClip popup;
    public AudioClip popdown;
    public AudioClip talkBoxChatSound;


    //외부에서 싱글톤으로 넣어줌
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
    /// 사운드재생기
    /// </summary>
    /// <param name="_clip"> 재생클립</param>
    /// <param name="_volume"> 볼륨0f ~ 1.0f </param>
   
   
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
