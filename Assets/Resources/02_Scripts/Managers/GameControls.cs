using UnityEngine;
using System.Collections;

public class GameControls : GameState
{
    // degbug mode of all scripts
    public bool Debugging = false;
    /**
     * Controls how the game will be played between levels
     **/
     void Start()
    {
        Debugging = false;
    }
    void Update()
    {
        KeyboardControls();//Use Default Controls when navigating around the game
    }
    //Default gameControls
    public void KeyboardControls()
    {
        KeyboardControls(KeyCode.P, KeyCode.R, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.M);
    }

    public void KeyboardControls(KeyCode pause,KeyCode restart, KeyCode left, KeyCode right,KeyCode playtest)
    {
        KeyCode DevKey = KeyCode.F1;
        if (Input.GetKeyDown(pause) && Input.GetKey(DevKey))
        {
            InGame.Pause();
        }
        if (Input.GetKeyDown(restart) && Input.GetKey(DevKey))
        {
            InGame.RestartLevel(false);
        }
        if (Input.GetKeyDown(left) && Input.GetKey(DevKey))
        {
            InGame.PreviousLevel();
        }
        if (Input.GetKeyDown(right) && Input.GetKey(DevKey))
        {
            InGame.NextLevel();
        }
    }

}
