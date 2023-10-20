using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalAniSC : MonoBehaviour
{
    [SerializeField] BoxCollider2D Box;
    AudioSource Audio;
    [SerializeField] AudioClip[] clip;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        
    }
    private void Start()
    {
        Audio.clip = clip[0];
        Audio.Play();

        StartCoroutine(playSound());
    }
    IEnumerator playSound()
    {
     
        if (Audio.isPlaying)
        {
            yield return null;
        }

        if (!Audio.isPlaying)
        {
            Audio.clip = clip[1];
            Audio.Play();
        }
    }
    private void A_PortalAni()
    {
        if (!Box.enabled)
        {
            Box.enabled = true;
        }
    }

}
