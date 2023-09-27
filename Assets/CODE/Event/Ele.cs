using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ele : MonoBehaviour
{
    AudioSource Audio;
    MoveFlatForm sc;
    private void Awake()
    {
       
        Audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        sc = transform.parent.Find("Elve").GetComponent<MoveFlatForm>();
    }

    bool once;
    private void Update()
    {
      
            if (sc.soundOn && !once)
            {
                once = true;
                Audio.Play();
                //StartCoroutine(Sounds());

            }
            else if (!sc.soundOn)
            {
                once = false;
                Audio.Stop();
            }



    }

    IEnumerator Sounds()
    {
        if(sc.soundOn) 
        {
            yield return null;
        }
        else
        {
            Audio.Stop();
            once = false;
        }
    }
}
