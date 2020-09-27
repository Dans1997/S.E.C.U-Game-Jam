using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] Text scoreText = null;

    // State
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreText)
        {
            scoreText.text = "0";
            Target.OnTargetDestroy += Target_TargetDestroyed;
        }
    }

    private void Target_TargetDestroyed(Target targetHit)
    {
        int scoreToAdd = targetHit.scoreValue;
        Debug.Log("Target Destroyed!");
        AddScore(scoreToAdd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int scoreToAdd)
    {
        if (scoreText)
        {
            score += scoreToAdd;
            scoreText.text = score.ToString();
        }
    }
}
