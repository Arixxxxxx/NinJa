using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScan : MonoBehaviour
{
    public bool Trap_on;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.F_SoundPlay(SoundManager.instance.firetrapOn, 0.8f);
            Trap_on = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Trap_on = false;
        }
    }
}
