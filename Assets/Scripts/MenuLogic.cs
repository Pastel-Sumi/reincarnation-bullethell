using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public string LevelName;
    public string menuscene = "InfoScreen";
    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(LevelName);
    }

    public void InfoMenu()
    {
        SceneManager.LoadScene(menuscene);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Level2()
    {
        SceneManager.LoadScene("OnGame2");
    }
}
