using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs
{ 
    public static int bestScore
    {
        get => PlayerPrefs.GetInt(GameConst.BestScore, 0);

        set
        {
            int currentBestScore = PlayerPrefs.GetInt(GameConst.BestScore);

            if(value > currentBestScore)
            {
                PlayerPrefs.SetInt(GameConst.BestScore, value);
            }
        }
    }

}
