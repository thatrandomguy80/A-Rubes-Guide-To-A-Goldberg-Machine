using UnityEngine;
using System.Collections;

public class MathExt : MonoBehaviour {

    public static float getAngle(Vector3 pivotPoint, Vector3 rotateTowards) {
        Vector3 verticalRefPoint = pivotPoint + Vector3.up;//Set a point directly up from the pivotPoint
        return getAngle(pivotPoint, verticalRefPoint, rotateTowards);
    }

    public static float getAngle(Vector3 pivotPointPos, Vector3 referencePoint, Vector3 rotateTowardsPos) {
        float angle = Vector2.Angle(pivotPointPos - referencePoint, rotateTowardsPos - pivotPointPos);//Calculate the angle
        return angle;
    }
}
