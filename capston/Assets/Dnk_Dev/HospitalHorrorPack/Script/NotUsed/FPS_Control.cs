using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Control : MonoBehaviour {
    public float moveSpeed = 5.0f;
    public float rotSpeed = 3.0f;

    public Camera fpsCam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveCtr1();
        RotCtr1();
	}

    void MoveCtr1() {
        //움직임 키 받기
        if (Input.GetKey(KeyCode.W)) {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    void RotCtr1() {
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;

        //오브젝트 연결 - 카메라가 좌/우로 회전
        this.transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        //내용은 같음 -> 카메라를 연결, x축 기준으로 회전(위,아래), -는 방향 바꾸기
        fpsCam.transform.localRotation *= Quaternion.Euler(-rotX, 0, 0);
    }
}
