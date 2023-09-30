using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public GameObject ShockWave;
    public Queue<GameObject> ShockQUE = new Queue<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        for(int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(ShockWave, transform.position, Quaternion.identity, transform);
            obj.gameObject.SetActive(false);
            ShockQUE.Enqueue(obj);
        }
    }

    private void Update()
    {
        MeleeSkill1();
    }

    private void MeleeSkill1()
    {
        if (GameManager.Instance.isGetMeleeItem)
        {
            if (GameManager.Instance.meleeMode)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Player.instance.SwordAni.SetTrigger("R");
                    GameObject obj = ShockQUE.Dequeue();
                    obj.transform.position = Player.instance.transform.Find("Skill").transform.position;
                    if(Player.instance.isLeft)
                    {
                        obj.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    obj.gameObject.SetActive(true);
                    obj.GetComponent<ShockWave>().F_ShockWave();
                }



            }

        }
    }

    private void MeleeSkill2()
    {
        if (GameManager.Instance.isGetMeleeItem)
        {
            if (GameManager.Instance.meleeMode)
            {




            }

        }
    }




}
