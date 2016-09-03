using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {
    /*Can only be accessed if a script inherits the GameState script*/

    //Used to handle ingame controls
    protected static class InGame
    {
        //Keeps track of if the game is paused or not
        public static bool gamePaused;

        /*Pause the game*/
        public static void Pause()
        {
            Debug.Log("Game has been Paused");
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                gamePaused = true;
            }
            else
            {
                Time.timeScale = 1;
                gamePaused = false;
            }
        }
        /* Restarts the current level*/
        public static void RestartLevel()
        {
            EndGame.playerWon = false;
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();

            Time.timeScale = 1;
            //Reloads the scene
            SceneManager.LoadScene(currentLevel.buildIndex);
        }
        /*Load the next Level*/
        public static void NextLevel()
        {
            EndGame.playerWon = false;
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();
            //Gets the number of current scenes in the build
            int numberOfScenes = SceneManager.sceneCountInBuildSettings;
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
            EndGame.playerWon = false;
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();
            //Gets the number of current scenes in the build
            int numberOfScenes = SceneManager.sceneCountInBuildSettings;
            //Gets the previous level index
            int previousLevel = (currentLevel.buildIndex - 1) % numberOfScenes;

            print(previousLevel);
            if(previousLevel < 0)
            {
                previousLevel = numberOfScenes - 1;
            }

            Debug.Log("Level : " + previousLevel + "will be loaded");

            Time.timeScale = 1;
            //Loads the next level
            SceneManager.LoadScene(previousLevel);
        }

        /*Handles all of the stars*/
        public static class Stars
        {
            //number of stars player currently has
            private static int stars;
            //When the player collides with the star they get a point
            public static void Add()
            {
                stars += 1;
            }
            //Gets the current number of stars collected
            public static int Get()
            {
                return stars;
            }
            //Resets the star count to 0
            public static void Reset()
            {
                stars = 0;
            }
        }
        
    }
    //Controls end of game controls
    protected static class EndGame
    {
        //Keeps track if the player has won
        public static bool playerWon;

        //Player Wins the Game
        public static void Win()
        {
            print("Player Wins");
            playerWon = true;
        }
        //Player Loses the Game
        public static void Lose()
        {
            print("Player Lost");
            playerWon = false;
            InGame.RestartLevel();
        }
        //Player moves to main menu
        public static void MainMenu()
        {
            //playerWon = false;
            print("Main Menu has not been implemented yet");
        }
    }
}
