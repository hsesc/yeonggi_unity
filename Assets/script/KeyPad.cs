using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour {
    
    public string password = "12345";

    public GameObject keypadScreen;
    public Text input;

    private string curInput;
    private string output;
    private bool doorOpen;
    private bool showKeyPadScreen;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = GameObject.FindWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<ActionController>();
            //playerHand.SetText("살펴보기 <color=yellow>(F)</color>");
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player")) //ComparTag 가 속도면에서 gameObject.tag 보다 나은것 같다
        {
            if (playerHand.onTrigger == true) //F 누르면
            {
                if (!doorOpen) //문이 잠겨있는 경우
                {
                    showKeyPadScreen = !showKeyPadScreen; //키패드 활성/비활성화
                }                
            }

            if (showKeyPadScreen)
            {
                keypadScreen.SetActive(true);
            }
            else
            {
                keypadScreen.SetActive(false);
            }

            if (keypadScreen.activeSelf)  //키패드 활성화 되어 있으면
            {
                GetInput(); //키패드 값 검사

                playerHand.SetText("그만두려면 <color=yellow>(F)</color>");
                player.fixCamera = true;
                player.lockInventory = true;
            }
            else //키패드 비활성화 되어 있으면
            {
                playerHand.SetText("");
                player.fixCamera = false;
                player.lockInventory = false;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand.SetText("");
            playerHand = null;

            curInput = "";
            input.text = "";
            keypadScreen.SetActive(false);

            player.fixCamera = false;
            player.lockInventory = false;
        }
    }

    void Start()
    {
        keypadScreen.SetActive(false);
    }

    void Update()
    {
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

            curInput = "";
            input.text = "";
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

        if (output == password)
        {
            keypadScreen.SetActive(false);
            doorOpen = true;
        } 
    }
}
