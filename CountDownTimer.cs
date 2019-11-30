using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerGameTimer : MonoBehaviour
{
    public GameObject controller;
    public GameObject StartButton;
    public GameObject ExitButton;
    public bool gameStarted = false;
    public bool gameOver = false;
    public bool timerStarted = false;
    private float startTime;
    private float gameTime;
    public int gameTimeInt;
    private int previousGameTimeInt;
    public int totalGameTime;
    public Text spGameTime;
    public GameObject WarningSpeaker;
    public AudioClip WarningSound;
    private int GameMinutes;
    private int GameSeconds;
    private string Colon = ":";
    private string Minutes;
    private string Seconds;
    GameControllerSingle gameController;
    AudioSource WarningSpeakerSource;
    // Use this for initialization
    void Start()
    {
        gameController = controller.GetComponent<GameControllerSingle>();
        WarningSpeakerSource = WarningSpeaker.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (!timerStarted)
            {
                startTime = Time.time;
                timerStarted = true;
                gameTimeInt = totalGameTime;
                previousGameTimeInt = totalGameTime;
            }
            else
            {
                gameTime = Time.time - startTime;
                gameTimeInt = totalGameTime - (int)gameTime;
            }
            if (gameTimeInt < previousGameTimeInt && gameTimeInt >= 0)
            {
                if (gameTimeInt / 60 >= 1)
                {
                    GameMinutes = (int)(gameTimeInt / 60);
                    GameSeconds = gameTimeInt - (GameMinutes * 60);
                }
                else if (gameTimeInt / 60 < 1 && gameTimeInt > 30)
                {
                    GameMinutes = 0;
                    GameSeconds = gameTimeInt;
                }
                else if (gameTimeInt / 60 < 1 && gameTimeInt > 10)
                {
                    spGameTime.color = Color.yellow;
                    GameMinutes = 0;
                    GameSeconds = gameTimeInt;
                }
                else if (gameTimeInt / 60 < 1 && gameTimeInt > 0)
                {
                    WarningSpeakerSource.PlayOneShot(WarningSound, 1f);
                    spGameTime.color = Color.red;
                    GameMinutes = 0;
                    GameSeconds = gameTimeInt;
                }
                else
                {
                    GameMinutes = 0;
                    GameSeconds = 0;
                }
                Minutes = GameMinutes.ToString();
                Seconds = GameSeconds.ToString("d2");
                spGameTime.text = Minutes + Colon + Seconds;
                previousGameTimeInt = gameTimeInt;
            }
            else if (gameTimeInt == 0)
            {
                gameStarted = false;
                gameOver = true;
            }
        }
        else
        {
            if (gameController.StartPauseTimerComplete)
            {
                spGameTime.color = Color.black;
                gameStarted = true;
            }
        }
        if (gameOver)
        {
            while (gameController.Balls.Count > 0)
            {
                Destroy(gameController.Balls[0]);
                gameController.Balls.RemoveAt(0);
            }
            gameStarted = false;
            timerStarted = false;
            gameController.PlayerReady = false;
            StartButton.SetActive(true);
            ExitButton.SetActive(true);
            gameOver = false;
        }
    }
}