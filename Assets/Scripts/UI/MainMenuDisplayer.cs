using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuDisplayer : MonoBehaviour
{
    [SerializeField] GameObject resumeButton;

    private void Start()
    {
        if (SaveSystem.IsThereASave())
        {
            resumeButton.SetActive(true);
        } else
        {
            resumeButton.SetActive(false);
        }
    }
}
