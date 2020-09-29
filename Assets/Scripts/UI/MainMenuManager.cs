using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("Sandbox");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
