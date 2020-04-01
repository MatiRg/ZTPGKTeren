using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public GameObject target;
    public float mouseSpeed = 1.0f;
    Vector3 offset;
    float x = 0.0f;
    float y = 0.0f;
    float yMax = 30.0f;
    float yMin = -15.0f;
    Vector3 cameraVelocity = Vector3.zero;
    public float smoothTime = 0.25f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - target.transform.position;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * mouseSpeed;
        y -= Input.GetAxis("Mouse Y") * mouseSpeed;

        x = WrapAngle(x);
        y = ClampAngle(y, yMin, yMax);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * offset + target.transform.position;

        transform.rotation = rotation;
        transform.position = Vector3.SmoothDamp(transform.position, position, ref cameraVelocity, smoothTime);
    }

    float WrapAngle(float a)
    {
        if (a >= 360.0f) a -= 360.0f;
        if (a <= -360.0f) a += 360.0f;
        return a;
    }

    float ClampAngle(float a, float min, float max)
    {
        a = WrapAngle(a);
        if (a < min) a = min;
        if (a > max) a = max;
        return a;
    }
}
