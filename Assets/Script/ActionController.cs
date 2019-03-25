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

    //private GameObject pickHere; //소지품

    //아이템 레이어에만 반응하도록 레이어마스크 설정
    [SerializeField]
    private LayerMask layerMask1;
    [SerializeField]
    private LayerMask layerMask2;

    //필요한 컴포넌트
    [SerializeField]
    private Text actionText;

    [SerializeField]
    private GameObject book2d;

    // Use this for initialization
    void Start()
    {
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
            CanPickup();    //물체를 들 수 있는 함수
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CanDrop();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward,  //transform.forward = transform.TransformDirection(Vector3,forward)
                out hitinfo, range, layerMask1)) //광선쏘기(플레이어의위치,플레이어가 바라보는 z축방향, 충돌체정보, 사정거리,레이어마스크)
        {
            pickupActivated = true;
            actionText.gameObject.SetActive(true);
            if (hitinfo.transform.tag == "item")
            {
                actionText.text = "획득하려면" + "<color=yellow>" + "(E)" + "</color>";
            }
            else if (hitinfo.transform.tag == "book")
            {
                actionText.text = "책 펼쳐보기" + "<color=yellow>" + "(E)" + "</color>";
            }

        }
        else
        {
            pickupActivated = false;
            actionText.gameObject.SetActive(false);
            book2d.gameObject.SetActive(false);
        }
    }

    private void CanPickup()
    {
        if (hitinfo.transform.tag == "item")
        {
            if (got == false)
            {
                if (pickupActivated)
                {
                    if (hitinfo.transform != null)
                    {
                        GameObject child = hitinfo.transform.gameObject;
                        Debug.Log("획득했습니다.");
                        /*Destroy(hitinfo.transform.gameObject);
                        InfoDisappear();*/

                        child.transform.parent = this.transform;
                        //pickHere = child;
                        //child.transform.rotation = new Quaternion(0, 0, 0, 0);
                        got = true;
                        child.GetComponent<Rigidbody>().useGravity = false;
                        child.GetComponent<BoxCollider>().isTrigger = true;
                        child.transform.localPosition = new Vector3(0, 0, 1);
                    }
                }
            }
        }
        else if (hitinfo.transform.tag == "book")
        {
            if (got == false)
            {
                if (pickupActivated)
                {
                    if (hitinfo.transform != null)
                    {
                        GameObject child = hitinfo.transform.gameObject;
                        Debug.Log("펼쳐보았습니다");

                        child.transform.parent = this.transform;
                        got = true;
                        child.GetComponent<Rigidbody>().useGravity = false;
                        child.GetComponent<BoxCollider>().isTrigger = true;
                        child.transform.localPosition = new Vector3(0, 0, 1);

                        book2d.gameObject.SetActive(true);//책 보여주기

                    }
                }
            }
        }

    }
    private void CanDrop()
    {
        if (got == true)
        {
            GameObject inhand = this.transform.GetChild(0).gameObject;
            inhand.GetComponent<Rigidbody>().useGravity = true;
            inhand.GetComponent<BoxCollider>().isTrigger = false;
            inhand.transform.parent = null;
            got = false;
        }
    }
}
