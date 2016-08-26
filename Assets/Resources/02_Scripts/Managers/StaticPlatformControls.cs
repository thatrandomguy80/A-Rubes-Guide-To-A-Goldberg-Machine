using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


[ExecuteInEditMode]
public class StaticPlatformControls : PlatformBuilder {
    public int numOfJoints = 2;
    private GameObject anchors;
    private GameObject JointPrefab;
    private List<GameObject> Joints;
    private bool Built = false;

    // Use this for initialization
    void Start() {
        anchors = this.transform.parent.GetChild(0).gameObject;
        Joints = new List<GameObject>();
        JointPrefab = Resources.Load("04_Prefabs/Joint") as GameObject;
        Constrain();

    }

    // Update is called once per frame
    void Update() {
        Constrain();
        CreateJoints();
        if (Application.isPlaying && !Built) {
            Build();
        }
    }

    private void Build() {
        int amount = anchors.transform.childCount;
        for (int i = 0; i < amount - 1; i++) {
            base.CreatePlatform(Joints.ElementAt(i), Joints.ElementAt(i + 1));//Look at scale setter
        }
        Built = !Built;
    }

    private void CreateJoints() {
        int amount = anchors.transform.childCount;

        if (Application.isPlaying) {//List was cleared from Start() reset it
            for (int i = 0; i < amount; i++) {
                Joints.Add(anchors.transform.GetChild(i).gameObject);
            }
        }
        for (int i = amount - numOfJoints; i < 0; i++) {
            AddJoint();
        }
    }

    private void AddJoint() {

        if (anchors.name == "PlatformAnchors") {
            GameObject temp = Instantiate(JointPrefab, transform.parent.parent.position, Quaternion.identity) as GameObject;
            temp.transform.Rotate(new Vector3(90, 0, 0));
            temp.transform.parent = anchors.transform;
            Joints.Add(temp);
        } else {
            Debug.LogError("Couldn't Find PlatformAnchors object");
        }
    }

    private void Constrain() {
        //Joints.TrimExcess();
        if (numOfJoints < 2) {
            numOfJoints = 2;
        }
    }
}
