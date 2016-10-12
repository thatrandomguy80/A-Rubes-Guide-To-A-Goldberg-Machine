using UnityEngine;
using System.Collections;

public class jointInst : MonoBehaviour {

	// moves joints forward to avoid render issue

	void Start () {
        transform.Translate(0, 0, 0.25f);
	}
}
