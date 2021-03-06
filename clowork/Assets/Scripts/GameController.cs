﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    #region LIVES
    [Header("Lives")]
    public int GameLives;
    public UnityEngine.UI.Image[] livesImages;
    public Color32 darkenedColour;
    public AudioSource wrongSound;
    #endregion

    #region BUTTON
    [Header("Button")]
    public UnityEngine.UI.Image ImgA;
    public UnityEngine.UI.Image ImgS, ImgD, ImgF, ImgJ, ImgK, ImgL, ImgSemi;
    public Color32 buttonNoPressed, buttonPressed;
    public AudioSource buttonSound;
    #endregion

    [Header("Reminder Sounds")]
    public AudioSource reminderSound;
    public AudioClip sound1, sound2, sound3, sound4, sound5, sound6, sound7, sound8;

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
            foreach (KeyValuePair<KeyCode, List<System.Guid>> x in listOfTaskAtCurrentTime)
            {
                for (int i = 0; i < x.Value.Count; i++)
                {
                    UIManager.Instance.RemoveUiTask(x.Value[i]);
                }
            }
        }
        listOfTaskAtCurrentTime = new Dictionary<KeyCode, List<System.Guid>>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) { isPaused = !isPaused;    UIManager.Instance.GamePaused(isPaused);
                                                                                                            TimeManager.Instance.PauseGame(isPaused);
                                                                                                            TaskController.Instance.SetPaused(isPaused); }

        if (isPaused)
            return;

        if (Input.GetKeyDown(KeyCode.A)) { onKeyPressed(KeyCode.A); ImgA.color = buttonPressed; }
        if (Input.GetKeyUp(KeyCode.A)) { ImgA.color = buttonNoPressed; }

        if (Input.GetKeyDown(KeyCode.S)) { onKeyPressed(KeyCode.S); ImgS.color = buttonPressed; }
        if (Input.GetKeyUp(KeyCode.S)) { ImgS.color = buttonNoPressed; }
        
        if (Input.GetKeyDown(KeyCode.D)) { onKeyPressed(KeyCode.D); ImgD.color = buttonPressed; }
        if (Input.GetKeyUp(KeyCode.D)) { ImgD.color = buttonNoPressed; }

        if (Input.GetKeyDown(KeyCode.F)) { onKeyPressed(KeyCode.F); ImgF.color = buttonPressed; }
        if (Input.GetKeyUp(KeyCode.F)) { ImgF.color = buttonNoPressed; }

        if (Input.GetKeyDown(KeyCode.J)) { onKeyPressed(KeyCode.J); ImgJ.color = buttonPressed; }
        if (Input.GetKeyUp(KeyCode.J)) { ImgJ.color = buttonNoPressed; }

        if (Input.GetKeyDown(KeyCode.K)) { onKeyPressed(KeyCode.K); ImgK.color = buttonPressed; }
        if (Input.GetKeyUp(KeyCode.K)) { ImgK.color = buttonNoPressed; }

        if (Input.GetKeyDown(KeyCode.L)) { onKeyPressed(KeyCode.L); ImgL.color = buttonPressed; }
        if (Input.GetKeyUp(KeyCode.L)) { ImgL.color = buttonNoPressed; }

        if (Input.GetKeyDown(KeyCode.Semicolon)) { onKeyPressed(KeyCode.Semicolon); ImgSemi.color = buttonPressed; }
        if (Input.GetKeyUp(KeyCode.Semicolon)) { ImgSemi.color = buttonNoPressed; }

        
    }

    private void onKeyPressed(KeyCode code)
    {
        //Debug.Log("Entered via " + code.ToString());
        buttonSound.Play();
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
            
            switch(code)
            {
                case KeyCode.A:
                    reminderSound.PlayOneShot(sound1);
                    break;
                case KeyCode.S:
                    reminderSound.PlayOneShot(sound2);
                    break;
                case KeyCode.D:
                    reminderSound.PlayOneShot(sound3);
                    break;
                case KeyCode.F:
                    reminderSound.PlayOneShot(sound4);
                    break;
                case KeyCode.J:
                    reminderSound.PlayOneShot(sound5);
                    break;
                case KeyCode.K:
                    reminderSound.PlayOneShot(sound6);
                    break;
                case KeyCode.L:
                    reminderSound.PlayOneShot(sound7);
                    break;
                case KeyCode.Semicolon:
                    reminderSound.PlayOneShot(sound8);
                    break;
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
        livesImages[currentLives].color = darkenedColour;
        wrongSound.Play();
        if (currentLives < 1)
        {
            UIManager.Instance.GameOver(TaskController.Instance.GetScore());
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
