using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public Canvas instructionsCanvas;
    public UnityEngine.UI.GraphicRaycaster instructionsRaycaster;

    public void EnterGameScene()
    {
        GameManager.Instance.LoadScene("GameScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnClickInstruction(bool isOpen)
    {
        instructionsCanvas.enabled = isOpen;
        instructionsRaycaster.enabled = isOpen;
    }

    public void PlayHoverSound(UnityEngine.UI.Button inButton)
    {
        inButton.GetComponent<AudioSource>().Play();
    }
}
