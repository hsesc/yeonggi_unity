using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// edit -> project settings -> script execution order -> + 
// -> itemDatabase: 100, Inventory: 200(스크립트 우선 순위 정하기)
public class Inventory : MonoBehaviour
{
    public List<Slot> slots = new List<Slot>();
    public GameObject tooltip;

    private itemDatabase db;

    // Use this for initialization
    void Start()
    {
        // 먼저 리스트에 빈 오브젝트 추가
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<Slot>());
            transform.GetChild(i).GetComponent<Slot>().number = i;
        }

        // 디비 가져와서 (태그 설정과 스크립트 작성이 되어 있어야함)
        db = GameObject.FindGameObjectWithTag("Item Database").GetComponent<itemDatabase>();
    }

    void Update()
    {

    }

    public void ShowTooltip(Item item, Vector3 position)
    {
        tooltip.SetActive(true);
        tooltip.transform.GetChild(0).GetComponent<Text>().text = " <color=yellow>Item name: </color><b>" + item.itemName + "</b>\n <color=yellow>Item Description:\n </color>" + item.itemDescription + "";
        tooltip.transform.position = position;
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    void AddItem(string name)
    {
        for (int i = 0; i < slots.Count; i++) // 전체 인벤토리를 모두 찾아서
        {
            if (slots[i].item.itemValue == 0) // 인벤토리가 빈자리면 
            {
                for (int j = 0; j < db.items.Count; j++) // 디비에 있는 값까지 모두 찾은 다음에
                {
                    if (db.items[j].itemName == name) // 디비의 아이템의 이름과 입력한 이름이 같다면,
                    {
                        slots[i].item = db.items[j]; // 빈 인벤토리에 디비에 저장된 아이템 적용
                        return;
                    }
                }
            }
        }
    }

    public void AddItem(string name, int number, int index)
    {
        if (slots[number].item.itemValue == 0) // 인벤토리가 빈자리면 
        {
            for (int i = 0; i < db.items.Count; i++) // 디비에 있는 값까지 모두 찾은 다음에
            {
                if (db.items[i].itemName == name) // 디비의 아이템의 이름과 입력한 이름이 같다면,
                {
                    slots[number].item = db.items[i]; // 빈 인벤토리에 디비에 저장된 아이템 적용
                    slots[number].item.itemID = index; // 아이디값 저장

                    //이미지 변경
                    ItemImageChange(slots[number]);
                    return;
                }
            }
        }
    }

    void RemoveItem(string name)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item.itemName == name)
            {
                slots[i].item = null;
                slots[i].transform.GetChild(0).gameObject.SetActive(false);
                break;
            }
        }
    }

    public void RemoveItem(string name, int number)
    {
        if (slots[number].item.itemName == name)
        {
            slots[number].item = new Item();
            ItemImageChange(slots[number]);
        }
    }

    public void ItemImageChange(Slot slot)
    {
        if (slot.item.itemValue == 0)
            slot.transform.GetChild(0).gameObject.SetActive(false);
        else
        {
            slot.transform.GetChild(0).gameObject.SetActive(true);
            slot.transform.GetChild(0).GetComponent<Image>().sprite = slot.item.itemImage;
        }
    }



}
