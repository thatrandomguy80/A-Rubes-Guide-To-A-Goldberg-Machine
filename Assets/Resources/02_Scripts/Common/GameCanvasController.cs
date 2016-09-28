using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

		SetupStars ();
		DisplayCurrentWorldAndLevel ();

    }

	//Sets up what stars are showing
	private void SetupStars(){
		int totalStars = InGame.Stars.Get ();
		GameObject starBorder = GameObject.Find ("StarBorder");
		for (int i = 0; i < starBorder.transform.childCount; i++) {
			Image star = starBorder.transform.GetChild (i).GetComponent<Image> ();
			if (i < totalStars) {
				star.color = Color.yellow;
			} else {
				star.color = Color.white;
			}
		}
	}
	private void DisplayCurrentWorldAndLevel(){
		int level = SceneManager.GetActiveScene().buildIndex-1;
		int[] wrldAndLevel = PreGame.getCurrentWorldAndLevel(level);
		print ("World : " + wrldAndLevel[0] + " // Level : " + wrldAndLevel[1]);

		Text worldDet = GameObject.Find ("SceneDetails/World").GetComponent<Text> ();
		worldDet.text = "World " + wrldAndLevel [0] + "-" + wrldAndLevel [1];

	}


    //Pauses the game and brings up the pause game GUI
	public void PauseButton()
    {
        InGame.Pause();
    }
    //Restarts the level
    public void RestartButton()
    {
        //Restarts the game
        InGame.RestartLevel();
        
    }
    //Moves to level select
    public void LevelSelect()
    {
		EndGame.LevelSelect();
    }

    //Player Continues to the next level
    public void NextLevelButton()
    {
        InGame.NextLevel();
    }
}
