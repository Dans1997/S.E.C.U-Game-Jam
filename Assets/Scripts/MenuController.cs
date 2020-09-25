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
    public GameObject camObj;
    public CinemachineFreeLook freeLook;
    public Slider sliderX;
    public Slider sliderY;

    public bool isOnPause = false;

    public void Start()
    {
        camObj = GameObject.FindWithTag("Third Person Camera");
        freeLook = camObj.GetComponent<CinemachineFreeLook>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOnPause)
        {
            showPause();
        }
        
        else if (Input.GetKeyDown(KeyCode.Escape) && !isOnPause)
        {
            exitPause();
        }

        //Debug.Log(freeLook.m_XAxis.m_MaxSpeed);
        //Debug.Log(freeLook.m_YAxis.m_MaxSpeed);
    }

    public void Play()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void showPause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        
        isOnPause = false;

        // Unlocking Mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void exitPause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

        isOnPause = true;

        // Locking Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        applySense();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void applySense()
    {
        // Free look camera player sense
        freeLook.m_XAxis.m_MaxSpeed = sliderX.value;
        freeLook.m_YAxis.m_MaxSpeed = sliderY.value;

        // Relative sense to the player camera sense
        TowerController.mouseSensitivity = sliderX.value / 4.5f;
    }

}
