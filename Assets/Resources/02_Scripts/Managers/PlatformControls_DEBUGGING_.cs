using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlatformControls))]
[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class PlatformControls_DEBUGGING_ : MonoBehaviour {

    //Keep track of debugging mode
    public bool DEBUGGING_MODE;

    public  Transform leftAnchor, rightAnchor;
    private LineRenderer debuggingLine;
    void Start()
    {
        debuggingLine = GetComponent<LineRenderer>();
    }

    //Displys where the platform will be spawned on play
    private void displayPlatform()
    {
        if (DEBUGGING_MODE)
        {
            debuggingLine.enabled = true;
            debuggingLine.SetPosition(0, leftAnchor.position);
            debuggingLine.SetPosition(1, rightAnchor.position);
        }
        else
        {
            debuggingLine.enabled = false;
        }
    }
    public bool checkDebugging() {
        return GameControls.Debugging;
    }
    void Update()
    {
        checkDebugging();
        displayPlatform();
    }
}
