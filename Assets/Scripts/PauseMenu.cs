using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider FxVolume;
    [SerializeField] private AudioMixer mixer;
    public static bool isPaused;

    public void Awake() {
        musicVolume.value = SaveData.instance.musicVolume;
        FxVolume.value = SaveData.instance.sfxVolume;
    }

    public void Back() {
        isPaused = false;
        gameObject.SetActive(false);
        SaveData.instance.Save();
    }
    
    public void ReturnToMenu() {
        isPaused = false;
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void SetMusicVolume() {
        mixer.SetFloat("Music", Mathf.Log10(musicVolume.value) * 20);
        SaveData.instance.musicVolume = musicVolume.value;
    }
    public void SetFxVolume() {
        mixer.SetFloat("Fx", Mathf.Log10(FxVolume.value) * 20);
        SaveData.instance.sfxVolume = FxVolume.value;
    }

    public void Pause() {
        isPaused = true;
        gameObject.SetActive(true);
    }
}
