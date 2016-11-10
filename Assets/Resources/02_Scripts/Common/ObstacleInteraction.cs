using UnityEngine;
using System.Collections;

public class ObstacleInteraction : MonoBehaviour {

    public virtual void Interact() {
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameObject tut = GameObject.Find("Tut");
        if (tut != null) {
            tut.SetActive(false);
        }
    }
    public virtual void Interact(Vector3 input) {
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameObject tut = GameObject.Find("Tut");
        if (tut != null) {
            tut.SetActive(false);
        }
    }
}
