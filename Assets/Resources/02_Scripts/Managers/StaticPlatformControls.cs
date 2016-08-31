using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


[ExecuteInEditMode]
public class StaticPlatformControls : PlatformBuilder {
    public int numOfJoints = 2;
    private GameObject anchors;
    private GameObject JointPrefab;
    private List<GameObject> Joints;// scrip maintained list of joints that are in use
    private bool Built = false;//allows only one build per runtime

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

    //calls each joint pairs platform to be created
    private void Build() {
        int amount = anchors.transform.childCount;
        for (int i = 0; i < amount - 1; i++) {
            base.CreatePlatform(Joints.ElementAt(i), Joints.ElementAt(i + 1));//Look at scale setter
        }
        Built = !Built;
    }

    //adds all joints required and during runtime inst them properly
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

    //adds a joint to the object and list
    private void AddJoint() {

        if (anchors.name == "PlatformAnchors") {
            GameObject temp = Instantiate(JointPrefab, transform.parent.position, Quaternion.identity) as GameObject;
            temp.transform.Translate(new Vector3(0, 0, -1f));
            temp.transform.Rotate(new Vector3(90, 0, 0));
            temp.transform.parent = anchors.transform;
            Joints.Add(temp);
        } else {
            Debug.LogError("Couldn't Find PlatformAnchors object");
        }
    }

    //ensures that there are at least 2 joints
    private void Constrain() {
        //Joints.TrimExcess();
        if (numOfJoints < 2) {
            numOfJoints = 2;
        }
    }

}
