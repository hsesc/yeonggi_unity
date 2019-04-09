using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_test2 : MonoBehaviour
{
    private List<GameObject> items = new List<GameObject>();
    private int count = 3;
    private int firstItemNum = 2;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < count; i++)
        {
            items.Add(null);
        }
	}
	
	// Update is called once per frame
	void Update () {

        SearchObj(); //오브젝트 가져와서
        objEvent(); //조건에 맞으면 이벤트 실행

	}

    private void SearchObj()
    {
        for (int i = 0; i < count; i++)
        {
            int bookNum = i + firstItemNum;
            items[i] = GameObject.Find("Book" + bookNum.ToString());
        }
    }
    private void objEvent()
    {
        if (items[0].transform.position.x > items[1].transform.position.x
            && items[1].transform.position.x > items[2].transform.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(8, 0, 8), 0.5f);
        }
    }
}
