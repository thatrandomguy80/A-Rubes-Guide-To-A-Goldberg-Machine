using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectCanvas : GameState {

	/*Handles the level select canvas*/

	#region Members
	public static LevelSelectCanvas instance;
	public GameObject leftArrow, rightArrow;
	#endregion


	void Awake()
	{
		if (instance == null) {
			//Make this audio player the only audio player
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		
		Init ();
	}

	public void MainMenu(){
		EndGame.MainMenu ();
	}

	//Sets up the initial conditions of the world
	void Init(){
		//Get the UI arrows
		GameObject worldSelection = GameObject.Find (transform.name + "/World Selection");

		leftArrow = worldSelection.transform.GetChild (0).gameObject;
		rightArrow = worldSelection.transform.GetChild (1).gameObject;

		//Show the star count
		Text starCount = transform.GetChild (1).GetChild (0).GetComponent<Text> ();
		starCount.text = InGame.Stars.Total ().ToString();


		float delay = 1;
		float speed = 0.5f;
		//Slide in the arrows
		//StartCoroutine (SlideInArrows (delay,speed));
		//Slide in star score
		//StartCoroutine(SlideInScore(delay,speed));
	}
	//Slide in star score
	IEnumerator SlideInScore(float delay,float speed){
		//Get the rect transforms for the arrows
		RectTransform starScore = transform.GetChild (1).GetComponent<RectTransform> ();
		//Get the start and end pos of the animation
		Vector3 scoreEndPos = starScore.transform.localPosition;
		Vector3 YDiff = new Vector3 (0, starScore.sizeDelta.y + 10);
		Vector3 scoreStartPos = scoreEndPos + YDiff;

		starScore.transform.localPosition = scoreStartPos;

		float time = 0;
		//Delay the animation for 'delay' time
		yield return new WaitForSeconds (delay);
		//Move into postion
		while (time < 1) {			
			time += Time.deltaTime / speed;
			starScore.transform.localPosition = Vector3.Lerp (scoreStartPos, scoreEndPos, time);
			yield return new WaitForEndOfFrame ();
		}
	}
	//Handles interpolation of sliding arrows into postion
	IEnumerator SlideInArrows(float delay,float speed){
		//Get the rect transforms for the arrows
		RectTransform leftRect = leftArrow.GetComponent<RectTransform> ();
		RectTransform rightRect = rightArrow.GetComponent<RectTransform> ();

		//Get the start and end pos of the animation
		Vector3 leftEndPos = leftRect.transform.localPosition;
		Vector3 rightEndPos = rightRect.transform.localPosition;
		Vector3 Xdiff = new Vector3 (leftRect.sizeDelta.x, 0);
		Vector3 leftStartPos = leftEndPos - Xdiff;
		Vector3 rightStartPos = rightEndPos + Xdiff;

		leftRect.transform.localPosition = leftStartPos;
		rightRect.transform.localPosition = rightStartPos;

		float time = 0;
		//Delay the animation for 'delay' time
		yield return new WaitForSeconds (delay);
		//Move into postion
		while (time < 1) {			
			time += Time.deltaTime / speed;
			leftRect.transform.localPosition = Vector3.Lerp (leftStartPos, leftEndPos, time);
			rightRect.transform.localPosition = Vector3.Lerp (rightStartPos, rightEndPos, time);
			yield return new WaitForEndOfFrame ();
		}

			
	}
}
