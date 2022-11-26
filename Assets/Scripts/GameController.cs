using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {
    
    public static GameObject floor;
    private int maxZ;
    private int minZ;
    
    private Level lvl1;
    private void Start() {
        floor = GameObject.Find("Floor");

        maxZ = (int) GameObject.Find("Backwall").transform.position.z;
        minZ = (int) GameObject.Find("Frontwall").transform.position.z;

        int levelWidth = Mathf.Abs(maxZ) + Mathf.Abs(minZ);
        int levelHeight = 5;
        int levelLength = 30;
        lvl1 = new Level(levelWidth, levelHeight, levelLength, 
        "1[1,0,0,3,0]." + "1[3,0,0,4,1]." + "1[3,1,0,2,0].");
        lvl1.GenerateLevel();
    }

    private void Update() {
        
    }
}