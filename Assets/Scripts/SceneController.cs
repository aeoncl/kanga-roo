using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void LoadFirstScene()
    {
        SceneManager.LoadScene("1_Level1");
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("0MainMenu");
    }    

    public static void LoadSuccess()
    {
        SceneManager.LoadScene("LevelSuccess", LoadSceneMode.Additive);
    }

    public static void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public static void LoadGameOverScene()
    {
        SceneManager.LoadScene("0_GameOver");
    }
}