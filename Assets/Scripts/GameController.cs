using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool start = false;

    [Header("Refrences")]
    public GameView gameView;
    public CarController carController;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", carController.score);
            gameView.highScoreText.text = carController.score.ToString();
        }

        if (start)
        {
            Time.timeScale = 1f;
            gameView.SetStartUIFalse();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        start = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
