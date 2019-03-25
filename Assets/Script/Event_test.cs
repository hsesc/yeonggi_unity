using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_test : MonoBehaviour {
    
    public GameObject Target;
    bool isSearch;

	// Use this for initialization
	void Start () {
        this.GetComponent<Rigidbody>().useGravity = false;
    }

    void Search()
    {
        float distance = Vector3.Distance(Target.transform.position, transform.position);

        //거리가 가까워지면 탐색에 걸림
        if (distance <= 5)
            isSearch = true;
    }

    void ballEvent() {
        this.GetComponent<Rigidbody>().useGravity = true;//물체 중력 생성
    }

    // Update is called once per frame
    void Update () {
        Search();
        if (isSearch == true)
        { //기존과 탐색 조건을 추가함
            ballEvent(); //공격기능            
        }
    }
}
