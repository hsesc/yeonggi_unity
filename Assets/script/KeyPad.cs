using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPad : MonoBehaviour {

    public string password = "12345";
    private string input;
    private string OkInput;
    private bool onTrigger;
    private bool doorOpen;
    private bool keypadScreen;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fc;

    void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }
    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        keypadScreen = false;
        input = "";
        fc.fixCamera = false;
        fc.lockInventory = false;
    }

    void Start()
    {
        fc = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    void Update()
    {
        if(OkInput == password)
        {
            doorOpen = true;
        }

        if (doorOpen)
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 250);
            transform.rotation = newRot;

            fc.fixCamera = false;
            fc.lockInventory = false;
        }

    }
    void OnGUI()
    {
        if (!doorOpen)
        {
            if (onTrigger)
            {
                GUI.Box(new Rect(0, 0, 200, 25), "Press 'F' to open keypad");

                if (Input.GetKeyDown(KeyCode.F))
                {
                    keypadScreen = true;
                    onTrigger = false;
                }
            }
            if (keypadScreen)
            {
                fc.fixCamera = true;
                fc.lockInventory = true;

                GUI.Box(new Rect(350, 50, 320, 455), "");
                GUI.Box(new Rect(355, 55, 310, 25), input);

                if (GUI.Button(new Rect(355, 85, 100, 100), "1"))
                {
                    input = input + "1";
                }
                if (GUI.Button(new Rect(460, 85, 100, 100), "2"))
                {
                    input = input + "2";
                }
                if (GUI.Button(new Rect(565, 85, 100, 100), "3"))
                {
                    input = input + "3";
                }
                if (GUI.Button(new Rect(355, 190, 100, 100), "4"))
                {
                    input = input + "4";
                }
                if (GUI.Button(new Rect(460, 190, 100, 100), "5"))
                {
                    input = input + "5";
                }
                if (GUI.Button(new Rect(565, 190, 100, 100), "6"))
                {
                    input = input + "6";
                }
                if (GUI.Button(new Rect(355, 295, 100, 100), "7"))
                {
                    input = input + "7";
                }
                if (GUI.Button(new Rect(460, 295, 100, 100), "8"))
                {
                    input = input + "8";
                }
                if (GUI.Button(new Rect(565, 295, 100, 100), "9"))
                {
                    input = input + "9";
                }
                if (GUI.Button(new Rect(460, 400, 100, 100), "0"))
                {
                    input = input + "0";
                }
                if (GUI.Button(new Rect(355, 400, 100, 100), "OK"))
                {
                    OkInput = input;
                }
                if (GUI.Button(new Rect(565, 400, 100, 100), "DEL"))
                {
                    input = "";
                }

            }
        }

    }
}
