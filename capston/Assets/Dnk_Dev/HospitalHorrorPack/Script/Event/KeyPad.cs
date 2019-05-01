using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour {
    
    public string password = "12345";   //비밀번호

    public GameObject keypadScreen;     //키패드
    public Text input;                  //키패드에서 받는 값

    private string curInput;            //현재 키패드 값의 합
    private string output;              //출력 값
    private bool doorOpen;              //문이 열렸는지 아닌지
    private bool showKeypadScreen;      //키패드 보이게 할 것인지 아닌지

    private bool onTrigger; //범위에 들어가서 에임을 맞추었는지 아닌지
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = GameObject.FindWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<ActionController>();
            onTrigger = true; //범위에 들어갔는지 아닌지 판별
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand.SetText("");
            playerHand = null;
            onTrigger = false;

            showKeypadScreen = false;

            player.fixCamera = false;
            player.lockInventory = false;
            player = null;
        }
    }

    void Start()
    {
        keypadScreen.SetActive(false);
    }

    void Update()
    {
        TryEvent();
    }

    private void TryEvent()
    {
        if (onTrigger) // 범위에 들어갔고
        {
            if (playerHand.hitinfo.transform == transform.GetChild(0)) // 에임이 물체에 있는 상태에서
            {
                if (Input.GetKeyDown(KeyCode.F)) //키 누르면(한 번만 실행할 부분)
                {
                    if (!doorOpen)                              //문이 잠겨있는 경우
                    {
                        showKeypadScreen = !showKeypadScreen;   //키패드 활성/비활성화 하고
                    }

                    if (showKeypadScreen)  //키패드 활성화 되어 있으면
                    {
                        playerHand.SetText("그만두려면 <color=yellow>(F)</color>"); //물체 텍스트 설정
                        player.fixCamera = true;        // 화면 멈추고 커서 나타나기
                        player.lockInventory = true;    // 인벤토리 잠금
                    }
                    else //비활성화 되어 있으면
                    {
                        playerHand.SetText("");
                        player.fixCamera = false;
                        player.lockInventory = false;
                    }
                }
            }
            else
            {
                playerHand.SetText("");
            }
        }

        if (showKeypadScreen) //키패드 활성화 되어 있으면
        {
            keypadScreen.SetActive(true);
            GetInput(); //키패드 값 검사
        }
        else
        {
            keypadScreen.SetActive(false);
            curInput = ""; //키패드 값 초기화
            input.text = "";
        }

        if (doorOpen)
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 250);
            transform.rotation = newRot;
        }
    }

    private void GetInput()
    {
        if (input.text == "OK")
        {
            output = curInput;
            input.text = curInput;
        } 
        else if (input.text == "DEL")
        {
            curInput = "";
            input.text = "";
        }
        else if (curInput != input.text)
        {
            curInput = curInput + input.text;
            input.text = curInput;
        }

        if (output == password) //출력 값이 비밀번호와 일치하면
        {
            showKeypadScreen = false;       //키패드 비활성화하고
            //(GetInput 함수의 상위 식을 비활성화 하므로 아래 식들은 1번만 실행됨)
            doorOpen = true;                //문 열리고

            playerHand.SetText("");
            player.fixCamera = false;       //화면 움직이고
            player.lockInventory = false;   //인벤토리 잠금 풀기
        } 
    }
}
