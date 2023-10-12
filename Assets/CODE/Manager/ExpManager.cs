using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static ExpManager instance;
    public List<float> expList = new List<float>();
    private float curExp;
    private float curLvNeedExp;
    private int lv;
    private int statsPoint;
    private int skillPoint;
    private int AttackPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void Start()
    {
        curLvNeedExp = expList[lv - 1];
    }
    private void Update()
    {
        CurExpMathf();
    }

    bool once;
    private void CurExpMathf()
    {
       

        if(curExp > curLvNeedExp && !once)
        {
            once=true;
            statsPoint++;
            skillPoint++;
            AttackPoint++;
            lv++;
            curLvNeedExp = expList[lv - 1];
            //레벨업 됬다 이펙트 발동
            //스탯추가생겼다 UI 알려줘야됨 + [포인트가 있다면]

        }

    }
}
