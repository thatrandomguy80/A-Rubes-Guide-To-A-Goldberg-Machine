﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCanvasController : GameState {

    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject nextLvlButton;
	public GameObject onScreenPanel;


    void Update()
    {
        //If the game is paused and the player has not won display the pause panel
        pausePanel.SetActive(InGame.gamePaused && !EndGame.playerWon);
        //And Hide the buttons
		onScreenPanel.SetActive(!InGame.gamePaused && !EndGame.playerWon);
        
        if (EndGame.playerWon)
        {
            winPanel.SetActive(EndGame.playerWon);
            SetupStarsForWinGame();
        }

        nextLvlButton.GetComponent<Button>().interactable = true;
        /*
        if (IsThisTheFinalLevelOfWorld())
        {
            print("This is the final level");
            nextLvlButton.GetComponent<Button>().interactable = DoesPlayerHaveEnoughStars();
        }else
        {
            nextLvlButton.GetComponent<Button>().interactable = true;
        }*/
        //If its the final level
        if (IsThisTheFinalLevelOfWorld())
        {
            int world = PreGame.getCurrentWorldAndLevel(SceneManager.GetActiveScene().buildIndex)[0];
            bool enoughStars = DoesPlayerHaveEnoughStars();

            nextLvlButton.GetComponent<Button>().interactable = enoughStars;
           

        }else
        {
            nextLvlButton.GetComponent<Button>().interactable = true;
        }


        if (InGame.gamePaused && !EndGame.playerWon) {
			SetupStarsForPause ();
			DisplayCurrentWorldAndLevel ();
		}

    }

	public Sprite activeStar,disabledStar;


	//Displays the stars the player has collected on the pause screen
	private void SetupStarsForPause(){
		//Gets the total number of stars
		int totalStars = InGame.Stars.Get ();
		//Gets the container
		GameObject starBorder = GameObject.Find ("StarBorder");
		//Gets each star if the player has enough stars then deisplay them
			for (int i = 0; i < starBorder.transform.childCount; i++) {
				Image star = starBorder.transform.GetChild (i).GetComponent<Image> ();
				if (i < totalStars) {
					star.sprite = activeStar;
				} else {
					star.sprite = disabledStar;
				}
			}
	}
    //Set up stars for Win Game
    private void SetupStarsForWinGame()
    {
        //Gets the total number of stars
        int totalStars = InGame.Stars.Get();
        //Gets the container
        GameObject starBorder = GameObject.Find("BorderStarWin");
        //Gets each star if the player has enough stars then deisplay them
        for (int i = 0; i < starBorder.transform.childCount; i++)
        {
            Image star = starBorder.transform.GetChild(i).GetComponent<Image>();
            if (i < totalStars)
            {
                star.sprite = activeStar;
            }
            else
            {
                star.sprite = disabledStar;
            }
        }

    }
    //Displays the current world and level selected on the pause screen
    private void DisplayCurrentWorldAndLevel(){
		int level = SceneManager.GetActiveScene().buildIndex;
		int[] wrldAndLevel = PreGame.getCurrentWorldAndLevel(level);

		Text worldDet = GameObject.Find ("SceneDetails/World").GetComponent<Text> ();
		worldDet.text = "World " + wrldAndLevel [0] + "-" + wrldAndLevel [1];

	}

	//Changes the camera view
    public void ChangeCameraView()
    {
        Camera cam = Camera.main;
        CameraMovement camMove = cam.GetComponent<CameraMovement>();
        camMove.ChangeCameraMode();
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
        InGame.RestartLevel(true);
        
    }
    //Moves to level select
    public void LevelSelect()
    {
		EndGame.LevelSelect();
    }

    /*Checks if player has enough stars*/
    private bool DoesPlayerHaveEnoughStars()
    {
        int totalNumOfStars = InGame.Stars.Total();
        int level = SceneManager.GetActiveScene().buildIndex- (PreGame.nonGameLevels - 1);
        int world = PreGame.getCurrentWorldAndLevel(level)[0];
        int starsNeeded = 0;
        for(int i = 0;i < world; i++)
        {
            starsNeeded += PreGame.starThreshold[i];
        }
        return (totalNumOfStars >= starsNeeded);
    }
    //Checks if the current level is the final level in the world
    private bool IsThisTheFinalLevelOfWorld()
    {
        //Current level
        int level = SceneManager.GetActiveScene().buildIndex-(PreGame.nonGameLevels-1);
        
        int totLvl = 0;
        for(int i = 0;i< PreGame.levelsBetweenWorlds.Length; i++)
        {
            //If the current level is the same as the last level
            if(level == PreGame.levelsBetweenWorlds[i] + totLvl)
            {
                return true;
            }
            totLvl += PreGame.levelsBetweenWorlds[i];
        }
        return false;
    }


    //Player Continues to the next level
    public void NextLevelButton()
    {
        InGame.NextLevel();
    }

    //Mutes or unmutes the music
    public void MuteButton()
    {
        AudioPlayer.instance.Mute();
    }
}
