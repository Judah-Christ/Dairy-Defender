using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scan : MonoBehaviour
{
    private bool isActive;
    private bool leftToRight;
    Quaternion angle;
    float zAngle;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        leftToRight = true;
        zAngle = 0;
        speed = 50;

    }
    public void SpeedStop()
    {
        isActive = false;
    }
    public void SpeedStart()
    {
        isActive = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive)
        {
            transform.rotation = angle;
            if (leftToRight)
            {
                zAngle -= Time.deltaTime * speed;
                angle = Quaternion.Euler(0, 0, zAngle);
                if (zAngle <= -90)
                {
                    leftToRight = !leftToRight;
                }
            }
            else
            {
                zAngle += Time.deltaTime * speed;
                angle = Quaternion.Euler(0, 0, zAngle);
                if (zAngle >= 90)
                {
                    leftToRight = !leftToRight;
                }
            }
        }
    }
}
