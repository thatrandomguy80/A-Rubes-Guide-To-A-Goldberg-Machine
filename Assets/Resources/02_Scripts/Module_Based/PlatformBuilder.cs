﻿using UnityEngine;
using System.Collections;

public class PlatformBuilder : MonoBehaviour {
    [Header("This gets Mats from the Materail folder")]
    public string MatName = "Primary Color";
    public bool menu = false;

    private bool newledge = true;

    protected void CreatePlatform(GameObject leftAnchor, GameObject rightAnchor) {
        //Create a new Platform
        GameObject newPlatform;
        if (newledge)
        {
            GameObject prefab = Resources.Load("04_Prefabs/" + "ledge") as GameObject;
            newPlatform = Instantiate(prefab,Vector3.zero,Quaternion.identity) as GameObject;
        }
        else
        {
            newPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        newPlatform.name = "Platform (Display)";

        //Create a gameobject to hold the collider
        GameObject newPlatformCollider = new GameObject(newPlatform.name + " Collider");
        newPlatformCollider.transform.SetParent(newPlatform.transform);
        newPlatformCollider.AddComponent<BoxCollider2D>();

        //Remove the boxCollider
        Destroy(newPlatform.GetComponent<BoxCollider>());

        //Change the material to the one from the main platform
        if (!newledge)
        {
            Material temp = Resources.Load("03_Materials/" + MatName) as Material;
            if (temp != null)
            {
                newPlatform.transform.GetComponent<Renderer>().material = temp;
                newPlatform.transform.GetComponent<Renderer>().reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
                //Don't accept the balls reflection prob
            }
            else
            {
                Debug.LogError("Not a valid Mat name on: " + this.transform.parent.name);
            }
        }

        //Place the platform in the center
        transform.position = Vector2.Lerp(leftAnchor.transform.position, rightAnchor.transform.position, 0.5f);
        newPlatform.transform.position = Vector2.Lerp(leftAnchor.transform.position, rightAnchor.transform.position, 0.5f);

        //if (newledge)
        //{
        //    newPlatform.transform.Translate(0, -0.5f,0);
        //}



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

        //Set the anchors to the platform
        newPlatform.transform.SetParent(leftAnchor.transform.parent);

    }
}
