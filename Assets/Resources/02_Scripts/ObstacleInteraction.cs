using UnityEngine;
using System.Collections;

public class ObstacleInteraction : MonoBehaviour {

	public virtual void Interact()
    {
        print("Player interacted with " + gameObject.name);
    }
}
