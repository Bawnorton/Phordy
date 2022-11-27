using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Object = UnityEngine.Object;

// levelString is a string of 0s and 1s, with 0s being spikes and 1s being platforms
// first characters are a|b|c| where a is an integer representing the width, b, height, and c, length of the level
// a b and c can be any integer from 0 to 100
// then the rest of the string is the level data formatted such that:
// each platform object is represented by a 1 followed by "[x,y,z,length,offset]" where x, y, and z are integers representing the position of the platform
// each spike object is represented by a 0 followed by "[x,y,z,length,offset]" where x, y, and z are integers representing the position of the spike
// and where length is the length of the platform and offset is the offset of the platform from the origin
// followed by a . and then the next platform object
// example a 3x3x3 cube with a 1x1x1 platform in the center would be represented as "3|3|3|1[1,1,1,1,0]." 
// example a 2x2x2 cube of platforms would be represented as "2|2|2|1[0,0,0,1,0].1[1,0,0,1,0].1[0,1,0,1,0].1[1,1,0,1,0].1[0,0,1,1,0].1[1,0,1,1,0].1[0,1,1,1,0].1[1,1,1,1,0]."
// example a 2x2x2 cube of spikes would be represented as "2|2|2|0[0,0,0,1,0].0[1,0,0,1,0].0[0,1,0,1,0].0[1,1,0,1,0].0[0,0,1,1,0].0[1,0,1,1,0].0[0,1,1,1,0].0[1,1,1,1,0]."
// the last character is a period, which is ignored
//
//COINS: use 2 followed by [x,y,z,a,b] for coins. Values a, b are ignored but still need to be present for formatting.
public class Level {
    private string coinData = "";
    
    // 3d array of tiles
    private GameObject[] platforms;

    private int levelWidth;
    private int levelHeight;
    private int levelLength;

    private readonly Material platformMaterial = GameObject.Find("Platform").GetComponent<Renderer>().material;

    //prefabs
    private readonly GameObject coin;
    private readonly GameObject winZone;
    private readonly GameObject spike;

    private void Init(int w, int h, int l, string levelString) {
        levelWidth = w;
        levelHeight = h;
        levelLength = l;
        platforms = new GameObject[levelWidth * levelHeight * levelLength];

        // get the level data
        string[] levelData = levelString.Split('.');
        for (int i = 0; i < levelData.Length; i++) {
            string type = levelData[i].Substring(0, 1);
            string[] data = levelData[i].Substring(2, levelData[i].Length - 3).Split(',');
            try {
                int x = int.Parse(data[0]);
                int y = int.Parse(data[1]);
                int z = int.Parse(data[2]);
                int length = int.Parse(data[3]);
                int offset = int.Parse(data[4]);
                if (type == "1") {
                    platforms[i] = CreatePlatform(x, y, z, length, offset);
                }
                else if (type == "0") {
                    int halfLength = length / 2;
                    for(int j = 0; j < length; j++) {
                        CreateSpike(x, y, z + j - halfLength);
                    }
                }
                else if (type == "2") {
                    coinData += levelData[i] + ".";
                    CreateCoin(x, y, z);
                }
            }
            catch (FormatException) {
                Debug.Log("Level data is not formatted correctly");
                Debug.Log(levelString + " at " + levelData[i]);
            }
        }
        coinData = coinData.Substring(0, coinData.Length - 1);
    }

    public Level(string levelString) {
        coin = Resources.Load<GameObject>("Prefabs/Coin");
        winZone = Resources.Load<GameObject>("Prefabs/WinZone");
        spike = Resources.Load<GameObject>("Prefabs/Spike");
        string[] levelDimensions = levelString.Split('|');
        int w = int.Parse(levelDimensions[0]);
        int h = int.Parse(levelDimensions[1]);
        int l = int.Parse(levelDimensions[2]);
        Init(w, h, l, levelDimensions[3]);
    }

    public Level(int w, int h, int l, string levelString) {
        coin = Resources.Load<GameObject>("Prefabs/Coin");
        winZone = Resources.Load<GameObject>("Prefabs/WinZone");
        spike = Resources.Load<GameObject>("Prefabs/Spike");
        Init(w, h, l, levelString);
    }

    private GameObject CreatePlatform(int x, int y, int z, int length, int offset) {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(x, y, z);
        cube.transform.localScale = new Vector3(1, 1, length);
        cube.AddComponent<PlatformController>();
        cube.GetComponent<PlatformController>().startZ = offset;
        cube.GetComponent<Renderer>().material = platformMaterial;
        cube.name = "Platform";
        cube.SetActive(false);
        return cube;
    }

    private void CreateSpike(int x, int y, int z) {
        if (spike != null) Object.Instantiate(spike, new Vector3(x, y - 0.16f, z), Quaternion.identity);
    }

    private void CreateCoin(int x, int y, int z) {
        if (coin != null) Object.Instantiate(coin, new Vector3(x, y, z), Quaternion.identity);
    }

    private void CreateWinZone() {
        if(winZone != null) {
            GameObject obj = Object.Instantiate(winZone, new Vector3(levelLength, 0, 0), Quaternion.identity);
            obj.transform.localScale = new Vector3(1,30,levelWidth);
        }
    }

    public void RegenerateCoins() {
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("Coin")) {
            Object.Destroy(c);
        }
        
        string[] coins = coinData.Split(".");
        foreach (var coinString in coins) {
            string[] data = coinString.Substring(2, coinString.Length - 3).Split(',');
            int x = int.Parse(data[0]);
            int y = int.Parse(data[1]);
            int z = int.Parse(data[2]);
            CreateCoin(x, y, z);
        }
    }

    public void ActivateLevel()
    {
        foreach (var platform in platforms) {
            if (platform != null) platform.SetActive(true);
        }
        CreateWinZone();
    }
    
    public void DisableLevel() {
        foreach (var platform in platforms) {
            if (platform != null) platform.SetActive(false);
        }
    }
}