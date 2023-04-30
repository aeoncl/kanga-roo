using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void LoadFirstScene()
    {
        SceneManager.LoadScene("EggMovementPOC");
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
        SceneManager.LoadScene("GameOver");
    }
}