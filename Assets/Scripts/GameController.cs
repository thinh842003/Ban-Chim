using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public float SpawnTime;
    public int TimeLimit;
    
    int temp_TimeLimit;
    int temp_BirdKilled;
    bool temp_isGameover;

    public Bird[] birds;

    public bool isGameover { get => temp_isGameover; set => temp_isGameover = value; }
    public int BirdKilled { get => temp_BirdKilled; set => temp_BirdKilled = value; }

    public override void Awake()
    {
        MakeSingleton(false);
        temp_TimeLimit = TimeLimit;
    }

    public override void Start()
    {
        GameGUIController.Ins.ShowGameGUI(false);
        GameGUIController.Ins.UpdateCountingKilled(temp_BirdKilled);
    }

    public void PlayGame()
    {
        StartCoroutine(TimeBirdSpawn());
        StartCoroutine(TimeCountDown());
        GameGUIController.Ins.ShowGameGUI(true);
    }

    IEnumerator TimeCountDown()
    {
        while (temp_TimeLimit > 0)
        {
            yield return new WaitForSeconds(1);
            temp_TimeLimit--;

            if(temp_TimeLimit <= 0)
            {
                temp_isGameover = true;

                if(temp_BirdKilled > Prefs.bestScore)
                {
                    GameGUIController.Ins.GameDialog.UpdateDialog("NEW BEST", "BEST KILLED: x" + temp_BirdKilled);
                }
                else if(temp_BirdKilled < Prefs.bestScore)
                {
                    GameGUIController.Ins.GameDialog.UpdateDialog("YOUR BEST", "BEST KILLED: x" + Prefs.bestScore);
                }

                Prefs.bestScore = temp_BirdKilled;
                GameGUIController.Ins.GameDialog.ShowDialog(true);
                GameGUIController.Ins.CurrentDialog = GameGUIController.Ins.GameDialog;       
            }

            GameGUIController.Ins.UpdateTime(IntToTime(temp_TimeLimit));
        }
    }

    IEnumerator TimeBirdSpawn()
    {
        while (!temp_isGameover)
        {
            SpawnBird();
            yield return new WaitForSeconds(SpawnTime);
        }
    }

    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;

        float randCheck = Random.Range(0f, 1f);

        if(randCheck >= 0.5f)
        {
            spawnPos = new Vector3(11, Random.Range(1f, 4f), 0);
        }
        else
        {
            spawnPos = new Vector3(-11, Random.Range(1f, 4f), 0);
        }

        if(birds != null && birds.Length > 0)
        {
            int RandBird = Random.Range(0, birds.Length);

            if (birds[RandBird]  != null)
            {
                Bird bird = Instantiate(birds[RandBird], spawnPos, Quaternion.identity);
            }
        }
    }

    string IntToTime(int time)
    {
        float minute = Mathf.Floor(time / 60);
        float second = Mathf.RoundToInt(time % 60);

        return minute.ToString("00") + ":" + second.ToString("00");
    }
}
