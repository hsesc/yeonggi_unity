using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public string keyName;

    private bool openable;   //열 수 있는지 아닌지
    private bool doorOpen;  //문이 열렸는지 아닌지
    private GameObject key;

    private OVRActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<OVRActionController>();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.name == keyName) //문의 키와 이름이 같은 물체가
        {
            if (collider.gameObject.layer == 10)
            {
                if (collider.GetComponent<OculusSampleFramework.DistanceGrabbable>().isGrabbed) //손에 잡혀 있을 경우
                {
                    key = collider.gameObject;
                }
                else
                {
                    key = null;
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand.SetText("");
            playerHand.onTrigger = false;
            playerHand = null;
        }

        if (collider.name == keyName)
        {
            key = null;
        }
    }

    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame -> OnTriggerStay보다 자연스러움
    void Update()
    {
        TryEvent();
    }

    private void TryEvent()
    {
        if (playerHand) // 범위에 들어갔고
        {
            if (playerHand.hitinfo.transform == transform.GetChild(0)) // 에임이 물체에 있는 상태에서
            {
                playerHand.onTrigger = true; //범위에 들어갔는지 아닌지 알려줌

                if (OVRInput.GetDown(OVRInput.Button.One)) //키 누르면(한 번만 실행할 부분)
                {
                    if (!doorOpen)                  //문이 닫혀있고
                    {
                        if (key != null)            //키가 있는 경우
                        {
                            Destroy(key);           //키가 사라지고
                            key = null;
                            openable = true;        //문을 열 수 있게 됨
                        }

                        if (openable)               //문을 열 수 있는 상태이면
                        {
                            doorOpen = true;        //문 열기
                            playerHand.SetText(""); //물체 텍스트 설정
                        }
                        else //문을 열 수 없는 상태이면
                        {
                            playerHand.SetText("키가 필요할 것 같다.");
                        }
                    }
                    else                            //문이 열린 경우
                    {
                        doorOpen = false;           //문 닫기
                    }
                }
            }
            else
            {
                playerHand.SetText("");
                playerHand.onTrigger = false;
            }
        }

        if (!doorOpen)
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;

        }
        else
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
    }
}
