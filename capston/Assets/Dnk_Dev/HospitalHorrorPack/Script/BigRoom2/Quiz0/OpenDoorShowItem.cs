using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorShowItem : MonoBehaviour
{
    public GameObject door;
    public GameObject item;

    private bool dooropen;

    private Vector3 initPos;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Finger"))
        {
            dooropen = true;
            OVRGrabber ovrGrabber = collider.GetComponentInParent<OVRGrabber>();
            VibrationManager.singleton.TriggerVibration(20, 2, 128, ovrGrabber.GetController());
        }
    }

    private void Start()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        if (dooropen)
        {
            var newPos = Vector3.MoveTowards
                (transform.position, new Vector3(initPos.x, initPos.y, initPos.z + 0.02f), 0.001f);
            transform.position = newPos; //버튼 위치 이동하고(눌림)
            
            item.SetActive(true); //아이템 나타나고

            var newRot = Quaternion.RotateTowards(door.transform.localRotation, Quaternion.Euler(0.0f, 0.0f, 90.0f), Time.deltaTime * 200);
            door.transform.localRotation = newRot; //문열림
        }
        else
        {
            item.SetActive(false);
        }
    }
}
