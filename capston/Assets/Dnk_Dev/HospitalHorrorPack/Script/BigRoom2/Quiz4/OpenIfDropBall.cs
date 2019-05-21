using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenIfDropBall : MonoBehaviour
{
    public GameObject target;
    public GameObject item;
    private bool openBox;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Sphere")
        {
            openBox = true;
        }
    }

    private void Update()
    {
        if(openBox)
        {
            item.SetActive(true); //아이템 나타나고
            item.transform.parent = null; //상속 해제하고

            var newRot = Quaternion.RotateTowards(target.transform.localRotation, Quaternion.Euler(-90.0f, 0.0f, 0.0f), Time.deltaTime * 200);
            target.transform.localRotation = newRot;//박스 열림
        }
        else
        {
            item.SetActive(false);
        }
    }
}
