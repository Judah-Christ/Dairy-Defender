using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEventCamera : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Canvas canvas = gameObject.GetComponent<Canvas>();

        canvas.worldCamera = mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
