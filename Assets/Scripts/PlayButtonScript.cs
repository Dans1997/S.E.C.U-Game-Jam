using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Sandbox");
    }
}
