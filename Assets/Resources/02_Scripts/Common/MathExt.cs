using UnityEngine;
using System.Collections;

public class MathExt : MonoBehaviour {



	/*Get the Angle from from two points*/
    public static float getAngle(Vector3 pivotPoint, Vector3 rotateTowards) {
        Vector3 verticalRefPoint = pivotPoint + Vector3.up;//Set a point directly up from the pivotPoint
        return getAngle(pivotPoint, verticalRefPoint, rotateTowards);
    }
    public static float getAngle(Vector3 pivotPointPos, Vector3 referencePoint, Vector3 rotateTowardsPos) {
        float angle = Vector2.Angle(pivotPointPos - referencePoint, rotateTowardsPos - pivotPointPos);//Calculate the angle
        return angle;
    }

	//Converts the RGB number to dec
	public static Color rgbToDec(float r,float g, float b){
		float conv = 255;
		return new Color(r / conv, g / conv, b / conv);
	}

    //Combines two lists together
    public static GameObject[] Concat(GameObject[] list1,GameObject[] list2)
    {
        GameObject[] newList = new GameObject[list1.Length + list2.Length];

        for(int i = 0; i < list1.Length; i++)
        {
            newList[i] = list1[i];
        }
        for(int i = list1.Length;i< newList.Length; i++)
        {
            newList[i] = list2[i - list1.Length];
        }

        return newList;
    }
}
