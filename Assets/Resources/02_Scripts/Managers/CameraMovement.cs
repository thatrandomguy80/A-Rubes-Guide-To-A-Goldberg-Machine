using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    //Holds the player object
    private GameObject character;

    //new position
    private Vector3 moveTemp;
    private float xDiff, yDiff; //Distance between the center of the camera and the player
    public float moveThresh = 1; //How far the player can move from the center of the camera
    private float camMoveSpeed = 8;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
    }
    //Controls camera movement
    private void MoveCamera()
    {
        //calculate the distance between the 
        xDiff = Mathf.Abs(character.transform.position.x - transform.position.x);
        yDiff = Mathf.Abs(character.transform.position.y - transform.position.y);


        if (xDiff >= moveThresh || yDiff >= moveThresh)
        {
            moveTemp = character.transform.position;
            moveTemp.z = -10;
            //Camera keeps moving towards player
            //the further away the character is to the camera
            //The faster the camera will move to keep up with the player
            transform.position = Vector3.MoveTowards(transform.position, moveTemp, Time.deltaTime * camMoveSpeed);
            //transform.position = Vector3.Lerp(transform.position, moveTemp, Time.deltaTime* camMoveSpeed);
        }
    }
	// Update is called once per frame
	void Update () {
        MoveCamera();      
	}
}
