using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    //리지드바디 없고 콜라이더 있고 이즈트리거 해제되어 있는 물체에 적용하기
    private float posX, posY, posZ;
    private GameObject getItem;

    // Use this for initialization
    void Start()
    {
        posX = this.transform.position.x;
        posY = this.transform.position.y;
        posZ = this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    { //같이 이동
        if (posX != this.transform.position.x)
        {
            getItem.transform.position = new Vector3(this.transform.position.x, getItem.transform.position.y, getItem.transform.position.z);
            posX = this.transform.position.x;
        }
        if (posY != this.transform.position.y)
        {
            getItem.transform.position = new Vector3(getItem.transform.position.x, this.transform.position.y, getItem.transform.position.z);
            posY = this.transform.position.y;
        }
        if (posZ != this.transform.position.z)
        {
            getItem.transform.position = new Vector3(getItem.transform.position.x, getItem.transform.position.y, this.transform.position.z);
            posZ = this.transform.position.z;
        }

    }

    private void OnCollisionStay(Collision collision) // 충돌물체 계속 인식
    {
        if (collision.gameObject.layer == 8)
        {
            getItem = collision.gameObject;
        }
    }

}