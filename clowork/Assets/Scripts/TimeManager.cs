using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    private static TimeManager _instance;
    public static TimeManager Instance { get { return _instance; } }

    #region TIME
    private const int TIME_UNIT = 12;

    private List<System.Action>[][] timeBucketList; // [hour][minute] - minute in intervals of 5
    private int currentHour, currentMinute;
    private bool hasGameStarted, isPaused;
    private float currentTime, intervalBetweenEachMinute;

    public AudioSource tickSound;
    #endregion

    public ClockController ClockController;
    public GameController GameController;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        initTimeManager();
    }

    private void Start()
    {
        hasGameStarted = true;
    }

    private void Update()
    {
        if (hasGameStarted && !isPaused)
        {
            if (currentTime > intervalBetweenEachMinute)
            {
                currentTime = 0;
                currentMinute++;
                if (currentMinute >= TIME_UNIT)
                {
                    currentMinute = 0;
                    currentHour++;
                    if (currentHour >= TIME_UNIT)
                    {
                        currentHour = 0;
                    }
                }
                GameController.Instance.CheckForEventsLeft();
                ClockController.SetHourHand(currentHour);
                ClockController.SetMinuteHand(currentMinute);
                tickSound.Play();
                fireTimedEvents();
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    public void AddTimedEvent(System.Action callback, int hour, int minute)
    {
        if (hour >= TIME_UNIT || minute >= TIME_UNIT)
        {
            Debug.LogError("Hour and/or minute is more than " + TIME_UNIT + "! (Time start from 0)");
            return;
        }

        timeBucketList[hour][minute].Add(callback);
    }

    private void fireTimedEvents()
    {
        for (int i = 0; i < timeBucketList[currentHour][currentMinute].Count; i++)
        {
            timeBucketList[currentHour][currentMinute][i]();
        }
        timeBucketList[currentHour][currentMinute] = new List<System.Action>(); // clean list
    }

    private void initTimeManager()
    {
        timeBucketList = new List<System.Action>[TIME_UNIT][];
        for (int i = 0; i < TIME_UNIT; i++)
        {
            timeBucketList[i] = new List<System.Action>[TIME_UNIT];
            for (int j = 0; j < TIME_UNIT; j++)
            {
                timeBucketList[i][j] = new List<System.Action>();
            }
        }

        currentHour = currentMinute = 0;
        currentTime = 0f;
        hasGameStarted = isPaused = false;
    }

    public void GameStart()
    {
        hasGameStarted = true;
    }

    public void PauseGame(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    public int GetCurrentMinute()
    {
        return currentMinute;
    }

    public int GetCurrentHour()
    {
        return currentHour;
    }

    public void SetNewTimeIntervalBetweenMinutes(float interval)
    {
        intervalBetweenEachMinute = interval;
    }

    public void StopGame()
    {
        hasGameStarted = false;
    }
}
