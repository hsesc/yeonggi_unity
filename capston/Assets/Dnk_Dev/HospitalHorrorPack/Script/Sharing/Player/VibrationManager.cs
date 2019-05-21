using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager singleton;
    
    void Start() //create a singleton of a class
    {
        if (singleton && singleton != this)
            Destroy(this);
        else
            singleton = this;
    }

    public void TriggerVibration(AudioClip vibrationAudio, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip(vibrationAudio);

        if (controller == OVRInput.Controller.LTouch)
        {
            //Trigger On Left Controller
            OVRHaptics.LeftChannel.Preempt(clip);
        }
        if (controller == OVRInput.Controller.RTouch)
        {
            //Trigger On Right Controller
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }
    //진동횟수, 진동 간 간격, 진동강도
    public void TriggerVibration(int iteration, int frequency, int strength, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip();

        for(int i = 0; i < iteration; i++)
        {
            clip.WriteSample(i % frequency == 0 ? (byte)strength : (byte)0);
        }
        if (controller == OVRInput.Controller.LTouch)
        {
            //Trigger On Left Controller
            OVRHaptics.LeftChannel.Preempt(clip);
        }
        if (controller == OVRInput.Controller.RTouch)
        {
            //Trigger On Right Controller
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }
}
