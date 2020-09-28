using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Level Round Time in Seconds")]
    [SerializeField] int gameTime = 60;

    // Cached Components
    [SerializeField] Text timeText = null;
    MenuController menuController = null;

    // State
    int targetsHit = 0;
    [SerializeField] int targetsHitForBonus = 5;
    [SerializeField] int timeBonus = 10;

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = gameTime.ToString();
        StartCoroutine(Timer());
        menuController = FindObjectOfType<MenuController>();

        // Target Hit Handler
        Target.OnTargetHitEvent += HandleTimeBonus;
    }

    private void HandleTimeBonus(Target obj)
    {
        targetsHit++;
        if (targetsHit >= targetsHitForBonus)
        {
            targetsHit = 0;
            gameTime += timeBonus;
            if (timeText.text != null)
                timeText.text = gameTime.ToString();
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            gameTime--;
            timeText.text = gameTime.ToString();
            if (gameTime == 0)
            {
                menuController.isGameOver = true;
            }
        }
    }
}
