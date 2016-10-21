using UnityEngine;
using System.Collections;

public class OpeningController : MonoBehaviour {
    public GameObject canvas;
    public GameObject curtains;
    public bool curtainOpening;
    private static bool instanceCreated = false;
	public bool movingBackToMainMenu;
	public GameObject title;

    public static OpeningController instance;


    void Start()
    {
		if (instanceCreated == false)
		{
			instance = this;
			instanceCreated = true;
			movingBackToMainMenu = false;
			DontDestroyOnLoad(transform.parent.gameObject);
			curtainOpening = false;
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
	public void CloseCurtain(){
		
		StartCoroutine (ICloseCurtain (1));
	}

    public void OpenCurtain()
    {
        curtainOpening = true;
		title.SetActive(false);
        canvas.SetActive(false);
    }


	IEnumerator ICloseCurtain(float curtainSpeed){
		curtainOpening = false;


		SkinnedMeshRenderer curtainMesh = curtains.GetComponent<SkinnedMeshRenderer>();

		title.gameObject.SetActive(true);
		canvas.SetActive(true);


		while (curtainMesh.GetBlendShapeWeight (1) > 1) {
			curtainMesh.SetBlendShapeWeight(1, Mathf.Lerp(curtainMesh.GetBlendShapeWeight(1), 0, Time.deltaTime*curtainSpeed));
			yield return new WaitForEndOfFrame ();
		}




	}

    public void DestroyCurtains()
    {
        instanceCreated = false;
        Destroy(transform.parent.gameObject);
    }

}
