using UnityEngine;
using System.Collections;

public class DeathBarrier : GameState {

    void Start()
    {
        //Hides the barrier
        hideArea();
    }
    //Hides the defined area of play
    private void hideArea()
    {
        MeshRenderer meshRen = GetComponent<MeshRenderer>();
        meshRen.enabled = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //If the player exits the area they lose
        if (other.transform.tag.Equals("Player"))
        {
            EndGame.Lose();
        }
    }
}
