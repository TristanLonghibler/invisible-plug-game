using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    public static bool isOfflineMode = true;
    public static bool timerRunning = true;
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

    // [Tooltip("Left Camera")]
    // [SerializeField]
    // private GameObject leftCamera;

    // [Tooltip("Right Camera")]
    // [SerializeField]
    // private GameObject rightCamera;

    // [Tooltip("Main Camera")]
    // [SerializeField]
    // private GameObject mainCamera;

    // ExitGames.Client.Photon.Hashtable CustomValue;

    [Tooltip("Timer Text")]
    [SerializeField]
    private GameObject timerText;

    // [Tooltip("Left Button")]
    // [SerializeField]
    // private GameObject leftButton;

    // [Tooltip("Right Button")]
    // [SerializeField]
    // private GameObject rightButton;

    // [Tooltip("Up Button")]
    // [SerializeField]
    // private GameObject upButton;

    // [Tooltip("Down Button")]
    // [SerializeField]
    // private GameObject downButton;

    // [Tooltip("CWButton")]
    // [SerializeField]
    // private GameObject CWButton;

    // [Tooltip ("CCWButton")]
    // [SerializeField]
    // private GameObject CCWButton;

    private Player plugPlayer;
    private Player observerPlayer;
    public static GameManager Instance;

    void OnCountdownTimerHasExpired() {
        loseText.text = "You lose.";
    }

    // Start is called before the first frame update
    void Start()
    {
        // Instance = this;
        plugModel.transform.position = new Vector3(Random.Range(-39, 70), Random.Range(-43, 17), Random.Range(-40, -8)); // Initialize the plug in random location at start
        
        // Photon.Pun.UtilityScripts.CountdownTimer timer = timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>();
        // PlayerButton CW = CWButton.GetComponent<PlayerButton>();
        // PlayerButton CCW = CCWButton.GetComponent<PlayerButton>();
        // UnityEngine.UI.Text timerUI = timerText.GetComponent<UnityEngine.UI.Text>();
        // // CustomValue = new ExitGames.Client.Photon.Hashtable();
        // // double startTime = PhotonNetwork.Time;
        // switch (Difficulty.currentDifficulty) {
        //     case Difficulty.Difficulties.Easy:
        //         timer.Countdown = 60f;
        //         timerUI.text = 60f.ToString();
        //         break;
        //     case Difficulty.Difficulties.Medium:
        //         timer.Countdown = 50f;
        //         timerUI.text = 50f.ToString();
        //         break;
        //     case Difficulty.Difficulties.Hard:
        //         timer.Countdown = 40f;
        //         timerUI.text = 40f.ToString();
        //         break;
        //     default:
        //         Debug.Log("Default case");
        //         break;
        // }

        Photon.Pun.UtilityScripts.CountdownTimer timer = timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>();
        UnityEngine.UI.Text timerUI = timerText.GetComponent<UnityEngine.UI.Text>();

        float time = GetTime();

        timer.Countdown = time;
        timerUI.text = time.ToString();

        Debug.Log("Offline Mode: " + isOfflineMode);
        if (isOfflineMode) {
            PhotonNetwork.OfflineMode = true;
            // Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
        }
        else if (PhotonNetwork.IsMasterClient)
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
        // Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerHasExpired;
        if (!plugPlayerConnected || !observerConnected) {
            return;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Trigger"))
        {
            winText.text = "You Win!";
            // didWin = true;
            // restartButton.gameObject.SetActive(true); // Show button when game is over
        }
        else if(other.gameObject.CompareTag("Trigger1"))
        {
            winText.text = "You Win!";
            // didWin = true;
            // restartButton.gameObject.SetActive(true); // Show button when game is over
        }
    }


    public float GetTime() {
        // Photon.Pun.UtilityScripts.CountdownTimer timer = timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>();
        // PlayerButton CW = CWButton.GetComponent<PlayerButton>();
        // PlayerButton CCW = CCWButton.GetComponent<PlayerButton>();
        // UnityEngine.UI.Text timerUI = timerText.GetComponent<UnityEngine.UI.Text>();
        // // CustomValue = new ExitGames.Client.Photon.Hashtable();
        // // double startTime = PhotonNetwork.Time;
        switch (Difficulty.currentDifficulty) {
            case Difficulty.Difficulties.Easy:
                return 60f;
                break;
            case Difficulty.Difficulties.Medium:
                return 50f;
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
        //These next two lines are for debugging the timer, we do NOT want these here in the final build.
        // Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerHasExpired;
        // Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
        plugModel.GetComponent<ObserverController>().enabled = false;
        
        // Photon.Pun.UtilityScripts.CountdownTimer test = new Photon.Pun.UtilityScripts.CountdownTimer();
        // timerText.text = test.Text.text;
        // Debug.Log(timerText.text);

        // Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
        // timerText.text = Photon.Pun.UtilityScripts.CountdownTimer.Text.text;
        
        // loseText.SetActive(false);
        // winText.SetActive(false);
        // leftCamera.SetActive(false);
        // rightCamera.SetActive(false);
        // mainCamera.SetActive(false);
    }

    void SetObserver()
    {
        observerConnected = true;
        plugModel.GetComponent<PlugPlayerController>().enabled = false;
        // observerPlayer = new PlayerController();
        // PlayerButton left = leftButton.GetComponent<PlayerButton>();
        // PlayerButton right = rightButton.GetComponent<PlayerButton>();
        // PlayerButton up = upButton.GetComponent<PlayerButton>();
        // PlayerButton down = downButton.GetComponent<PlayerButton>();
        // PlayerButton CW = CWButton.GetComponent<PlayerButton>();
        // PlayerButton CCW = CCWButton.GetComponent<PlayerButton>();
        // Debug.Log("SetObserver called.");
        // leftCamera.SetActive(true);
        // rightCamera.SetActive(false);
        // mainCamera.SetActive(false);

        // left.SetEnabled(false);
        // right.SetEnabled(false);
        // up.SetEnabled(false);
        // down.SetEnabled(false);
        // CW.SetEnabled(false);
        // CCW.SetEnabled(false);

        Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerHasExpired;
        Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
    }



    public void StartGame() {
        
    }

    // public void gameOver()
    // {
    //     loseText.text.text = "You Lose";
    //     trigger.SetActive(false);
    //     triggerd.SetActive(false);
    //     restartButton.gameObject.SetActive(true); // Show button when game is over
    // }

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
