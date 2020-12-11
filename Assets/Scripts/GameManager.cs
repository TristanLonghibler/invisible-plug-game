using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    public static bool isOfflineMode = true; //This is mainly for development purposes. This stays true if the Game scene is directly loaded in the Unity editor, and allows one player to have both the plug and observer controls at once.
    public bool didWin = false;
    private bool plugPlayerConnected = false;
    private bool observerConnected = false;

    [Tooltip("Lose Text")]
    [SerializeField]
    private Text loseText;

    [Tooltip("Win Text")]
    [SerializeField]
    private Text winText;

    [Tooltip("Plug Model")]
    [SerializeField]
    private GameObject plugModel;

    [Tooltip("Trigger")]
    [SerializeField]
    public GameObject trigger;

    [Tooltip("Trigger1")]
    [SerializeField]
    public GameObject triggerd;

    [Tooltip("Timer Text")]
    [SerializeField]
    private GameObject timerText;

    [Tooltip("Reset Button")]
    [SerializeField]
    private Button restartButton;

    private Player plugPlayer;
    private Player observerPlayer;
    public static GameManager Instance;

    void OnCountdownTimerHasExpired() {
        Instance.EndGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        plugModel.transform.position = new Vector3(Random.Range(-39, 70), Random.Range(-43, 17), Random.Range(-40, -8)); // Initialize the plug in random location at start

        Photon.Pun.UtilityScripts.CountdownTimer timer = timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>();
        UnityEngine.UI.Text timerUI = timerText.GetComponent<UnityEngine.UI.Text>();

        float time = GetTime();

        timer.Countdown = time;
        timerUI.text = time.ToString();

        restartButton.onClick.AddListener(OnRestartButtonClick);
        restartButton.gameObject.SetActive(false);

        Debug.Log("Offline Mode: " + isOfflineMode);
        if (isOfflineMode) {
            PhotonNetwork.OfflineMode = true;
            plugPlayerConnected = true;
            observerConnected = true;
        }
        else if (PhotonNetwork.IsMasterClient)
        {
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

    public float GetTime() {
        switch (Difficulty.currentDifficulty) {
            case Difficulty.Difficulties.Easy:
                return 60f;
                break;
            case Difficulty.Difficulties.Medium:
                return 120f;
                break;
            case Difficulty.Difficulties.Hard:
                return 40f;
                break;
            default:
                Debug.Log("Default case");
                return 60f;
                break;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.Disconnect();
    }

    void SetPlugPlayer()
    {
        plugPlayerConnected = true;
        plugModel.GetComponent<ObserverController>().enabled = false;
        
        Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerHasExpired;
    }

    void SetObserver()
    {
        observerConnected = true;
        plugModel.GetComponent<PlugPlayerController>().enabled = false;

        Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerHasExpired;
        Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
    }

    [PunRPC]
    public void EndGame() {
        if(didWin) {
            winText.enabled = true;
            Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerHasExpired;
            timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>().enabled = false;
        }
        else if(!didWin) {
            loseText.enabled = true;
        }

        plugModel.GetComponent<PlugPlayerController>().disableButtons();
        plugModel.GetComponent<PlugPlayerController>().enabled = false;

        plugModel.GetComponent<ObserverController>().disableButtons();
        plugModel.GetComponent<ObserverController>().enabled = false;
        restartButton.gameObject.SetActive(true);
    }

    //This function is not run by the MasterClient, only other clients. (In other words, only the observer.)
    public void ResetGame() {
        winText.enabled = false;
        loseText.enabled = false;

        Photon.Pun.UtilityScripts.CountdownTimer timer = timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>();
        UnityEngine.UI.Text timerUI = timerText.GetComponent<UnityEngine.UI.Text>();

        float time = GetTime();

        timer.Countdown = time;
        timerUI.text = time.ToString();

        restartButton.gameObject.SetActive(false);

        //These need to be explicity reenable by the observer because the Plug Player (who is the MasterClient) reloads the scene, and gets its scripts reenabled that way.
        plugModel.GetComponent<ObserverController>().enabled = true;
        plugModel.GetComponent<ObserverController>().enableButtons();

        timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>().enabled = true;
        Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerHasExpired;
        Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
    }

    [PunRPC]
    public void OnRestartButtonClick() {
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel("Game"); // Restart the game
        }
        else {
            ResetGame();
        }
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
