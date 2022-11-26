using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {
    
    public static GameObject floor;
    public const int MAX_Z = 5;
    public const int MIN_Z = -5;
    private void Start() {
        floor = GameObject.Find("Floor");
    }

    private void Update() {
        
    }
}