using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    private Text textComponent;
    private int currentDialogueIndex = 0;
    private string textToDraw;
    private int letterIndex = 0;
    private int fixedUpdateOffset = 0;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private List<string> dialogue = new List<string>()
    {
        "Hey, you!",
        "Have you ever ordered something and thought...",
        "“Damn, I wish my package could arrive faster”",
        "Of course you have.",
        "With FRAGILE™, the newest delivery service in town, we make that wish come true!",
        "Never again will you have to wait for days for your package to arrive.",
        "Our personnel handle the fragile packages with the utmost care.",
        "Don’t you worry.",
        "Only 99% of our packages ever get destroyed or lost during the delivery.",
        "...",
        "... I mean 1 percent",
        "1 percent of our packages get destroyed, not 99 percent.",
        "Obviously...",
        "Hehe...",
    };

    void Awake()
    {
        textComponent = GetComponent<Text>();
        textToDraw = dialogue[0];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentDialogueIndex++;
            if (currentDialogueIndex <= dialogue.Count - 1)
            {
                textToDraw = dialogue[currentDialogueIndex];
                letterIndex = 0;
            }
            else
            {
                SceneController.LoadFirstScene();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneController.LoadFirstScene();
        }
    }

    private void FixedUpdate()
    {
        if (textToDraw != null && letterIndex <= textToDraw.Length)
        {
            textComponent.text = textToDraw.Substring(0, letterIndex++);
            if (fixedUpdateOffset % 4 == 0)
            {
                audioSource.PlayOneShot(audioClip);
            }   
            fixedUpdateOffset++;
        }
    }
}