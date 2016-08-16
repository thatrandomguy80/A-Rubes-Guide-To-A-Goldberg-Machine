using UnityEngine;
using System.Collections;

public class RotatingObject : MonoBehaviour
{
    [Header("In degrees per tick")]
    public float RotateSpeed = 2;//in degrees per tick
    [Header("On is clockwise")]
    public bool RotateDirection = true;

    private int dir = 1;

    // Update is called once per frame
    void Update()
    {
        if (!RotateDirection)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
        this.transform.Rotate(new Vector3(0, 0, RotateSpeed * dir * Time.deltaTime));
    }
}
