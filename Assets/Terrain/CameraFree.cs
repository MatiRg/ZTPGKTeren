using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFree : MonoBehaviour
{
    private float speed = 4.0f;
    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        x += Input.GetAxis("Mouse X") * speed;
        y -= Input.GetAxis("Mouse Y") * speed;
        x = WrapAngle( x );
        y = WrapAngle( y );

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;

        Vector3 position = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"))).normalized;
        position *= speed;
        transform.position += position * 0.2f;
    }

    float WrapAngle(float a)
    {
        if( a >= 360.0f ) a -= 360.0f;
        if( a <= -360.0f ) a += 360.0f;
        return a;
    }
}
