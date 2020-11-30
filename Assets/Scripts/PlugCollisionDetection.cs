using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlugCollisionDetection : MonoBehaviourPunCallbacks
{
    public GameObject trigger;
    public GameObject triggerd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Trigger"))
        {
            // GameManager.didWin = true;
            GameManager.Instance.EndGame();
            // winText.text = "You Win!";
            // didWin = true;
            // restartButton.gameObject.SetActive(true); // Show button when game is over
        }
        else if(other.gameObject.CompareTag("Trigger1"))
        {
            // GameManager.didWin = true;
            GameManager.Instance.EndGame();
            // winText.text = "You Win!";
            // didWin = true;
            // restartButton.gameObject.SetActive(true); // Show button when game is over
        }
    }
}
