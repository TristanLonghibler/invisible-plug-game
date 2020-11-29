using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugPlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public PlayerButton upButton, rightButton, leftButton, downButton, CWButton, CCWButton;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        upButton.enabled = true;
        rightButton.enabled = true;
        leftButton.enabled = true;
        downButton.enabled = true;
        CWButton.enabled = true;
        CCWButton.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal") * speed;
        float v = Input.GetAxis("Vertical") * speed;

        rb.velocity = new Vector3(h, v, 0.0f);

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(0, 0, 10f * speed);
        }
        else if(Input.GetKey(KeyCode.B))
        {
            rb.AddForce(0, 0, -10f * speed);
        }

        if (rightButton.IsPressed) rb.AddForce(10f * speed, 0, 0);
        if (upButton.IsPressed) rb.AddForce(0, 10f * speed, 0);
        if (leftButton.IsPressed) rb.AddForce(-10f * speed, 0, 0);
        if (downButton.IsPressed) rb.AddForce(0, -10f * speed, 0);
        if (CWButton.IsPressed) rb.transform.Rotate(0, 0, 0.5f);
        if (CCWButton.IsPressed) rb.transform.Rotate(0, 0, -0.5f);
    }
}
