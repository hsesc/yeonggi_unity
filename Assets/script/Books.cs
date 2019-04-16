using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Books : MonoBehaviour
{
    private List<GameObject> books = new List<GameObject>();
    private bool onTrigger;
    private bool bookEvent;
    private bool targetEvent;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fc;

    public GameObject target;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        onTrigger = true;
    }
    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        bookEvent = false;
        fc.fixCamera = false;
        fc.lockInventory = false;
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            books.Add(transform.GetChild(i).gameObject);
            //Debug.Log(books[i]);
        }

        fc = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bookEvent)
        {
            fc.fixCamera = true;
            fc.lockInventory = true;

            for (int i = 0; i < transform.childCount; i++)
            {
                books[i].GetComponent<DragSwap>().active = true;
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                books[i].GetComponent<DragSwap>().active = false;
            }
        }

        if (books[0].transform.position.x > books[1].transform.position.x
                 && books[1].transform.position.x > books[2].transform.position.x)
        {
            targetEvent = true;
        }

        if (targetEvent)
        {
            var newPos = Vector3.MoveTowards(target.transform.position, new Vector3(8, 0, 8), 0.5f);
            target.transform.position = newPos;

            bookEvent = false;
            fc.fixCamera = false;
            fc.lockInventory = false;
        }
    }

    void OnGUI()
    {
        if (!targetEvent)
        {
            if (onTrigger)
            {
                GUI.Box(new Rect(0, 0, 200, 25), "Press 'F' to moving books");

                if (Input.GetKeyDown(KeyCode.F))
                {
                    onTrigger = false;
                    bookEvent = true;
                }
            }
        }
    }
}