using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {
    /*Can only be accessed if a script inherits the GameState script*/

    //Handles before game controls
    protected static class PreGame {
        public readonly static int nonGameLevels = 2;
        public readonly static int[] starThreshold = { 12, 22, 40, 80 };
        public static int[] levelsBetweenWorlds = { 12, 5, 1, 1 };

        private readonly static string levelKey = "levelKey";
        //Keeps track of what level the player is up to
        public static int getCurrentLevel() {
            int currLevel = PlayerPrefs.GetInt(levelKey);
            //If the player hasn't beaten anything yet unlock
            //the first level
            if (currLevel < nonGameLevels) {
                currLevel = nonGameLevels;
            }
            return currLevel;
        }
        public static int[] getCurrentWorldAndLevel(int CURRENT_LEVEL) {
            int level = CURRENT_LEVEL - 1;
            int[] wrld = new int[2];


            for (int i = 0; i < levelsBetweenWorlds.Length; i++) {
                if (level <= levelsBetweenWorlds[i]) {
                    wrld[0] = i + 1;
                    break;
                } else {
                    level -= levelsBetweenWorlds[i];
                }
            }
            wrld[1] = level;
            return wrld;
        }

        //Sets the next level cap
        public static void setNextLevel() {
            int highestLevel = getCurrentLevel();
            int sceneNun = SceneManager.GetActiveScene().buildIndex;
            if (sceneNun == highestLevel) {
                PlayerPrefs.SetInt(levelKey, highestLevel + 1);
            }
        }
    }
    //Used to handle ingame controls
    protected static class InGame {
        //Keeps track of if the game is paused or not
        public static bool gamePaused;
        /*Pause the game*/
        public static void Pause() {
            Debug.Log("Game has been Paused");
            if (Time.timeScale == 1) {
                Time.timeScale = 0;
                gamePaused = true;
            } else {
                Time.timeScale = 1;
                gamePaused = false;
            }
        }
        /* Restarts the current level*/
        public static void RestartLevel(bool lost) {
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();

            Time.timeScale = 1;
            gamePaused = false;
            EndGame.playerWon = false;
            if (lost) {
                PlayTestMetrics.numOfRestarts++;
                PlayTestMetrics.saveTime("Lost");
            } else {
                PlayTestMetrics.numOfWins++;
                PlayTestMetrics.saveTime("Won but restarted");
            }

            //Reloads the scene
            SceneManager.LoadScene(currentLevel.buildIndex);
        }
        /*Load the next Level*/
        public static void NextLevel() {
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();
            //Gets the number of current scenes in the build
            int numberOfScenes = SceneManager.sceneCountInBuildSettings;
            //Gets the next level index
            int nextLevel = (currentLevel.buildIndex + 1) % numberOfScenes;


            Time.timeScale = 1;
            gamePaused = EndGame.playerWon = false;
            PlayTestMetrics.outputMetrics();
            PlayTestMetrics.reset(SceneManager.GetSceneAt(nextLevel).name);
            //Loads the next level
            SceneManager.LoadScene(nextLevel);
        }
        /*Load previous Level*/
        public static void PreviousLevel() {
            //Gets the currently loaded scene
            Scene currentLevel = SceneManager.GetActiveScene();
            //Gets the number of current scenes in the build
            int numberOfScenes = SceneManager.sceneCountInBuildSettings;
            //Gets the previous level index
            int previousLevel = (currentLevel.buildIndex - 1) % numberOfScenes;

            print(previousLevel);
            if (previousLevel < PreGame.nonGameLevels) {
                previousLevel = numberOfScenes - 1;
            }

            Debug.Log("Level : " + previousLevel + "will be loaded");

            Time.timeScale = 1;
            gamePaused = EndGame.playerWon = false;
            PlayTestMetrics.outputMetrics();
            PlayTestMetrics.reset(SceneManager.GetSceneAt(previousLevel).name);
            //Loads the next level
            SceneManager.LoadScene(previousLevel);
        }

        /*Handles all of the stars*/
        public static class Stars {
            //number of stars player currently has
            private static int stars;
            //Key to save and get star rating
            private static string starKey = "StarCount";

            //When the player collides with the star they get a point
            public static void Add() {
                stars += 1;
            }
            //Gets the current number of stars collected
            public static int Get() {
                return stars;
            }
            //Resets the star count to 0
            public static void Reset() {
                stars = 0;
            }
            //Gets the number of all stars collected
            public static int Total() {
                int[] stars = GetAllHighScores();
                int total = 0;

                for (int i = 0; i < stars.Length; i++) {
                    total += stars[i];
                }
                return total;
            }
            //Saves the star rating for the level
            public static void SaveHighScore(int levelNum, int starCount) {
                PlayerPrefs.SetInt(levelNum + starKey, starCount);
            }
            //Returns the star rating for the level
            public static int GetHighScore(int levelNum) {
                return PlayerPrefs.GetInt(levelNum + starKey);
            }
            //Returns a list of all the highscores
            public static int[] GetAllHighScores() {
                //Gets all levels (excluding the main menu)
                int[] allScores = new int[SceneManager.sceneCountInBuildSettings - 1];
                //Gets the highscores for each level
                for (int i = 0; i < allScores.Length; i++) {
                    allScores[i] = GetHighScore(i + 1);
                }
                return allScores;
            }
            //Erases all of the highscores
            public static void EraseAllHighScores() {
                //Sets all the highscores to 0
                for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
                    SaveHighScore(i, 0);
                }
            }

        }

    }
    //Controls end of game controls
    protected static class EndGame {
        //Keeps track if the player has won
        public static bool playerWon;

        //Player Wins the Game
        public static void Win() {
            //Player can only win once each level
            if (!playerWon) {
                playerWon = true;

                //If the highest level move it up one
                PreGame.setNextLevel();

                //Hide the cutting trail
                GameObject.Find("Trail").SetActive(false);

                //if the players current star count is higher the highscore
                //Save the new score
                SetNewHighScore(InGame.Stars.Get());
            }

        }

        //Handles setting new highscore
        private static void SetNewHighScore(int starCount) {
            //Gets the current scene number
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            //Gets the current highscore for the level
            int highScoreStars = InGame.Stars.GetHighScore(currentScene);

            //If the player has gained a new highscore
            if (starCount > highScoreStars) {
                print("New HighScore");
                //Then the new highscore will be saved
                InGame.Stars.SaveHighScore(currentScene, starCount);
            } else {
                print("Failed to get new highscore");
            }
        }

        //Player Loses the Game
        public static void Lose() {
            print("Player Lost");
            InGame.RestartLevel(true);
        }
        //Player moves to levelselect
        public static void LevelSelect() {
            InGame.gamePaused = false;
            playerWon = false;
            Time.timeScale = 1;
            PlayTestMetrics.saveTime("Exit to level select");
            PlayTestMetrics.reset(SceneManager.GetSceneAt(1).name);
            SceneManager.LoadScene(1);
        }

        //Move player to main menu
        public static void MainMenu() {
            InGame.gamePaused = false;
            playerWon = false;
            Time.timeScale = 1;
            PlayTestMetrics.saveTime("Exit to menu");
            PlayTestMetrics.reset(SceneManager.GetSceneAt(0).name);
            SceneManager.LoadScene(0);
        }
    }

    public class Attempt {
        public float timeTaken;
        public string result;
        public Attempt(float time, string resultIN) {
            result = resultIN;
            timeTaken = time;
        }
        public string print() {
            return "Time taken: " + timeTaken.ToString() + " || Reason terminated: " + result;
        }
    }

    public static class PlayTestMetrics {
        public static int numOfRestarts;
        public static int numOfWins;
        public static int starsGathered;
        public static string levelName;
        public static List<Attempt> timeTaken = new List<Attempt>();//this is per attempt

        private static float startTime;

        public static void saveTime(string result) {
            timeTaken.Add(new Attempt(Time.time - startTime, result));
            startTime = Time.time;
        }

        public static void outputMetrics() {
            //write user name,timestamp,and all above vars to file.
            starsGathered = InGame.Stars.Get();
            string dataPath = Application.dataPath + "/Playtest Metrics";
            string pathName = dataPath + "/Report.txt";
            string Text = "";
            Text += "\n\n\n";
            Text += "Level: " + levelName;
            Text += "\nWins: " + numOfWins + " || Loses: " + numOfRestarts;
            Text += "\nOverall stars: " + starsGathered;
            Text += "\nAttempts\n";
            try {
                File.AppendAllText(pathName, Text);

                //iterate over attempts and print;
                foreach (Attempt a in timeTaken) {
                    File.AppendAllText(pathName, a.print() + "\n");
                }
            } catch { Debug.LogError("Write Playtest File Failed"); }
            timeTaken = new List<Attempt>();
        }

        public static void reset(string mapName) {
            startTime = Time.time;
            levelName = mapName;
            numOfRestarts = 0;
            numOfWins = 0;
            starsGathered = 0;
        }
    }
}
