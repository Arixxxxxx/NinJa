using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMan : MonoBehaviour
{
       public AudioSource  Audio;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }
}
