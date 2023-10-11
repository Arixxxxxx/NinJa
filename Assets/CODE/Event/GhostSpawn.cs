using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    Vector3 OriginSclae;
    AudioSource Audio;
    [SerializeField] AudioClip[] SfxList;
    private void Awake()
    {
        OriginSclae = transform.localScale;
        transform.localScale = new Vector3(0, 0, 1);
    }
    private void Start()
    {
        Audio = GetComponent<AudioSource>();
        OpenGate();
        Audio.clip = SfxList[0];
        Audio.Play();
    }

    [SerializeField] float SizeSpeed;
    private void OpenGate()
    {
        if (transform.localScale.x < 1)
        {
            transform.localScale += new Vector3(SizeSpeed, SizeSpeed, transform.localScale.z) * Time.deltaTime;
            Invoke("OpenGate", 0.01f);
            
        }
        else if (transform.localScale.x >= 1)
        {
            StartCoroutine(SpawnGhost());
            Audio.clip = SfxList[1];
            Audio.Play();
        }
    }
    bool once2;
    private void CloseGate()
    {
        if (transform.localScale.x > 0.05f)
        {
            
            if (transform.localScale.x < 0.5f && !once2)
            {
                once2= true;
                Debug.Log("ÁøÀÔ");
                Audio.clip = SfxList[2];
                Audio.Play();
            }
                transform.localScale -= new Vector3(SizeSpeed, SizeSpeed, transform.localScale.z) * Time.deltaTime;
            Invoke("CloseGate", 0.01f);
        }
        else if (transform.localScale.x <= 0.05f)
        {
            
            PoolManager.Instance.F_ReturnObj(gameObject,"Portal");
        }
    }

    [SerializeField] float spawnInterval;
    [SerializeField] float SpawnCount;
    [SerializeField] float PushPower;
    float dir;
    Vector3 spawnPos;
    Vector3 SpawnPos;
    float X, Y;
    int pettenCounter;
    
    IEnumerator SpawnGhost()
    {
        
        while (SpawnCount > 0)
        {
            SpawnCount--;
            GameObject obj = PoolManager.Instance.F_GetObj("Ghost");
            obj.transform.position = transform.position;
            obj.GetComponent<CircleCollider2D>().enabled = false;
            Ghost sc = obj.GetComponent<Ghost>();
            sc.type = Ghost.AttackType.OnlyAttack;
            
            //spawnPos = GameManager.Instance.player.transform.position - transform.position;
            //dir = Mathf.Sign(spawnPos.x);
            switch (pettenCounter)
            {
                case 0:
                    X = -1;
                    Y = 0;
                    break;

                case 1:
                    X = -1;
                    Y = 1;
                    break;

                case 2:
                    X = 0;
                    Y = 1;
                    break;

                case 3:
                    X = 1;
                    Y = 1;
                    break;

                case 4:
                    X = 1;
                    Y = 0;
                    break;
            }


            sc.Rb.AddForce(new Vector3(X, Y) * PushPower, ForceMode2D.Impulse);
            yield return new WaitForSeconds(spawnInterval);
            obj.GetComponent<CircleCollider2D>().enabled = true;
            pettenCounter++;
        }
        
        yield return new WaitForSeconds(5);

        CloseGate();
    }
}
