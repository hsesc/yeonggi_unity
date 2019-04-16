using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOpen : MonoBehaviour
{
    private bool onTrigger;
    private bool drawerOpen;
    private Vector3 initPos;

    void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
    }

    // Use this for initialization
    void Start () {
        initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if(onTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                drawerOpen = !drawerOpen;
            }
        }

        if(drawerOpen)
        {
            var newPos = Vector3.MoveTowards
                (transform.position, new Vector3(initPos.x, initPos.y, initPos.z - 0.5f), 0.05f);
            transform.position = newPos;
        }
        else
        {
            var newPos = Vector3.MoveTowards
                (transform.position, new Vector3(initPos.x, initPos.y, initPos.z), 0.05f);
            transform.position = newPos;
        }
	}
}
