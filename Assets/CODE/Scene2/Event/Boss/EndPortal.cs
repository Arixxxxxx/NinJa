using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPortal : MonoBehaviour
{
    [SerializeField] private AudioClip[] Audio_clip;
    AudioSource Audio;
    [SerializeField] Canvas Ending;
    [SerializeField] Image WhiteScreen;
    bool once;
    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        
    }
    private void OnEnable()
    {
        StartCoroutine(PlayAudio());
        
    }
    
    IEnumerator PlayAudio()
    {
        Audio.clip = Audio_clip[0];
        Audio.Play();

        while(Audio.isPlaying) 
        {
            yield return null;
        }

        Audio.clip = Audio_clip[1];
        Audio.Play();
    }

    private void Update()
    {
        
        PlayAudioRepeat();

    }

    private void PlayAudioRepeat()
    {
        if (!once)
        {
            if (Audio.clip == Audio_clip[1] && !Audio.isPlaying)
            {
                Audio.Play();
            }
        }
        else
        {
            Audio.Stop();
            
        }
       

    }

 
    [SerializeField]float SreenSpeed;
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            SoundManager.instance.F_SoundPlay(SoundManager.instance.enterGate, 0.8f);
            GameManager.Instance.F_MoveStop(0);
            StartCoroutine(EndGame());
            //¿£µå¾À
        }
    }
    
    IEnumerator EndGame()
    {

        while(WhiteScreen.color.a < 1)
        {
            WhiteScreen.color += new Color(1, 1, 1, 0.015f);
            SoundManager.instance.Audio.volume -= 0.02f;
            yield return new WaitForSeconds(SreenSpeed);
        }
        Player.instance.F_PlayerMovePosition(new Vector2(179.5f, -1.44f));


        GameManager.Instance.gameUI.gameObject.SetActive(false);
        Ending.gameObject.SetActive(true);
    }
}
