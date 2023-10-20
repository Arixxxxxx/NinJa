using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    [SerializeField] private AudioClip[] Audio_clip;
    AudioSource Audio;
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

        if(Audio.clip == Audio_clip[1] && !Audio.isPlaying)
        {
            Audio.Play();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //¿£µå¾À
        }
    }
}
