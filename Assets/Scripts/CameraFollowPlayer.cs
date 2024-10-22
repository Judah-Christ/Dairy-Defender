using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera primecam;
    [SerializeField] CinemachineVirtualCamera[] virtcam;
    [SerializeField] string triggerTag;

    // Start is called before the first frame update
    void Start()
    {
        SwitchToCamera(primecam);
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetcamera)
    {
        foreach(CinemachineVirtualCamera camera in virtcam)
        {
            camera.enabled = camera == targetcamera;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
            CinemachineVirtualCamera targetcamera = collision.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera (targetcamera);
            print("other cam");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
            SwitchToCamera(primecam);
            print("back to primcam");
        }
    }


}
