using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public AudioClip HoverButtonSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddTaskPrefab()
    {

    }

    public void AddTaskDescription(string inTaskDescription)
    {

    }

    public void AddTaskNumber(string inTaskNumber)
    {

    }

    public void AddTaskTime(string inTaskTime)
    {

    }

    public void ReloadGameScene()
    {
        EnterGameScene();
    }

    public void EnterGameScene()
    {
        SceneManager.LoadScene("GameScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayHoverSound(Button inButton)
    {
        inButton.GetComponent<AudioSource>().Play();
    }
}
