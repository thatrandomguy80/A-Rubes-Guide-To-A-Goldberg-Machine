﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class LevelSelectionButton : GameState {

	public int level;//Level button will load too
    public bool hover;//Is the mouse hovering over the button
    public float lerp;//For linear interpolation

    public LevelManager levelManager;

    void Start()
    {
        lerp = 0;
        hover = false;
		RotatingObject rot = transform.GetChild(1).gameObject.AddComponent<RotatingObject> ();
		rot.Rotation = RotatingObject.RotationTypes.Local;
		rot.rotationSpeeds = new Vector3 (0, 0, 10);

		SetupLevelNum ();


    }

	//Sets up some text to display the level number
	private void SetupLevelNum(){
		GameObject levelNum = new GameObject ();


		levelNum.name = "LevelNum : " + level;
		levelNum.transform.SetParent (transform);
		levelNum.transform.localPosition = new Vector3 (0, 0, 0.1f);
		levelNum.transform.localRotation = new Quaternion (0, 0, 0, 1);
		levelNum.transform.localScale = Vector3.one * 0.8f;


        Sprite levelSprite = Resources.Load<Sprite>("07_Images/LevelNumbers/"+ PreGame.getCurrentWorldAndLevel(level)[1]);

        SpriteRenderer levelNumber = levelNum.AddComponent<SpriteRenderer>();
        levelNumber.sprite = levelSprite;
	}

	//Displays the level stars
    private void DisplayStars()
    {
        GameObject stars = levelManager.levelSelectStars;
        int starHighscore = InGame.Stars.GetHighScore(level);

        for(int i = 0; i < stars.transform.childCount; i++)
        {
			stars.transform.GetChild(i).GetChild(1).GetChild(1).gameObject.SetActive(i < starHighscore);
        }
    }
    //If the player hovers over the button it will move forward
    private void MouseHover()
    {
        
        float speed = Time.deltaTime * 5;
        if (hover)
        {
            DisplayStars();
            //If the mouse is over a GUI element
            //cancel the operation
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                lerp += speed;
            }
        }
        else
        {
            lerp -= speed;
        }
        lerp = Mathf.Clamp01(lerp);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Lerp(0, -0.5f, lerp));
    }
    void Update()
    {
        MouseHover();
    }

    void OnMouseEnter()
    {
        hover = true;
    }
    void OnMouseExit()
    {
        hover = false;
    }

	//On mouse Click Load new level
	void OnMouseDown(){
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            
            LoadLevel(level);
            OpeningController.instance.DestroyCurtains();

        }
	}
    //Select level to load
    private void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
    }


}
