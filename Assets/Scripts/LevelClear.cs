using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelClear : MonoBehaviour {
    
    [SerializeField] private TMP_Text scoreText;
    
    public void Start() {
        scoreText.text = "Score: " + ScoreController.score;
    }

    public void Continue() {
        SceneManager.LoadScene("Scenes/LevelSelect");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu"); // main menu
    }
}
