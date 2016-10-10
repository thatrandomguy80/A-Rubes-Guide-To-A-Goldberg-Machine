using UnityEngine;
using System.Collections;

public class ObstacleInteraction : MonoBehaviour {

	public virtual void Interact()
    {
        print("Player interacted with " + gameObject.name);
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
    public virtual void Interact(Vector3 input) {
        print("Player interacted with " + gameObject.name + "At Location" + input.ToString());
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

    }
}
