using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRActionController : MonoBehaviour
{
    public bool onTrigger;                  //범위에 들어갔는지 아닌지

    [SerializeField]                        // 이 표시 있으면 외부에서 값을 가져와도 내부에서 보호할 수 있음
    private float range;                    //습득가능한 최대 거리

    public RaycastHit hitinfo;              //layerMask의 충돌체 정보 저장

    //아이템 레이어에만 반응하도록 레이어마스크 설정
    [SerializeField]
    private LayerMask layerMask;

    //필요한 컴포넌트
    [SerializeField]
    private Text actionText;                //아이템이 아닌 물체에 대한 텍스트
    Outline masking;                        //외곽선 설정

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TryAction();
    }

    public void SetText(string text) // 물체 텍스트 설정
    {
        actionText.text = text;
    }

    private void TryAction()
    {
        CheckItem(); //어떤 아이템인지 보고
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, //transform.forward = transform.TransformDirection(Vector3,forward)
                out hitinfo, range, layerMask)) //광선쏘기(플레이어의위치,플레이어가 바라보는 z축방향, 충돌체정보, 사정거리, 레이어마스크)
        {
            if (onTrigger) //범위 내에 있는 아이템이라면
            {
                if (masking != null) //이전 마스킹은 해제
                {
                    masking.enabled = false;
                }
                masking = hitinfo.transform.GetComponent<Outline>(); //외곽선 설정하고
                masking.enabled = true;
            }
        }
        else //레이캐스트를 벗어나면
        {
            if (masking != null) //외곽선 해제하고
            {
                masking.enabled = false;
                masking = null;
            }
        }
    }
}
