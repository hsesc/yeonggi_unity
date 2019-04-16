using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    private bool got = false;   //아이템을 들고있는지 아닌지

    [SerializeField]
    private float range;    //습득가능한 최대 거리

    private bool pickupActivated = false;   //습득 가능할 시 true

    private RaycastHit hitinfo; // 충돌체 정보 저장

    //아이템 레이어에만 반응하도록 레이어마스크 설정
    [SerializeField]
    private LayerMask layerMask1; //Item
    [SerializeField]
    private LayerMask layerMask2;

    //필요한 컴포넌트
    [SerializeField]
    private Text actionText;

    [SerializeField]
    private GameObject openBook;

    private List<GameObject> items = new List<GameObject>();// 3d 아이템 저장할 리스트

    // Use this for initialization
    void Start()
    {
        //3d 아이템들 저장
        items.Add(GameObject.Find("Key1"));
        items.Add(GameObject.Find("Book1"));
        
        openBook.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TryAction();        //행동의 함수
    }

    private void TryAction()
    {
        CheckItem();        //어떤 아이템인지 보고 정보 표시
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (got == false) //손에 들고 있는게 없음
            {
                PickupItem(); //물체를 들 수 있는 함수
            }
            else //손에 들고 있는게 있음
            {
                DropItem(); //물체를 떨어뜨릴 수 있는 함수
            }
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward,  //transform.forward = transform.TransformDirection(Vector3,forward)
                out hitinfo, range, layerMask1)) //광선쏘기(플레이어의위치,플레이어가 바라보는 z축방향, 충돌체정보, 사정거리, 레이어마스크)
        {
            pickupActivated = true;
            actionText.gameObject.SetActive(true);
            if (got == false)
            {
                if (hitinfo.transform.tag == "getItem")
                {
                    actionText.text = "획득하려면" + "<color=yellow>" + "(E)" + "</color>";
                }
                else if (hitinfo.transform.tag == "readItem")
                {
                    actionText.text = "읽어보려면" + "<color=yellow>" + "(E)" + "</color>";
                }
            }
            else
            {
                actionText.text = "";
            }
        }
        else
        {
            pickupActivated = false;
            actionText.gameObject.SetActive(false);
        }
    }

    private void PickupItem()
    {
        if (hitinfo.transform != null)
        {
            GameObject child = hitinfo.transform.gameObject;
            child.transform.parent = this.transform;
            child.GetComponent<Rigidbody>().useGravity = false;
            child.GetComponent<BoxCollider>().isTrigger = true;
            child.transform.localPosition = new Vector3(0, 0, 1);
            got = true;

            if (hitinfo.transform.tag == "getItem")
            {
                if (pickupActivated)
                {
                    Debug.Log("획득했습니다.");
                }
            }
            else if (hitinfo.transform.tag == "readItem")
            {
                if (pickupActivated)
                {
                    Debug.Log("읽고 있습니다");

                    if (child.name == "Book1")
                    {
                        openBook.gameObject.SetActive(true);//책 보여주기
                    }
                }
            }
        }
    }
    public void pickupItemFromInventory(int id) //Inventory.cs에서 사용
    {
        GameObject child = items[id]; // 아이디 == 저장위치
        child.transform.parent = this.transform;
        child.GetComponent<Rigidbody>().useGravity = false;
        child.GetComponent<BoxCollider>().isTrigger = true;
        child.transform.localPosition = new Vector3(0, 0, 1);
        got = true;
        child.SetActive(true);

        if (child.name == "Book1")
        {
            openBook.gameObject.SetActive(true);
        }
    }

    private void DropItem()
    {
        GameObject child = this.transform.GetChild(0).gameObject;
        child.GetComponent<Rigidbody>().useGravity = true;
        child.GetComponent<BoxCollider>().isTrigger = false;
        child.transform.parent = null;
        got = false;

        if (child.name == "Book1")
        {
            openBook.gameObject.SetActive(false);
        }
    }
    public int DropItemToInventory() //Inventory.cs에서 사용
    {
        GameObject child = this.transform.GetChild(0).gameObject;
        child.GetComponent<Rigidbody>().useGravity = true;
        child.GetComponent<BoxCollider>().isTrigger = false;
        child.transform.parent = null;
        got = false;
        child.SetActive(false);

        if (child.name == "Book1")
        {
            openBook.gameObject.SetActive(false);
        }
        
        int i;
        for (i = 0; i < items.Count; i++)
        {
            Debug.Log(i);
            if (child.name == items[i].name)
            {
                break;
            }
        }
        return i; // 리스트 인덱스 전달해서 아이템 아이디로 사용
    }

    public void UseItem(GameObject child) //Door.cs에서 사용
    {
        Destroy(child);
        got = false;
    }
}
