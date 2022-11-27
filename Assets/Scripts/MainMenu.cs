using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider FxVolume;
    [SerializeField] private TMP_Dropdown colourCorrection;
    [SerializeField] private Button resetButton;
    private static AudioMixer mixer;
    private void Awake() {
        mixer = Resources.Load<AudioMixer>("Audio/MainMixer");
        SaveData.instance.Load();
        musicVolume.value = SaveData.instance.musicVolume;
        FxVolume.value = SaveData.instance.sfxVolume;
        GameObject c = GameObject.FindGameObjectWithTag("MainCamera");
        c.GetComponent<SimulateColorBlindness>().SetMode(SaveData.instance.colourCorrection);
    }

    public void Start() {
        mixer.SetFloat("Music", Mathf.Log10(SaveData.instance.musicVolume) * 20);
        mixer.SetFloat("Fx", Mathf.Log10(SaveData.instance.sfxVolume) * 20);
        colourCorrection.value = SaveData.instance.colourCorrection; 
        
        resetButton.onClick.AddListener(SaveData.instance.Clear);
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
    
    public void SetColourCorrection(int index) {
        GameObject c = GameObject.FindGameObjectWithTag("MainCamera");
        c.GetComponent<SimulateColorBlindness>().SetMode(index);
        SaveData.instance.colourCorrection = index;
        SaveOptions();
    }

    public void SaveOptions() {
        SaveData.instance.Save();
    }
}
