using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EnvironmentAligner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AlignToZero (gameObject);
	}

	public void AlignToZero(GameObject startingObj){
		if (startingObj.transform.childCount > 0) {
			for (int i = 0; i < startingObj.transform.childCount; i++) {
				AlignToZero (startingObj.transform.GetChild (i).gameObject);
			}
		}
		float zValue = Mathf.Clamp (transform.position.z,-1,1);
		startingObj.transform.position = new Vector3 (startingObj.transform.position.x, startingObj.transform.position.y, zValue);
	}

}
