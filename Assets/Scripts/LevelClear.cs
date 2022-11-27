using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelClear : MonoBehaviour {
    
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text deathText;
    
    public void Start() {
        scoreText.text = "Score: " + ScoreController.score;
        deathText.text = "Deaths: " + ScoreController.deaths;git
    }

    public void Continue() {
        SceneManager.LoadScene("Scenes/LevelSelect");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu"); // main menu
    }
}
