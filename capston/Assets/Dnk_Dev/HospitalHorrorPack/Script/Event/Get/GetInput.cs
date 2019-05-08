using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetInput : MonoBehaviour
{
    public string password = "12345";   //비밀번호

    public void Input(string input)
    {
        if (input == "OK")
        {
            if (GetComponent<Text>().text == password)
            {
                GetComponent<Text>().text = "CORRECT";
            }
        }
        else if (input == "DEL")
        {
            GetComponent<Text>().text = "";
        }
        else
        {
            GetComponent<Text>().text += input;
        }
    }
}
