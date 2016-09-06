﻿using UnityEngine;
using System.Collections;

public class trailCollider : MonoBehaviour {

    public TrailRendererWith2DCollider parent; // set by trailrenderwith2dcollider 

    //Script used to handel collisions with the trail objects and call all mouse interactions accordingly

    void Start() {
    }
    void Update() {
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Rope"))) {
            BridgeSuspender b = other.transform.GetComponent<BridgeSuspender>();
            if (b != null && parent !=null) {
                b.Interact(parent.currPos);
            }
        }
    }
}
