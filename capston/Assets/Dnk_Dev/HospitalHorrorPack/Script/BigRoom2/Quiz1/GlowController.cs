using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowController : MonoBehaviour
{
    public GameObject l_light; //불빛

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(l_light.activeSelf) //불이 켜져 있을 때
        {
            if(transform.childCount > 0)
            {
                for(int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Outline>().enabled = false; //야광 효과 끄기
                }
            }
        }
        else //불이 꺼져 있을 때
        {
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Outline>().enabled = true; //야광 효과 켜기
                }
            }
        }
    }
}
