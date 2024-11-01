using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class ScoreManager : MonoBehaviour
{ 
    [SerializeField] private List<int> scores = new List<int>();
    [SerializeField] private List<TMP_Text> scoreFields = new List<TMP_Text>();
    private int CurScore;

    public static ScoreManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        CurScore = PlayerPrefs.GetInt("Score");
        
    }
    private void Start()
    {      
        scores = scores.OrderByDescending(x => x).ToList();

        if(scores.Count >= 10)
            if(CurScore > scores[scores.Count - 1])
            {
                scores.RemoveAt(scores.Count - 1);
                scores.Add(CurScore);
                scores = scores.OrderByDescending(x => x).ToList();
            }
        
        PlaceScores();
    }
    private void PlaceScores()
    {
        for (int i = 0; i < 10; i++)
        {
            TMP_Text txt;
            txt = scoreFields[i];
            txt.SetText(scores[i].ToString());
        }
    }
}
