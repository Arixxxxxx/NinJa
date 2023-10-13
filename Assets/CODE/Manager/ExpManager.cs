using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    public static ExpManager instance;
    [SerializeField] GameObject lvUpPrefab;
    [SerializeField] Transform PrePos;    
    
    [SerializeField] private List<float> expList = new List<float>();
    public Dictionary<string, float> EnemyExp = new Dictionary<string, float>();
    
    [SerializeField] private float curExp;
    [SerializeField] private float curLvNeedExp;
    [SerializeField] private int lv = 1;
    [SerializeField] private Image ExpBar;
    [SerializeField] TMP_Text expText;
    

    SkillPointWindow SPW;

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

       

        EnemyExp.Add("S", 15);
        EnemyExp.Add("M", 20);
        EnemyExp.Add("L", 30);
        
    }

    private void Start()
    {
        SPW = GameManager.Instance.gameUI.GetComponent<SkillPointWindow>();
        curLvNeedExp = expList[lv - 1];
    }
    private void Update()
    {
        StartCoroutine(LevelUpSystem());
    }

    bool once;
    float chekingExp;
    int MaxLv= 20;
    [SerializeField] float fillSpeed;
    IEnumerator LevelUpSystem()
    {
        if(lv < MaxLv)
        {
            expText.text = $"{((curExp / curLvNeedExp) * 100).ToString("0.0")}%";
            chekingExp = curExp / curLvNeedExp;

            if (ExpBar.fillAmount < chekingExp)
            {
                ExpBar.fillAmount += Time.deltaTime * fillSpeed;
            }


            if (curExp >= curLvNeedExp && !once)
            {
                once = true;
                GameObject obj = Instantiate(lvUpPrefab, transform.position, Quaternion.identity, PrePos);
                obj.transform.position = PrePos.position;

                SPW.F_GetStatsPoint(1); // ���ȷ� ���� �Լ�
                SPW.F_SetActiveSkillTree(); // ��ųƮ�� �������ִ°� Ȱ��ȭ����
                GameUI.instance.F_LevelUp();


                curExp = curExp - curLvNeedExp;
                lv++;
                            
                curLvNeedExp = expList[lv - 1];
              
                ExpBar.fillAmount = curExp / curLvNeedExp;

                //������ ��� ����Ʈ �ߵ�
                //�����߰������ UI �˷���ߵ� + [����Ʈ�� �ִٸ�]

                yield return new WaitForSeconds(0.1f);
                once = false;
            }
        }
        else if(lv == MaxLv)
        {
            expText.text = string.Empty;
            chekingExp = 1;
        }
    }

    public void F_GmModeGetExp()
    {
        curExp += 50;
    }

        /// <summary>
        /// ����� ����ġ
        /// </summary>
        /// <param name="_Exp">ȹ�淮 �Լ��� ����������</param>
    public void F_SetExp(float _Exp)
    {
        curExp += _Exp;
    }
 
}