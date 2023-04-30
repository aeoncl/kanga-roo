using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{

    private float timeRemaining = 3;
    public bool timerIsRunning = false;

    private float elapsedSeconds;
    private bool elapsed = false;
    public Text timeText;

    private System.Timers.Timer timer;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        elapsedSeconds = timeRemaining;
        timerIsRunning = true;
        timer = new System.Timers.Timer(1000);
        timer.AutoReset = true;
        timer.Elapsed += (sender, args) => {
                this.elapsedSeconds-=1;
        };

        timer.Start();
    }
    // Update is called once per frame
    void Update()
    {
        DisplayTime(this.elapsedSeconds);

        if(this.elapsedSeconds == 0){
             timer.Stop();   
             Time.timeScale = 1;
             timeText.enabled = false;
        }
    }

     void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 0;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:0}", seconds);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        this.timer.Stop();
    }
}
