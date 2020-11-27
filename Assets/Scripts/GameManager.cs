using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [Tooltip("Lose Text")]
    [SerializeField]
    private GameObject loseText;

    [Tooltip("Win Text")]
    [SerializeField]
    private GameObject winText;

    [Tooltip("Left Camera")]
    [SerializeField]
    private GameObject leftCamera;

    [Tooltip("Right Camera")]
    [SerializeField]
    private GameObject rightCamera;

    [Tooltip("Main Camera")]
    [SerializeField]
    private GameObject mainCamera;

    // [Tooltip("Timer Text")]
    // [SerializeField]
    // private Text timerText;


    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
            SetPlugPlayer();
        }

    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    public void LeaveRoom()
    {
        PhotonNetwork.Disconnect();
    }

    void SetPlugPlayer()
    {
        // Photon.Pun.UtilityScripts.CountdownTimer test = new Photon.Pun.UtilityScripts.CountdownTimer();
        // timerText.text = test.Text.text;
        // Debug.Log(timerText.text);

        // Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
        // timerText.text = Photon.Pun.UtilityScripts.CountdownTimer.Text.text;
            
        loseText.SetActive(false);
        winText.SetActive(false);
        leftCamera.SetActive(false);
        rightCamera.SetActive(false);
        mainCamera.SetActive(false);
    }

    void SetObserver()
    {
        leftCamera.SetActive(true);
        rightCamera.SetActive(false);
        mainCamera.SetActive(false);
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        // if (PhotonNetwork.IsMasterClient) {
        //     Photon.Pun.UtilityScripts.CountdownTimer test = new Photon.Pun.UtilityScripts.CountdownTimer();
        //     timerText = test.Text;
        //     Debug.Log(timerText);
        // }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SetPlugPlayer();
        }
    }
}
