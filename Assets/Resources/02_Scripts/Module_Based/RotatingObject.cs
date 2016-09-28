using UnityEngine;
using System.Collections;

public class RotatingObject : MonoBehaviour
{
    
    [Header("In degrees per tick")]
    public Vector3 rotationSpeeds;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeeds*Time.deltaTime,Space.World);
    }
}
