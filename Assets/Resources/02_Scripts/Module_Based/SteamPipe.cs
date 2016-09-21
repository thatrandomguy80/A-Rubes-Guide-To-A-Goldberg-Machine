using UnityEngine;
using System.Collections;

public class SteamPipe : MonoBehaviour {

    public ParticleSystem steam;//Gets the steam from the pipe
    private GameObject dir;//Holds the game object that is used to give a direction
    public float power = 50;//How powerful is the steam
    public float distance = 5;//How long is the steam

    private bool createdExcessSteam;


    

	// Use this for initialization
	void Start () {

        createdExcessSteam = false;
        


        steam.startSpeed = distance;
        CURRENT_STEAM_DIST = distance;
        dir = transform.GetChild(2).gameObject;
        dir.transform.localPosition = new Vector3(0, distance, 0);

        rayStart = transform.position;
        rayDir = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.up;


    }

    protected void DrawRay()
    {
        

        //Cast a ray in the position and direction given
        RaycastHit2D hit2d = Physics2D.Raycast(rayStart, rayDir, CURRENT_STEAM_DIST);
        Ray2D ray2d = new Ray2D(rayStart, rayDir);
        Debug.DrawRay(ray2d.origin,ray2d.direction,Color.cyan,0.5f);

        //If the object hits the collider
        if (hit2d.collider != null)
        {

            Rigidbody2D rig2d = hit2d.transform.GetComponent<Rigidbody2D>();
            rayHit = hit2d.point;
            //If the ray hits the player
            if (hit2d.transform.tag.Equals("Player"))
            {
                //Push the ball away
                rig2d.AddForce(rayDir * power);
                print("Steam hit the ball");
            }
            else
            {
                if (!createdExcessSteam)
                {
                    //CreateExcess(hit2d);
                    print("Deflect off " + hit2d.transform.name);
                    createdExcessSteam = true;
                }
            }
        }
    }

    private void CreateExcess(RaycastHit2D hit2d)
    {
        float distDiff = Vector2.Distance(rayStart, rayHit);
        Vector2 incomingVec = (rayHit - rayStart).normalized;
        Vector2 rStart = hit2d.point;//Move new point to where ray hit
        Vector2 rDir = Vector2.Reflect(incomingVec, hit2d.normal); //Deflects the ray

        float remainingSteam = CURRENT_STEAM_DIST - distDiff;
        GameObject excessSteam = new GameObject("ExcSteam");
        excessSteam.transform.position = rStart;
        excessSteam.transform.SetParent(transform);
        excessSteam.AddComponent<ExcessSteam>();
        ExcessSteam excSteam = excessSteam.GetComponent<ExcessSteam>();
        excSteam.rayStart = rStart;
        excSteam.rayDir = rDir;
        excSteam.rSteam = remainingSteam;
        excSteam.power = power;
    }

    public Vector2 rayStart,rayHit, rayDir;
    protected float CURRENT_STEAM_DIST;

	// Update is called once per frame
	void Update () {

        DrawRay();

    }
}
