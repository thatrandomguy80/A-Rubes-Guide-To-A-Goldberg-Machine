using UnityEngine;
using System.Collections;

public class ScreenSwipe : MonoBehaviour {

	private TrailRenderer trail;

	// Use this for initialization
	void Start () {

	}

	void Update()
	{
		if(Input.GetMouseButton(0)){
			transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = new Vector3 (transform.position.x, transform.position.y-2, 5);
		}
	}

}
