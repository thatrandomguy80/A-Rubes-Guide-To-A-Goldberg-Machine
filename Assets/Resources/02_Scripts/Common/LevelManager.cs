﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelManager : GameState {

    //Keeps track of all the worlds in the game
    private GameObject[] worlds;
    private int worldSelected;

	public GameObject leftArrow, rightArrow;

    private int latestlevel;//Keeps track of what level the player is up to
    private int buttonsPlaced;//Keeps track of how many buttons are placed

    private int completedLevels;
    // Use this for initialization
    void Start () {
        //Set up initial conditions
        Init();

        //Sets the camera to look at the the current world.
        SetUpCamStartingPos();
    }

    private void Init()
    {
        buttonsPlaced = 0;
        latestlevel = PreGame.getCurrentLevel() - 1;
        print("Latest Level : " + latestlevel + " - Number of Stars : " + InGame.Stars.Total());
        int numberOfWorlds = transform.childCount;
        worlds = new GameObject[numberOfWorlds];
        PreGame.levelsBetweenWorlds = new int[worlds.Length];
        for (int i = 0; i < numberOfWorlds; i++)
        {
            worlds[i] = transform.GetChild(i).gameObject;
            ActivateButtons(worlds[i].transform.GetChild(0).gameObject);
            ActivateButtons(worlds[i].transform.GetChild(1).gameObject);

            PreGame.levelsBetweenWorlds[i] = worlds[i].transform.GetChild(0).childCount + worlds[i].transform.GetChild(1).childCount;
        }
    }

    /*Sets up which world the camera is looking at*/
    private void SetUpCamStartingPos()
    {
        LevelSelectionButton[] levelsButts = FindObjectsOfType(typeof(LevelSelectionButton)) as LevelSelectionButton[];
        completedLevels = levelsButts.Length;

        int n = 0;
        for (int i = 0; i < worlds.Length; i++)
        {
            if (completedLevels < n + PreGame.levelsBetweenWorlds[i])
            {
                worldSelected = i;
                break;
            }
            else
            {
                n += PreGame.levelsBetweenWorlds[i];
            }
        }
        Camera.main.transform.position = new Vector3(worlds[worldSelected].transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    //Handles setting up which buttons are interactable for a side
    private void ActivateButtons(GameObject levelSide)
    {
        int currentButton = 0;
        while (currentButton < levelSide.transform.childCount)
        {
            GameObject button = levelSide.transform.GetChild(currentButton).gameObject;
            Material mat = button.GetComponent<Renderer>().material;
            if (latestlevel > 0)
            {
                mat.color = Color.red;
                LevelSelectionButton lsBut = button.AddComponent<LevelSelectionButton>();
                lsBut.level = currentButton + PreGame.nonGameLevels + buttonsPlaced;
                latestlevel--;
            }
            else
            {
                mat.color = Color.blue;
            }
            currentButton++;
        }
        buttonsPlaced += levelSide.transform.childCount;
    }

    void Update () {
        CameraMovement();
        DisableArrows(completedLevels);
        //Change the text on the right arrow
        changeText();
    }

    //Handles camera movement
    private void CameraMovement()
    {
        //Moves the camera to the world selected
        float speed = Time.deltaTime * 2;
        Camera cam = Camera.main;
        float newXPos = Mathf.Lerp(cam.transform.position.x, worlds[worldSelected].transform.position.x, speed);
        cam.transform.position = new Vector3(newXPos, cam.transform.position.y, cam.transform.position.z);
    }

    //Handles disabling the GUI arrows
	void DisableArrows(int totalNumberOfLevels){
		Button leftBut = leftArrow.GetComponent<Button> ();
		Button rightBut = rightArrow.GetComponent<Button> ();

        //The left arrow is disables if the player is on the first world
		if (worldSelected == 0) {
			leftBut.interactable = false;
		} else {
			leftBut.interactable = true;
		}
        //The right arrow is disabled if the player is on the last world
        if (worldSelected == worlds.Length - 1) {
            rightBut.interactable = false;
        } else {
            //Gets the total number of levels for the current world and the
            //previous worlds
            int totLevels = 0;
            for(int i = 0; i <= worldSelected; i++)
            {
                totLevels += PreGame.levelsBetweenWorlds[i];
            }

            //If the total number of levels completed are greater than the current world and
            //previous world levels then the player can progess
            if (totalNumberOfLevels > totLevels)
            {
                rightBut.interactable = true;
            }
            else
            {
                rightBut.interactable = false;
            }
		}
	}

    //Selects the next world
    public void NextWorld()
    {
        if (worldSelected < worlds.Length-1)
        {
            if (InGame.Stars.Total() >= PreGame.starThreshold[worldSelected])
            {
                worldSelected++;
            }
        }
    }
    //Selects the previous world
    public void PreviousWorld()
    {
        if (worldSelected > 0)
        {
            worldSelected--;
        }
    }
    public void changeText()
    {
		Text txt = rightArrow.transform.GetChild(0).GetComponent<Text>();
		if (InGame.Stars.Total() < PreGame.starThreshold[worldSelected])
        {
			txt.text = PreGame.starThreshold[worldSelected].ToString();
        }else
        {
            txt.text = "";
        }

    }
}
