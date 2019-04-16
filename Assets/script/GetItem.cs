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
        if (collision.gameObject.layer == 8)
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
    { //같이 이동

        if (curPos != transform.position)
        {
            if (getItem != null)
            {
                getItem.transform.parent = transform;
                getItem.transform.GetComponent<BoxCollider>().isTrigger = true;
                getItem.transform.GetComponent<Rigidbody>().useGravity = false;
            }
            curPos = transform.position; 
        }
    }

}