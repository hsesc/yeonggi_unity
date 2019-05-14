using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{

    public GameObject keypadScreen;     //키패드
    public Text output;              //키패드 출력 값

    private bool doorOpen;              //문이 열렸는지 아닌지
    private bool showKeypadScreen;      //키패드 보이게 할 것인지 아닌지

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

            showKeypadScreen = false;
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
                    if (!doorOpen)                              //문이 잠겨있는 경우
                    {
                        showKeypadScreen = !showKeypadScreen;   //키패드 활성/비활성화 하고
                    }
                }
            }
            else
            {
                playerHand.onTrigger = false;
            }
        }

        if (showKeypadScreen) //키패드 활성화 되어 있으면
        {
            keypadScreen.SetActive(true);

            if (output.text == "CORRECT")
            {
                output.text = "";
                showKeypadScreen = false;
                doorOpen = true;
            }
        }
        else
        {
            keypadScreen.SetActive(false);
        }

        if (doorOpen)
        {
            transform.GetChild(0).gameObject.SetActive(false);//왼쪽 오른쪽 구분하기 싫으면 그냥 없애기..
        }
    }
}
