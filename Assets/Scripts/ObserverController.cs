using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ObserverController : MonoBehaviour
{
    public Camera mainCam;
    public Camera leftCam;
    public Camera rightCam;
    public Camera plugPlayerCam;
    public Button leftCameraButton;
    public Button mainCameraButton;
    public Button rightCameraButton;
    // Start is called before the first frame update
    void Start()
    {
        mainCam.enabled = true;
        leftCam.enabled = false;
        rightCam.enabled = false;
        plugPlayerCam.enabled = false;
        leftCameraButton.gameObject.SetActive(true);
        mainCameraButton.gameObject.SetActive(true);
        rightCameraButton.gameObject.SetActive(true);

        leftCameraButton.onClick.AddListener(switchLeftCamera);
        mainCameraButton.onClick.AddListener(switchMainCamera);
        rightCameraButton.onClick.AddListener(switchRightCamera);
    }

    // Update is called once per frame
    void Update()
    {
        // Switch between the main, left, and right cameras
        // Key 1 swicthes to main (center), 2 switches to left, and 3 switches to right angled camera
        if(Input.GetKeyDown (KeyCode.Alpha1)) switchLeftCamera();
        else if(Input.GetKeyDown (KeyCode.Alpha2)) switchMainCamera();
        else if(Input.GetKeyDown (KeyCode.Alpha3)) switchRightCamera();
    }

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
}
