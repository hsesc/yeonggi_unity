﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler // OnPointerEnter 메서드를 사용할 때 필요한 인터페이스.
 
{
    public int number;
    public Item item;

    private ActionController ac;
    private GameObject child;

    private Inventory inventory;

    void Start()
    {
        ac = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ActionController>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (item.itemValue > 0) //슬롯에 아이템이 있고
        {
            Debug.Log(item.itemName);
            if (ac.transform.childCount > 1) //손에 든 아이템이 있을 때(0은 캔버스)
            {
                child = ac.transform.GetChild(1).gameObject;

                int id = item.itemID;
                inventory.RemoveItem(item.itemName, number);

                int index = ac.DropItemToInventory();
                inventory.AddItem(child.name, number, index);

                ac.pickupItemFromInventory(id);
                Debug.Log(name + " - 아이템 스왑 완료");

            }
            else //손에 든 아이템이 없을 때
            {
                ac.pickupItemFromInventory(item.itemID);
                Debug.Log(item.itemName + " - 꺼내기 완료");
                inventory.RemoveItem(item.itemName, number); //오버로딩
            }

        }
        else //슬롯에 아이템이 없고
        {
            if (ac.transform.childCount > 1) //손에 든 아이템이 있을 때
            {
                child = ac.transform.GetChild(1).gameObject;
                int index = ac.DropItemToInventory();
                Debug.Log(name + " - 넣기 완료");
                inventory.AddItem(child.name, number, index); //오버로딩
            }
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).parent = inventory.draggingItem; //이미지의 부모를 인벤토리에
            inventory.draggingItem.GetChild(0).position = data.position; //마우스 따라감
        }

    }

    public void OnPointerEnter(PointerEventData data)
    {
        inventory.enteredSlot = this;

        if (item.itemValue > 0) //슬롯에 아이템이 있으면
        {
            inventory.ShowTooltip(item);
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        inventory.enteredSlot = null;

        inventory.HideTooltip();
    }

    public void OnEndDrag(PointerEventData data)
    {
        inventory.draggingItem.GetChild(1).parent = transform; //이미지의 부모를 다시 원래대로
        transform.GetChild(1).localPosition = Vector3.zero; //위치도 원래대로

        if (inventory.enteredSlot != null) // 아이템 스왑
        {
            Item tempItem = item;
            item = inventory.enteredSlot.item;
            inventory.enteredSlot.item = tempItem;

            inventory.ItemImageChange(this);
            inventory.ItemImageChange(inventory.enteredSlot);
        }   
    }

    /*
    public Button theButton;
    private float timeCount;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element. " + eventData);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("The cursor clicked the selectable UI element. " + eventData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("The cursor exited the selectable UI element. " + eventData);
    }
    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("OnBeginDrag: " + data.position);
        data.pointerDrag = null;
    }
    public void OnDrag(PointerEventData data)
    {
        if (data.dragging)
        {
            timeCount += Time.deltaTime;
            if (timeCount > 1.0f)
            {
                Debug.Log("Dragging:" + data.position);
                timeCount = 0.0f;
            }
        }
    }
    public void OnEndDrag(PointerEventData data)
    {
        Debug.Log("OnEndDrag: " + data.position);
    }*/
}