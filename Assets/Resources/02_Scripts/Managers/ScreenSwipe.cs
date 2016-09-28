using UnityEngine;
using System.Collections;

public class ScreenSwipe : GameState {

	private TrailRenderer trail;

	// Now swipe objects can't collide with ball or others but can get location of collisions.
	void Start () {
        GameObject trailobj = GameObject.Find("Trail");
        trailobj.AddComponent<trailCollider>();
    }

	void Update()
	{
		if (!EndGame.playerWon && !InGame.gamePaused) {
			MoveScreenCursor ();
		}
	}

    //If the player holds the mouse button down move the screen cursor
    private void MoveScreenCursor()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y - 2, 5);
        }
    }

}
