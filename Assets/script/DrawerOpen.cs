using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOpen : MonoBehaviour
{
    private Vector3 initPos;
    private bool drawerOpen;

    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
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
                //playerHand.SetText("아무것도 없다");
                drawerOpen = !drawerOpen;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //playerHand.SetText("");
            playerHand = null;
        }
    }

    // Use this for initialization
    void Start () {
        initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

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
