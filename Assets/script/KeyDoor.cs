using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public string keyName;
    private bool getKey;
    private bool handKey;
    private bool doorOpen;

    private GameObject child;
    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<ActionController>();

            CheckKey(); //알맞은 키 검사

            if (!doorOpen)
            {
                if (!handKey && !getKey) //손에 키가 없고 내장된 키도 없을 경우
                {
                    playerHand.SetText("키가 필요할 것 같다.");
                }
                else
                {
                    playerHand.SetText("");
                }
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player")) //ComparTag 가 속도면에서 gameObject.tag 보다 나은것 같다
        {
            CheckKey(); //알맞은 키 검사

            if (playerHand.onTrigger == true) //F 누르면
            {
                if (!doorOpen)
                {
                    if (handKey || getKey) //손에 키가 있거나 / 이미 키를 내장한 경우
                    {
                        doorOpen = true;
                    }
                }
                else
                {
                    doorOpen = false;
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

            RemoveKey();
            
        }
    }

    private void CheckKey()
    {
        if (playerHand.transform.childCount > 1)
        {
            child = playerHand.transform.GetChild(1).gameObject;
            if (child.name == keyName)
            {
                handKey = true;
            }
        }
        else
        {
            handKey = false;
        }
    }

    private void RemoveKey()
    {
        if (handKey) //손에 키가 있는경우
        {
            handKey = false;    //손에 있는 키 비활성화
            getKey = true;      //내장 키 활성화

            playerHand.UseItem(child); //키 오브젝트 삭제
        }
    }
}
