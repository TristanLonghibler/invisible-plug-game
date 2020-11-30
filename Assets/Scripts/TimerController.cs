using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [Tooltip("Timer Text")]
    [SerializeField]
    private GameObject timerText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.isOfflineMode) {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTimer() {
        Photon.Pun.UtilityScripts.CountdownTimer timer = timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>();
        // PlayerButton CW = CWButton.GetComponent<PlayerButton>();
        // PlayerButton CCW = CCWButton.GetComponent<PlayerButton>();
        UnityEngine.UI.Text timerUI = timerText.GetComponent<UnityEngine.UI.Text>();
        // // CustomValue = new ExitGames.Client.Photon.Hashtable();
        // // double startTime = PhotonNetwork.Time;
        switch (Difficulty.currentDifficulty) {
            case Difficulty.Difficulties.Easy:
                timer.Countdown = 60f;
                timerUI.text = 60f.ToString();
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
    }
}
