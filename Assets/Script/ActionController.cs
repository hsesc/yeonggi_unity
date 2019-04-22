using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    public bool onTrigger;                  //범위에 들어갔는지 아닌지

    private bool got = false;               //아이템을 들고있는지 아닌지

    [SerializeField]                        // 이 표시 있으면 외부에서 값을 가져와도 내부에서 보호할 수 있음
    private float range;                    //습득가능한 최대 거리

    private bool pickupActivated = false;   //습득 가능한지 아닌지

    private RaycastHit hitinfo1;            //layerMask1의 충돌체 정보 저장
    public RaycastHit hitinfo2;             //layerMask2의 충돌체 정보 저장

    //아이템 레이어에만 반응하도록 레이어마스크 설정
    [SerializeField]
    private LayerMask layerMask1;           //Item
    [SerializeField]
    private LayerMask layerMask2;           //Prop

    //필요한 컴포넌트
    [SerializeField]
    private Text ItemText;                  //아이템(layer: Item)에 대한 텍스트
    [SerializeField]
    private Text propText;                  //아이템이 아닌 물체에 대한 텍스트

    [SerializeField]
    private GameObject openBook;

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
        openBook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Search();           // F키 - 물체와 상호작용하는 함수
        ShowInventory();    // Q키 - 인벤토리 함수
        TryAction();        // E키 - 아이템 집고 떨어뜨리는 행동 함수
    }

    private void Search()
    {
        //동시에 여러 물체 상호작용을 막기위해 레이캐스트 사용
        Physics.Raycast(transform.position, transform.forward, out hitinfo2, range, layerMask2); 

        if (Input.GetKeyDown(KeyCode.F)) //키 누르면
        {
            onTrigger = true; //범위에 들어갔는지 아닌지 판별
        }/*
        else
        }
        // 가끔 안먹힐 때가 있음
        // true -> 다른 스크립트 진행 -> false 되어야하는데 true -> false -> 다른 스크립트 진행.. 이렇게 되는 것 같다..
        else 
        {
            onTrigger = false;
        }
    }

    public void SetText(string text) // 물체 텍스트 설정
    {
        propText.text = text;
    }

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
                if (openBook.activeSelf == false) //책이 펼쳐져있지 않으면
                {
                    player.fixCamera = false; //화면 움직이기
                }
            }
        }
    }

    private void TryAction()
    {
        CheckItem(); //어떤 아이템인지 보고 아이템 텍스트 설정
        if (Input.GetKeyDown(KeyCode.E)) //키 누르고
        {
            if (got == false) //손에 들고 있는게 없으면
            {
                PickupItem(); //물체를 드는 함수
            }
            else //손에 들고 있는게 있으면
            {
                DropItem(); //물체를 떨어뜨리는 함수
            }
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward,  //transform.forward = transform.TransformDirection(Vector3,forward)
                out hitinfo1, range, layerMask1)) //광선쏘기(플레이어의위치,플레이어가 바라보는 z축방향, 충돌체정보, 사정거리, 레이어마스크)
        { //레이캐스트에 닿으면
            pickupActivated = true; //습득 가능한 상태가 되고
            if (got == false) //손에 들고 있는게 없으면
            {
                if (hitinfo1.transform.tag == "getItem") //레이캐스트에 닿은 물체의 태그가 다음과 같을 시
                {
                    ItemText.text = "획득하려면 <color=yellow>(E)</color>"; //아이템 텍스트 설정
                }
                else if (hitinfo1.transform.tag == "readItem")
                {
                    ItemText.text = "읽어보려면 <color=yellow>(E)</color>";
                }
            }
            else //손에 들고 있는게 있으면
            {
                ItemText.text = "";
            }
        }
        else //레이캐스트를 벗어나면
        {
            pickupActivated = false; //습득 불가능한 상태가 됨
            ItemText.text = ""; 
        }
    }

    private void PickupItem()
    {
        if (hitinfo1.transform != null)                              // 레이캐스트에 닿은게 있으면
        {
            GameObject child = hitinfo1.transform.gameObject;        //레이캐스트에 닿은 물체를
            child.transform.parent = this.transform;                //자식으로 설정하고
            child.GetComponent<Rigidbody>().useGravity = false;     //중력 비활성화
            child.GetComponent<BoxCollider>().isTrigger = true;     //트리거 활성화
            child.transform.localPosition = new Vector3(0, 0, 1);   //위치설정
            got = true;                                             //아이템을 들고 있지 않다고 설정한다

            if (hitinfo1.transform.tag == "getItem")
            {
                if (pickupActivated)
                {
                    Debug.Log("획득했습니다.");

                    if (child.name == "Flash")
                    {
                        if (child.transform.childCount > 0)
                        {
                            child.transform.GetChild(0).gameObject.SetActive(true);
                            child.transform.localRotation = Quaternion.Euler(0, -90, -100);
                        }
                    }
                }
            }
            else if (hitinfo1.transform.tag == "readItem")
            {
                if (pickupActivated)
                {
                    Debug.Log("읽고 있습니다");

                    if (child.name == "Book1")
                    {
                        openBook.SetActive(true);
                        player.fixCamera = true;
                    } 
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
        child.transform.localPosition = new Vector3(0, 0, 1);
        got = true;
        child.SetActive(true);                                  //꺼낸 아이템 활성화

        if (child.name == "Book1")
        {
            openBook.SetActive(true);
        }
    }

    private void DropItem()
    {
        GameObject child = this.transform.GetChild(1).gameObject;
        child.GetComponent<Rigidbody>().useGravity = true;
        child.GetComponent<BoxCollider>().isTrigger = false;
        child.transform.parent = null;
        got = false;

        if (child.name == "Flash")
        {
            if (child.transform.childCount > 0)
            {
                child.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (child.name == "Book1")
        {
            openBook.SetActive(false);
            if (showInventory == false)     //인벤토리가 닫혀있으면
            {
                player.fixCamera = false;   //화면 움직이기
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

        if (child.name == "Book1")
        {
            openBook.SetActive(false);
        }
        
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
