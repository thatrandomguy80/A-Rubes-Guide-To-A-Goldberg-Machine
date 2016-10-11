using UnityEngine;
using System.Collections;

public class RotatingObject : MonoBehaviour
{
    
    [Header("In degrees per tick")]
    public Vector3 rotationSpeeds;

	public RotationTypes Rotation = RotationTypes.Global;

	public enum RotationTypes{
		Local,Global
	};

    // Update is called once per frame
    void Update()
    {
		if (Rotation == RotationTypes.Global) {
			transform.Rotate (rotationSpeeds * Time.deltaTime, Space.World);
		} else {
			transform.Rotate (rotationSpeeds * Time.deltaTime);
		}
    }
}
