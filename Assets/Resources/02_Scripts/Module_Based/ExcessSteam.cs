using UnityEngine;
using System.Collections;

public class ExcessSteam : MonoBehaviour {

    public Vector3 rayStart, rayDir, rayHit;

    public float rSteam,power;
    private bool createdExcessSteam;

    void Start()
    {
        createdExcessSteam = false;
    }
    void Update()
    {
        DrawRay();
    }

    protected void DrawRay()
    {
        //Cast a ray in the position and direction given
        RaycastHit2D hit2d = Physics2D.Raycast(rayStart, rayDir, rSteam);
        Ray2D ray2d = new Ray2D(rayStart, rayDir);
        Debug.DrawRay(ray2d.origin, ray2d.direction, Color.cyan, 0.5f);

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
                    float distDiff = Vector2.Distance(rayStart, rayHit);
                    Vector2 incomingVec = (rayHit - rayStart).normalized;
                    Vector2 rStart = hit2d.point;//Move new point to where ray hit
                    Vector2 rDir = Vector2.Reflect(incomingVec, hit2d.normal); //Deflects the ray

                    float remainingSteam = rSteam - distDiff;


                    GameObject excessSteam = new GameObject("ExcSteam");
                    excessSteam.transform.SetParent(transform);
                    excessSteam.AddComponent<ExcessSteam>();
                    ExcessSteam excSteam = excessSteam.GetComponent<ExcessSteam>();
                    excSteam.rayStart = rStart;
                    excSteam.rayDir = rDir;
                    excSteam.rSteam = remainingSteam;

                    print("Deflect off " + hit2d.transform.name);
                    createdExcessSteam = true;
                }
            }
        }
    }
}
