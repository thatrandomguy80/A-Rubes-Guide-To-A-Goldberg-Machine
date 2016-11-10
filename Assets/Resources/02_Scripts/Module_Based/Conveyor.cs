using UnityEngine;
using System.Collections;

public class Conveyor : MonoBehaviour {


    public float speed; //Keeps track of how fast it is moving
    public bool moveTowardsEnd; //Checks weather its moving to the end point or the starting point
    public Material conMat;
    public bool x, y;
    float xoffset = 0f, yoffset = 0;
    // Use this for initialization
    void Start () {
        conMat.SetTextureOffset("_MainTex", new Vector2(0, 0));
	}
	
	// Update is called once per frame
	void Update () {
        if (moveTowardsEnd)
        {
            xoffset += Time.deltaTime * speed;
            yoffset += Time.deltaTime * speed;
        }
        else
        {
            xoffset -= Time.deltaTime * speed;
            yoffset -= Time.deltaTime * speed;
        }
        conMat.SetTextureOffset("_MainTex", new Vector2(xoffset * System.Convert.ToInt32(x), yoffset * System.Convert.ToInt32(y)));
    }
}
