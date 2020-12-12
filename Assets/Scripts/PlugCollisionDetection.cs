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
            GameManager.Instance.didWin = true;
            GameManager.Instance.EndGame();
        }
        else if(other.gameObject.CompareTag("Trigger1"))
        {
            GameManager.Instance.didWin = true;
            GameManager.Instance.EndGame();
        }
    }
}
