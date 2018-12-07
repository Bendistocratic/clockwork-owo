using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Level
{
    [Range(0, 8)]
    public int NumberOfButtons;
    public float NewTaskCountdown;
    public int NumberOfTaskClearedToNext;
    public float IntervalBetweenEachMinute;
}

public struct Task
{
    public int Hour;
    public int Minute;
    public System.Guid TaskId;
    public KeyCode Code;
    public string TaskDescription;
}

public class TaskController : MonoBehaviour {
    private static TaskController _instance;
    public static TaskController Instance { get { return _instance; } }

    public bool debugMode;
    public Level[] Levels;
    public string[] TaskDescription;

    private const int TIME_UNIT = 12;

    private int currentLevel, numberOfTasksCleared;
    private bool hasGameStarted;
    private float currentTime;
    private List<KeyCode> keyCodeList;
    private TimeManager tm;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        keyCodeList = new List<KeyCode>(8);
        keyCodeList.Add(KeyCode.A);
        keyCodeList.Add(KeyCode.S);
        keyCodeList.Add(KeyCode.D);
        keyCodeList.Add(KeyCode.F);
        keyCodeList.Add(KeyCode.J);
        keyCodeList.Add(KeyCode.K);
        keyCodeList.Add(KeyCode.L);
        keyCodeList.Add(KeyCode.Semicolon);
        keyCodeList.Shuffle();
    }

    private void Start()
    {
        Reset();
        tm = TimeManager.Instance;
        tm.SetNewTimeIntervalBetweenMinutes(Levels[currentLevel].IntervalBetweenEachMinute);
        hasGameStarted = true;
    }

    private void Update()
    {
        if (hasGameStarted)
        {
            if (currentTime < 0)
            {
                currentTime = Levels[currentLevel].NewTaskCountdown;
                tm.SetNewTimeIntervalBetweenMinutes(Levels[currentLevel].IntervalBetweenEachMinute);
                Task temp = generateTask(Levels[currentLevel].NumberOfButtons);
                tm.AddTimedEvent(() =>
                {
                    //Debug.Log("Fired" + temp.Code);
                    GameController.Instance.AddTasks(temp.Code, temp.TaskId);
                    //Debug.Log("Fired timed event");
                }, temp.Hour, temp.Minute);
                UIManager.Instance.AddUiTask(temp);

                if (debugMode)
                    hasGameStarted = false;
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
        currentTime = Levels[currentLevel].NewTaskCountdown;
    }

    public void AddNumberOfTasksCleared()
    {
        numberOfTasksCleared++;
        if (numberOfTasksCleared >= Levels[currentLevel].NumberOfTaskClearedToNext)
        {
            if (currentLevel + 1 < Levels.Length) // else max level reached
                currentLevel++;
        }
    }

    private Task generateTask(int numberOfButtons)
    {
        Task task;
        task.Hour = Random.Range(tm.GetCurrentHour(), tm.GetCurrentHour() + 3);
        task.Minute = Random.Range(tm.GetCurrentMinute() + 3, tm.GetCurrentMinute() + 5);
        if (task.Minute >= TIME_UNIT)
        {
            task.Minute %= TIME_UNIT;
            task.Hour++;
        }
        task.Hour %= TIME_UNIT;

        if (debugMode)
        {
            task.Hour = 0;
            task.Minute = 3;
        }

        task.TaskId = System.Guid.NewGuid();
        task.Code = getRandomKeyCode(numberOfButtons);
        task.TaskDescription = TaskDescription[Random.Range(0, TaskDescription.Length)];
        return task;
    }

    private KeyCode getRandomKeyCode(int numberOfButtons)
    {
        return keyCodeList[Random.Range(0, numberOfButtons)];
    }

    public void StopGame()
    {
        hasGameStarted = false;
    }
}
