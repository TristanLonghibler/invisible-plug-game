using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    public bool didWin = false;
    public static bool inEditor = true;

    [Tooltip("Lose Text")]
    [SerializeField]
    private Text loseText;

    [Tooltip("Win Text")]
    [SerializeField]
    private Text winText;

    [Tooltip("Plug Model")]
    [SerializeField]
    private GameObject plugModel;

    [Tooltip("Timer Text")]
    [SerializeField]
    private GameObject timerText;

    [Tooltip("Reset Button")]
    [SerializeField]
    private Button restartButton;

    public static GameManager Instance;

    void OnCountdownTimerHasExpired() {
        Instance.EndGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        plugModel.transform.position = new Vector3(Random.Range(-39, 70), Random.Range(-43, 17), Random.Range(-40, -8)); // Initialize the plug in random location at start

        //The timer used in the game uses a script that is part of Photon. From the project root directory, the path to the script is Assets/Photon/PhotonUnityNetworking/UtilityScripts/Room/CountdownTimer.cs
        Photon.Pun.UtilityScripts.CountdownTimer timer = timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>();
        UnityEngine.UI.Text timerUI = timerText.GetComponent<UnityEngine.UI.Text>();

        float time = GetTime();

        timer.Countdown = time; //Sets the timer object's countdown, which is the time it starts counting down from. 
        timerUI.text = time.ToString(); //Sets the text in the UI element for the timer to the starting time.

        //Assigns the function for resetting the game to the reset button, and hides the reset button from view at the start of the game.
        restartButton.onClick.AddListener(OnRestartButtonClick);
        restartButton.gameObject.SetActive(false);

        if (inEditor) {
            PhotonNetwork.OfflineMode = true; //This is strictly for development and testing purposes.
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

    //Function that gets the time for the timer. The switch statement ultimately does nothing currently because difficulty selection was not
    //fully implemented. Unless the difficulty variable is changed in Difficulty.cs, the difficulty is always Medium.
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
        plugModel.GetComponent<ObserverController>().enabled = false; //Disables the observer controls for the plug player.
        
        Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerHasExpired; //Anytime this line of code appears, it is assigning the function OnCountdownTimerHasExpired to be called when the timer ends.
    }

    //Function that sets up the observer's UI and control scripts.
    void SetObserver()
    {
        plugModel.GetComponent<PlugPlayerController>().enabled = false; //Disables the plug player controls for the observer.

        Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerHasExpired;
        Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime(); //This starts the countdown timer.
    }

    //Function that ends the game.
    //Known Issue: If the plug is plugged in just as the timer runs out, there is a chance that the win text and the loss text will both be enabled.
    [PunRPC]
    public void EndGame() {
        if(didWin) {
            winText.enabled = true; //Displays the win text.
            Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerHasExpired; //Makes it so that nothing happens when the countdown timer ends.
            timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>().enabled = false; //Makes the timer stop, at least on the UI.
        }
        else if(!didWin) {
            loseText.enabled = true; //Displays the lose text.
        }

        //Disables the script and buttons for the plug player when the game ends.
        plugModel.GetComponent<PlugPlayerController>().disableButtons();
        plugModel.GetComponent<PlugPlayerController>().enabled = false;

        //Disables the script and buttons for the observer when the game ends.
        plugModel.GetComponent<ObserverController>().disableButtons();
        plugModel.GetComponent<ObserverController>().enabled = false;

        restartButton.gameObject.SetActive(true); //Displays the restart button.
    }

    //This function is not run by the MasterClient, only other clients. (In other words, only the observer.)
    //It mirrors a lot of the functionality that exists in the Start function since only the MasterClient reloads the scene.
    public void ResetGame() {
        winText.enabled = false;
        loseText.enabled = false;
        didWin = false; //Resets didWin bool to false, because if it is not reset, the win text is displayed when the timer runs out after a reset.

        Photon.Pun.UtilityScripts.CountdownTimer timer = timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>();
        UnityEngine.UI.Text timerUI = timerText.GetComponent<UnityEngine.UI.Text>();

        float time = GetTime();

        timer.Countdown = time;
        timerUI.text = time.ToString();

        restartButton.gameObject.SetActive(false);

        //The observer script and buttons need to be explicity re-enable by the observer because the Plug Player (who is the MasterClient) reloads the scene, and gets its scripts re-enabled that way.
        plugModel.GetComponent<ObserverController>().enabled = true;
        plugModel.GetComponent<ObserverController>().enableButtons();

        timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>().enabled = true; //Re-enables the timer.
        Photon.Pun.UtilityScripts.CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerHasExpired; //Re-enables the function that executes when the timer runs out.
        Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime(); //Starts the timer again.
    }

    [PunRPC]
    public void OnRestartButtonClick() {
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel("Game"); //Restart the game for the MasterClient.
        }
        else {
            ResetGame(); //Reset the game for the observer.
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
