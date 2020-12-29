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
            gameView.SetHighScoreText();
        }

        if (start)
        {
            Time.timeScale = 1f;
            gameView.SetStartUIFalse();
        }
    }

    public void StartGame()
    {
        start = true;
    }

}
