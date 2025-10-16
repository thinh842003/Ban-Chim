using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIController : Singleton<GameGUIController>
{
    public GameObject GameGUI;
    public GameObject HomeGUI;

    public Dialog GameDialog;
    public Dialog PauseDialog;

    public Image FireRateFilled;
    public Text timerText;
    public Text CountBirdKilled;

    Dialog temp_currentDialog;

    public Dialog CurrentDialog { get => temp_currentDialog; set => temp_currentDialog = value; }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGameGUI(bool isShow)
    {
        if (GameGUI)
        {
            GameGUI.SetActive(isShow);
        }

        if (HomeGUI)
        {
            HomeGUI.SetActive(!isShow);
        }
    }

    public void UpdateTime(string time)
    {
        if (timerText)
        {
            timerText.text = time;
        }
    }

    public void UpdateCountingKilled(int killed)
    {
        if (CountBirdKilled)
        {
            CountBirdKilled.text = "x" + killed.ToString();
        }
    }

    public void UpdateFireRateFilled(float filled)
    {
        if (FireRateFilled)
        {
            FireRateFilled.fillAmount = filled;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        if (PauseDialog)
        {
            PauseDialog.ShowDialog(true);
            PauseDialog.UpdateDialog("PAUSE GAME", "BEST KILLED: x" + Prefs.bestScore);
            temp_currentDialog = PauseDialog;
            
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if (temp_currentDialog)
        {
            temp_currentDialog.ShowDialog(false);
        }
    }

    public void Replay()
    {
        if (temp_currentDialog)
        {
            temp_currentDialog.ShowDialog(false);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToHome()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Application.Quit();
    }
}
