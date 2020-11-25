using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public PlayerButton upButton, rightButton, leftButton, downButton, CWButton, CCWButton;
    public bool buttonPressed;
    public float speed = 5f;
    public Text winText;
    public Text loseText;
    public float timer;
    public int seconds;
    public Text timerText;
    public GameObject trigger;
    public GameObject triggerd;
    public bool didWin = false;
    Camera mainCam;
    public Camera leftCam;
    public Camera rightCam;
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trigger = GameObject.FindWithTag("Trigger");
        triggerd = GameObject.FindWithTag("Trigger1");
        mainCam = Camera.main;
        mainCam.enabled = true;
        leftCam.enabled = false;
        rightCam.enabled = false;
        transform.position = new Vector3(Random.Range(-39, 70), Random.Range(-43, 17), Random.Range(-40, -8)); // Initialize the plug in random location at start
        winText.text = "";
        loseText.text = "";

        // switch (Difficulty.currentDifficulty) {
        //     case Difficulty.Difficulties.Easy:
        //         timer = 60f;
        //         timerText.text = timer.ToString();
        //         break;
        //     case Difficulty.Difficulties.Medium:
        //         timer = 50f;
        //         timerText.text = timer.ToString();
        //         break;
        //     case Difficulty.Difficulties.Hard:
        //         timer = 40f;
        //         timerText.text = timer.ToString();
        //         break;
        //     default:
        //         Debug.Log("Default case");
        //         break;
        // }
        timer = 60f; // Set timer for 60 seconds
        timerText.text = timer.ToString();
        restartButton.gameObject.SetActive(false); // Hide restart button at start
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

        // Continue counting down until player wins or loses
        if(didWin == false) gameTimer();

        // Switch between the main, left, and right cameras
        // Key 1 swicthes to main (center), 2 switches to left, and 3 switches to right angled camera
        if(Input.GetKeyDown (KeyCode.Alpha1)) switchMainCamera();
        else if(Input.GetKeyDown (KeyCode.Alpha2)) switchLeftCamera();
        else if(Input.GetKeyDown (KeyCode.Alpha3)) switchRightCamera();

        if (rightButton.IsPressed) rb.AddForce(10f * speed, 0, 0);
        if (upButton.IsPressed) rb.AddForce(0, 10f * speed, 0);
        if (leftButton.IsPressed) rb.AddForce(-10f * speed, 0, 0);
        if (downButton.IsPressed) rb.AddForce(0, -10f * speed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Trigger"))
        {
            winText.text = "You Win!";
            didWin = true;
            restartButton.gameObject.SetActive(true); // Show button when game is over
        }
        else if(other.gameObject.CompareTag("Trigger1"))
        {
            winText.text = "You Win!";
            didWin = true;
            restartButton.gameObject.SetActive(true); // Show button when game is over
        }
    }

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     Debug.Log("OnPointerDown function called.");
    //     buttonPressed = true;
    // }
 
    // public void OnPointerUp(PointerEventData eventData)
    // {
    //     buttonPressed = false;
    // }

    // public void onUpButtonPress()
    // {
    //     if(buttonPressed) rb.transform.Translate(1f * speed, 0, 0);
    // }

    // public void onRightButtonPress()
    // {
    //     Debug.Log("Right Button Pressed.");
    //     rb.velocity = new Vector3(0, 10f * speed, 0);
    // }

    // public void onLeftButtonPress()
    // {
    //     while(buttonPressed) rb.velocity = new Vector3(0, -1f * speed, 0);
    // }

    // public void onDownButtonPress()
    // {
    //     while (buttonPressed) rb.velocity = new Vector3(-1f * speed, 0, 0);
    // }

    // public void onCWButtonPress()
    // {
    //     Debug.Log("CW Burron press.");
    //     while (buttonPressed) rb.transform.Rotate(0, 0, 0.5f);
    // }

    // public void onCCWButtonPress()
    // {
    //     Debug.Log("CCW Button Pressed.");
    //     while (buttonPressed) rb.transform.Rotate(0, 0, -0.5f);
    // }

    public void gameTimer()
    {
        // Timer runs until it either hits 0 or the player loses by touching a pickup
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            seconds = (int)timer % 60;
            seconds = seconds + 1;
            timerText.text = seconds.ToString();
        }
        else
        {
            timerText.text = "0";
            gameOver();
        }
    }

    public void gameOver()
    {
        loseText.text = "You Lose";
        trigger.SetActive(false);
        triggerd.SetActive(false);
        restartButton.gameObject.SetActive(true); // Show button when game is over
    }

    // Switch to main camera
    public void switchMainCamera(){
        mainCam.enabled = true;
        leftCam.enabled = false;
        rightCam.enabled = false;
    }

    // Switch to left camera
    public void switchLeftCamera(){
        mainCam.enabled = false;
        leftCam.enabled = true;
        rightCam.enabled = false;
    }

    // Switch to right camera
    public void switchRightCamera(){
        mainCam.enabled = false;
        leftCam.enabled = false;
        rightCam.enabled = true;
    }

    // Resets game if player hits reset button
    public void onRestartButtonPress()
    {
        SceneManager.LoadScene("Game"); // Restart the game
    }
}
