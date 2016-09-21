using UnityEngine;
using System.Collections;

public class LevelSelectionButton : LevelManager {

	public int level;

	//On mouse Click Load new level
	void OnMouseDown(){
		LoadLevel (level);
	}
}
