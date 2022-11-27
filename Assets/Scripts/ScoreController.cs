using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public static int score;
    public static int deaths;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text deathText;
    
    private void Update() {
        scoreText.text = "Score: " + score;
        deathText.text = "Deaths: " + deaths;
    }
}
