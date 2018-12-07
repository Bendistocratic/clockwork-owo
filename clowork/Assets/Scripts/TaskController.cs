using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Level
{
    [Range(0, 8)]
    public int NumberOfButtons;
    public float NewTaskCountdown;
    public int NumberOfTaskCleared;
}

public struct Task
{
    public int Hour;
    public int Minute;
    public System.Guid TaskId;
    public KeyCode Code;
}

public class TaskController : MonoBehaviour {
    private static TaskController _instance;
    public static TaskController Instance { get { return _instance; } }

    public Level[] Levels;

    private const int TIME_UNIT = 12;

    private int currentLevel, numberOfTasksCleared;
    private bool hasGameStarted;
    private float currentTime;
    private List<KeyCode> keyCodeList;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        keyCodeList = new List<KeyCode>(8);
        keyCodeList[0] = KeyCode.A;
        keyCodeList[1] = KeyCode.S;
        keyCodeList[2] = KeyCode.D;
        keyCodeList[3] = KeyCode.F;
        keyCodeList[4] = KeyCode.J;
        keyCodeList[5] = KeyCode.K;
        keyCodeList[6] = KeyCode.L;
        keyCodeList[7] = KeyCode.Semicolon;
        keyCodeList.Shuffle();
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (hasGameStarted)
        {
            if (currentTime < 0)
            {
                currentTime = Levels[currentLevel].NewTaskCountdown;
                Task temp = generateTask(Levels[currentLevel].NumberOfButtons);
                TimeManager.Instance.AddTimedEvent(() =>
                {
                    GameController.Instance.AddTasks(temp.Code, temp.TaskId);
                }, temp.Hour, temp.Minute);
                // Add to Ui
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
    }

    private void Reset()
    {
        currentLevel = numberOfTasksCleared = 0;
    }

    public void AddNumberOfTasksCleared()
    {
        numberOfTasksCleared++;
        if (numberOfTasksCleared >= Levels[currentLevel].NumberOfTaskCleared)
        {
            if (currentLevel + 1 < Levels.Length) // else max level reached
                currentLevel++;
        }
    }

    private Task generateTask(int numberOfButtons)
    {
        Task task;
        task.Hour = Random.Range(0, TIME_UNIT);
        task.Minute = Random.Range(0, TIME_UNIT);
        task.TaskId = System.Guid.NewGuid();
        task.Code = getRandomKeyCode(numberOfButtons);
        return task;
    }

    private KeyCode getRandomKeyCode(int numberOfButtons)
    {
        return keyCodeList[Random.Range(0, numberOfButtons)];
    }
}
