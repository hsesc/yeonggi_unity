using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Books : MonoBehaviour
{
    public GameObject target;   // 이벤트를 실행할 타겟

    private List<GameObject> books = new List<GameObject>(); //DragSwap(옮기기) 스크립트를 적용할 책들 리스트
    private bool bookEvent;     //책 옮기기 이벤트가 발생했는지 아닌지
    private bool targetEvent;   //타겟 이벤트가 발생했는지 아닌지

    private bool onTrigger; //범위에 들어가서 에임을 맞추었는지 아닌지
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    private ActionController playerHand;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = GameObject.FindWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            playerHand = GameObject.FindWithTag("MainCamera").GetComponent<ActionController>();
            onTrigger = true; //범위에 들어갔는지 아닌지 판별
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHand.SetText("");
            playerHand = null;
            onTrigger = false;

            bookEvent = false;

            player.fixCamera = false;
            player.lockInventory = false;
            player = null;
        }
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)      //자식으로 있는 책들
        {
            books.Add(transform.GetChild(i).gameObject);    //리스트에 전부 추가
            //Debug.Log(books[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TryEvent();
    }

    private void TryEvent()
    {
        if (onTrigger) // 범위에 들어갔고
        {
            if (playerHand.hitinfo.transform == transform.GetChild(0)
                || playerHand.hitinfo.transform == transform.GetChild(1)
                || playerHand.hitinfo.transform == transform.GetChild(2)) // 에임이 물체에 있는 상태에서
            {
                if (Input.GetKeyDown(KeyCode.F)) //키 누르면(한 번만 실행할 부분)
                {
                    if (!targetEvent)               //아직 타겟의 이벤트가 발생하지 않은 경우
                    {
                        bookEvent = !bookEvent;     //책옮기기 이벤트 활성/비활성화 하고
                    }

                    if (bookEvent) //책옮기기 이벤트 활성화 되어 있으면
                    {
                        playerHand.SetText("그만두려면 <color=yellow>(F)</color>"); //물체 텍스트 설정
                        player.fixCamera = true;        // 화면 멈추고 커서 나타나기
                        player.lockInventory = true;    // 인벤토리 잠금
                    }
                    else //비활성화 되어 있으면
                    {
                        playerHand.SetText("");
                        player.fixCamera = false;
                        player.lockInventory = false;
                    }
                }
            }
            else
            {
                playerHand.SetText("");
            }
        }

        if (bookEvent) //책 옮기기 이벤트 활성화인 경우
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                books[i].GetComponent<DragSwap>().active = true; // 옮기기 스크립트 활성화
            }
            CompareBooks(); // 책 위치 비교
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                books[i].GetComponent<DragSwap>().active = false;
            }
        }

        if (targetEvent) //타겟 이벤트 활성화인 경우
        {
            var newPos = Vector3.MoveTowards(target.transform.position, new Vector3(8, 0, 8), 0.5f);
            target.transform.position = newPos; //타겟의 위치 변경
        }
    }

    private void CompareBooks()
    {
        if (books[0].transform.position.x > books[1].transform.position.x
                 && books[1].transform.position.x > books[2].transform.position.x) //위치가 다음과 같을 시
        {
            bookEvent = false;              //책 옮기기 이벤트 비활성화하고
            //(CompareBooks 함수의 상위 식을 비활성화 하므로 아래 식들은 1번만 실행됨)
            targetEvent = true;             //타겟 이벤트 활성화하고

            playerHand.SetText("");
            player.fixCamera = false;       //화면 움직이고
            player.lockInventory = false;   //인벤토리 잠금 풀기
        }
    }
}