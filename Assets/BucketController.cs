using UnityEngine;
using System.Collections;

public class BucketController : GameState {

	void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag.Equals("Player"))
        {
            EndGame.Win();
        }
    }
}
