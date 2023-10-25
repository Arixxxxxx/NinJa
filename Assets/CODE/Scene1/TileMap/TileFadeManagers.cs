using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFadeManagers : MonoBehaviour
{
    [SerializeField] Animator JungleTileAni;
    [SerializeField] Animator[] DengeonAni;
    [SerializeField] Animator TreeRoomAni;


    public void F_TileFadeOnOff(TilePoint.TileType type, float dir)
    {

        switch (type)
        {
            case TilePoint.TileType.Jungle:
                if (dir < 0)
                {
                    Debug.Log("1");
                    JungleTileAni.SetTrigger("Off");
                }
                if (dir > 0)
                {
                    Debug.Log("2");
                    JungleTileAni.SetTrigger("On");
                }
                    break;

            case TilePoint.TileType.Dengoen:
                 if(dir > 0)
                {
                    
                    int count = DengeonAni.Length;
                    for(int i = 0; i < count; i++)
                    {
                        DengeonAni[i].SetTrigger("Off");
                        GameManager.Instance.F_SetLigtValume(true);
                    }
                }
                 if(dir < 0)
                {
                    
                    int count = DengeonAni.Length;
                    for (int i = 0; i < count; i++)
                    {
                        DengeonAni[i].SetTrigger("On");
                        GameManager.Instance.F_SetLigtValume(false);
                    }
                }

                break;

            case TilePoint.TileType.TreeRoom:
                TreeRoomAni.SetTrigger("Off");
                break;
        }

    }
}
