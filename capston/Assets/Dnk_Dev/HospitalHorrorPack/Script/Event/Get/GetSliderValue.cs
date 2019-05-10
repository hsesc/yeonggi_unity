using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSliderValue : MonoBehaviour
{
    public Slider slider;
    public string correctValue;

    float time = 0.0f;
    float wait = 2.0f;

    private void Update()
    {
        int val = (int)slider.value * 10;
        GetComponent<Text>().text = val.ToString(); //값을 받고

        if (GetComponent<Text>().text == correctValue) //알맞는 값이 입력되면
        {
            time += Time.deltaTime; //시간 움직이기
            if (time > wait) //일정시간 이상 경과하면
            {
                GetComponent<Text>().text = "CORRECT";
                time = 0.0f;
            }
        }
        else //알맞는 값이 아닐 경우 시간 초기화
        {
            time = 0.0f;
        }
    }
}
