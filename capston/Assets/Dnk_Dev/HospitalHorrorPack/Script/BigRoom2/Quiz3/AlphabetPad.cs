using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetPad : MonoBehaviour
{
    public GameObject item;
    public GameObject alphabetScreen;     //알파벳

    private bool doorOpen;                //문이 열렸는지 아닌지
    private bool showAlphabetScreen;      //알파벳 보이게 할 것인지 아닌지

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

            showAlphabetScreen = false;
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
                    if (!doorOpen) //문이 잠겨있는 경우
                    {
                        showAlphabetScreen = true; //알파벳 활성
                    }
                }
            }
            else
            {
                playerHand.onTrigger = false;
            }
        }

        if (showAlphabetScreen) //알파벳 활성화 되어 있으면
        {
            alphabetScreen.SetActive(true);

            if (alphabetScreen.GetComponent<GetDropdownValue>().checkedValues)
            {
                showAlphabetScreen = false;
                doorOpen = true;
            }
        }
        else
        {
            alphabetScreen.SetActive(false);
        }

        if (doorOpen)
        {
            item.SetActive(true); //아이템 나타나고

            var newRot = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 200);
            transform.localRotation = newRot; //문열림
        }
        else
        {
            item.SetActive(false);
        }
    }
}
