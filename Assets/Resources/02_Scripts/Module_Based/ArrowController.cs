using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
    private MeshRenderer m;
	// Use this for initialization
	void Start () {
        m = GetComponent<MeshRenderer>();
        m.enabled = false;
       
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(0, -7, -5);
        Vector3 diff = GameObject.Find("Bucket").transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        if (Input.GetMouseButtonDown(1)) {
            
            StartCoroutine("arrow");
        }
	}

    IEnumerator arrow() {
        Debug.Log("RMB");
        m.enabled = true;
        yield return new WaitForSeconds(5);
        m.enabled = false;
        Debug.Log("RMB after");
    }
}
