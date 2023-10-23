using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource Audio;
    [SerializeField] private AudioClip[] edMusic;
    bool isStartMusic;
    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        StartCoroutine(AudioPlays());
    }

    // Update is called once per frame
    void Update()
    {
        if (!Audio.isPlaying && isStartMusic)
        {
            Audio.clip = edMusic[1];
            Audio.Play();
        }
    }

    IEnumerator AudioPlays()
    {
        yield return new WaitForSeconds(1.5f);
        Audio.clip = edMusic[0];
        Audio.Play();
        isStartMusic = true;
    }
}
