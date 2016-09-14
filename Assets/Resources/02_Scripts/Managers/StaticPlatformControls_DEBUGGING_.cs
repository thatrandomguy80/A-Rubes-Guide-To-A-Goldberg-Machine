using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(StaticPlatformControls))]
[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class StaticPlatformControls_DEBUGGING_ : MonoBehaviour {

    private GameObject anchors;
    private GameControls controller;
    private LineRenderer debuggingLine;
    void Start() {
        anchors = this.transform.parent.GetChild(0).gameObject;
        debuggingLine = GetComponent<LineRenderer>();
    }

    //Displys where the platform will be spawned on play
    private void displayPlatform() {
        if (checkDebugging()) {
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
        return GameObject.Find("Game Managment").GetComponent<GameControls>().Debugging;
    }

    void Update() {
        displayPlatform();
    }
}
