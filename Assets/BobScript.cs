using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BobScript : MonoBehaviour
{
    [SerializeField] public GameObject mainMenuCanvas;
    [SerializeField] public GameObject levelMenuCanvas;

    [SerializeField] Button PurinButton;
    [SerializeField] Button RatButton;

    public void ToMainMenu()
    {
        levelMenuCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void ToLevelMenu()
    {
        mainMenuCanvas.SetActive(false);
        levelMenuCanvas.SetActive(true);
        if(MainMenuScript.Instance.visitedScenes.Contains(1) == false)
        {
            PurinButton.interactable = false;
        }
        else
        {
            PurinButton.interactable = true;
        }
        if(MainMenuScript.Instance.visitedScenes.Contains(2) == false)
        {
            RatButton.interactable = false;
        }
        else
        {
            RatButton.interactable = true;
        }
    }
}
