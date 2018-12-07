using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public AudioClip HoverButtonSound;
    public GameObject List;
    public GameObject TaskPrefab;

    public Dictionary<System.Guid, GameObject> PrefabList = new Dictionary<System.Guid, GameObject>();

    private TextMeshProUGUI taskText;
    private TextMeshProUGUI timeText;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        if (TaskPrefab != null)
        {
            taskText = TaskPrefab.transform.Find("TaskText").GetComponent<TextMeshProUGUI>();
            timeText = TaskPrefab.transform.Find("TimerText").GetComponent<TextMeshProUGUI>();
            buttonText = TaskPrefab.transform.Find("GameButton").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "GameScreen")
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                Debug.Log("Pause Game");
            }
            else
            {
                Time.timeScale = 1;
                Debug.Log("Unpause Game");
            }
        }
    }

    public void AddTaskPrefab(System.Guid inGuid)
    {
        GameObject taskPrefab = Instantiate(TaskPrefab, List.transform);
        PrefabList.Add(inGuid, taskPrefab);
    }

    public void AddTaskDescription(string inTaskDescription)
    {
        taskText.text = inTaskDescription;
    }

    public void AddTaskTime(string inTaskTime)
    {
        timeText.text = inTaskTime;
    }

    public void AddButtonText(string inButtonText)
    {
        buttonText.text = inButtonText;
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
