using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSwap : MonoBehaviour {
    // 콜라이더 있고 리지드 바디 있는 물체에 설정
    private Vector3 prePosition;    //다음 위치
    private Vector3 curPosition;    //현재 위치

    public bool active;             //활성화되어 있는지 아닌지

	// Use this for initialization
    void Start()
    {
        curPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnMouseDown()
    {
        curPosition = transform.position;
    }

    void OnMouseDrag() // 왼쪽 마우스 버튼 클릭 진행중이면
    {
        //Debug.Log("드래그 중");
        if (active)
        {
            Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position); // 오브젝트의 월드 좌표를 스크린 좌표로 변환
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z); // 현재 마우스의 스크린좌표
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition); //물체가 마우스 따라 이동

            transform.GetComponent<Collider>().isTrigger = true;    //트리거 활성화
            transform.GetComponent<Rigidbody>().useGravity = false; //중력 비활성화
        }
    }

    void OnMouseUp()
    {
        transform.position = curPosition; //물체 원위치로

        transform.GetComponent<Collider>().isTrigger = false;
        transform.GetComponent<Rigidbody>().useGravity = true;
    }

    void OnTriggerEnter(Collider collider) // 스왑하는 부분
    {
        if (collider.gameObject.layer == 9) //레이어가 Prop인 경우
        {
            prePosition = collider.gameObject.transform.position;   //다음 위치 설정
            collider.gameObject.transform.position = curPosition;   //물체를 현재 위치로
            curPosition = prePosition; //현재 위치를 다음 위치로 변경
        }
    }
}
