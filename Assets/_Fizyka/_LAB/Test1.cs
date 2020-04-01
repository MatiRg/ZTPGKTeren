using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public Rigidbody rb;
    float speed = 50.0f;
    float jumpSpeed = 75.0f;
    string colText = "No";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (Input.GetKey(KeyCode.Space))
        {
            movement += Vector3.up * jumpSpeed;
        }

        rb.AddForce(movement * speed);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0,0, 100, 33), colText);
    }

    void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.tag == "pk" )
        {
            //colText = "Enter";
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "pk")
        {
            //colText = "Stay";
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "pk")
        {
            //colText = "No";
        }
    }
}
