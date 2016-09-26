using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour {

    public float extraForce = 1f;
    public float loopThreshold = .75f;
    public float rotSpeed = 750f;

    private Rigidbody2D rb;
    private GameObject lastCollider;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 getDir(GameObject ot) {
        Vector2 dir = new Vector2(0, 0);
        //left of hammer
        if (ot.transform.position.x < transform.position.x) {
            dir.x = -1;
        }
        //right of the hammer
        if (ot.transform.position.x > transform.position.x) {
            dir.x = 1;
        }
        if (dir.x == 0) {
            dir.x = 1;
        }
        return dir;
    }

    public void OnCollisionEnter2D(Collision2D other) {
        GameObject ot = other.gameObject;
        Rigidbody2D otrb = ot.GetComponent<Rigidbody2D>();
        if (ot.layer == LayerMask.NameToLayer("Reflection") && lastCollider == other.gameObject) {//the second hit
            //turns of the motor
            HingeJoint2D hj = GetComponent<HingeJoint2D>();
            hj.useMotor = false;

            //get dir the ball is in 
            Vector2 dir = getDir(ot);

            //add extra force back to the ball
            otrb.AddForce(dir * extraForce);
            lastCollider = null;

        } else if (ot.layer == LayerMask.NameToLayer("Reflection")) {//the first hit

            if (otrb.velocity.x > loopThreshold || otrb.velocity.x < -loopThreshold) {
                HingeJoint2D hj = GetComponent<HingeJoint2D>();
                hj.useMotor = true;

                JointMotor2D jm = hj.motor;
                //sets motor to run in the correct direction to complete the loopdeloop
                float speed = rotSpeed;
                speed *= getDir(ot).x;
                jm.motorSpeed = speed;
                hj.motor = jm;
            }
            //set last object
            lastCollider = other.gameObject;
        }
    }
}
