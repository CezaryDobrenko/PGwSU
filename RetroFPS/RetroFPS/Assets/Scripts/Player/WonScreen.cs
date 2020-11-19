using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

//This is script handling winning condition of the game
//To win you need to kill boss

public class WonScreen : MonoBehaviour
{

    public AudioClip deathScreenSound;
    public float speed;
    AudioSource source;
    Image death;
    Text deathText;
    GameObject buttons;
    bool isFaded = false;

    void OnEnable()
    {
        death = transform.Find("Image").GetComponent<Image>();
        deathText = transform.Find("Text").GetComponent<Text>();
        buttons = transform.Find("Buttons").gameObject;
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        deathText.canvasRenderer.SetAlpha(0.0f);
        foreach (Transform button in buttons.transform)
        {
            button.Find("Text").GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
        }
        source.PlayOneShot(deathScreenSound);
        death.material.SetFloat("_Level", 1.0f);
        StartCoroutine(FadeScreen());
    }

    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (death.material.GetFloat("_Level") <= 0 && isFaded == false)
        {
            isFaded = true;
            deathText.CrossFadeAlpha(1.0f, 1.0f, false);
            foreach (Transform button in buttons.transform)
            {
                button.Find("Text").GetComponent<Text>().CrossFadeAlpha(1.0f, 1.0f, false);
            }
        }
    }

    IEnumerator FadeScreen()
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime * speed;
            death.material.SetFloat("_Level", t);
            yield return null;
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
