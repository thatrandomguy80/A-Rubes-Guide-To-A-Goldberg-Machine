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
            transform.position = Vector3.MoveTowards(transform.position, moveTemp, Time.deltaTime * camMoveSpeed);
        }
    }

	private bool bDragging;
	private Vector3 oldPos;
	private Vector3 panOrigin;
	private float panSpeed = 3;

	private void MouseCameraMovement(){
		
			
	}

	//Handles camera zoom
	private void Zoom(float zoomSpeed){
		float dis = Input.GetAxis ("Mouse ScrollWheel");

		Camera gameCam = Camera.main;
		if (dis > 0) {//Scroll Forward
			gameCam.orthographicSize -= zoomSpeed * Time.deltaTime;
		} else if (dis < 0) {//Scroll Back
			gameCam.orthographicSize += zoomSpeed * Time.deltaTime;

		}
	}

	// Update is called once per frame
	void Update () {
        MoveCamera(); 
		float zoomSpeed = 20;
		Zoom (zoomSpeed);
		MouseCameraMovement ();
	}
}
