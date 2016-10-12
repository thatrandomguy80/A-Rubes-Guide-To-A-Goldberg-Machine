using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwipeTutController : MonoBehaviour {

    Image img;
    float Timer;
    void Start() {
        img = GetComponent<Image>();
        Timer = Time.time;
    }

    // Update is called once per frame
    void Update() {
        float val = Mathf.Lerp(0, 1, Mathf.PingPong(Mathf.Sin(Time.time * 2), 1));
        //val = 
        img.fillAmount = val;
    }
}
