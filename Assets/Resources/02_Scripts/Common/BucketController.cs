using UnityEngine;
using System.Collections;

public class BucketController : GameState {

    //If the player collides with the bottom of the bucket
    //They win and move to the next level
	void OnCollisionEnter2D(Collision2D coll)
    {
		
        if (coll.transform.tag.Equals("Player"))
        {
			Rigidbody2D rig2d = coll.transform.GetComponent<Rigidbody2D> ();
			rig2d.constraints = RigidbodyConstraints2D.FreezeAll;
			InGame.Stars.Add (); //They also get a star
            ConfettiControllor cc = transform.parent.parent.GetChild(2).GetComponent<ConfettiControllor>();
            if (cc != null) {
                cc.Play();
            }
            EndGame.Win();

        }
    }
    void OnTriggerEnter2D(Collider2D coll) {//trigger copy 

        if (coll.transform.tag.Equals("Player")) {
            Rigidbody2D rig2d = coll.transform.GetComponent<Rigidbody2D>();
            rig2d.constraints = RigidbodyConstraints2D.FreezeAll;
            InGame.Stars.Add(); //They also get a sta
            ConfettiControllor cc = transform.parent.parent.GetChild(2).GetComponent<ConfettiControllor>();
            if (cc != null) {
                cc.Play();
            }

            EndGame.Win();

        }
    }
}
