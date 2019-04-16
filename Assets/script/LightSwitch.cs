using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
    private bool onTrigger;
    private bool active;

    [SerializeField]
    private GameObject l_light;

    [SerializeField]
    private Text l_text;

    void OnTriggerEnter(Collider collider)
    {
        onTrigger = true;
    }
    void OnTriggerExit(Collider collider)
    {
        onTrigger = false;
        l_text.text = "";
    }

    void Start()
    {
        l_text.text = "";
        l_light.SetActive(false);
    }
    void Update()
    {
        if (onTrigger)
        {
            if (!l_light.activeSelf)
            {
                l_text.text = "불을 키려면" + "<color=yellow>" + "(F)" + "</color>";
            }
            else
            {
                l_text.text = "불을 끄려면" + "<color=yellow>" + "(F)" + "</color>";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                active = !active;
            }
        }

        if (active)
        {
            l_light.SetActive(true);
        }
        else
        {
            l_light.SetActive(false);
        }
    }
}