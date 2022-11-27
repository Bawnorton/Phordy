using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour {

    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider FxVolume;
    private static AudioMixer mixer;
    private void Awake() {
        mixer = Resources.Load<AudioMixer>("Audio/MainMixer");
        SaveData.instance.Load();
        musicVolume.value = SaveData.instance.musicVolume;
        FxVolume.value = SaveData.instance.sfxVolume;
    }

    public void Start() {
        mixer.SetFloat("Music", Mathf.Log10(SaveData.instance.musicVolume) * 20);
        mixer.SetFloat("Fx", Mathf.Log10(SaveData.instance.sfxVolume) * 20);
    }
    public void Play() {
        SceneManager.LoadScene("Scenes/LevelSelect");
    }

    public void Quit() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SetMusicVolume() {
        mixer.SetFloat("Music", Mathf.Log10(musicVolume.value) * 20);
        SaveData.instance.musicVolume = musicVolume.value;
    }
    public void SetFxVolume() {
        mixer.SetFloat("Fx", Mathf.Log10(FxVolume.value) * 20);
        SaveData.instance.sfxVolume = FxVolume.value;
    }

    public void SaveOptions() {
        SaveData.instance.Save();
    }
}
