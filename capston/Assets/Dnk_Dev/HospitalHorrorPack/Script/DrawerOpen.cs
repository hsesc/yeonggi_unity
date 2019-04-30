using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOpen : MonoBehaviour
{
    private Vector3 initPos;    //원래 위치
    private bool drawerOpen;    //서랍이 열렸는지 아닌지

    private bool onTrigger; //범위에 들어가서 에임을 맞추었는지 아닌지
    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<ActionController>();
            onTrigger = true; //범위에 들어갔는지 아닌지 판별
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand = null;
            onTrigger = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        TryEvent();
    }

    private void TryEvent()
    {
        if (onTrigger) // 범위에 들어갔고
        {
            if (playerHand.hitinfo.transform == transform.GetChild(0)) // 에임이 물체에 있는 상태에서
            {
                if (Input.GetKeyDown(KeyCode.F)) //키 누르면(한 번만 실행할 부분)
                {
                    drawerOpen = !drawerOpen;       //서랍 열리기 활성화/비활성화
                }
            }
        }

        if (drawerOpen) //서랍 열리면
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
