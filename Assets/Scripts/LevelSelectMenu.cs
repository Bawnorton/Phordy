using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    public static int num;

    private void Start() {
        // find object w/ tag "MainCamera"
        GameObject c = GameObject.FindGameObjectWithTag("MainCamera");
        c.GetComponent<SimulateColorBlindness>().SetMode(SaveData.instance.colourCorrection);
    }
    public void Clicked(int n) {
        num = n;
        SceneManager.LoadScene("Scenes/Game");
    }
    
    public void BackToMenu() {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
