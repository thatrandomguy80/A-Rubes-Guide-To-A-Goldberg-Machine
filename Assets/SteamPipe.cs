using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SteamPipe : MonoBehaviour {

    public ParticleSystem part;
    public GameObject dir;
    private float power;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        float dist = Vector2.Distance(transform.position, dir.transform.position);
        //Direction ray should be facing
        Vector2 direction = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.up;
        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, direction, dist);
        part.startSpeed = dist;

        if(hit2d.collider != null)
        {
           
            print(hit2d.transform.name);
            if (hit2d.transform.name.Equals("Ball"))
            {
                hit2d.transform.GetComponent<Rigidbody2D>().AddForceAtPosition(direction, hit2d.transform.position);
            }
        }

    }
}
