using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour {

    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider FxVolume;
    private AudioMixer mixer;
    private void Awake()
    {
        mixer = Resources.Load<AudioMixer>("Audio/MainMixer");
    }
    public void Play() {
        SceneManager.LoadScene("Scenes/LevelSelect");
    }

    public void Quit() {
        Debug.Log("Quit");
        Application.Quit();
    }


    public void SetMusicVolume()
    {
        mixer.SetFloat("Music",musicVolume.value);
    }
    public void SetFxVolume()
    {
        mixer.SetFloat("Fx", FxVolume.value);
    }
}
