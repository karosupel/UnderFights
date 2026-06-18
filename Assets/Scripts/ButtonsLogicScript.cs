using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsLogicScript : MonoBehaviour
{
    public void ToMainMenu()
    {
        MainMenuScript.Instance.ToMainMenu();
    }

    public void NextScene()
    {
        MainMenuScript.Instance.NextScene();
    }

    public void ToLevelMenu()
    {
        MainMenuScript.Instance.ToLevelMenu();
    }

    public void ToTutorial()
    {
        MainMenuScript.Instance.ToTutorial();
    }

    public void ToPurin()
    {
        MainMenuScript.Instance.ToPurin();
    }

    public void ToRat()
    {
        MainMenuScript.Instance.ToRat();
    }

    public void Exit()
    {
        MainMenuScript.Instance.Exit();
    }

    public void ReloadScene()
    {
        MainMenuScript.Instance.ReloadScene();
    }
}
