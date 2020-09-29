using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.AudioManagerInstance.PlaySound(AudioManager.SoundKey.Transition);
        AudioManager.AudioManagerInstance.PlaySound(AudioManager.SoundKey.LevelAmbience);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
