using UnityEngine;
using System.Collections;

public class trailCollider : MonoBehaviour {

    public TrailRendererWith2DCollider parent; // set by trailrenderwith2dcollider 
    //Script used to handel collisions with the trail objects and call all mouse interactions accordingly

    void Start() {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Rope"), LayerMask.NameToLayer("Default"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Rope"), LayerMask.NameToLayer("Rope"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Rope"), LayerMask.NameToLayer("Reflection"));
    }
    void Update() {
    }

    /*public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Rope"))) {
            BridgeSuspender b = other.transform.GetComponent<BridgeSuspender>();
            if (b != null && parent != null) {
                result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                result = new Vector3(result.x, result.y - 2, 5);
                other.gameObject.layer = LayerMask.NameToLayer("Default");

                b.Interact(result);
            }
        }
    }
    */
}
