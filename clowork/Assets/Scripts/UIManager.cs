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
    public GameObject GamePauseScreen;
    public GameObject Hammer;

    #region SCORE
    public TextMeshProUGUI scoreText, highScoreText;
    #endregion

    public Dictionary<System.Guid, GameObject> PrefabList;
    private int numberOfTask;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        PrefabList = new Dictionary<System.Guid, GameObject>();
    }

    public void AddUiTask(Task task)
    {
        if (numberOfTask >= 8)
            return;

        TaskAttributes taskAttr = Instantiate(TaskPrefab, List.transform).GetComponent<TaskAttributes>();
        PrefabList.Add(task.TaskId, taskAttr.gameObject);
        taskAttr.AddTaskAttributes(task);
        numberOfTask++;
    }

    public void RemoveUiTask(System.Guid id)
    {
        if (PrefabList.ContainsKey(id))
        {
            Hammer.GetComponent<Animator>().Play("Hammer");
            GameObject temp = PrefabList[id];
            Destroy(temp);
            PrefabList.Remove(id);
            numberOfTask--;
        }
        else
            Debug.LogWarning("Unidentified Guid");
    }

    private void ReloadGameScene()
    {
        GameManager.Instance.LoadScene("GameScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver(int score)
    {
        GameOverScreen.SetActive(true);
        highScoreText.text = GameManager.Instance.GetHighScore(score).ToString();
        scoreText.text = score.ToString();
    }

    public void GamePaused(bool p)
    {
        GamePauseScreen.SetActive(p);
    }

    public void PlayHoverSound(Button inButton)
    {
        inButton.GetComponent<AudioSource>().Play();
    }
}
