using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCube : MonoBehaviour
{
    public string CubeName;
    public bool get;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == CubeName)
        {
            get = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name == CubeName)
        {
            get = false;
        }
    }
}
