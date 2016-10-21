using UnityEngine;
using System.Collections;

public class OpeningController : MonoBehaviour {
    public GameObject canvas;
    public GameObject curtains;
    public bool curtainOpening;
    private static bool instanceCreated = false;

    public static OpeningController instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        DontDestroyOnLoad(transform.parent.gameObject);
        curtainOpening = false;
        if (instanceCreated == false)
        {
            instanceCreated = true;
        }else
        {
            Destroy(transform.parent.gameObject);
        }
    }
    void Update()
    {
        if (curtainOpening)
        {
            SkinnedMeshRenderer curtainMesh = curtains.GetComponent<SkinnedMeshRenderer>();

            curtainMesh.SetBlendShapeWeight(1, Mathf.Lerp(curtainMesh.GetBlendShapeWeight(1), 100, Time.deltaTime));
        }
    }

    public void OpenCurtain()
    {
        curtainOpening = true;
        //transform.GetChild(1).gameObject.SetActive(false);
        //transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        canvas.SetActive(false);
    }

    public void DestroyCurtains()
    {
        instanceCreated = false;
        Destroy(transform.parent.gameObject);
    }

}
