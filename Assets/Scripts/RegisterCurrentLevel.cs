using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterCurrentLevel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        LevelProgressSingleton.Instance.currentLevelName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
