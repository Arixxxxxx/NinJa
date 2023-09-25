using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointThemaSoundChanger : MonoBehaviour
{
      public enum MapType
    {
        StartPoint, City, JungleCave, Deongen, TreeCave
    }

    public MapType type;

    SoundManager Audio;

    private void Start()
    {
        Audio = SoundManager.instance;
    }
  

    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!once)
            {
                switch (type)
                {
                    case MapType.StartPoint:
                        once = true;
                        SoundManager.instance.AudioChanger(Audio.mainThema);
                        break;

                    case MapType.City:
                        once = false;
                        once = true;
                        SoundManager.instance.AudioChanger(Audio.cityThema);
                        break;

                    case MapType.JungleCave:
                        once = true;
                        SoundManager.instance.AudioChanger(Audio.jungleCaveThema);
                        break;

                    case MapType.Deongen:
                        once = true;
                        SoundManager.instance.AudioChanger(Audio.Deongen);
                        break;

                    case MapType.TreeCave:
                        once = true;
                        SoundManager.instance.AudioChanger(Audio.CaveThema);
                        break;


                }
            }

        }
        
    }
}
