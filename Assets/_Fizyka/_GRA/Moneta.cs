using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneta : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    private float y = 0.0f;
    private Vector3 startAngle;

    void Start()
    {
        startAngle = transform.eulerAngles;
        y = startAngle.y;
    }

    void Update()
    {
        y += rotationSpeed;
        if( y >= 360.0f )
        {
            y -= 360.0f;
        }
        Quaternion rotation = Quaternion.Euler(startAngle.x, y, startAngle.z);
        transform.rotation = rotation;
    }
}
