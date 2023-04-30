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
        SceneManager.LoadScene("0_MainMenu");
    }    

    public static void LoadSuccess()
    {
        SceneManager.LoadScene("0_LevelSuccess");
    }

    public static void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public static void LoadRetry() {
        LoadSceneByName(LevelProgressSingleton.Instance.currentLevelName);
    }

    public static void LoadNextLevelOrTitleScreen() {
       var singl = LevelProgressSingleton.Instance;
       if (singl.IsLastLevel()) {
         LoadMainMenu();
         //Load end game menu
       } else {
        LoadSceneByName(singl.NextLevelName);
       }
    }

    public static void LoadGameOverScene()
    {
        SceneManager.LoadScene("0_GameOver");
    }

    internal static void LoadSceneByName(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
}