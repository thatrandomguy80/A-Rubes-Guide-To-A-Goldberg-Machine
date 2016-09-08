using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(StaticPlatformControls))]
[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class StaticPlatformControls_DEBUGGING_ : MonoBehaviour {

    //Keep track of debugging mode
    public bool DEBUGGING_MODE;

    private GameObject anchors;
    private GameControls controller;
    private LineRenderer debuggingLine;
    void Start() {
        anchors = this.transform.parent.GetChild(0).gameObject;
        debuggingLine = GetComponent<LineRenderer>();
        controller = GameObject.Find("Ball").transform.GetChild(1).GetComponent<GameControls>();
    }

    //Displys where the platform will be spawned on play
    private void displayPlatform() {
        if (DEBUGGING_MODE) {
            debuggingLine.enabled = true;
            //get amount of joints
            int amount = anchors.transform.childCount;
            if (Application.isPlaying) {
                amount -= 3;
            }

            if (amount >= 0) {
                debuggingLine.SetVertexCount(amount);
                //run through each joint pair and draw them
                for (int i = 0; i < amount; i++) {
                    //results[i] = joints[i].transform.position;
                    debuggingLine.SetPosition(i, anchors.transform.GetChild(i).position);
                }
            }
        } else {
            debuggingLine.enabled = false;
        }
    }

    public bool checkDebugging() {
        return GameControls.Debugging;
    }

    void Update() {
        checkDebugging();
        displayPlatform();
    }
}
