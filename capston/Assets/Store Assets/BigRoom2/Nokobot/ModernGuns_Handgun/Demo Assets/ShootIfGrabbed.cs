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
            //Trigger Haptic:
            //DONT DO THIS !! OVRInput.SetControllerVibration(0.5f, 0.5f, ovrGrabbable.grabbedBy.GetController());

            //DO THIS
            //VibrationManager.singleton.TriggerVibration(shootingAudio, ovrGrabbable.grabbedBy.GetController());
            VibrationManager.singleton.TriggerVibration(40, 2, 255, ovrGrabbable.grabbedBy.GetController());

            GetComponent<AudioSource>().PlayOneShot(shootingAudio);

            simpleShoot.TriggerShoot();

            maxNumberOfBullet--;
            bulletText.text = maxNumberOfBullet.ToString();
        }
    }
}
