// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using Photon.Pun;
// using Photon.Realtime;

// public class TimerController : MonoBehaviour
// {
//     [Tooltip("Timer Text")]
//     [SerializeField]
//     private Text timerText;

//     bool startTimer = false;
//     double timerIncrementValue;
//     double startTime;
//     double timer = GetTime();
//     ExitGames.Client.Photon.Hashtable CustomValue;

//     // Start is called before the first frame update
//     void Start()
//     {
//         if (PhotonNetwork.IsMasterClient) {
//             CustomValue = new ExitGames.Client.Photon.Hashtable();
//             startTime = PhotonNetwork.Time;
//             startTimer = true;
//             CustomValue.Add("StartTime", startTime);
//             PhotonNetwork.CurrentRoom.SetCustomProperties(CustomValue);
//         }
//         else {
//             startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
//             startTimer = true;
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (!startTimer) return;

//         timerIncrementValue = PhotonNetwork.Time - startTime;

//         timerText.text = timerIncrementValue.ToString("n0");

//         if (timerIncrementValue >= timer) {
//             GameManager.timerRunning = false;
//         }
//     }

//     public static float GetTime() {
//         // Photon.Pun.UtilityScripts.CountdownTimer timer = timerText.GetComponent<Photon.Pun.UtilityScripts.CountdownTimer>();
//         // PlayerButton CW = CWButton.GetComponent<PlayerButton>();
//         // PlayerButton CCW = CCWButton.GetComponent<PlayerButton>();
//         // UnityEngine.UI.Text timerUI = timerText.GetComponent<UnityEngine.UI.Text>();
//         // // CustomValue = new ExitGames.Client.Photon.Hashtable();
//         // // double startTime = PhotonNetwork.Time;
//         switch (Difficulty.currentDifficulty) {
//             case Difficulty.Difficulties.Easy:
//                 return 60f;
//                 break;
//             case Difficulty.Difficulties.Medium:
//                 return 50f;
//                 break;
//             case Difficulty.Difficulties.Hard:
//                 return 40f;
//                 break;
//             default:
//                 Debug.Log("Default case");
//                 return 60f;
//                 break;
//         }
//     }
// }
