﻿using UnityEngine;
using System.Collections;
using System;

public class CameraMovement : MonoBehaviour
{

    //Holds the player object
    private Transform character;

    //new position
    private Vector3 moveTemp;
    private float xDiff, yDiff; //Distance between the center of the camera and the player
    public float moveThresh = 3; //How far the player can move from the center of the camera
    public float camMoveSpeed = 10; //the speed that the camera moves by
    public float startingZoom = 11; //starting ortho size of the camera so that specific levels can have different levels of zoom.
    [Header("This will lock the axis for some levels if required")]
    public bool xLoc = false;
    public bool yLoc = false;
    //these can be set during runtime if you wan't certain sections to stop looking out.
    void Start()
    {
        Camera gameCam = Camera.main;
        character = GameObject.FindGameObjectWithTag("Player").transform;
        gameCam.orthographicSize = startingZoom; //sets starting zoom of the level.
    }
    //Controls camera movement
    private void MoveCamera()
    {
        float gameCamSize = Camera.main.orthographicSize;
        //calculate the distance between the 
        xDiff = character.position.x - transform.position.x;
        yDiff = character.position.y - transform.position.y;

        //get differences and times them by the int value of the lock bools
        Vector3 result = new Vector3(xDiff * Convert.ToInt32(!xLoc), yDiff * Convert.ToInt32(!yLoc), 0);
        //find closest you can get and block it


        //setup vars
        Transform bounds = GameObject.Find("Death Barrier").transform;
        float vertExtent = gameCamSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        //check bounds
        if (bounds != null)
        {
            if ((this.transform.position.x + horzExtent >= bounds.position.x + bounds.position.x + bounds.lossyScale.x / 2 && xDiff > 0) || //checks if the cameras bounds have reached the death bounds.
                (this.transform.position.x - horzExtent <= bounds.position.x - bounds.position.x - bounds.lossyScale.x *2 && xDiff < 0))
            {
                result.x = 0;
            }
            if ((this.transform.position.y + vertExtent >= bounds.position.y + bounds.position.y + bounds.lossyScale.y / 2 && yDiff > 0) ||
                (this.transform.position.y - vertExtent <= bounds.position.y - bounds.position.y - bounds.lossyScale.y * 2 && yDiff < 0) )
            {
                result.y = 0;
            }
        }
        //Camera keeps moving towards player
        transform.Translate(result * (0.5f * result.magnitude) * Time.deltaTime);
    }

    private bool bDragging;
    private Vector3 oldPos;
    private Vector3 panOrigin;
    private float panSpeed = 3;

    private void MouseCameraMovement()
    {


    }

    //Handles camera zoom
    private void Zoom(float zoomSpeed)
    {
        float dis = Input.GetAxis("Mouse ScrollWheel");

        Camera gameCam = Camera.main;
        if (dis > 0)
        {//Scroll Forward
            gameCam.orthographicSize -= zoomSpeed * Time.deltaTime;
        }
        else if (dis < 0)
        {//Scroll Back
            gameCam.orthographicSize += zoomSpeed * Time.deltaTime;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCamera();
        float zoomSpeed = 20;
        Zoom(zoomSpeed);
        MouseCameraMovement();
    }
}
