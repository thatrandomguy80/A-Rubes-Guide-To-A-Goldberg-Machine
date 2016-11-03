using UnityEngine;
using System.Collections;

public class BucketController : GameState {

    //If the player collides with the bottom of the bucket
    //They win and move to the next level
	void OnCollisionEnter2D(Collision2D coll)
    {
		WinGameBucket(coll);
    }
    void OnTriggerEnter2D(Collider2D coll) {//trigger copy 
		WinGameBucket(coll);    
    }

	void WinGameBucket(Collision2D coll){
		if (coll.transform.tag.Equals("Player")) {
			Rigidbody2D rig2d = coll.transform.GetComponent<Rigidbody2D>();
			rig2d.constraints = RigidbodyConstraints2D.FreezeAll;
			InGame.Stars.Add(); //They also get a sta

			coll.transform.parent = transform;


			ConfettiControllor cc = transform.parent.parent.GetChild(2).GetComponent<ConfettiControllor>();
			if (cc != null) {
				cc.Play();
			}



			EndGame.Win();

		}
	}
	void WinGameBucket(Collider2D coll){
		if (coll.transform.tag.Equals("Player")) {
			Rigidbody2D rig2d = coll.transform.GetComponent<Rigidbody2D>();
			rig2d.constraints = RigidbodyConstraints2D.FreezeAll;

			coll.transform.parent = transform;


			ConfettiControllor cc = transform.parent.parent.GetChild(2).GetComponent<ConfettiControllor>();
			if (cc != null) {
				cc.Play();
			}



            EndGame.Win();

		}
	}

}
