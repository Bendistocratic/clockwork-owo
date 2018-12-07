using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    private static TimeManager _instance;
    public static TimeManager Instance { get { return _instance; } }

    #region TIME
    private const int TIME_UNIT = 12;

    public float IntervalBetweenEachMinute;

    private List<System.Action>[][] timeBucketList; // [hour][minute] - minute in intervals of 5
    private int currentHour, currentMinute;
    private bool hasGameStarted, isPaused;
    private float currentTime;
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this);

        initTimeManager();
    }

    private void Update()
    {
        if (hasGameStarted && !isPaused)
        {
            if (currentTime > IntervalBetweenEachMinute)
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
}
