using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    private bool got = false;               //아이템을 들고있는지 아닌지

    [SerializeField]                        // 이 표시 있으면 외부에서 값을 가져와도 내부에서 보호할 수 있음
    private float range;                    //습득가능한 최대 거리

    private bool pickupActivated = false;   //습득 가능한지 아닌지

    public RaycastHit hitinfo;              //layerMask의 충돌체 정보 저장

    //아이템 레이어에만 반응하도록 레이어마스크 설정
    [SerializeField]
    private LayerMask layerMask;            //Item

    //필요한 컴포넌트
    [SerializeField]
    private Text actionText;                //아이템이 아닌 물체에 대한 텍스트
    Outline masking;                        //외곽선 설정

    private List<GameObject> items = new List<GameObject>();// 3d 아이템 저장할 리스트

    [SerializeField]
    private GameObject inventory;           // 인벤토리 가져오기
    private bool showInventory;             // 인벤토리 보이게 할 것인지 아닌지

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        //3d 아이템들 저장(다른 방식으로 바꿈)
        //items.Add(GameObject.Find("Key1"));
        //items.Add(GameObject.Find("Book1"));

        // 먼저 리스트에 빈 오브젝트 추가
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            items.Add(null);
        }

        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ShowInventory();    // Q키 - 인벤토리 함수
        TryAction();        // R키 - 아이템 집고 떨어뜨리는 행동 함수
                            // F키(다른 스크립트에 있음) - 물체와 상호작용하는 함수
    }
    /*
    private void ShowInventory()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //키 누르고
        {
            if (player.lockInventory == false) // 인벤토리 잠금되어 있지 않다면
            {
                showInventory = !showInventory; // 누를때마다 참>거짓>참>거짓>... 인벤토리 활성화/비활성화
            }

            if (showInventory)
            {
                inventory.SetActive(true);
                player.fixCamera = true; //화면 고정
            }
            else
            {
                inventory.SetActive(false);
                inventory.GetComponent<Inventory>().tooltip.SetActive(false); //인벤토리 닫을 때는 켜져 있을 툴팁도 없애야함

                player.fixCamera = false;
            }
        }
    }*/

    public void SetText(string text) // 물체 텍스트 설정
    {
        actionText.text = text;
    }

    private void TryAction()
    {
        CheckItem(); //어떤 아이템인지 보고 아이템 텍스트 설정
        if (Input.GetKeyDown(KeyCode.R)) //키 누르고
        {
            if (got == false) //손에 들고 있는게 없으면
            {
                if (pickupActivated == true)// 습득가능한 상태가 되면
                {
                    PickupItem(); //물체를 드는 함수
                }
            }
            else //손에 들고 있는게 있으면
            {
                DropItem(); //물체를 떨어뜨리는 함수
            }
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, //transform.forward = transform.TransformDirection(Vector3,forward)
                out hitinfo, range, layerMask)) //광선쏘기(플레이어의위치,플레이어가 바라보는 z축방향, 충돌체정보, 사정거리, 레이어마스크)
        { 
            if(masking != null) //이전 마스킹은 해제
            {
                masking.enabled = false;
            }
            masking = hitinfo.transform.GetComponent<Outline>(); //외곽선 설정하고
            masking.enabled = true;

            if (hitinfo.transform.tag == "getItem") //레이캐스트에 닿은 물체의 태그가 다음과 같을 시
            {
                pickupActivated = true; //습득 가능한 상태가 됨
            }
        }
        else //레이캐스트를 벗어나면
        {
            if (masking != null) //외곽선 해제하고
            {
                masking.enabled = false;
                masking = null;
            }

            pickupActivated = false; //습득 불가능한 상태가 됨
        }
    }

    private void PickupItem()
    {
        GameObject child = hitinfo.transform.gameObject;       //레이캐스트에 닿은 물체를
        child.transform.parent = this.transform;                //자식으로 설정하고
        child.GetComponent<Rigidbody>().useGravity = false;     //중력 비활성화
        child.GetComponent<BoxCollider>().isTrigger = true;     //트리거 활성화
        child.transform.localPosition = new Vector3(0.5f, 0, 1);//위치 설정
        got = true;                                             //아이템을 들고 있지 않다고 설정한다

        if (hitinfo.transform.tag == "getItem")
        {
            Debug.Log("획득했습니다.");

            if (child.name == "Flash") // 후레시를 들었을 경우
            {
                if (child.transform.childCount > 0)
                {
                    child.transform.GetChild(0).gameObject.SetActive(true); //후레시 키고
                    child.transform.localRotation = Quaternion.Euler(0, -100, -100); //방향 설정
                }
            }
        }
    }

    public void pickupItemFromInventory(int id) //Inventory.cs에서 사용, 인벤토리에 아이템 넣는 함수
    {
        GameObject child = items[id];                           // 아이디 == 저장위치, 리스트에서 아이템 꺼내서
        items[id] = null;                                       // 리스트에 해당 아이템 지우고
        child.transform.parent = this.transform;
        child.GetComponent<Rigidbody>().useGravity = false;
        child.GetComponent<BoxCollider>().isTrigger = true;
        child.transform.localPosition = new Vector3(0.5f, 0, 1);
        got = true;
        child.SetActive(true);                                  //꺼낸 아이템 활성화
    }

    private void DropItem()
    {
        GameObject child = this.transform.GetChild(1).gameObject;
        child.GetComponent<Rigidbody>().useGravity = true;
        child.GetComponent<BoxCollider>().isTrigger = false;
        child.transform.parent = null;
        got = false;

        if (child.name == "Flash") // 후레시를 들었을 경우
        {
            if (child.transform.childCount > 0)
            {
                child.transform.GetChild(0).gameObject.SetActive(false); //후레시 끄기
            }
        }
    }

    public int DropItemToInventory() //Inventory.cs에서 사용, 인벤토리에서 아이템 꺼내는 함수
    {
        GameObject child = this.transform.GetChild(1).gameObject;
        child.GetComponent<Rigidbody>().useGravity = true;
        child.GetComponent<BoxCollider>().isTrigger = false;
        child.transform.parent = null;
        got = false;
        child.SetActive(false);
        
        int i;
        for (i = 0; i < items.Count; i++)   //리스트 찾아서
        {
            Debug.Log(i);
            if (items[i] == null)           //널인 자리가 있으면
            {
                items[i] = child;           //아이템 넣어둠(인벤토리에서 아이템 꺼냈을 때 SetActive 함수 사용하기 위해)
                break;
            }
            /*
            if (child.name == items[i].name)
            {
                break;
            }*/
        }
        return i; // 리스트 인덱스 전달해서 아이템 아이디로 사용
    }

    public void UseItem(GameObject child) //Door.cs에서 사용, 사용하면 아이템 사라지는 함수
    {
        Destroy(child);
        got = false;
    }
}
