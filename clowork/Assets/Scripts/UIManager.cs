using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    public AudioClip HoverButtonSound;
    public GameObject List;
    public GameObject TaskPrefab;
    public GameObject GameOverScreen;

    public Dictionary<System.Guid, GameObject> PrefabList = new Dictionary<System.Guid, GameObject>();

    private TextMeshProUGUI taskText;
    private TextMeshProUGUI timeText;
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        if (TaskPrefab != null)
        {
            taskText = TaskPrefab.transform.Find("TaskText").GetComponent<TextMeshProUGUI>();
            timeText = TaskPrefab.transform.Find("TimerText").GetComponent<TextMeshProUGUI>();
            buttonText = TaskPrefab.transform.Find("GameButton").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        }
    }

    public void AddUiTask(Task task)
    {
        AddTaskPrefab(task.TaskId);
        AddTaskDescription(task.TaskDescription);
        AddTaskTime(task.Hour + ":" + (task.Minute * 5));
        switch(task.Code)
        {
            case KeyCode.A:
                AddButtonText("A");
                break;
            case KeyCode.S:
                AddButtonText("S");
                break;
            case KeyCode.D:
                AddButtonText("D");
                break;
            case KeyCode.F:
                AddButtonText("F");
                break;
            case KeyCode.J:
                AddButtonText("J");
                break;
            case KeyCode.K:
                AddButtonText("K");
                break;
            case KeyCode.L:
                AddButtonText("L");
                break;
            case KeyCode.Semicolon:
                AddButtonText(";");
                break;
        }
    }

    public void RemoveUiTask(System.Guid id)
    {
        if (PrefabList.ContainsKey(id))
        {
            GameObject temp = PrefabList[id];
            Destroy(temp);
            PrefabList.Remove(id);
        }
        else
            Debug.LogWarning("Unidentified Guid");
    }

    private void AddTaskPrefab(System.Guid inGuid)
    {
        GameObject taskPrefab = Instantiate(TaskPrefab, List.transform);
        PrefabList.Add(inGuid, taskPrefab);
    }

    private void AddTaskDescription(string inTaskDescription)
    {
        taskText.text = inTaskDescription;
    }

    private void AddTaskTime(string inTaskTime)
    {
        timeText.text = inTaskTime;
    }

    private void AddButtonText(string inButtonText)
    {
        buttonText.text = inButtonText;
    }

    private void ReloadGameScene()
    {
        GameManager.Instance.LoadScene("GameScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
    }

    public void PlayHoverSound(Button inButton)
    {
        inButton.GetComponent<AudioSource>().Play();
    }
}
