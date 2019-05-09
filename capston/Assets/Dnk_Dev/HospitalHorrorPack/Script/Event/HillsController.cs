using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HillsController : MonoBehaviour
{
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetComponent<GetCube>().get == false)
                {
                    text.GetComponent<Animator>().SetBool("Appear", false);
                    break;
                }

                if(i == transform.childCount - 1)
                {
                    text.GetComponent<Animator>().SetBool("Appear", true);
                }
            }
        }
    }
}
