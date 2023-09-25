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


    public AudioClip mainThema; //메인 테마곡
    public AudioClip cityThema; // 전투교관있는 마을
    public AudioClip jungleCaveThema; // 전투장비있는 동굴
    public AudioClip Deongen; // 전투장비있는 던전
    public AudioClip CaveThema; //동굴안 테마곡
    private AudioClip caseThema; // 함수 사용 케이스

    [Header("# 플레이어")] public AudioClip playerStep;


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
