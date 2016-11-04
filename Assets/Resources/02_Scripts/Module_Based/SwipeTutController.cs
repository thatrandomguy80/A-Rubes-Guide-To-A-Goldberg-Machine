using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwipeTutController : MonoBehaviour {

    Image img;
    float Timer;
    void Start() {
        img = GetComponent<Image>();
        Timer = Time.time;
        if (!(Application.loadedLevelName == "W1-1 (first)" || Application.loadedLevelName == "W1-2 (multicut tut)" || Application.loadedLevelName == "W1-3 (zoom tut)")){
            
            transform.parent.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        float val = Mathf.Lerp(0, 1, Mathf.PingPong(Mathf.Sin(Time.time * 2), 1));
        //val = 
        img.fillAmount = val;
    }
}
