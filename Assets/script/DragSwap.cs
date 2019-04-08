using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSwap : MonoBehaviour {
    // 콜라이더는 물체보다 약간 더 작게 설정
    private Vector3 prePosition;
    private Vector3 curPosition;

	// Use this for initialization
    void Start()
    {
        prePosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnMouseDrag() // 왼쪽 마우스 버튼 클릭 중이면
    {
        //Debug.Log("드래그 중");

        Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position); // 오브젝트의 월드 좌표를 스크린 좌표로 변환
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z); // 현재 마우스의 스크린좌표
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.GetComponent<Collider>().isTrigger = true;
        transform.GetComponent<Rigidbody>().useGravity = false;
    }

    void OnMouseUp()
    {
        transform.position = prePosition;

        transform.GetComponent<Collider>().isTrigger = false;
        transform.GetComponent<Rigidbody>().useGravity = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 8)
        {
            curPosition = collider.gameObject.transform.position;
            collider.gameObject.transform.position = prePosition;
            prePosition = curPosition;
        }
    }
}
