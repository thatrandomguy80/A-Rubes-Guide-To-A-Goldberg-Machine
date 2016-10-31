using UnityEngine;
using System.Collections;

public class ScreenSwipe : GameState {


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
			transform.position = new Vector3(transform.position.x, transform.position.y-2.5f, 5);
        }
    }

}
