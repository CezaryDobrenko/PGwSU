using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private GameObject deathScreen;
    private GameObject wonScreen;
    public GameObject MainMenu;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        } else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        InitGame();
    }

    private void Start()
    {
        deathScreen = transform.Find("DeathScreen").gameObject;
        wonScreen = transform.Find("WinningScreen").gameObject;
    }

    void InitGame()
    {
        MainMenu.SetActive(true);
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        loadGameSettings(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        MainMenu.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        loadGameSettings(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayerDeath()
    {
        deathScreen.SetActive(true);
        freezeGame();
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayerWon()
    {
        wonScreen.SetActive(true);
        freezeGame();
        Cursor.lockState = CursorLockMode.None;
    }

    private void loadGameSettings(bool value)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player.GetComponent<PlayerMovement>().enabled = value;
        player.GetComponent<PlayerHealth>().enabled = value;
        foreach (Transform child in player.transform)
        {
            if (child.tag != "MainCamera")
                child.gameObject.SetActive(value);
        }
        Cursor.visible = !value;
    }

    private void freezeGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerHealth>().enabled = false;
        foreach (Transform child in player.transform)
        {
            if (child.tag != "MainCamera")
                child.gameObject.SetActive(false);
        }
        Cursor.visible = true;
        player.tag = "Untagged";
    }

}
