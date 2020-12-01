using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ResetButton : MonoBehaviourPunCallbacks
{
    public Button resetButton;
    // Start is called before the first frame update
    void Start()
    {
        resetButton.onClick.AddListener(GameManager.Instance.OnRestartButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void OnResetButtonClick() {
    //     resetButton.onClick.AddListener(GameManager.Instance.OnRestartButtonClick);
    // }
}
