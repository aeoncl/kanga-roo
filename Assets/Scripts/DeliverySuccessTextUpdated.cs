using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySuccessTextUpdated : MonoBehaviour
{
    private Text _textComponent;
    private void Awake()
    {
        _textComponent = GetComponent<Text>();
        _textComponent.text = $"Destroyed: {LevelProgressSingleton.Instance.destroyedPackages}";
    }
}
