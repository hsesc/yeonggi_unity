using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// edit -> project settings -> script execution order -> + 
// -> itemDatabase: 100, Inventory: 200(스크립트 우선 순위 정하기)
public class Inventory : MonoBehaviour
{
    public List<Slot> slots = new List<Slot>(); //슬롯 리스트(인벤토리)
    public GameObject tooltip;                  //툴팁

    private itemDatabase db;                    //아이템 데이터베이스

    // Use this for initialization
    void Start()
    {
        // 먼저 리스트에 빈 오브젝트 추가
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<Slot>());  //정확하는 오브젝트보단 스크립트!
            transform.GetChild(i).GetComponent<Slot>().number = i;  //슬롯에 순서번호 매기기
        }

        tooltip.SetActive(false);
        db = GameObject.FindGameObjectWithTag("Item Database").GetComponent<itemDatabase>();
    }

    void Update()
    {

    }

    public void ShowTooltip(Item item, Vector3 position) //Slot.cs에서 사용, 툴팁 활성화 함수
    {
        tooltip.SetActive(true);
        tooltip.transform.GetChild(0).GetComponent<Text>().text = 
            " <color=yellow>Item name: </color><b>" + item.itemName + "</b>\n <color=yellow>Item Description:\n </color>" + item.itemDescription + "";
        tooltip.transform.position = position; //세부 위치는 프로그램에서 툴팁의 피벗값 설정
    }

    public void HideTooltip() //Slot.cs에서 사용, 툴팁 비활성화 함수
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
        if (slot.item.itemValue == 0) //존재하는 아이템이 없다면
            slot.transform.GetChild(0).gameObject.SetActive(false); //이미지 비활성화
        else //있다면
        {
            slot.transform.GetChild(0).gameObject.SetActive(true); //이미지 활성화하고
            slot.transform.GetChild(0).GetComponent<Image>().sprite = slot.item.itemImage; //이미지 변경
        }
    }



}
