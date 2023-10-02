using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private bool gameIsPaused = false;
    private GameObject pauseMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = this.transform.GetChild(0).gameObject;
        pauseMenu.SetActive(false);     // make sure to turn off the pause menu at start.
    }

    // Update is called once per frame
    void Update()
    {
        // press escape to pause or unpause the game
        if(Input.GetKeyDown(KeyCode.Escape)) PauseGame();
    }

    public void PauseGame() {
        if(!gameIsPaused) {
            // the game is not paused, let's pause.
            gameIsPaused = true;
            // show the pause menu
            pauseMenu.SetActive(true);
            // set timescale to zero
            Time.timeScale = 0;
        }
        else {
            // the game is already paused, let's unpause.
            gameIsPaused = false;
            pauseMenu.SetActive(false);
            // set timescale to one
            Time.timeScale = 1;
        }
    }
}
