using UnityEngine;
using System.Collections;

public class PlatformControls : PlatformBuilder {

    public SpringJoint2D[] springJoints;

    // Use this for initialization
    void Start()
    {
        //Get all the spring joints for the platform
        springJoints = GetComponents<SpringJoint2D>();
        GameObject anchors = GameObject.Find(transform.parent.name + "/PlatformAnchors");
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
        for (int i = 0; i < springJoints.Length; i++)
        {
            springJoints[i].autoConfigureDistance = false;
            springJoints[i].autoConfigureConnectedAnchor = false;
        }
    }

    public void LinkJoints(GameObject leftAnchor, GameObject rightAnchor) {

        //Get the distance from the centre of the platform to the edge
        float widthOfPlatform = Vector2.Distance(leftAnchor.transform.position, rightAnchor.transform.position);
        float distToEdge = widthOfPlatform / 2;
        //Get the distance from the centre of the platform to the left anchor
        float distToAnchor = Mathf.Abs(leftAnchor.transform.position.x - transform.position.x);

        float xdist = distToAnchor / distToEdge;
        float ydist = transform.position.y - leftAnchor.transform.position.y;
        springJoints[0].anchor = new Vector2(springJoints[0].anchor.x * xdist, -ydist);
        springJoints[1].anchor = new Vector2(springJoints[1].anchor.x * xdist, ydist);
    }
	
}
