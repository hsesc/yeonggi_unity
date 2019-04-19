using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    //리지드바디 없고 콜라이더 있고 이즈트리거 해제되어 있는 물체에 적용하기
    private GameObject getItem;
    private Vector3 curPos;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8) //충돌한 물체가 아이템이라면 저장
        {
            getItem = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        getItem = null;
    }

    // Use this for initialization
    void Start()
    {
        curPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (curPos != transform.position) //물체가 이동했는데
        {
            if (getItem != null) //아이템이 들어있었다면
            {
                getItem.transform.parent = transform; //같이 움직이기
                getItem.transform.GetComponent<BoxCollider>().isTrigger = true;
                getItem.transform.GetComponent<Rigidbody>().useGravity = false;
            }
            curPos = transform.position; 
        }
    }

}