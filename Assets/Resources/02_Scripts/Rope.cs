using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

    // Script for handeling rope movement and inst
    //unit scale is 0.0166 per unit

    public GameObject tail;
    public GameObject head;
    private HingeJoint2D headjoint;

    public void startUp(GameObject anchor, float rot, Vector3 tailPos) {
        float Hlength = 1.5f;//half the length
        //tailPos.y - anchor.transform.position.y;
        // if (length < 0) length = -length;//make pos
        //this.transform.localScale = new Vector3(0, length * 0.0166f, 0);
        this.transform.Rotate(new Vector3(0f, 0f, rot));
        this.transform.Translate(new Vector3(0, -Hlength, 0));

        if (anchor.GetComponent<Rigidbody2D>() == null) {//stationary anchors have rbs platfom ones dont
            Rigidbody2D rb = anchor.transform.parent.parent.GetComponentInChildren<Rigidbody2D>();
            headjoint = head.GetComponent<HingeJoint2D>();
            if (rb != null && headjoint != null) {
                headjoint.connectedBody = rb;
            }
        }
        //tail.transform.position = tailPos;
        
    }
}
