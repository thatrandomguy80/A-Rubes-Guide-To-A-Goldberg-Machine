using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

    // Script for handeling rope movement and inst

    public GameObject tail;
    public GameObject head;
    private Transform headloc;
    private HingeJoint2D headjoint;

    void start() {
        headloc = head.transform;
        headjoint = head.GetComponent<HingeJoint2D>();
    }

    public void attach(Rigidbody2D rb) {//attach head to rigid body
        headjoint.connectedBody = rb;
    }

    public void move(Vector3 a) {//move head to location
        headloc.position = a;
    }
}
