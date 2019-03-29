using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// edit -> project settings -> script execution order -> + 
// -> itemDatabase: 100, Inventory: 200(스크립트 우선 순위 정하기)
public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    private itemDatabase db;

    public int slotX, slotY; // 인벤토리 가로 세로 속성 설정
    public List<Item> slots = new List<Item>();

    private bool showInventory = false;
    // edit -> project settings -> input -> size: 20 -> cancel 
    // -> name: Inventory, positive button: i, 이 외의 버튼에 대한 설정 삭제
    public GUISkin skin;
    // assets -> creat -> GUIskin -> custom styles -> element0 
    // -> name: slot background, background: (texture2d), padding: (적절하게..5정도?) 
    // -> Inventory 컴포넌트의 skin으로 드래그
    private bool showTooltip;
    private string tooltip;
    // GUIskin -> custom styles -> size: 2 -> slot background
    // -> name: tooltip, background: (texture2d), padding: (적절하게..5정도?)

    private bool dragItem; // 아이템을 드래그 한 것인지에 대한 여부
    private Item draggedItem; // 드래그하고 있는 아이템을 담을 임시 그릇
    private int prevIndex; // 선택했던 아이템의 전 위치를 저장

    private ActionController ac;

    // Use this for initialization
    void Start()
    {
        // 먼저 리스트에 빈 오브젝트 추가
        for (int i = 0; i < slotX * slotY; i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }

        // 디비 가져와서 (태그 설정과 스크립트 작성이 되어 있어야함)
        db = GameObject.FindGameObjectWithTag("Item Database").GetComponent<itemDatabase>();

        //AddItem("Key1"); //아이템 Name 호출
        //AddItem("Book1");

        //RemoveItem(1001);

        /*
        // 디비에 있는 값을 인벤토리에 저장
        for (int i = 0;i < slotX * slotY; i++)
        {
            if (db.items[i] != null)
            {
                inventory[i] = db.items[i];
            }
            else
            {
                // 디비의 아이템칸이 비어있다면 다른 행동을 하도록 유도
            }
        }
        */
        /* for(int i=0; i<n; i++) {
         *      inventory.Add(db.items[i]);
         * }
         */
        ac = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ActionController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory")) // Inventory(i)버튼이 눌리면 아래 내용 실행
        {
            showInventory = !showInventory; // 누를때마다 참>거짓>참>거짓>...
        }
    }

    void OnGUI() //update()와 비슷한 성격
    {
        tooltip = "";
        GUI.skin = skin;
        // Skin 을 skin 답게
        if (showInventory)
        {
            DrawInventory(); // 인벤토리 그리기

            if (showTooltip) // 툴팁 그리기
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 5, Event.current.mousePosition.y + 2, 200, 200), tooltip, skin.GetStyle("tooltip"));
                // 아이템 설명창을 마우스의 좌표에 컨트롤 되게 설정하였으며, GUI skin을 응용하여 설정하였음
            }
        }
        if (dragItem) // 드래그한 아이템 그리기
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x - 5, Event.current.mousePosition.y - 5, 50, 50), draggedItem.itemIcon);
        }
    }

    void DrawInventory()
    {
        int k = 0;
        Event e = Event.current; // 마우스 상태 공동 사용을 위한 변수로 지정
        GameObject child = ac.transform.GetChild(0).gameObject; // 손에 든 아이템 가져오기

        for (int i = 0; i < slotX; i++)
        {
            for (int j = 0; j < slotY; j++)
            {
                GUI.Box(new Rect(i * 52 + 100, j * 52 + 30, 50, 50), "", skin.GetStyle("slot background"));
                // 각 박스의 생성 위치 설정

                slots[k] = inventory[k]; //인벤토리에 저장된 디비 값을 슬롯에도 저장(계속 인벤토리 값 참조)
                //사실 slots안써도 상관 없을 것 같다
                if (slots[k].itemName != null) //슬롯에 아이템 있으면
                {
                    GUI.DrawTexture(new Rect(i * 52 + 100, j * 52 + 30, 50, 50), slots[k].itemIcon);//아이템 그리기
                    Debug.Log(slots[k].itemName);

                    // 마우스가 해당 인벤토리 창-버튼-위로 올라온다면,
                    if (new Rect(i * 52 + 100, j * 52 + 30, 50, 50).Contains(e.mousePosition))
                    {
                        tooltip = CreateTooltip(slots[k]); // 툴팁 생성, 보내는 속성은 k번째 슬롯
                        showTooltip = true; // 툴팁을 만들면 뷰 활성화

                        // 마우스 상태가 0(왼쪽 클릭)이면서 동시에 '마우스 드래그' 상태인 조건
                        if (e.button == 0 && e.type == EventType.mouseDrag && !dragItem)
                        {
                            dragItem = true; // 드래그 아이템을 참으로
                            prevIndex = k; // 선택했던 위치 저장
                            draggedItem = slots[k]; // 현재 슬롯 아이템 저장
                            inventory[k] = new Item();
                        }
                        // 마우스 업(클릭x) 하고 드래그 하고 있는 아이템이 있다면,
                        if (e.type == EventType.mouseUp && dragItem)
                        {
                            inventory[prevIndex] = inventory[k]; // 아이템의 전 위치에 현재 아이템을 놓고
                            inventory[k] = draggedItem; // 현재 아이템의 위치에 드래그하고 있는 아이템을 놓고
                            dragItem = false; // 드래그 옵션은 false로 종료하고
                            draggedItem = null; // 드래그 중인 아이템은 없는걸로 하고
                        }
                    }
                }
                else // 슬롯에 아이템 없고
                {
                    // 마우스가 해당 인벤토리 창-버튼-위로 올라온다면,
                    if (new Rect(i * 52 + 100, j * 52 + 30, 50, 50).Contains(e.mousePosition))
                    {
                        // 마우스 업 하고 드래그 하고 있는 아이템이 없다면,
                        if (e.type == EventType.mouseUp && dragItem)
                        {
                            //inventory[prevIndex] = inventory[k];
                            //빈 공간으로의 아이템 옮기기
                            inventory[k] = draggedItem;
                            dragItem = false;
                            draggedItem = null;
                        }
                        // 마우스 다운이면서 손에 아이템이 있을 때
                        if (e.type == EventType.mouseDown && child != null) 
                        {
                            string name= child.name;
                            Debug.Log(child.name);
                            AddItem(name, k); //오버로딩
                            child.SetActive(false);
                        }
                    }
                }

                if (tooltip == "")
                {
                    showTooltip = false;
                }

                k++; // 갯수 증가
            }
        }
    }

    string CreateTooltip(Item item)
    {
        tooltip = " Item name: <color=#a10000><b>" + item.itemName + "</b></color>\n Item Description: <color=#ffffff>" + item.itemDescription + "</color>";
        /* html 태그가 어느정도 먹힘
         * <color=#000000> 말 </color>
         * <b> 두껍게 </b>
         * ... emdemdemd
         */

        return tooltip;
    }

    void AddItem(string name)
    {
        for (int i = 0; i < inventory.Count; i++) // 전체 인벤토리를 모두 찾아서
        {
            if (inventory[i].itemName == null) // 인벤토리가 빈자리면 
            {
                for (int j = 0; j < db.items.Count; j++) // 디비에 있는 값까지 모두 찾은 다음에
                {
                    if (db.items[j].itemName == name) // 디비의 아이템의 이름과 입력한 이름이 같다면,
                    {
                        inventory[i] = db.items[j]; // 빈 인벤토리에 디비에 저장된 아이템 적용
                        return;
                    }
                }
            }
        }
    }
    void AddItem(string name, int k)
    {
        if (inventory[k].itemName == null) // 인벤토리가 빈자리면 
        {
            for (int j = 0; j < db.items.Count; j++) // 디비에 있는 값까지 모두 찾은 다음에
            {
                if (db.items[j].itemName == name) // 디비의 아이템의 이름과 입력한 이름이 같다면,
                {
                    inventory[k] = db.items[j]; // 빈 인벤토리에 디비에 저장된 아이템 적용
                    return;
                }
            }
        }
    }
    bool inventoryContains(string name)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            return (inventory[i].itemName == name);
        }
        return false;
    }

    void RemoveItem(string name)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == name)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }
}
