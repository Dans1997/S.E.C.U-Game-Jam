using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Level Round Time in Seconds")]
    [SerializeField] int gameTime = 180;

    // Cached Components
    [SerializeField] Text timeText;
    MenuController menuController = null;

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = gameTime.ToString();
        StartCoroutine(Timer());
        menuController = FindObjectOfType<MenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
