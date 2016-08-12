using UnityEngine;
using System.Collections;

public class PlatformControls : MonoBehaviour {

    public SpringJoint2D[] springJoints;

    // Use this for initialization
    void Start()
    {
        //Get all the spring joints for the platform
        springJoints = GetComponents<SpringJoint2D>();
        //Create The Platform
        CreateNewPlatform();
        //Starts from the top left corner then goes clockwise.

        //Turn off autoconfigure of distance and connected anchor
        for (int i = 0; i < springJoints.Length; i++)
        {
            springJoints[i].autoConfigureDistance = false;
            springJoints[i].autoConfigureConnectedAnchor = false;
        }
    }

    //Create a platform between the two anchors
    public void CreateNewPlatform()
    {
        //Get the left and right platform anchors
        GameObject anchors = GameObject.Find(transform.parent.name + "/PlatformAnchors");
        GameObject leftAnchor = anchors.transform.GetChild(0).gameObject;
        GameObject rightAnchor = anchors.transform.GetChild(1).gameObject;

        //Create a new Platform
        GameObject newPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newPlatform.name = "Platform (Display)";

        //Create a gameobject to hold the collider
        GameObject newPlatformCollider = new GameObject(newPlatform.name + " Collider");
        newPlatformCollider.transform.SetParent(newPlatform.transform);
        newPlatformCollider.AddComponent<BoxCollider2D>();

        //Remove the boxCollider
        Destroy(newPlatform.GetComponent<BoxCollider>());
        //Change the material to the one from the main platform
        newPlatform.transform.GetComponent<Renderer>().material = transform.GetComponent<Renderer>().material;

        //Place the platform in the center
        transform.position = Vector2.Lerp(leftAnchor.transform.position, rightAnchor.transform.position, 0.5f);
        newPlatform.transform.position = Vector2.Lerp(leftAnchor.transform.position, rightAnchor.transform.position, 0.5f);


        
        //Rotate the platform to align with the left and right anchor point 
        float angle = MathExt.getAngle(newPlatform.transform.position, rightAnchor.transform.position);
        if (leftAnchor.transform.position.x > rightAnchor.transform.position.x)
        {
            newPlatform.transform.eulerAngles = new Vector3(0, 0, -angle - 90);
        }
        else
        {
            newPlatform.transform.eulerAngles = new Vector3(0, 0, angle - 90);
        }


        //Set the width of the platform
        float widthOfPlatform = Vector2.Distance(leftAnchor.transform.position, rightAnchor.transform.position);
        transform.localScale = new Vector3(widthOfPlatform, 1, 1);
        newPlatform.transform.localScale = new Vector3(widthOfPlatform, 1, 1);

        //Get the distance from the centre of the platform to the edge
        float distToEdge = widthOfPlatform / 2;
        //Get the distance from the centre of the platform to the left anchor
        float distToAnchor = Mathf.Abs(leftAnchor.transform.position.x - transform.position.x);

        float xdist = distToAnchor / distToEdge;
        float ydist = transform.position.y - leftAnchor.transform.position.y;
        springJoints[0].anchor = new Vector2(springJoints[0].anchor.x * xdist, -ydist);
        springJoints[1].anchor = new Vector2(springJoints[1].anchor.x * xdist, ydist);


        //Set the anchors to the platform
        anchors.transform.SetParent(transform);
        newPlatform.transform.SetParent(anchors.transform);
    }
	
}
