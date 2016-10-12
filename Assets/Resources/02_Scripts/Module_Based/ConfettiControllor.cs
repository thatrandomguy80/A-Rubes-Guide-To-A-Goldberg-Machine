using UnityEngine;
using System.Collections;

public class ConfettiControllor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.rotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play() {
        transform.GetChild(3).gameObject.SetActive(true);
        Debug.Log("called");
    }
}
