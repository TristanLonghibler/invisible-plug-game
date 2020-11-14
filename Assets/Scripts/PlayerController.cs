using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Rigidbody rb;
    public Button upButton, rightButton, leftButton, downButton;
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

        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(0, 0, 10f * speed);
        }
        else if(Input.GetKey(KeyCode.B))
        {
            rb.AddForce(0, 0, -10f * speed);
        }
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
        while(buttonPressed) rb.velocity = new Vector3(0, 10f * speed, 0);
    }

    public void onLeftButtonPress()
    {
        while(buttonPressed) rb.velocity = new Vector3(0, -1f * speed, 0);
    }

    public void onDownButtonPress()
    {
        while(buttonPressed) rb.velocity = new Vector3(-1f * speed, 0, 0);
    }
}
