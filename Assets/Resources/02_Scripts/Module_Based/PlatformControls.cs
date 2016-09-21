using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlatformControls : PlatformBuilder {

	public DistanceJoint2D[] distJoints;

    private bool contacts = false;

    // Use this for initialization
    void Start()
    {
        //Get all the spring joints for the platform
		distJoints = GetComponents<DistanceJoint2D>();
		GameObject anchors = transform.parent.GetChild (3).gameObject;
        GameObject leftAnchor = anchors.transform.GetChild(0).gameObject;
        GameObject rightAnchor = anchors.transform.GetChild(1).gameObject;

        //Create The Platform
        base.CreatePlatform(leftAnchor, rightAnchor);
        //this line only needed for non static Platforms
        leftAnchor.transform.parent.SetParent(transform);
        //link the joints after calling base platform creator
        LinkJoints(leftAnchor, rightAnchor);
       
        

        //Starts from the top left corner then goes clockwise.

        //Turn off autoconfigure of distance and connected anchor
		for (int i = 0; i < distJoints.Length; i++)
        {
			distJoints[i].autoConfigureDistance = false;
			distJoints[i].autoConfigureConnectedAnchor = false;
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
			distJoints [0].anchor = new Vector2 (distJoints [0].anchor.x * xdist, -ydist);
			distJoints [1].anchor = new Vector2 (distJoints [1].anchor.x * xdist, ydist);
			//If the platform has 1 suspenders
		} else if (distJoints.Length == 1) {
			distJoints [0].anchor = new Vector2 (distJoints [0].anchor.x * xdist, -ydist);
		} else {
			Debug.Log ("No Suspenders given");
		}
    }

	
}
