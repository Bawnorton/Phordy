using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    public void Play() {
        SceneManager.LoadScene("Scenes/LevelSelect");
    }

    public void Quit() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
