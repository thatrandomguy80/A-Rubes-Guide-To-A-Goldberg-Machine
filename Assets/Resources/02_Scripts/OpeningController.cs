using UnityEngine;
using System.Collections;

public class OpeningController : MonoBehaviour {

    public GameObject curtains;

    void Start()
    {
        //StartCoroutine(OpenCurtains(2));
    }
    void Update()
    {
        SkinnedMeshRenderer curtainMesh = curtains.GetComponent<SkinnedMeshRenderer>();

        curtainMesh.SetBlendShapeWeight(1, Mathf.Lerp(curtainMesh.GetBlendShapeWeight(1), 100, Time.deltaTime));
    }
        
    IEnumerator OpeningAnimation()
    {

        yield return new WaitForEndOfFrame();
        OpenCurtains(2);

    }
    IEnumerator OpenCurtains(float speed)
    {
        SkinnedMeshRenderer curtainMesh = curtains.GetComponent<SkinnedMeshRenderer>();

        float time = 0;
        while(time < 1)
        {
            time += Time.deltaTime/speed;
            curtainMesh.SetBlendShapeWeight(1, time * 100);
            
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
    }
}
