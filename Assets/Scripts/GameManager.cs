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

    ExitGames.Client.Photon.Hashtable CustomValue;

    [Tooltip("Timer Text")]
    [SerializeField]
    private GameObject timerText;

    [Tooltip("Left Button")]
    [SerializeField]
    private GameObject leftButton;

    [Tooltip("Right Button")]
    [SerializeField]
    private GameObject rightButton;

    [Tooltip("Up Button")]
    [SerializeField]
    private GameObject upButton;

    [Tooltip("Down Button")]
    [SerializeField]
    private GameObject downButton;

    [Tooltip("CWButton")]
    [SerializeField]
    private GameObject CWButton;

    [Tooltip ("CCWButton")]
    [SerializeField]
    private GameObject CCWButton;

    private Player plugPlayer;
    private Player observerPlayer;
    public static GameManager Instance;

    void Start()
    {
        Instance = this;
        Photon.Pun.UtilityScripts.CountdownTimer timer = timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>();
        PlayerButton CW = CWButton.GetComponent<PlayerButton>();
        PlayerButton CCW = CCWButton.GetComponent<PlayerButton>();
        UnityEngine.UI.Text timerUI = timerText.GetComponent<UnityEngine.UI.Text>();
        // CustomValue = new ExitGames.Client.Photon.Hashtable();
        // double startTime = PhotonNetwork.Time;
        switch (Difficulty.currentDifficulty) {
            case Difficulty.Difficulties.Easy:
                timer.Countdown = 60f;
                timerUI.text = 60f.ToString();
                CW.SetEnabled(false);
                CCW.SetEnabled(false);
                break;
            case Difficulty.Difficulties.Medium:
                timer.Countdown = 50f;
                timerUI.text = 50f.ToString();
                break;
            case Difficulty.Difficulties.Hard:
                timer.Countdown = 40f;
                timerUI.text = 40f.ToString();
                break;
            default:
                Debug.Log("Default case");
                break;
        }

        // Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();

        if (PhotonNetwork.IsMasterClient)
        {
            // Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
            SetPlugPlayer();
        }
        else {
            SetObserver();
        }

    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update() {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
            return;
        }


    }

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
        observerPlayer = new PlayerController();
        PlayerButton left = leftButton.GetComponent<PlayerButton>();
        PlayerButton right = rightButton.GetComponent<PlayerButton>();
        PlayerButton up = upButton.GetComponent<PlayerButton>();
        PlayerButton down = downButton.GetComponent<PlayerButton>();
        PlayerButton CW = CWButton.GetComponent<PlayerButton>();
        PlayerButton CCW = CCWButton.GetComponent<PlayerButton>();
        Debug.Log("SetObserver called.");
        leftCamera.SetActive(true);
        rightCamera.SetActive(false);
        mainCamera.SetActive(false);

        left.SetEnabled(false);
        right.SetEnabled(false);
        up.SetEnabled(false);
        down.SetEnabled(false);
        CW.SetEnabled(false);
        CCW.SetEnabled(false);

        Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
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
