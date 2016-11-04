using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackgroundCanvasController : GameState {

    public Sprite worldOneBackground;
    public Sprite worldTwoBackground;
    public Sprite worldThreeBackground;

    private Image background;

	// Use this for initialization
	void Start () {
        background = transform.GetChild(0).GetComponent<Image>();
        int world = PreGame.getCurrentWorldAndLevel(SceneManager.GetActiveScene().buildIndex)[0];

        if(world == 1)
        {
            background.sprite = worldOneBackground;
        }else if(world == 2)
        {
            background.sprite = worldTwoBackground;
        }else if(world == 3)
        {
            background.sprite = worldThreeBackground;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
