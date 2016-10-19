using UnityEngine;
using System.Collections;
using System;

public class CameraMovement : MonoBehaviour
{
    #region Members
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

    private bool bDragging;
    private Vector3 oldPos;
    private Vector3 panOrigin;
    private float panSpeed = 3;

    Camera gameCam;
    #endregion

    //Handles what cameratype you want to use
    public enum CameraType
    {
        LockOn,ViewAll
    }
    public CameraType camType = CameraType.LockOn;

    //these can be set during runtime if you wan't certain sections to stop looking out.
    void Start()
    {
        gameCam = Camera.main;
        character = GameObject.FindGameObjectWithTag("Player").transform;
        gameCam.orthographicSize = startingZoom; //sets starting zoom of the level.
    }
    //Controls camera movement
    private void MoveCamera()
    {
        float gameCamSize = gameCam.orthographicSize;
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
        //Locks z on -10 (FIXING BUG)
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
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
        //Normal camera where it locks on to the balls movement
        if (camType == CameraType.LockOn)
        {
            MoveCamera();
            float zoomSpeed = 20;
            Zoom(zoomSpeed);
        }else if (camType == CameraType.ViewAll)
        {
            //Get a rect that contains all of the players inside
            Rect boundingBox = CalculateTargetsBoundingBox();
            //Move the camera into the middle of all the players
            transform.position = CalculateCameraPosition(boundingBox);
            gameCam.orthographicSize = CalculateOrthographicSize(boundingBox);
        }
    }

    #region ViewAll
    //Keeps track of the elements on the screen
    GameObject[] elements;
    //Space between edge of the screen and the players
    public float boundingBoxPadding = 8f;
    //Min zoom for orthographic camera
    public float minimumOrthographicSize = 8f;
    //Camera zoom speed
    public float zoomSpeed = 10f;
    #endregion

    /*Calculates a box that contains all of the players inside*/
    Rect CalculateTargetsBoundingBox()
    {
        /*
         * Finds the minimum X,Y and the Maximum X,Y from all of the players
         */
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        //Finds all of the playable characters
        //And all of the enemies
        elements = GameObject.FindGameObjectsWithTag("Player");
        GameObject bucket = GameObject.Find("Bucket");
        GameObject player = elements[0];
        elements = new GameObject[2];
        elements[0] = player;
        elements[1] = bucket;
        

        foreach (GameObject target in elements)
        {
            Vector3 position = target.transform.position;

            minX = Mathf.Min(minX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxX = Mathf.Max(maxX, position.x);
            maxY = Mathf.Max(maxY, position.y);
        }
        //Returns the rect with the escess padding
        return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
    }
    /*Centers the camera between all of the players*/
    Vector3 CalculateCameraPosition(Rect boundingBox)
    {
        Vector2 boundingBoxCenter = boundingBox.center;

        return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, gameCam.transform.position.z);
    }
    /*Calculate the orthographics size of the camera*/
    float CalculateOrthographicSize(Rect boundingBox)
    {
        //Get the current size
        float orthographicSize = gameCam.orthographicSize;
        Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
        Vector3 topRightAsViewport = gameCam.WorldToViewportPoint(topRight);

        if (topRightAsViewport.x >= topRightAsViewport.y)
            orthographicSize = Mathf.Abs(boundingBox.width) / gameCam.aspect / 2f;
        else
            orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

        return Mathf.Clamp(Mathf.Lerp(gameCam.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
    }


    public void ChangeCameraMode()
    {
        if(camType == CameraType.LockOn)
        {
            camType = CameraType.ViewAll;
        }else
        {
            camType = CameraType.LockOn;
        }
    }
}
