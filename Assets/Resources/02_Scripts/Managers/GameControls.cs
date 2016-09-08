using UnityEngine;
using System.Collections;

public class GameControls : GameState
{
    // degbug mode of all scripts
    public bool Debugging = true;
    /**
     * Controls how the game will be played between levels
     **/
    void Update()
    {
        KeyboardControls();//Use Default Controls when navigating around the game
    }
    //Default gameControls
    public void KeyboardControls()
    {
        KeyboardControls(KeyCode.P, KeyCode.R, KeyCode.LeftArrow, KeyCode.RightArrow);
    }

    public void KeyboardControls(KeyCode pause,KeyCode restart, KeyCode left, KeyCode right)
    {
        if (Input.GetKeyDown(pause))
        {
            InGame.Pause();
        }
        if (Input.GetKeyDown(restart))
        {
            InGame.RestartLevel();
        }
        if (Input.GetKeyDown(left))
        {
            InGame.PreviousLevel();
        }
        if (Input.GetKeyDown(right))
        {
            InGame.NextLevel();
        }
    }

}
