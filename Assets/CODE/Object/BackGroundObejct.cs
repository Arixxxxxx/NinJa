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
    private float turnValue;
    private Quaternion curRotation;
    void Start()
    {
        
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
                    turnValue += Time.deltaTime * Speed ;
                   
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

                leafCounter += Time.deltaTime;

                if (leafCounter < dirTime)
                {
                    turnValue += Time.deltaTime * Speed;
                }
                else if (leafCounter >= dirTime && leafCounter < dirTime * 2)
                {
                    turnValue -= Time.deltaTime * Speed;
                }
                else if (leafCounter >= dirTime * 2)
                {
                    leafCounter = 0;
                }

                // ���� ������Ʈ�� ���� ������ �����ɴϴ�.
                Vector3 curRotation = transform.eulerAngles;

                // Z �� ������ turnValue�� ���ϰų� ���� ��鵵�� �����մϴ�.
                curRotation.z = Mathf.Sin(Time.time * Speed) * MaxAngle;

                // ������ ������ �����մϴ�.
                transform.eulerAngles = curRotation;
                break;
        }
                
    }
}