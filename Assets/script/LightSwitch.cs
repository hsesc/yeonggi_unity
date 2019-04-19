using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
    private bool lightOn;

    public GameObject l_light; 
    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<ActionController>();

            if (!l_light.activeSelf)
            {
                playerHand.SetText("불 키려면 <color=yellow>(F)</color>");
            }
            else
            {
                playerHand.SetText("불 끄려면 <color=yellow>(F)</color>");
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player")) //ComparTag 가 속도면에서 gameObject.tag 보다 나은것 같다
        {
            if (playerHand.onTrigger == true) //F 누르면
            {
                lightOn = !lightOn;
            }

            if (!l_light.activeSelf)
            {
                playerHand.SetText("불 키려면 <color=yellow>(F)</color>");
            }
            else
            {
                playerHand.SetText("불 끄려면 <color=yellow>(F)</color>");
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