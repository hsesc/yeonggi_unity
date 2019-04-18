using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour
{

    public List<Item> items = new List<Item>();

    void Start()
    {
        // items.Add(new Item(이미지 이름, 이름, 아이템아이디, 설명, 아이템 속성여부))
        items.Add(new Item("16_7", "Key1", 1001, 1, "This is key for door", Item.ItemType.Key));
        items.Add(new Item("16_8", "Key2", 1002, 1, "This is key for door", Item.ItemType.Key));
        items.Add(new Item("29_4", "Book1", 1003, 1, "This book is can read", Item.ItemType.Clue));
        // 원하는 만큼 만들어주기(나중에 디비가 생긴다면 이거 안해도 되는데, 디비 없이 할거면 해야함..!)
    }
}
