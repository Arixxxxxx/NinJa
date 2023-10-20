using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.instance.F_LegGroundCheaker(collision);
    }

 
}
