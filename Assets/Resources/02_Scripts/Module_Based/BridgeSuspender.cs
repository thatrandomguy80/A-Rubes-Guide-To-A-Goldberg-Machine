using UnityEngine;
using System.Collections;


//Executes the code in edit mode
[ExecuteInEditMode]
public class BridgeSuspender : ObstacleInteraction {

    public GameObject stationaryAnchor, bridgeAnchor;//Keep track of the anchors
    public int suspenderIndex;//Keeps track of what suspender is being cut [CHANGE]
    private bool isOneSided = false;//shouldn't need anymore

    Vector3 result;

    private bool tick = false; //for testing [CHANGE]

    void Start() {
        tick = false;
    }
    void Update() {
        //Re-adjust Suspender
        MoveSuspender();
        Debug.DrawLine(result, Vector3.zero);
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

        //find rotation needed 
        Vector3 diff = input - transform.position;
        diff.Normalize();
        //remember this from comp330?
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        //this is the platforms rope
        GameObject tempRope = Instantiate(Resources.Load("04_Prefabs/cylinderRopeNewshort"), bridgeAnchor.transform.position, Quaternion.Euler(0f, 0f, rot_z + 90)) as GameObject;
        tempRope.GetComponent<Rope>().startUp(bridgeAnchor, 180f, lowerA);
        tempRope.transform.parent = bridgeAnchor.transform.GetChild(0);
        tempRope.name = bridgeAnchor.name + " Rope";

        //this is the upper anchor rope
        tempRope = Instantiate(Resources.Load("04_Prefabs/cylinderRopeNewshort"), stationaryAnchor.transform.position, Quaternion.Euler(0f, 0f, rot_z + 90)) as GameObject;
        tempRope.GetComponent<Rope>().startUp(stationaryAnchor, 0f, upperA);
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
            if (!isOneSided) {
                CutSuspender(input);
            }
            tick = !tick;
        }

    }

    public void OnTriggerEnter2D(Collider2D other) {
        Vector3 cutPos = new Vector3(0, 0, 0);
        result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (other.gameObject.layer == LayerMask.NameToLayer("Swipe")) {
            TrailRendererWith2DCollider trail = GameObject.Find("SwipeControls").GetComponent<TrailRendererWith2DCollider>();
            if (trail != null) {
                cutPos = trail.getPos();
            }

            result = new Vector3(result.x, result.y - 3, 5);
            Interact(cutPos);

        }
    }
}
