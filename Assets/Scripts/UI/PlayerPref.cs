using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerPref : MonoBehaviour
{

    public Slider sliderX;
    public Slider sliderY;

    public Toggle crosshairToggled;
    
    public GameObject camObj;
    public CinemachineFreeLook freeLook;

    void Start()
    {
        camObj = GameObject.FindWithTag("Third Person Camera");
        freeLook = camObj.GetComponent<CinemachineFreeLook>();

        float SavedSenseX = PlayerPrefs.GetFloat("MouseSenseX", 450f);
        float SavedSenseY = PlayerPrefs.GetFloat("MouseSenseY", 4f);

        int isCrosshairToggled = PlayerPrefs.GetInt("CrosshairToggle", 0);

        sliderX.value = SavedSenseX;
        sliderY.value = SavedSenseY;

        freeLook.m_XAxis.m_MaxSpeed = SavedSenseX;
        freeLook.m_YAxis.m_MaxSpeed = SavedSenseY;

        crosshairToggled.isOn = isCrosshairToggled == 1 ? true : false;

        TowerController.mouseSensitivity = SavedSenseX / 4.5f;
    }

}
