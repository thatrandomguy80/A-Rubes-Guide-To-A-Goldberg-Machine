using UnityEngine;
using System.Collections;

public class AntiGravityArea : MonoBehaviour {

    //When an object enters the anti gravity area
    //Their gravity will be inverted
    void OnTriggerEnter2D(Collider2D other)
    {
        FlipGravity(other.gameObject);
    }
    //When an object exits the anti gravity area
    //Their gravity will return to normal
    void OnTriggerExit2D(Collider2D other)
    {
        FlipGravity(other.gameObject);
    }
    //Flips the gravity of an object
    private void FlipGravity(GameObject obj)
    {
        //If the object has a rigidbody
        Rigidbody2D rigid2D = obj.GetComponent<Rigidbody2D>();
        if (rigid2D != null)
        {
            //Flip the gravity
            rigid2D.gravityScale = rigid2D.gravityScale*-1;
        }
    }
    
}
