using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    public static int num;
    [SerializeField] private Button level1;
    [SerializeField] private Button level2;
    [SerializeField] private Button level3;

    private void Start() {
        GameObject c = GameObject.FindGameObjectWithTag("MainCamera");
        c.GetComponent<SimulateColorBlindness>().SetMode(SaveData.instance.colourCorrection);

        int[] levels = SaveData.instance.completedLevels;
        ColorBlock cb1 = level1.colors;
        ColorBlock cb2 = level2.colors;
        ColorBlock cb3 = level3.colors;
        if(levels[0] == 1) {
            cb1.normalColor = Color.yellow;
        } else if (levels[0] == 2) {
            cb1.normalColor = Color.green;
        }
        if(levels[1] == 1) {
            cb2.normalColor = Color.yellow;
        } else if (levels[1] == 2) {
            cb2.normalColor = Color.green;
        }
        if(levels[2] == 1) {
            cb3.normalColor = Color.yellow;
        } else if (levels[2] == 2) {
            cb3.normalColor = Color.green;
        }
        level1.colors = cb1;
        level2.colors = cb2;
        level3.colors = cb3;
    }
    public void Clicked(int n) {
        num = n;
        SceneManager.LoadScene("Scenes/Game");
    }
    
    public void BackToMenu() {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
