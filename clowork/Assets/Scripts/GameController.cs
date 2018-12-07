using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    public int GameLives;

    private Dictionary<KeyCode, List<System.Guid>> listOfTaskAtCurrentTime;
    private int currentLives;
    private bool isPaused;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        Reset();
    }

    public void AddTasks(KeyCode keyCode, System.Guid eventIndicator) // change buttonToPress int to enum
    {
        //Debug.Log(listOfTaskAtCurrentTime.ContainsKey(keyCode));
        if (listOfTaskAtCurrentTime.ContainsKey(keyCode))
        {
            listOfTaskAtCurrentTime[keyCode].Add(eventIndicator);
        }
        else
        {
            //Debug.Log("Keycode no exist");
            List<System.Guid> tempList = new List<System.Guid>();
            tempList.Add(eventIndicator);
            listOfTaskAtCurrentTime.Add(keyCode, tempList);
            //Debug.Log(listOfTaskAtCurrentTime.Count);
        }
    }

    public void CheckForEventsLeft()
    {
        if (listOfTaskAtCurrentTime.Count > 0)
        {
            minusHealth();
        }
        //listOfTaskAtCurrentTime = new Dictionary<KeyCode, List<System.Guid>>();
    }

    private void Update()
    {
        if (isPaused)
            return;

        if (Input.GetKeyDown(KeyCode.A)) { onKeyPressed(KeyCode.A); }

        if (Input.GetKeyDown(KeyCode.S)) { onKeyPressed(KeyCode.S); }

        if (Input.GetKeyDown(KeyCode.D)) { onKeyPressed(KeyCode.D); }

        if (Input.GetKeyDown(KeyCode.F)) { onKeyPressed(KeyCode.F); }

        if (Input.GetKeyDown(KeyCode.J)) { onKeyPressed(KeyCode.J); }

        if (Input.GetKeyDown(KeyCode.K)) { onKeyPressed(KeyCode.K); }

        if (Input.GetKeyDown(KeyCode.L)) { onKeyPressed(KeyCode.L); }

        if (Input.GetKeyDown(KeyCode.Semicolon)) { onKeyPressed(KeyCode.Semicolon); }

        if (Input.GetKeyDown(KeyCode.Space)) { isPaused = !isPaused; }
    }

    private void onKeyPressed(KeyCode code)
    {
        //Debug.Log("Entered via " + code.ToString());
        if (listOfTaskAtCurrentTime.ContainsKey(code))
        {
            //Debug.Log("In If statement");
            List<System.Guid> temp = listOfTaskAtCurrentTime[code];
            listOfTaskAtCurrentTime.Remove(code);
            for (int i = 0; i < temp.Count; i++)
            {
                //Debug.Log("Key Pressed Id" + temp[i]);
                UIManager.Instance.RemoveUiTask(temp[i]);
                TaskController.Instance.AddNumberOfTasksCleared();
            }
        }
        else
        {
            /*
            foreach (KeyValuePair<KeyCode, List<System.Guid>> x in listOfTaskAtCurrentTime)
            {
                Debug.Log(x.Key);
            } */
            minusHealth();
        }
    }

    private void minusHealth()
    {
        currentLives--;
        // Update UI
        if (currentLives < 1)
        {
            UIManager.Instance.GameOver();
            TaskController.Instance.StopGame();
            TimeManager.Instance.StopGame();
            isPaused = true;
        }
    }

    private void Reset()
    {
        listOfTaskAtCurrentTime = new Dictionary<KeyCode, List<System.Guid>>();
        currentLives = GameLives;
        isPaused = false;
    }
}
