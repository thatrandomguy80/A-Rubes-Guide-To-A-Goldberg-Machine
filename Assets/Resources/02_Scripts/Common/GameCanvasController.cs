using UnityEngine;
using System.Collections;

public class GameCanvasController : GameState {

    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject winPanel;
    public GameObject restartButton;




    void Update()
    {
        //If the game is paused and the player has not won display the pause panel
        pausePanel.SetActive(InGame.gamePaused && !EndGame.playerWon);
        //And Hide the pause button
        pauseButton.SetActive(!InGame.gamePaused && !EndGame.playerWon);
        restartButton.SetActive(!InGame.gamePaused && !EndGame.playerWon);

        winPanel.SetActive(EndGame.playerWon);

    }

    //Pauses the game and brings up the pause game GUI
	public void PauseButton()
    {
        InGame.Pause();
    }
    //Restarts the level
    public void RestartButton()
    {
        //Un-pauses the game
        InGame.gamePaused = false;
        //Restarts the game
        InGame.RestartLevel();
        
    }
    //Moves to main menu
    public void MainMenu()
    {
        EndGame.MainMenu();
    }

    //Player Continues to the next level
    public void NextLevelButton()
    {
        InGame.NextLevel();
    }
}
