using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFadeManagers : MonoBehaviour
{
    [SerializeField] Animator JungleTileAni;
    [SerializeField] Animator DengeonAni;
    [SerializeField] Animator TreeRoomAni;

    public void F_TileFadeOnOff(TilePoint.TileType type)
    {
        switch (type)
        {
            case TilePoint.TileType.Jungle:
                JungleTileAni.SetTrigger("Off");
                break;

            case TilePoint.TileType.Dengoen:
                DengeonAni.SetTrigger("Off");   
                break;

            case TilePoint.TileType.TreeRoom:
                TreeRoomAni.SetTrigger("Off");
                break;
        }

    }
}
