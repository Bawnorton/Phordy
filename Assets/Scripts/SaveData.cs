using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour {
    
    public static SaveData instance;
    public float musicVolume;
    public float sfxVolume;
    public int colourCorrection;
    public int[] completedLevels = {0,0,0};

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    
    public void Save() {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.SetInt("colourCorrection", colourCorrection);
        for (int i = 0; i < completedLevels.Length; i++) {
            PlayerPrefs.SetInt("completedLevel" + i, completedLevels[i]);
        }
    }
    
    public void Load() {
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        colourCorrection = PlayerPrefs.GetInt("colourCorrection");
        for (int i = 0; i < completedLevels.Length; i++) {
            completedLevels[i] = PlayerPrefs.GetInt("completedLevel" + i);
        }
    }

    public void Clear() {
        Debug.Log("Clearing Save Data");
        musicVolume = 0.5f;
        sfxVolume = 0.5f;
        colourCorrection = 0;
        for (int i = 0; i < completedLevels.Length; i++) {
            completedLevels[i] = 0;
        }
        Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
