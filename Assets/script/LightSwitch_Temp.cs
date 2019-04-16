using UnityEngine;
using System.Collections;

public class LightSwitch_Temp : MonoBehaviour
{
    public GameObject l_light;
    public GameObject textO;
    public GameObject textC;

    void Start()
    {
        textC.SetActive(false);
        textO.SetActive(false);
        l_light.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            textO.SetActive(!l_light.activeSelf);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            textO.SetActive(false);
            textC.SetActive(false);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // toggle the light. If off turn it on,  if on turn it off
                l_light.SetActive(true);
                // update the texts based on the new active state.
                textO.SetActive(!l_light.activeSelf);
                textC.SetActive(l_light.activeSelf);
            }
        }
    }
}