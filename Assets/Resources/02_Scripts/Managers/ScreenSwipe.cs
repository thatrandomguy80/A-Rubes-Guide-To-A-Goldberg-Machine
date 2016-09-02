using UnityEngine;
using System.Collections;

public class ScreenSwipe : MonoBehaviour {

	private TrailRenderer trail;

	// Now swipe objects can't collide with ball or others but can get location of collisions.
	void Start () {
        GameObject trailobj = GameObject.Find("Trail");
        trailobj.layer = LayerMask.NameToLayer("Swipe");

        //make swipe unable to hit ball or normal items
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Swipe"), LayerMask.NameToLayer("Default"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Swipe"), LayerMask.NameToLayer("Reflection"), true);

        //makes sure that all rope only collides with the mouse swipe which is in the "Swipe" layer.
        //also can't hit the ball
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Rope"), LayerMask.NameToLayer("Default"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Rope"), LayerMask.NameToLayer("Reflection"), true);
    }

	void Update()
	{
		if(Input.GetMouseButton(0)){
			transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = new Vector3 (transform.position.x, transform.position.y-2, 5);
		}
	}

}
