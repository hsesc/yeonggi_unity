using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    //리지드바디 없고 콜라이더 있고 이즈트리거 해제되어 있는 물체에 적용하기
    private GameObject getItem; //특정 아이템
    private Vector3 curPos;     //현재 위치

    private void OnCollisionEnter(Collision collision) //트리거일 경우 통과하므로 콜리전 사용
    {
        if (collision.gameObject.layer == 8) //레이어가 Item인 경우
        {
            getItem = collision.gameObject; //해당 아이템 저장
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
                getItem.transform.GetComponent<BoxCollider>().isTrigger = true; //트리거 활성화
                getItem.transform.GetComponent<Rigidbody>().useGravity = false; //중력 비활성화
            }
            curPos = transform.position; //현재위치를 이동한 위치로 변경
        }
    }

}