using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelSelectionButton : MonoBehaviour {

	public int level;//Level button will load too
    public bool hover;//Is the mouse hovering over the button
    public float lerp;//For linear interpolation

    void Start()
    {
        lerp = 0;
        hover = false;
		RotatingObject rot = gameObject.AddComponent<RotatingObject> ();
		rot.rotationSpeeds = new Vector3 (0, 0, 10);

    }
    //If the player hovers over the button it will move forward
    private void MouseHover()
    {
        float speed = Time.deltaTime * 5;
        if (hover)
        {
            lerp += speed;
        }
        else
        {
            lerp -= speed;
        }
        lerp = Mathf.Clamp01(lerp);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Lerp(0, -0.5f, lerp));
    }
    void Update()
    {
        MouseHover();
    }

    void OnMouseEnter()
    {
        hover = true;
    }
    void OnMouseExit()
    {
        hover = false;
    }

	//On mouse Click Load new level
	void OnMouseDown(){
		LoadLevel (level);
	}
    //Select level to load
    private void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
    }
}
