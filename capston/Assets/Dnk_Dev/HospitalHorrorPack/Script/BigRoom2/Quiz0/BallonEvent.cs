using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonEvent : MonoBehaviour
{
    public GameObject item;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.name == "Bullet_45mm_Bullet")
        {
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<OculusSampleFramework.DistanceGrabbable>().enabled = true;
            Destroy(transform);
        }
    }

    private void Start()
    {
        item.GetComponent<OculusSampleFramework.DistanceGrabbable>().enabled = false;
    }
}
