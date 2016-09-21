using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlatformControls))]
[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class PlatformControls_DEBUGGING_ : MonoBehaviour {

    public Transform leftAnchor, rightAnchor;
    private LineRenderer debuggingLine;
    void Start() {
        debuggingLine = GetComponent<LineRenderer>();
    }

    //Displys where the platform will be spawned on play
    private void displayPlatform() {
        if (checkDebugging()) {
            debuggingLine.enabled = true;
            debuggingLine.SetPosition(0, leftAnchor.position);
            debuggingLine.SetPosition(1, rightAnchor.position);
        } else {
            debuggingLine.enabled = false;
        }
    }
    public bool checkDebugging() {
        return GameObject.Find("Game Managment").GetComponent<GameControls>().Debugging;
    }
    void Update() {
        displayPlatform();
    }
}
