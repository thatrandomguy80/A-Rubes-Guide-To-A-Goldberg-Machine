using UnityEngine;
using System.Collections;

public class PlatformBuilder : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreatePlatform();
	}


    public void CreatePlatform() {
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
        if (leftAnchor.transform.position.x > rightAnchor.transform.position.x) {
            newPlatform.transform.eulerAngles = new Vector3(0, 0, -angle - 90);
        } else {
            newPlatform.transform.eulerAngles = new Vector3(0, 0, angle - 90);
        }


        //Set the width of the platform
        float widthOfPlatform = Vector2.Distance(leftAnchor.transform.position, rightAnchor.transform.position);
        transform.localScale = new Vector3(widthOfPlatform, 1, 1);
        newPlatform.transform.localScale = new Vector3(widthOfPlatform, 1, 1);
        anchors.transform.SetParent(transform);
        newPlatform.transform.SetParent(anchors.transform);
    }
}
