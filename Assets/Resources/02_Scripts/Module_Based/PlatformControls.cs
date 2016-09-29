using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlatformControls : PlatformBuilder {

    public DistanceJoint2D[] distJoints;

    private string[] gears = { "gear1", "gear2" };

    private bool contacts = false;

    private int recalls = 0;

    // Use this for initialization
    void Start() {

        //Get all the spring joints for the platform
        distJoints = GetComponents<DistanceJoint2D>();
        GameObject anchors = transform.parent.GetChild(3).gameObject;
        GameObject leftAnchor = anchors.transform.GetChild(0).gameObject;
        GameObject rightAnchor = anchors.transform.GetChild(1).gameObject;

        //Create The Platform
        base.CreatePlatform(leftAnchor, rightAnchor);

        //gear adding
        GameObject par = new GameObject();
        int j = Random.Range(0, 4);
        List<Vector3> prev = new List<Vector3>();
        for (int i = 0; i <= j; j--) {
            prev = AddGears(leftAnchor, rightAnchor, prev);
            recalls = 0;
        }


        //this line only needed for non static Platforms
        leftAnchor.transform.parent.SetParent(transform);
        //link the joints after calling base platform creator
        LinkJoints(leftAnchor, rightAnchor);



        //Starts from the top left corner then goes clockwise.

        //Turn off autoconfigure of distance and connected anchor
        for (int i = 0; i < distJoints.Length; i++) {
            distJoints[i].autoConfigureDistance = false;
            distJoints[i].autoConfigureConnectedAnchor = false;
        }
    }

    public List<Vector3> AddGears(GameObject leftAnchor, GameObject rightAnchor, List<Vector3> prevPos) {
        recalls++;
        if (recalls < 10) {
            Vector2 left = leftAnchor.transform.position;
            Vector2 right = rightAnchor.transform.position;
            float zee;
            if (Random.Range(-1, 1) > -1) {//sets behind or infront of platform.
                zee = 1f;
            } else {
                zee = -1f;
            }

            Vector3 midPos = new Vector3(Random.Range(left.x, right.x), Random.Range(left.y, right.y), zee);//pos of the gear

            float offset = 1f;//how close the gears are aloud to be
            bool tooClose = false;
            for (int i = 0; i < prevPos.Count; i++) {
                tooClose = tooClose || offsetCheck(midPos, prevPos[i], offset); // accumulate if it's too close for any prev pos
            }
            //need to check all now.
            if (tooClose) {//gears are too close
                AddGears(leftAnchor, rightAnchor, prevPos);
            } else {//gears arn't too close and isn't first call.
                makeGear(leftAnchor, rightAnchor, midPos, prevPos);
            }
            prevPos.Add(midPos);
        }
        return prevPos;//new result with midPos appended
    }

    private bool offsetCheck(Vector3 midPos, Vector3 prevPos, float off) {
        return (!((midPos.x < prevPos.x - off || midPos.x > prevPos.x + off) || (midPos.y < prevPos.y - off || midPos.y > prevPos.y + off)));
    }

    private void makeGear(GameObject leftAnchor, GameObject rightAnchor, Vector3 midPos, List<Vector3> prev) {
        Vector2 left = leftAnchor.transform.position;
        Vector2 right = rightAnchor.transform.position;
        if (Physics.Raycast(new Vector3(midPos.x, midPos.y, 3), new Vector3(0, 0, -1), 10)) {//double check your infront of a platform
            int rand = Random.Range(0, gears.Length);
            GameObject gear = Instantiate(Resources.Load("04_Prefabs/" + gears[rand]) as GameObject, midPos, Quaternion.identity) as GameObject;
            gear.transform.parent = transform.parent.GetChild(0);
            gear.name = "Gear";
            gear.transform.localEulerAngles += new Vector3(0, 180, 0);
            //randomises gear rotating speed
            RotatingObject a = gear.transform.GetChild(0).GetComponent<RotatingObject>();
            if (a != null) {
                a.rotationSpeeds.z = Random.Range(-50, 50);
            }
        } else {
            AddGears(leftAnchor, rightAnchor, prev);//recall if not infront of platform.
        }
    }

    //Sets up the joint positions
    public void LinkJoints(GameObject leftAnchor, GameObject rightAnchor) {

        //Get the distance from the centre of the platform to the edge
        float widthOfPlatform = Vector2.Distance(leftAnchor.transform.position, rightAnchor.transform.position);
        float distToEdge = widthOfPlatform / 2;
        //Get the distance from the centre of the platform to the left anchor
        float distToAnchor = Mathf.Abs(leftAnchor.transform.position.x - transform.position.x);

        float xdist = distToAnchor / distToEdge;
        float ydist = transform.position.y - leftAnchor.transform.position.y;
        //If the platform has 2 suspenders
        if (distJoints.Length == 2) {
            distJoints[0].anchor = new Vector2(distJoints[0].anchor.x * xdist, -ydist);
            distJoints[1].anchor = new Vector2(distJoints[1].anchor.x * xdist, ydist);
            //If the platform has 1 suspenders
        } else if (distJoints.Length == 1) {
            distJoints[0].anchor = new Vector2(distJoints[0].anchor.x * xdist, -ydist);
        } else {
            Debug.LogError("No Suspenders given");
        }
    }


}
