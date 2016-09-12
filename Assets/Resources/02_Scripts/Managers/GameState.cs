using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {
    /*Can only be accessed if a script inherits the GameState script*/

    //Handles before game controls
    protected static class PreGame
    {

    }
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
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();

            Time.timeScale = 1;
            gamePaused = false;
            EndGame.playerWon = false;
            //Reloads the scene
            SceneManager.LoadScene(currentLevel.buildIndex);
        }
        /*Load the next Level*/
        public static void NextLevel()
        {
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();
            //Gets the number of current scenes in the build
            int numberOfScenes = SceneManager.sceneCountInBuildSettings;
            //Gets the next level index
            int nextLevel = (currentLevel.buildIndex + 1) % numberOfScenes;
            if(nextLevel < 1)
            {
                nextLevel = 1;
            }
            Debug.Log("Level : " + nextLevel + " will be loaded");

            Time.timeScale = 1;
            gamePaused = EndGame.playerWon = false;
            //Loads the next level
            SceneManager.LoadScene(nextLevel);
        }
        /*Load previous Level*/
        public static void PreviousLevel()
        {
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();
            //Gets the number of current scenes in the build
            int numberOfScenes = SceneManager.sceneCountInBuildSettings;
            //Gets the previous level index
            int previousLevel = (currentLevel.buildIndex - 1) % numberOfScenes;

            print(previousLevel);
            if(previousLevel < 1)
            {
                previousLevel = numberOfScenes - 1;
            }

            Debug.Log("Level : " + previousLevel + "will be loaded");

            Time.timeScale = 1;
            gamePaused = EndGame.playerWon = false;
            //Loads the next level
            SceneManager.LoadScene(previousLevel);
        }

        /*Handles all of the stars*/
        public static class Stars
        {
            //number of stars player currently has
            private static int stars;
            //Key to save and get star rating
            private static string starKey = "StarCount";

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

            //Saves the star rating for the level
            public static void SaveHighScore(int levelNum,int starCount)
            {
                PlayerPrefs.SetInt(levelNum + starKey, starCount);
            }
            //Returns the star rating for the level
            public static int GetHighScore(int levelNum)
            {
                return PlayerPrefs.GetInt(levelNum + starKey);
            }
            //Returns a list of all the highscores
            public static int[] GetAllHighScores()
            {
                //Gets all levels (excluding the main menu)
                int[] allScores = new int[SceneManager.sceneCountInBuildSettings-1];
                //Gets the highscores for each level
                for(int i = 0; i < allScores.Length; i++)
                {
                    allScores[i] = GetHighScore(i+1);
                }
                return allScores;
            }
            //Erases all of the highscores
            public static void EraseAllHighScores()
            {
                //Sets all the highscores to 0
                for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
                {
                    SaveHighScore(i, 0);
                }
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
            //Player can only win once each level
            if (!playerWon)
            {
                playerWon = true;

                //Hide the cutting trail
                GameObject.Find("Trail").SetActive(false);

                //if the players current star count is higher the highscore
                //Save the new score
                SetNewHighScore(InGame.Stars.Get());
            }

        }

        //Handles setting new highscore
        private static void SetNewHighScore(int starCount)
        {
            //Gets the current scene number
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            //Gets the current highscore for the level
            int highScoreStars = InGame.Stars.GetHighScore(currentScene);

            //If the player has gained a new highscore
            if (starCount > highScoreStars)
            {
                print("New HighScore");
                //Then the new highscore will be saved
                InGame.Stars.SaveHighScore(currentScene, starCount);
            }
            else
            {
                print("Failed to get new highscore");
            }
        }

        //Player Loses the Game
        public static void Lose()
        {
            print("Player Lost");
            InGame.RestartLevel();
        }
        //Player moves to main menu
        public static void MainMenu()
        {
            InGame.gamePaused = false;
            playerWon = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
