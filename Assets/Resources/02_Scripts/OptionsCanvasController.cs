using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsCanvasController : MonoBehaviour {

	public void MoveToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void EraseAllData()
    {
        PlayerPrefs.DeleteAll();
    }

    void Update()
    {

    }

    public void EnableOrDisableMetrics()
    {
        GameObject currButton = EventSystem.current.currentSelectedGameObject;
        Text buttonText = currButton.transform.GetChild(0).GetComponent<Text>(); 
        GameState.PlayTestMetrics.Active = !GameState.PlayTestMetrics.Active;
        buttonText.text = "Metrics <" + GameState.PlayTestMetrics.Active + ">";
    }
}
