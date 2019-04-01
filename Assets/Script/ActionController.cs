using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    bool got = false;   //아이템을 들고있는지 아닌지
    [SerializeField]
    private float range;    //습득가능한 최대 거리

    private bool pickupActivated = false;   //습득 가능할 시 true

    private RaycastHit hitinfo; // 충돌체 정보 저장

    //아이템 레이어에만 반응하도록 레이어마스크 설정
    [SerializeField]
    private LayerMask layerMask1;
    [SerializeField]
    private LayerMask layerMask2;

    //필요한 컴포넌트
    [SerializeField]
    private Text actionText;

    [SerializeField]
    private GameObject openBook;

    // Use this for initialization
    void Start()
    {
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
                PickupItem();    //물체를 들 수 있는 함수
                //ItemFromInventory();
            }
            else //손에 들고 있는게 있음
            {
                DropItem();
                //ItemToInventory();
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
        GameObject child = hitinfo.transform.gameObject;
        child.transform.parent = this.transform;
        got = true;
        child.GetComponent<Rigidbody>().useGravity = false;
        child.GetComponent<BoxCollider>().isTrigger = true;
        child.transform.localPosition = new Vector3(0, 0, 1);

        if (hitinfo.transform.tag == "getItem")
        {
            if (pickupActivated)
            {
                if (hitinfo.transform != null)
                {
                    Debug.Log("획득했습니다.");
                }
            }
        }
        else if (hitinfo.transform.tag == "readItem")
        {
            if (pickupActivated)
            {
                if (hitinfo.transform != null)
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
    public void pickupItemToInventory(string name)
    {
        GameObject child = GameObject.Find(name);
        child.transform.parent = this.transform;
        got = true;
        child.GetComponent<Rigidbody>().useGravity = false;
        child.GetComponent<BoxCollider>().isTrigger = true;
        child.transform.localPosition = new Vector3(0, 0, 1);

        child.GetComponent<Renderer>().enabled = true;
        //child.SetActive(true);

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

        openBook.gameObject.SetActive(false);
    }
    public void DropItemToInventory()
    {
        GameObject child = this.transform.GetChild(0).gameObject;
        child.GetComponent<Rigidbody>().useGravity = true;
        child.GetComponent<BoxCollider>().isTrigger = false;
        child.transform.parent = null;
        got = false;
        //child.transform.localPosition = new Vector3(100, 100, 100);//멀리 던지기..ㅎ..근데 못돌아옴 ㅠㅠㅠ

        openBook.gameObject.SetActive(false);
        child.GetComponent<Renderer>().enabled = false;//단점.. 없애는게 아니라 안보이는거.. hitinfo나타남..근데setActive(false)하면 find할 수가 없음..ㅠㅠㅠ..어디 멀리 던져버려야함..
        //child.SetActive(false);
    }

    /*
     private void UseItem()
    {
        if (this.transform.GetChild(0).gameObject != null)
        {

        }
        else actionText.text = "손에 아이템이 읎어요";
    }*/
}
