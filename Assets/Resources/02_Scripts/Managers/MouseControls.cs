using UnityEngine;
using System.Collections;

public class MouseControls : MonoBehaviour {


    //Cast a sphere around the point where the mouse pressed
    //If there are any interactive objects inside activate them
    private void Spherecast(float radius)
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hit = Physics.SphereCastAll(ray, radius);
            for (int i = 0; i < hit.Length; i++)
            {
                InteractWithObject(hit[i]);
            }
        }
    }
    //Handles Object Interaction
    private void InteractWithObject(RaycastHit hit)
    {
        
        ObstacleInteraction newObject = hit.transform.GetComponent<ObstacleInteraction>();
        //If the object has a script that inherits the ObstacleInteraction script
        if (newObject != null)
        {
            //It will process the action
            //if (newObject.gameObject.GetComponent<BridgeSuspender>() != null) {//if it's a suspender send hit location(world space)
                newObject.Interact(hit.point);
            //} else {//default
                //newObject.Interact();
            //}
        }
    }
	
	// Update is called once per frame
	void Update () {
        float radiusOfCast = 0.5f;
        Spherecast(radiusOfCast);
    }
}
