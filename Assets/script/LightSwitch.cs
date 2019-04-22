﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
    public GameObject l_light;              //불빛

    private bool lightOn;                   //불이 켜졌는지 아닌지
    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<ActionController>();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))      //ComparTag 가 속도면에서 gameObject.tag 보다 나은것 같다
        {
            if (playerHand.hitinfo2.transform == transform.GetChild(0)) // 에임이 물체에 있는 상태에서
            {
                if (lightOn) //불빛 활성화되어 있으면
                {
                    playerHand.SetText("불 끄려면 <color=yellow>(F)</color>"); //물체 텍스트 설정
                }
                else//비활성화 되어 있으면
                {
                    playerHand.SetText("불 키려면 <color=yellow>(F)</color>");
                }

                if (playerHand.onTrigger == true)   //F 누르면(1번 실행)
                {
                    lightOn = !lightOn;             //불빛 활성화/비활성화
                }
            }
            else
            {
                playerHand.SetText("");
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand.SetText("");
            playerHand = null;
        }
    }

    void Start()
    {
        l_light.SetActive(false);
    }

    void Update()
    {
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