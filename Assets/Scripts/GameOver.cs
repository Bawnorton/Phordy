using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public void Quit() {
        Debug.Log("Quit");
        Application.Quit();
    }
    
    //TODO: ADD RETRY LEVEL WHEN LEVELS ARE A THING
}