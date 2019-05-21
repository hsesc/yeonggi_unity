using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectTransform : MonoBehaviour
{
    public GameObject obj;
    
    private void Update()
    {
        transform.position = obj.transform.position;
        transform.rotation = obj.transform.rotation;
    }
}
