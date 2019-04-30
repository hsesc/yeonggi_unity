using UnityEngine;
using System.Collections;

// 아이템의 속성을 정해주는 스크립트
// ItemDataBase 스크립트와 연계됨
[System.Serializable]
// 위 스크립트 한줄은 스크립트의 직렬화를 위한 소스코드
// 위 줄을 사용하면 유니티3D에서 직접 모든 변수에 대해 접근 가능
// 만약 위 코드를 사용하지 않는다면,
// 아래 코드 public class Item 코드를
// public class Item : MonoBehavior(?)로 작성해주어야 함
// 그냥 사용하자..
public class Item {
    public string itemName;         // 아이템의 이름
    public int itemID;              // 아이템의 고유번호
    public int itemValue;              // 아이템 존재 여부
    public string itemDescription;  // 아이템의 설명
    public Sprite itemImage;      // 아이템의 아이콘(2D)
    public ItemType itemType;       // 아이템의 속성 설정
    
    public enum ItemType            // 아이템의 속성 설정에 대한 갯수
    {
        Key,                        // 키(문, 자물쇠 등의 잠금을 푸는 도구)
        Clue,                       // 단서(문제 해결을 위한 소품)
        Use                         // 소모품류
    }

    //Inventory.cs 에서 사용
    public Item() { }

    //itemDatabase.cs 에서 사용
    public Item(string img, string name, int id, int val, string desc, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemValue = val;
        itemDescription = desc;
        itemType = type;
        itemImage = Resources.Load<Sprite>("itemImages/34x34icons180709_" + img);
    }
}
