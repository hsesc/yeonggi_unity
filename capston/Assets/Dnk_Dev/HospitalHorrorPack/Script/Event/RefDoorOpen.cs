using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefDoorOpen : MonoBehaviour
{

    private bool refDoorOpen; //냉장고 열렸는지 아닌지

    private OVRActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<OVRActionController>();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand.onTrigger = false;
            playerHand = null;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame -> OnTriggerStay보다 자연스러움
    void Update()
    {
        TryEvent();
    }

    private void TryEvent()
    {
        if (playerHand) // 범위에 들어갔고
        {
            if (playerHand.hitinfo.transform == transform.GetChild(0)) // 에임이 물체에 있는 상태에서
            {
                playerHand.onTrigger = true; //범위에 들어갔는지 아닌지 알려줌

                if (OVRInput.GetDown(OVRInput.Button.One)) //키 누르면(한 번만 실행할 부분)
                {
                    refDoorOpen = !refDoorOpen; //냉장고 열기 활성화/비활성화
                }
            }
            else
            {
                playerHand.onTrigger = false;
            }
        }

        if (refDoorOpen)
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
        else
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
    }
}
