using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleEvent : MonoBehaviour
{
    public GameObject target;
    public GameObject item;

    private bool dooropen;

    private void Update()
    {
        if (dooropen)
        {
            item.SetActive(true); //아이템 나타나고

            var newRot = Quaternion.RotateTowards(target.transform.localRotation, Quaternion.Euler(0.0f, 0.0f, 90.0f), Time.deltaTime * 200);
            target.transform.localRotation = newRot; //문열림
        }
        else
        {
            item.SetActive(false);
        }
    }

    public void OpenDoor()
    {
        dooropen = true;
    }
}
