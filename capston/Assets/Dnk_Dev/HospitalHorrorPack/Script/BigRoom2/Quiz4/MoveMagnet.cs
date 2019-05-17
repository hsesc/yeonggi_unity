using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMagnet : MonoBehaviour
{
    private void OnTriggerStay(Collider collider)
    {
        if (collider.name == "MagneticSphere")
        {
            var newPos = Vector3.MoveTowards(collider.transform.position, 
                new Vector3(transform.position.x, transform.position.y, collider.transform.position.z), 
                0.01f);
            collider.transform.position = newPos;
        }
    }
}
