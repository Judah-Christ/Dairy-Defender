using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public float followSpeed = 10f;
    public Transform target;
    public float yOffset = 1.5f;
    public float xOffset = 2f;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        Vector3 newpos = new Vector3(target.position.x + xOffset, target.position.y + yOffset, -10);
        transform.position = newpos;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
