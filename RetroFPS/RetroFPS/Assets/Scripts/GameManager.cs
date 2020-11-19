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
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerHealth>().enabled = false;
        foreach (Transform child in player.transform)
        {
            if (child.tag != "MainCamera")
                child.gameObject.SetActive(false);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        MainMenu.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerHealth>().enabled = true;
        foreach (Transform child in player.transform)
        {
            if (child.tag != "MainCamera")
                child.gameObject.SetActive(true);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayerDeath()
    {
        deathScreen.SetActive(true);
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerHealth>().enabled = false;
        foreach (Transform child in player.transform)
        {
            if (child.tag != "MainCamera")
                child.gameObject.SetActive(false);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.tag = "Untagged";
    }

    public void PlayerWon()
    {
        wonScreen.SetActive(true);
        GameObject player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerHealth>().enabled = false;
        foreach (Transform child in player.transform)
        {
            if (child.tag != "MainCamera")
                child.gameObject.SetActive(false);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.tag = "Untagged";
    }
}
