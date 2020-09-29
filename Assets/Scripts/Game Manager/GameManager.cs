using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Level Round Time in Seconds")]
    [SerializeField] int gameTime = 25;

    // Cached Components
    [SerializeField] Text timeText = null;

    // State
    int targetsHit = 0;
    [SerializeField] int targetsHitForBonus = 5;
    [SerializeField] int timeBonus = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Lock Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        timeText.text = gameTime.ToString();
        StartCoroutine(Timer());

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
            if (timeText != null)
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
                MenuController.instance.GameOver();
            }
        }
    }
}
