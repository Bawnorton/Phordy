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
        string spikeFloor = String.Format(
            "0[1,0,0,{0},0].0[2,0,0,{0},0]" +
            ".0[3,0,0,{0},0].0[4,0,0,{0},0].0[5,0,0,{0},0]" +
            ".0[6,0,0,{0},0].0[7,0,0,{0},0].0[8,0,0,{0},0]" +
            ".0[9,0,0,{0},0].0[10,0,0,{0},0].0[11,0,0,{0},0]" +
            ".0[12,0,0,{0},0].0[13,0,0,{0},0].0[14,0,0,{0},0]" +
            ".0[15,0,0,{0},0].0[16,0,0,{0},0].0[17,0,0,{0},0]" +
            ".0[18,0,0,{0},0].0[19,0,0,{0},0].0[20,0,0,{0},0]" +
            ".0[21,0,0,{0},0].0[22,0,0,{0},0].0[23,0,0,{0},0]" +
            ".0[24,0,0,{0},0].0[25,0,0,{0},0].0[26,0,0,{0},0]" +
            ".0[27,0,0,{0},0].0[28,0,0,{0},0].0[29,0,0,{0},0]" +
            ".0[30,0,0,{0},0].", levelWidth
        );
        string platforms = "1[0,0,0,2,0].1[2,1,0,3,1].1[3,1,0,4,2]" +
                           ".1[5,2,1,4,4].1[5,1,1,10,-5].1[11,1,0,6,4]" +
                           ".1[13,2,0,3,0].1[14,2,0,3,2].1[15,2,0,3,-2]" +
                           ".1[13,4,0,3,0].1[14,4,0,3,2].1[18,2,-1,6,-3]" +
                           ".1[25,0,0,2,0].1[27,1,0,3,1].1[28,2,-2,5,-1]" +
                           ".1[29,1,0,10,0].1[29,2,0,10,0].1[29,3,0,10,0]" +
                           ".1[29,0,0,10,0]";
        lvl1 = new Level(levelWidth, levelHeight, levelLength, spikeFloor + platforms);
        lvl1.GenerateLevel();
    }

    private void Update() {
        
    }
}