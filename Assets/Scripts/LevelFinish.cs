using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{

    public string nextScene;
    public float timerResult;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Egg"))
        {

            Debug.Log("LEVEL FINISH");
           var singl = LevelProgressSingleton.Instance;
           singl.NextLevelName = nextScene;
           singl.timer = timerResult;

           SceneController.LoadSuccess();
        }
    }
}
