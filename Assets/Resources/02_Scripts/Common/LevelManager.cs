using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelManager : GameState {

    //Is the left and rightSide of a world
    //Note: will need to change

    //Keeps track of all the worlds in the game
    public GameObject[] worlds;
    public int worldSelected;

    public readonly int[] starThreshold = { 5, 10, 15 };

    public int latestlevel;//Keeps track of what level the player is up to
    private int buttonsPlaced;//Keeps track of how many buttons are placed
	// Use this for initialization
	void Start () {
        buttonsPlaced = 0;
        //PlayerPrefs.DeleteAll();
		latestlevel = PreGame.getCurrentLevel ()-1;
        print("Latest Level : " + latestlevel);
        int numberOfWorlds = transform.childCount;
        worlds = new GameObject[numberOfWorlds];
        for(int i = 0; i < numberOfWorlds; i++)
        {
            worlds[i] = transform.GetChild(i).gameObject;
            ActivateButtons(worlds[i].transform.GetChild(0).gameObject);
            ActivateButtons(worlds[i].transform.GetChild(1).gameObject);
        }
        worldSelected = 0;   
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
	// Update is called once per frame
	void Update () {
        //Moves the camera to the world selected
        float speed = Time.deltaTime * 2;
        float newXPos = Mathf.Lerp(Camera.main.transform.position.x, worlds[worldSelected].transform.position.x, speed);
        Camera.main.transform.position = new Vector3(newXPos, Camera.main.transform.position.y, Camera.main.transform.position.z);

        changeText();

    }
    //Selects the next world
    public void NextWorld()
    {
        if (worldSelected < worlds.Length-1)
        {
            if (PreGame.TotalNumberOfStars() >= starThreshold[worldSelected])
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
        GameObject button = GameObject.Find("World Selection/Right");
        Text txt = button.transform.GetChild(0).GetComponent<Text>();
        if (PreGame.TotalNumberOfStars() < starThreshold[worldSelected])
        {
            txt.text = starThreshold[worldSelected].ToString();
        }else
        {
            txt.text = "";
        }

    }
}
