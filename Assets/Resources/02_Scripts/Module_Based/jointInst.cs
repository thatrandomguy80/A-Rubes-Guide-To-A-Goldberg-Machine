using UnityEngine;
using System.Collections;

public class jointInst : MonoBehaviour {

    // moves joints forward to avoid render issue
    public float amount = 0.25f;

    void Start() {
        transform.Translate(0, 0, amount);
    }


}
