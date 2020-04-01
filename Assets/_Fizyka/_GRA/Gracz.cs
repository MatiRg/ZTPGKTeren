using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gracz : MonoBehaviour
{
    public GameObject cameraObject; // obiekt Kamery
    Rigidbody rb;
    float speed; // Szybkość aktualna
    const int toWin = 3; // ilem monet aby wygrać
    const float speedOrginal = 11.0f; // szybkość bazowa
    const float jumpSpeed = 250.0f; // szybkość skoku
    bool jumped = false; // czy skok był
    int score = 0; // aktualna lcizba punktów

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = speedOrginal;
        score = 0;
        jumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate()
    {
        if (score >= toWin )
        {
            return;
        }

        Vector3 direction = new Vector3(Input.GetKey(KeyCode.D) ? 1.0f : Input.GetKey(KeyCode.A) ? -1.0f : 0.0f, 0.0f, 
            Input.GetKey(KeyCode.W) ? 1.0f : Input.GetKey(KeyCode.S) ? -1.0f : 0.0f);

        direction = cameraObject.transform.TransformDirection(direction).normalized;

        if (Input.GetKey(KeyCode.Space) && !jumped)
        {
            rb.AddForce( Vector3.up * jumpSpeed * rb.mass );
            jumped = true;
            speed = speedOrginal / 4.0f;
        }
        rb.AddForce(direction * speed * rb.mass);
        transform.rotation = new Quaternion(transform.rotation.x, cameraObject.transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 20), "Press R to Restart");
        GUI.Label(new Rect(0, 20, 200, 20), "You Have "+score.ToString()+" Points");
        if (score >= toWin)
        {
            string wonText = "You Won";
            int textWidth = wonText.Length * 10; // Aproksymacja dlugosci tekstu
            GUI.Label(new Rect(Screen.width / 2 - textWidth / 2, Screen.height / 2, textWidth, 20), wonText);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Teren")
        {
            jumped = false;
            speed = speedOrginal;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Moneta")
        {
            Destroy(other.gameObject);
            ++score;
        }
    }
}
