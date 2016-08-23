using UnityEngine;
using System.Collections;

public class StarBehaviour : GameState {

	// Use this for initialization
	void Start () {
        //At the start of the game reset the score
        InGame.Stars.Reset();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D coll) {
        //If the marble collides with the star
        if (coll.gameObject.tag == "Player") {
            //Add a star to the count and Destroy the object
            InGame.Stars.Add();
            print("Player has " + InGame.Stars.Get().ToString() + " stars");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //If the marble collides with the star
        if (coll.gameObject.tag == "Player") {
            //Add a star to the count and Destroy the object
            InGame.Stars.Add();
            print("Player has " + InGame.Stars.Get().ToString() + " stars");
            Destroy(gameObject);
        }

    }
}
