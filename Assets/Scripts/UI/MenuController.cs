using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;


public class MenuController : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject camObj;
    public CinemachineFreeLook freeLook;

    public Text gameOverScore;
    public Text gameScore;    

    public Slider sliderX;
    public Slider sliderY;

    public bool isOnPause;
    public bool isGameOver;

    public void Start()
    {
        camObj = GameObject.FindWithTag("Third Person Camera");
        freeLook = camObj.GetComponent<CinemachineFreeLook>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isOnPause)
        {
            if (!isGameOver)
            {
                showPause();
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.Escape) && isOnPause)
        {
            exitPause();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            isGameOver = true;
        }

        if (isGameOver)
        {
            Time.timeScale = 0f;
            
            gameOver.SetActive(true);
            gameOverScore.text = gameScore.text;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Debug.Log(freeLook.m_XAxis.m_MaxSpeed);
        //Debug.Log(freeLook.m_YAxis.m_MaxSpeed);
    }

    public void showPause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        
        isOnPause = true;

        // Unlocking Mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void exitPause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

        isOnPause = false;

        // Locking Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        applySense();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Default()
    {
        sliderX.value = 450f;
        sliderY.value = 4f;
    }

    public void applySense()
    {
        // Free look camera player sense
        freeLook.m_XAxis.m_MaxSpeed = sliderX.value;
        freeLook.m_YAxis.m_MaxSpeed = sliderY.value;

        // Relative sense to the player camera sense
        TowerController.mouseSensitivity = sliderX.value / 4.5f;

        // Player prefs
        PlayerPrefs.SetFloat("MouseSenseX", sliderX.value);
        PlayerPrefs.SetFloat("MouseSenseY", sliderY.value);
    }

}
