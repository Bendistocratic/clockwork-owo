using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAttributes : MonoBehaviour {
    public TMPro.TextMeshProUGUI taskText, timeText, buttonText;

    public void AddTaskAttributes(Task task)
    {
        AddTaskDescription(task.TaskDescription);
        AddTaskTime(task.Hour + ":" + (task.Minute * 5));

        switch (task.Code)
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
}
