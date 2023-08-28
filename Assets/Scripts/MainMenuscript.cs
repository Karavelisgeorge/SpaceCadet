using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuscript : MonoBehaviour
{
    public Text highscoreText;

    void Start()
    {
        highscoreText.text = "Highscore : " + PlayerPrefs.GetInt("Highscore");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
        highscoreText.text = "Highscore : " + PlayerPrefs.GetInt("Highscore");
    }

}