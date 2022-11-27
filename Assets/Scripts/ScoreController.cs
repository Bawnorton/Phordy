using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public static int score;
    [SerializeField] private TMP_Text scoreText;
    
    private void Update() {
        scoreText.text = "Score: " + score;
    }
}
