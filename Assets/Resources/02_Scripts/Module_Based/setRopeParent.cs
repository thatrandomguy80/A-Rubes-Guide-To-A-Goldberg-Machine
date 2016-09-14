using UnityEngine;
using System.Collections;

public class setRopeParent : MonoBehaviour {
    [Header("This sets the joints connected body at start")]
    public bool nothing;


    private float aliveTime = 0.25f; // this sets when the multicut function will be aloud to be used after inst
    private float timeAlive; // saving of time inst
    private HingeJoint2D joint;


    //This sets the joints connected body at start for the rope
    void Start () {
        joint = this.GetComponent<HingeJoint2D>();
        //joint.connectedBody = this.transform.parent.GetComponent<Rigidbody2D>();
        timeAlive = Time.time;
	}


    public void enableD() {//called if the platform has a movement obj script attached to better limit the movement
        this.GetComponent<DistanceJoint2D>().enabled = true;
    }

    public void detach() {//called to detach this section of rope
        Destroy(joint);
        Destroy(this.GetComponent<DistanceJoint2D>());
        Destroy(this.GetComponent<BoxCollider2D>());
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.transform.name.Equals("Trail") && Time.time - timeAlive > aliveTime) {//has it been alive for aliveTime amount of time.
            joint.connectedBody.useAutoMass = false;
            joint.connectedBody.mass = 500f;
            Destroy(joint);
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.name.Equals("Trail") && Time.time - timeAlive > aliveTime) {//has it been alive for aliveTime amount of time.
            joint.connectedBody.useAutoMass = false;
            joint.connectedBody.mass = 500f;
            DistanceJoint2D d = joint.connectedBody.transform.GetComponent<DistanceJoint2D>();
            if (d != null) {
                d.enabled = true;
            }
            detach();//calls detach and all child detaches removing thier joints
        }
    }
}
