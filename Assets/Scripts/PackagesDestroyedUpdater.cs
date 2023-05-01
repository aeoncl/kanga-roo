using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagesDestroyedUpdater : MonoBehaviour
{
    private Text _text;
    void Awake()
    {
        _text = GetComponent<Text>();
        int destroyedPackages = LevelProgressSingleton.Instance.destroyedPackages;
        _text.text = $"Destroyed packages : {destroyedPackages}";
    }

    void FixedUpdate()
    {
         int destroyedPackages = LevelProgressSingleton.Instance.destroyedPackages;
         _text.text = $"Destroyed packages : {destroyedPackages}";
    }
}
