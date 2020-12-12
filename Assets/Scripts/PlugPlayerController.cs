using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugPlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public PlayerButton upButton, rightButton, leftButton, downButton, CWButton, CCWButton, BackwardsButton, ForwardButton;
    public float speed = 5f;

    // public GameObject trigger;
    // public GameObject triggerd;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //Enables all the relevant buttons for the plug player.
        upButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        leftButton.gameObject.SetActive(true);
        downButton.gameObject.SetActive(true);

        //This is supposed to hide rotation buttons if the difficulty is set to easy. Difficulties were not fully implemented, so the buttons are
        //always enabled.
        if (Difficulty.currentDifficulty != Difficulty.Difficulties.Easy) {
            CWButton.gameObject.SetActive(true);
            CCWButton.gameObject.SetActive(true);
        }

        BackwardsButton.gameObject.SetActive(true);
        ForwardButton.gameObject.SetActive(true);
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
        if (BackwardsButton.IsPressed) rb.AddForce(0, 0, -10f * speed);
        if (ForwardButton.IsPressed) rb.AddForce(0, 0, 10f * speed);
    }

    //This function is supposed to disable all buttons similar to the function in ObserverController.cs, but it does not work properly
    //because the plug movement buttons use the PlayerButton script. Disabling the PlayerButton script still disables the functionality of the
    //buttons.
    public void disableButtons() {
        upButton.enabled = false;
        rightButton.enabled = false;
        leftButton.enabled = false;
        downButton.enabled = false;
        CWButton.enabled = false;
        CCWButton.enabled = false;
        BackwardsButton.enabled = false;
        ForwardButton.enabled = false;
    }
}
