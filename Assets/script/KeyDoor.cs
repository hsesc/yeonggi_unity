using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public string keyName;  //문에 알맞은 키 이름 받기
    private bool getKey;    //내장된 키
    private bool handKey;   //손에 들고 있는 키
    private bool doorOpen;  //문이 열렸는지 아닌지

    private GameObject child;
    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<ActionController>();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))  //ComparTag 가 속도면에서 gameObject.tag 보다 나은것 같다
        {
            if (playerHand.hitinfo2.transform == transform.GetChild(0)) // 에임이 물체에 있는 상태에서
            {
                if (playerHand.onTrigger == true)   //F 누르면(1번 실행)
                {
                    playerHand.onTrigger = false;

                    CheckHand();                    //알맞은 키를 가지고 있는지 검사
                    if (!doorOpen)                  //문이 닫혀있고
                    {
                        if (handKey || getKey)      //손에 키가 있거나 / 이미 키를 내장한 경우
                        {
                            doorOpen = true;        //문 열기
                            playerHand.SetText(""); //물체 텍스트 설정
                        }
                        else //키가 아예 없는 경우
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
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand.SetText("");
            playerHand = null;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (!doorOpen)
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
            
        }
        else
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;

            RemoveKey(); //키 삭제
            
        }
    }

    private void CheckHand()
    {
        if (playerHand.transform.childCount > 1) // 손에 들고 있는게 있고
        {
            child = playerHand.transform.GetChild(1).gameObject; //들고 있는게 
            if (child.name == keyName) //문의 키와 이름이 같은 경우
            {
                handKey = true; //손에 키가 있음
            }
        }
        else //손에 들고 있는게 없으면
        {
            handKey = false; //손에 키가 없음
        }
    }

    private void RemoveKey()
    {
        if (handKey) //손에 키가 있는 경우
        {
            handKey = false;    //손에 있는 키 비활성화
            getKey = true;      //내장 키 활성화

            playerHand.UseItem(child); //키 오브젝트 삭제
        }
    }
}
