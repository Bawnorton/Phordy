using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    [SerializeField] private Slider volume;
    public void Play() {
        SceneManager.LoadScene("Scenes/LevelSelect");
    }

    public void Quit() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Update()
    {
        AudioListener.volume = volume.value;
    }
}
