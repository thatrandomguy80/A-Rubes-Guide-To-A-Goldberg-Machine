using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SteamPipe : MonoBehaviour {

    private ParticleSystem steam;//Gets the steam from the pipe
    private GameObject dir;//Holds the game object that is used to give a direction
    public float power = 50;//How powerful is the steam
    public float distance = 5;//How long is the steam

    

	// Use this for initialization
	void Start () {
        steam = transform.GetChild(1).GetComponent<ParticleSystem>();
        dir = transform.GetChild(2).gameObject;
        dir.transform.localPosition = new Vector3(0, distance, 0);
	}

    //Applys force to the ball
    void Steam()
    {
        //Direction ray should be facing
        Vector2 direction = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.up;
        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, direction, distance);
        steam.startSpeed = distance;
        if (hit2d.collider != null)
        {
            if (hit2d.transform.tag.Equals("Player"))
            {
                Rigidbody2D rig2d = hit2d.transform.GetComponent<Rigidbody2D>();
                rig2d.AddForce(direction*power);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        Steam();

    }
}
