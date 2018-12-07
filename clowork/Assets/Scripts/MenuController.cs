using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public void EnterGameScene()
    {
        GameManager.Instance.LoadScene("GameScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
