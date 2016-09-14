using UnityEngine;
using System.Collections;
public class Rope : MonoBehaviour {

    // Script for handeling rope movement and inst
    //unit scale is 0.0166 per unit short is 0.05f

    public GameObject tail;
    public GameObject head;
    private HingeJoint2D headjoint;


    public void startUp(GameObject anchor, float rot, Vector3 tailPos) {
        Transform moveObj = anchor.transform.parent.parent.FindChild("MoveableObj");
        if (moveObj != null) {
            // call ropes bones to enable distance joint
            BroadcastMessage("enableD");
        }

        //calc the scale units required to get correctly sized rope
        //float length = Vector2.Distance(new Vector2(anchor.transform.position.x, anchor.transform.position.y), new Vector2(tailPos.x, tailPos.y));
        float length = anchor.transform.position.y - tailPos.y;
        if (length < 0) length = -length;//make pos
        float scale = 0.05f * length;

        //trashes
        this.transform.Translate(new Vector3(0, -(length / 2), 0));
        this.transform.Rotate(new Vector3(0, 0, rot));
        this.transform.localScale = new Vector3(0.1f, scale, 0.1f);

        //sets the heads connected body to the anchor
        Rigidbody2D rb = anchor.GetComponentInChildren<Rigidbody2D>();
        headjoint = head.GetComponent<HingeJoint2D>();
        if (rb != null && headjoint != null) {
            headjoint.autoConfigureConnectedAnchor = false;
            headjoint.connectedBody = rb;
            headjoint.connectedAnchor = new Vector2(0, 0);
        }

        //add force here if needed
        //tail.transform.position = tailPos;


    }
}
