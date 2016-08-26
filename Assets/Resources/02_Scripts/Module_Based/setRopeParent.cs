using UnityEngine;
using System.Collections;

public class setRopeParent : MonoBehaviour {
    [Header("This sets the joints connected body at start")]
    public bool nothing;
    //This sets the joints connected body at start
    // Use this for initialization
    void Start () {
        HingeJoint2D joint = this.GetComponent<HingeJoint2D>();
        joint.connectedBody = this.transform.parent.GetComponent<Rigidbody2D>();
	}

    public void enableD() {
        this.GetComponent<DistanceJoint2D>().enabled = true;
    }
}
