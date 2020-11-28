using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class SetTimer : MonoBehaviourPunCallbacks
{

    [SerializeField] Photon.Pun.UtilityScripts.CountdownTimer timer;
    // Start is called before the first frame update
    void Start()
    {
        Photon.Pun.UtilityScripts.CountdownTimer timer = new Photon.Pun.UtilityScripts.CountdownTimer();
        switch (Difficulty.currentDifficulty) {
            case Difficulty.Difficulties.Easy:
                timer.Countdown = 60f;
                Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
                // timerText.text = timer.ToString();
                // CWButton.SetEnabled(false);
                // CCWButton.SetEnabled(false);
                break;
            case Difficulty.Difficulties.Medium:
                // timer = 50f;
                // timerText.text = timer.ToString();
                break;
            case Difficulty.Difficulties.Hard:
                // timer = 40f;
                // timerText.text = timer.ToString();
                break;
            default:
                Debug.Log("Default case");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
