using UnityEngine;
using System.Collections;

public class AntiGravityController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    private void FlipGravityForAllObjects()
    {
        Rigidbody2D[] rigBods = FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
        for(int i = 0; i < rigBods.Length; i++)
        {
            rigBods[i].gravityScale = rigBods[i].gravityScale * -1;
        }

    }
    void OnMouseDown()
    {
        FlipGravityForAllObjects();
    }
	
}
