using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefDoorOpen : MonoBehaviour {

    private bool onTrigger;
    private bool refDoorOpen;

    void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (onTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                refDoorOpen = !refDoorOpen;
            }
        }

        if (refDoorOpen)
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
        else
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
    }
}
