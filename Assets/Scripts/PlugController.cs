using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlugController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Rigidbody rb;
    public Button upButton, rightButton, leftButton, downButton;
    public bool buttonPressed;
    public float speed = 5f;
    private float h, v;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        buttonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves plug based off user input from both arrow and WASD keys
        h = Input.GetAxis("Horizontal") * speed;
        v = Input.GetAxis("Vertical") * speed;

        rb.velocity = new Vector3(h, v, 0.0f);

        // Space key moves plug forward towards outlet and B key moves plug backwards
        if(Input.GetKey(KeyCode.Space)) rb.velocity = new Vector3(0, 0, 1f * speed);
        else if(Input.GetKey(KeyCode.B)) rb.velocity = new Vector3(0, 0, -1f * speed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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
        while(buttonPressed) rb.velocity = new Vector3(0, 1f * speed, 0);
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
