using UnityEngine;
using System.Collections;

public class ReAdjustDemoWorlds : MonoBehaviour {

	public GameObject platforms;

	// Use this for initialization
	void Start () {
		StartCoroutine (LoadDemo ());
	}

	IEnumerator LoadDemo(){
		yield return new WaitForEndOfFrame ();
		for (int i = 0; i < platforms.transform.childCount; i++) {
			GameObject currentPlatform = platforms.transform.GetChild (i).gameObject;
			currentPlatform.name = platforms.name + i;
			GameObject c = currentPlatform.transform.GetChild (0).GetChild (2).GetChild (2).gameObject;
			print (c.name);
			c.transform.localScale = new Vector3 (c.transform.localScale.x, 1, c.transform.localScale.z);

		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
