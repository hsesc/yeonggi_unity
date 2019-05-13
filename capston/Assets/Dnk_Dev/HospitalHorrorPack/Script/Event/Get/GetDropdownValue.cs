using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDropdownValue : MonoBehaviour
{
    public string[] alphabet = { "E", "K", "B", "C", "I"};
    public bool checkedValues;

    float time = 0.0f;
    float wait = 2.0f;

    private void Update()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetChild(0).GetComponent<Text>().text != alphabet[i])
                {
                    time = 0.0f;
                    break;
                }

                if (i == transform.childCount - 1) // 모든 값이 맞으면
                {
                    time += Time.deltaTime; //시간 움직이기
                    if (time > wait) //일정시간 이상 경과하면
                    {
                        checkedValues = true;
                    }
                }
            }
        }
    }
}
