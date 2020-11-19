using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple main menu script
//This flow is using Singleton model

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        GameManager.Instance.PlayGame();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
