using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {
    /*Can only be accessed if a script inherits the GameState script*/

    //Handles before game controls
    protected static class PreGame
    {
        public readonly static int nonGameLevels = 2;
		public readonly static int[] starThreshold = { 12, 30, 50 , 80};
		public static int[] levelsBetweenWorlds = {10,12,12,20};

		private readonly static string levelKey = "levelKey";
		//Keeps track of what level the player is up to
		public static int getCurrentLevel(){
			int currLevel = PlayerPrefs.GetInt (levelKey);
			//If the player hasn't beaten anything yet unlock
			//the first level
			if (currLevel < nonGameLevels) {
				currLevel = nonGameLevels;
			}
			return currLevel;
		}
		public static int[] getCurrentWorldAndLevel(int CURRENT_LEVEL){
			int level = CURRENT_LEVEL;
			int[] wrld = new int[2];


			for (int i = 0; i < levelsBetweenWorlds.Length; i++) {
				if (level <= levelsBetweenWorlds [i]) {
					wrld [0] = i+1;
					break;
				} else {
					level -= levelsBetweenWorlds [i];
				}
			}
			wrld [1] = level;
			return wrld;
		}

		//Sets the next level cap
		public static void setNextLevel(){
			int highestLevel = getCurrentLevel();
			int sceneNun = SceneManager.GetActiveScene ().buildIndex;
			if (sceneNun == highestLevel) {
				PlayerPrefs.SetInt (levelKey, highestLevel+1);
			}
		}
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

			bool playerCanProgress = true;
			for (int i = 0; i < PreGame.levelsBetweenWorlds.Length; i++) {
				if (nextLevel == PreGame.levelsBetweenWorlds [i] + PreGame.nonGameLevels) {
					if (Stars.Total() < PreGame.starThreshold[i]) {
						playerCanProgress = false;
						break;
					}
				}
			}
			if (playerCanProgress) {
				if (nextLevel < PreGame.nonGameLevels) {
					nextLevel = PreGame.nonGameLevels;
				}
				Time.timeScale = 1;
				gamePaused = EndGame.playerWon = false;
				//Loads the next level
				SceneManager.LoadScene (nextLevel);
			} else {
				SceneManager.LoadScene (1);
			}
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
            if(previousLevel < PreGame.nonGameLevels)
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
            //Gets the number of all stars collected
            public static int Total()
            {
                int[] stars = GetAllHighScores();
                int total = 0;

                for (int i = 0; i < stars.Length; i++)
                {
                    total += stars[i];
                }
                return total;
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

				//If the highest level move it up one
				PreGame.setNextLevel ();

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
		public static void LevelSelect()
        {
            InGame.gamePaused = false;
            playerWon = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }
}
