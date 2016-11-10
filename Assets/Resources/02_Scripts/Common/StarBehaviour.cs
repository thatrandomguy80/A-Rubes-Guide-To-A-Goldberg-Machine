using UnityEngine;
using System.Collections;

public class StarBehaviour : GameState {

	// Use this for initialization
	void Start () {
        //At the start of the game reset the score
        InGame.Stars.Reset();
	}

    void OnTriggerEnter2D(Collider2D coll) {
        //If the marble collides with the star
        if (coll.gameObject.tag == "Player") {
            //Add a star to the count and Destroy the object
            InGame.Stars.Add();
            Destroy(gameObject);

            //Play sound effect
			if (AudioPlayer.instance != null) {
				AudioPlayer.instance.PlaySoundEffects (Sound_Effects.SelectSFX.STAR);
			}
        }
    }
}
