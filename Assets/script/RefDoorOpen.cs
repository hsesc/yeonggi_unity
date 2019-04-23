using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefDoorOpen : MonoBehaviour {

    private bool refDoorOpen; //냉장고 열렸는지 아닌지

    private bool onTrigger; //범위에 들어가서 에임을 맞추었는지 아닌지
    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<ActionController>();
            //playerHand.SetText("살펴보기 <color=yellow>(F)</color>");
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))      //ComparTag 가 속도면에서 gameObject.tag 보다 나은것 같다
        {
            if (playerHand.hitinfo2.transform == transform.GetChild(0)) // 에임이 물체에 있는 상태에서
            {
                if (Input.GetKeyDown(KeyCode.F)) //키 누르면
                {
                    onTrigger = true; //범위에 들어갔는지 아닌지 판별
                }

                TryEvent();
            }
            else
            {
                //playerHand.SetText("");
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //playerHand.SetText("");
            playerHand = null;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (refDoorOpen)
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
        else
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
    }

    private void TryEvent()
    {
        if (onTrigger == true)   //F 누르면(1번 실행)
        {
            onTrigger = false;

            //playerHand.SetText("아무것도 없다");
            refDoorOpen = !refDoorOpen;     //냉장고 열기 활성화/비활성화
        }
    }
}
