using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {
    
    public static GameObject floor;
    private void Start() {
        floor = GameObject.Find("Floor");
    }

    private void Update() {
        
    }
}