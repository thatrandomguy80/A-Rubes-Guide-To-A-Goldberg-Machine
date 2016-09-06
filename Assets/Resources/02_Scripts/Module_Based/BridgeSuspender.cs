using UnityEngine;
using System.Collections;


//Executes the code in edit mode
[ExecuteInEditMode]
public class BridgeSuspender : ObstacleInteraction {

    public GameObject stationaryAnchor, bridgeAnchor;//Keep track of the anchors
    public int suspenderIndex;//Keeps track of what suspender is being cut [CHANGE]

    private bool tick = false; //for testing [CHANGE]

    void Start() {
        tick = false;
    }
    void Update() {
        //Re-adjust Suspender
        MoveSuspender();
    }
    //Re-adjust the suspender according to the movement of the platform
    private void MoveSuspender() {
        //Keep the suspender midway between the platform at the anchor
        float midWayPoint = 0.5f;
        transform.position = new Vector3(Mathf.Lerp(stationaryAnchor.transform.position.x, bridgeAnchor.transform.position.x, midWayPoint), Mathf.Lerp(stationaryAnchor.transform.position.y, bridgeAnchor.transform.position.y, midWayPoint), transform.position.z);

        //Scale the rope accordingly so it looks like its always attached
        float distance = Vector3.Distance(stationaryAnchor.transform.position, bridgeAnchor.transform.position);
        transform.localScale = new Vector3(transform.localScale.x, distance / 2, transform.localScale.z);


        float angle = MathExt.getAngle(transform.position, stationaryAnchor.transform.position);
        //Adjust the angle depending on the position of the platform
        if (transform.position.x >= stationaryAnchor.transform.position.x) {
            angle = Mathf.Abs(angle - 180);
        } else {
            angle = angle - 180;
        }
        //Rotate the suspender accordingly
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    //Remove the suspender from the platform
    public void RemoveSuspender() {
        //Detach the suspender from the platform
        GameObject platform = GameObject.Find(transform.parent.parent.name + "/Platform");
        DistanceJoint2D spring = platform.GetComponent<PlatformControls>().distJoints[suspenderIndex];
        spring.enabled = false;
        //transform.GetComponent<MeshRenderer>().enabled = false;//does this still need to be here it can collide with objects sometimes



        Destroy(gameObject);


    }

    public void CutSuspender(Vector3 input) {

        GameObject platform = GameObject.Find(transform.parent.parent.name + "/Platform");
        DistanceJoint2D spring = platform.GetComponent<PlatformControls>().distJoints[suspenderIndex];

        float yoff = 0f, xoff = 0f;//offset for new anchors 
        Vector3 upperA = new Vector3(input.x + xoff, input.y + yoff, input.z);//position of tails
        Vector3 lowerA = new Vector3(input.x + xoff, input.y - yoff, input.z);

        //this is the platforms rope
        GameObject tempRope = Instantiate(Resources.Load("04_Prefabs/cylinderRope"), bridgeAnchor.transform.position, Quaternion.identity) as GameObject;
        tempRope.GetComponent<Rope>().startUp(bridgeAnchor, Vector3.Angle(bridgeAnchor.transform.position,input), lowerA);
        tempRope.transform.parent = bridgeAnchor.transform.GetChild(0);
        tempRope.name = bridgeAnchor.name + " Rope";

        //this is the upper anchor rope
        tempRope = Instantiate(Resources.Load("04_Prefabs/cylinderRope"), stationaryAnchor.transform.position, Quaternion.identity) as GameObject;
        tempRope.GetComponent<Rope>().startUp(stationaryAnchor, Vector3.Angle(input, stationaryAnchor.transform.position), upperA);
        Debug.Log(Vector3.Angle(stationaryAnchor.transform.position, input));
        Debug.Log(Vector3.Angle(bridgeAnchor.transform.position, input));
        tempRope.transform.parent = platform.transform.parent.GetChild(1).GetChild(0);
        tempRope.name = stationaryAnchor.name + " Rope";
    }

    public override void Interact() {
        if (!tick) {
            base.Interact();
            RemoveSuspender();
            CutSuspender(new Vector3(0, 0, 0));
            tick = !tick;
        }
    }

    public override void Interact(Vector3 input) {//called by trail with it's transform as the cut location.
        if (!tick) {
            base.Interact(input);
            RemoveSuspender();
            CutSuspender(input);
            tick = !tick;
        }

    }

    public void OnCollisionEnter2D(Collision2D other) {// this is not used currently as the trail object calls the interact for point passing.
        if (other.transform.name.Equals("Trail")) {
            ContactPoint2D[] hit = other.contacts;
            if (hit != null) {
                Interact(new Vector3(hit[0].point.x, hit[0].point.y, 0));
            }
        }
    }
}
