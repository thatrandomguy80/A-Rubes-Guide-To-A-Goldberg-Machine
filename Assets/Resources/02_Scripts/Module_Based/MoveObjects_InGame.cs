using UnityEngine;
using System.Collections;

public class MoveObjects_InGame : MonoBehaviour {
    /*
     * Controls the movement of objects in game
     **/

    //Holds the object that can be moved
    public GameObject moveableObj;

    //Gets the starting point and the end point of the movement
    public Vector2 startPos, endPos;
    public float speed; //Keeps track of how fast it is moving
    public bool moveTowardsEnd; //Checks weather its moving to the end point or the starting point
	
    void Start()
    {
        //Puts the point straight in the middle of the end and start
        transform.localPosition = Vector3.Lerp(startPos, endPos, 0.5f);
    }

	void Update () {
        MovementBetweenTwoPoints();
        ObjToCurrentPos();
	}
    //Moves object selected to where the current pos is
    void ObjToCurrentPos()
    {
        moveableObj.transform.position = transform.position;
    }

    //Configures movement between two points
    void MovementBetweenTwoPoints()
    {
        if (moveTowardsEnd)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPos, speed*Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos, speed * Time.deltaTime);
        }
        float distThresh = 0.01f;
        if(Vector3.Distance(transform.localPosition,startPos) <= distThresh || Vector3.Distance(transform.localPosition, endPos) <= distThresh)
        {
            moveTowardsEnd = !moveTowardsEnd;
        }
    }
}
