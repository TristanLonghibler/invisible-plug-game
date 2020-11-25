using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Starts game if player hits button
    public void onStartButtonPress()
    {
        Difficulty.currentDifficulty = Difficulty.Difficulties.Hard; //This line is for testing purposes only. In the real implementation, we would have different buttons to click, and clicking those buttons is what would set the difficulty.
        SceneManager.LoadScene("SampleScene"); // Start the game
    }
}
