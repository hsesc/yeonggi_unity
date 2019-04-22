using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOpen : MonoBehaviour
{
    private Vector3 initPos;    //원래 위치
    private bool drawerOpen;    //서랍이 열렸는지 아닌지

    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
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
                if (playerHand.onTrigger == true)   //F 누르면(1번 실행)
                {
                    //playerHand.SetText("아무것도 없다");
                    drawerOpen = !drawerOpen;       //서랍 열리기 활성화/비활성화
                }
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
    void Start () {
        initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if(drawerOpen) //서랍 열리면
        {
            var newPos = Vector3.MoveTowards
                (transform.position, new Vector3(initPos.x, initPos.y, initPos.z - 0.5f), 0.05f);
            transform.position = newPos; //원래 위치에서 z 방향으로 약간 이동
        }
        else// 닫히면
        {
            var newPos = Vector3.MoveTowards
                (transform.position, new Vector3(initPos.x, initPos.y, initPos.z), 0.05f);
            transform.position = newPos; //원래 위치로
        }
	}
}
