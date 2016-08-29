using UnityEngine;
using System.Collections;

public class BucketController : GameState {

    //If the player collides with the bottom of the bucket
    //They win and move to the next level
	void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag.Equals("Player"))
        {
            EndGame.Win();
        }
    }
}
