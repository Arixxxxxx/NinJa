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
    [SerializeField] float fillSpeed;
    IEnumerator LevelUpSystem()
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
            GameUI.instance.F_LevelUp();
             
            
            curExp = curExp - curLvNeedExp;
            lv++;
            

            if (expList.Count < (lv - 1)) //���� �������� ������ ����ġ���� ������ 10%�� �÷��� �߰�
            {
                expList.Add(expList[lv - 2] * 1.1f);
                curLvNeedExp = expList[lv - 1];

            }
            else
            {
                curLvNeedExp = expList[lv - 1];
            }
            ExpBar.fillAmount = curExp / curLvNeedExp;

            //������ ��� ����Ʈ �ߵ�
            //�����߰������ UI �˷���ߵ� + [����Ʈ�� �ִٸ�]

            yield return new WaitForSeconds(0.1f);
            once = false;
        }
    }


    public void F_SetExp(float _Exp)
    {
        curExp += _Exp;
    }
 
}