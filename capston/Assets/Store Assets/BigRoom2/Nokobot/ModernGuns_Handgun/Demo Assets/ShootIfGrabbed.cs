using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootIfGrabbed : MonoBehaviour
{
    private SimpleShoot simpleShoot;
    private OVRGrabbable ovrGrabbable;
    public OVRInput.Button shootingButton;

    public int maxNumberOfBullet = 10;
    public Text bulletText;

    public AudioClip shootingAudio;

    // Start is called before the first frame update
    void Start()
    {
        simpleShoot = GetComponent<SimpleShoot>();
        ovrGrabbable = GetComponent<OVRGrabbable>();

        bulletText.text = maxNumberOfBullet.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (ovrGrabbable.isGrabbed && OVRInput.GetDown(shootingButton, ovrGrabbable.grabbedBy.GetController()) && maxNumberOfBullet > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(shootingAudio);

            simpleShoot.TriggerShoot();

            maxNumberOfBullet--;
            bulletText.text = maxNumberOfBullet.ToString();
        }
    }
}
