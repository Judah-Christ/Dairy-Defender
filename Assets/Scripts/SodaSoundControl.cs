using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class SodaSoundControl : MonoBehaviour
{
    private EventInstance sodaTower;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        sodaTower = AudioManager.instance.CreateEventInstance(FMODEvents.instance.sodaTower);
        sodaTower.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
        sodaTower.start();
    }

    void OnDestroy()
    {
        sodaTower.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        sodaTower.release();
    }
}
