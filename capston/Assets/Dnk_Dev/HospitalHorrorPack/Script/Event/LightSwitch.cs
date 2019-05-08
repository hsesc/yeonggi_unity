using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
    public GameObject l_light;              //불빛

    private bool lightOn;                   //불이 켜졌는지 아닌지
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
            playerHand.SetText("");
            playerHand.onTrigger = false;
            playerHand = null;
        }
    }

    // Use this for initialization
    void Start()
    {
        l_light.SetActive(false);
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
                    lightOn = !lightOn;             //불빛 활성화/비활성화
                }

                if (lightOn) //불빛 활성화되어 있으면
                {
                    playerHand.SetText(""); //물체 텍스트 설정
                }
                else//비활성화 되어 있으면
                {
                    playerHand.SetText("<color=white>불 키려면 </color><color=yellow>(A)</color>");
                }
            }
            else
            {
                playerHand.SetText("");
                playerHand.onTrigger = false;
            }
        }

        if (lightOn)
        {
            l_light.SetActive(true);
        }
        else
        {
            l_light.SetActive(false);
        }
    }
}