using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    //Used to handle ingame controls
    //Can only be accessed if a script inherits the GameState script
    protected static class InGame
    {
        /*Pause the game*/
        public static void Pause()
        {
            Debug.Log("Game has been Paused");
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        /* Restarts the current level*/
        public static void RestartLevel()
        {
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();

            Time.timeScale = 1;
            //Reloads the scene
            SceneManager.LoadScene(currentLevel.buildIndex);
        }
        /*Load the next Level*/
        public static void NextLevel()
        {
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();
            //Gets the number of current scenes in the build
            int numberOfScenes = SceneManager.sceneCount + 1;
            //Gets the next level index
            int nextLevel = (currentLevel.buildIndex + 1) % numberOfScenes;

            Debug.Log("Level : " + nextLevel + " will be loaded");

            Time.timeScale = 1;
            //Loads the next level
            SceneManager.LoadScene(nextLevel);
        }
        /*Load previous Level*/
        public static void PreviousLevel()
        {
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();
            //Gets the number of current scenes in the build
            int numberOfScenes = SceneManager.sceneCount + 1;
            //Gets the previous level index
            int previousLevel = (currentLevel.buildIndex - 1) % numberOfScenes;

            if (previousLevel < 0)
            {
                previousLevel = SceneManager.sceneCount;
            }//FIXS WEIRD BUG
            print(previousLevel);

            Debug.Log("Level : " + previousLevel + "will be loaded");

            Time.timeScale = 1;
            //Loads the next level
            SceneManager.LoadScene(previousLevel);
        }
    }
    //Controls end of game controls
    //Like winning and loseing
    protected static class EndGame
    {
        //Player Wins the Game
        public static void Win()
        {
            print("Player Wins");
            InGame.NextLevel();
        }
        public static void Lose()
        {
            print("Player Lost");
            InGame.RestartLevel();
        }
    }
}
