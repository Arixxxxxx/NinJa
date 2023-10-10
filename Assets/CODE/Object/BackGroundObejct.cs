using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundObejct : MonoBehaviour
{
    public enum ObjectName
    {
        Fog, tree, leaf, jungleLeaft
    }

    public ObjectName type;

    [Range(0f,30f)][SerializeField] private float Speed = 0.1f;
    [Range(0f,30f)][SerializeField] private float dirTime = 1.5f;
    [Range(0f,30f)][SerializeField] private float MaxAngle = 1.5f;
    private float fogConter;
    private float leafConter;
    private float leafCounter;
    [SerializeField] private float turnValue;
    private Quaternion curRotation;
    float randZ;
    void Start()
    {
        switch (type)
        {
            case ObjectName.Fog:
               
                break;

            case ObjectName.leaf:
                
                randZ = Random.Range(-4, 5);
                transform.rotation = Quaternion.Euler(0, 0, randZ);
                //transform.rotation = Quaternion.Euler(0,0,randZ);
                break;

        }

    }


    void Update()
    {
        switch (type)
        {
            case ObjectName.Fog:
                fogConter += Time.deltaTime;
                if(fogConter < dirTime)
                {
                    transform.position += Vector3.left * Speed * Time.deltaTime;
                }
                else if(fogConter > dirTime && fogConter < dirTime*2 ) 
                {
                    transform.position += Vector3.right * Speed * Time.deltaTime;
                }
                else if( fogConter > dirTime * 2)
                {
                    fogConter = 0; 
                }
                break;

            case ObjectName.leaf:
                leafConter += Time.deltaTime;
                if (leafConter < dirTime)
                {
                    
                    turnValue += Time.deltaTime * Speed;
                   
                    transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + turnValue);
                    
                }
                else if (leafConter > dirTime && leafConter < dirTime * 2)
                {
                    turnValue -= Time.deltaTime * Speed;

                    transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + turnValue);
                }
                else if (leafConter > dirTime * 2)
                {
                    leafConter = 0;
                }
                break; 
            
               case ObjectName.jungleLeaft:

                //leafCounter += Time.deltaTime;

                //if (leafCounter < dirTime)
                //{
                //    turnValue += Time.deltaTime * Speed;
                //}
                //else if (leafCounter >= dirTime && leafCounter < dirTime * 2)
                //{
                //    turnValue -= Time.deltaTime * Speed;
                //}
                //else if (leafCounter >= dirTime * 2)
                //{
                //    leafCounter = 0;
                //}

                // 현재 오브젝트의 현재 각도를 가져옵니다.
                Vector3 curRotation = transform.eulerAngles;
                float Originz = transform.eulerAngles.z;

                // Z 축 각도에 turnValue를 더하거나 빼서 흔들도록 설정합니다.
                curRotation.z = Mathf.Sin(Time.time * Speed) * MaxAngle;
                curRotation.z = curRotation.z + Originz;
                // 수정된 각도를 적용합니다.
                transform.eulerAngles = curRotation;
                break;
        }
                
    }
}
