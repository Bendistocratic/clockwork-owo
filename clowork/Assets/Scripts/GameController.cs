using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    public int GameLives;

    private Dictionary<KeyCode, List<System.Guid>> listOfTaskAtCurrentTime;
    private int currentLives;

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

    public void AddEvents(KeyCode keyCode, System.Guid eventIndicator) // change buttonToPress int to enum
    {
        if (listOfTaskAtCurrentTime.ContainsKey(keyCode))
        {
            listOfTaskAtCurrentTime[keyCode].Add(eventIndicator);
        }
        else
        {
            List<System.Guid> tempList = new List<System.Guid>();
            tempList.Add(eventIndicator);
            listOfTaskAtCurrentTime.Add(keyCode, tempList);
        }
    }

    public void CheckForEventsLeft()
    {
        if (listOfTaskAtCurrentTime.Count > 0)
        {
            minusHealth();
        }
        listOfTaskAtCurrentTime = new Dictionary<KeyCode, List<System.Guid>>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { onKeyPressed(KeyCode.A); }

        if (Input.GetKeyDown(KeyCode.S)) { onKeyPressed(KeyCode.S); }

        if (Input.GetKeyDown(KeyCode.D)) { onKeyPressed(KeyCode.D); }

        if (Input.GetKeyDown(KeyCode.F)) { onKeyPressed(KeyCode.F); }

        if (Input.GetKeyDown(KeyCode.J)) { onKeyPressed(KeyCode.J); }

        if (Input.GetKeyDown(KeyCode.K)) { onKeyPressed(KeyCode.K); }

        if (Input.GetKeyDown(KeyCode.L)) { onKeyPressed(KeyCode.L); }

        if (Input.GetKeyDown(KeyCode.Semicolon)) { onKeyPressed(KeyCode.Semicolon); }
    }

    private void onKeyPressed(KeyCode code)
    {
        if (listOfTaskAtCurrentTime.ContainsKey(code))
        {
            List<System.Guid> temp = listOfTaskAtCurrentTime[code];
            listOfTaskAtCurrentTime.Remove(code);
            // Remove from list in scene
            Debug.LogWarning("Remove from list in scene not done.");
        }
        else
        {
            minusHealth();
        }
    }

    private void minusHealth()
    {
        currentLives--;
        // Update UI
        if (currentLives < 1)
        {
            Debug.Log("You Lose");
        }
    }

    private void Reset()
    {
        listOfTaskAtCurrentTime = new Dictionary<KeyCode, List<System.Guid>>();
        currentLives = GameLives;
    }
}
