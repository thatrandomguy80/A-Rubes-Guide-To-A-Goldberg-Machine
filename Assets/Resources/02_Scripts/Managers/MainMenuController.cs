using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : GameState {

    //Quits the Game
	public void QuitGame()
    {
        Application.Quit();
    }

    //Loads the first level
    /*Note: Throwaway Code*/
    public void StartGame()
    {
        //Temp Function should change later
        SceneManager.LoadScene(1);
        OpeningController.instance.OpenCurtain();
    }
    //Load to Options
    public void Options()
    {
        SceneManager.LoadScene(2);
    }

    void Start()
    {
        int[] highscores = InGame.Stars.GetAllHighScores();
        for(int i = 0; i < highscores.Length; i++)
        {
            print((i+1) + " " + highscores[i]);
        }
    }

}
