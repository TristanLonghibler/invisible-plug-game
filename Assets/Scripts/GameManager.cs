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


    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
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

    }

    public override void OnPlayerLeftRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SetPlugPlayer();
        }
    }
}
