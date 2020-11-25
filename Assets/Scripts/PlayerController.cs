using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Rigidbody rb;
    public PlayerButton upButton, rightButton, leftButton, downButton, CWButton, CCWButton;
    public bool buttonPressed;
    public float speed = 5f;
    public Text winText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(Random.Range(-39, 70), Random.Range(-43, 17), Random.Range(-40, -8)); // Initialize the plug in random location at start
        winText.text = "";
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

        if (CWButton.IsPressed) rb.transform.Rotate(0, 0, 0.5f);
        if (CCWButton.IsPressed) rb.transform.Rotate(0, 0, -0.5f);
        if (rightButton.IsPressed) rb.AddForce(10f * speed, 0, 0);
        if (upButton.IsPressed) rb.AddForce(0, 10f * speed, 0);
        if (leftButton.IsPressed) rb.AddForce(-10f * speed, 0, 0);
        if (downButton.IsPressed) rb.AddForce(0, -10f * speed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Plug"))
        {
             winText.text = "You Win!";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown function called.");
        buttonPressed = true;
    }
 
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public void onUpButtonPress()
    {
        if(buttonPressed) rb.transform.Translate(1f * speed, 0, 0);
    }

    public void onRightButtonPress()
    {
        Debug.Log("Right Button Pressed.");
        rb.velocity = new Vector3(0, 10f * speed, 0);
    }

    public void onLeftButtonPress()
    {
        while(buttonPressed) rb.velocity = new Vector3(0, -1f * speed, 0);
    }

    public void onDownButtonPress()
    {
        while (buttonPressed) rb.velocity = new Vector3(-1f * speed, 0, 0);
    }

    public void onCWButtonPress()
    {
        Debug.Log("CW Burron press.");
        while (buttonPressed) rb.transform.Rotate(0, 0, 0.5f);
    }

    public void onCCWButtonPress()
    {
        Debug.Log("CCW Button Pressed.");
        while (buttonPressed) rb.transform.Rotate(0, 0, -0.5f);
    }
}
