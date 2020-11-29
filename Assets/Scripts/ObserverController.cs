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
    // Start is called before the first frame update
    void Start()
    {
        mainCam.enabled = true;
        leftCam.enabled = false;
        rightCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Switch between the main, left, and right cameras
        // Key 1 swicthes to main (center), 2 switches to left, and 3 switches to right angled camera
        if(Input.GetKeyDown (KeyCode.Alpha1)) switchMainCamera();
        else if(Input.GetKeyDown (KeyCode.Alpha2)) switchLeftCamera();
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
