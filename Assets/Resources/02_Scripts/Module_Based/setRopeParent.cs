using UnityEngine;
using System.Collections;

public class setRopeParent : MonoBehaviour {
    [Header("This sets the joints connected body at start")]
    public bool nothing;

    private HingeJoint2D joint;
    //This sets the joints connected body at start for the rope
    void Start () {
        joint = this.GetComponent<HingeJoint2D>();
        joint.connectedBody = this.transform.parent.GetComponent<Rigidbody2D>();
	}

    public void enableD() {//called if the platform has a movement obj script attached to better limit the movement
        this.GetComponent<DistanceJoint2D>().enabled = true;
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.transform.name.Equals("Trail")) {
            joint.connectedBody.useAutoMass = false;
            joint.connectedBody.mass = 20f;
            Destroy(joint);
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }
}
