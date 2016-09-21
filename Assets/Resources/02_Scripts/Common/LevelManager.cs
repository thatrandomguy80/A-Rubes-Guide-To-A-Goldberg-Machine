using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : GameState {

	public GameObject LeftSide;

	// Use this for initialization
	void Start () {

		int latestlevel = PreGame.getCurrentLevel ();
		int remainingChildren = LeftSide.transform.childCount - latestlevel;
		print ("Starting level is " + PreGame.getCurrentLevel () + " Remaining Levels left for left side " + remainingChildren);



		while (latestlevel > 0) {
			GameObject button = LeftSide.transform.GetChild (latestlevel-1).gameObject;
			Material mat = button.GetComponent<Renderer> ().material;
			mat.color = Color.red;
			LevelSelectionButton lsBut = button.AddComponent<LevelSelectionButton> ();
			lsBut.level = latestlevel;


			latestlevel--;
		}
		while (remainingChildren > 0) {
			GameObject button = LeftSide.transform.GetChild (PreGame.getCurrentLevel()-1 + remainingChildren).gameObject;
			Material mat = button.GetComponent<Renderer> ().material;
			mat.color = new Color(0,0,1,0.2f);

			remainingChildren--;
		}
	}

	protected void LoadLevel(int levelNum){
		SceneManager.LoadScene (levelNum);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
